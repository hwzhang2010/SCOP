using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using NLog;
using Dapper;

using Sodao.FastSocket.Server;

using TSFCS.SCOP.Helper;
using TSFCS.SCOP.Udp;

using TSFCS.SCOP.Model;
using TSFCS.SCOP.DAL;
using System.Collections.Concurrent;

namespace TSFCS.SCOP.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            Messenger.Default.Register<UdpMessage>(this, "Recv", HandleRecv);  //UDP, FastSocket: Receive
            Messenger.Default.Register<UdpMessage>(this, "Send", HandleSend);  //UDP, FastSocket: Send
            Messenger.Default.Register<SendModel>(this, "Storage", HandleStorage);  //Storage, Sqlite3存储
        }
        #endregion

        #region Messenger Handler
        private void HandleRecv(UdpMessage message)
        {
            if (message != null)
            {
                int length = message.Length;
                byte[] payload = new byte[length];  //接收数据
                Buffer.BlockCopy(message.Payload, 0, payload, 0, length);  //取得有效数据
                recvQueue.Enqueue(payload);  //有效数据放入队列

                GalaSoft.MvvmLight.Threading.DispatcherHelper.CheckBeginInvokeOnUI(new Action(() => { RecvCount += Convert.ToUInt16(length); }));  //刷新接收计数
            }
        }

        private void HandleSend(UdpMessage message)
        {
            //Messenger.Default.Send<string>(ByteHelper.Bytes2HexStr(message.Payload), "Alert");
            lock (lockSend)
            { 
                //发送缓冲区
                Buffer.BlockCopy(message.Payload, 0, sendByte, 0, CmdOperation.CmdCountConst);  
            }

            sendWaitHandler.Set();  //将事件状态设为终止状态，允许等待线程继续执行
        }

        private void HandleStorage(SendModel model)
        {
            model.SendTime = DateTime.Now;
            sendQueue.Enqueue(model);
        }
        #endregion

        #region Override Method
        /// <summary>
        /// Clean the Resource when dispose
        /// </summary>
        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);

            base.Cleanup();
        }
        #endregion

        #region Field
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();  //NLog日志记录
        private readonly string strConnection = "Data Source=db_scop.db3";
        private readonly int tlmHeader = 13;  //遥测帧头长度
        private readonly object lockSend = new object();  //发送时锁定数据
        private UdpServer<UdpMessage> udp = null;  //UDP, FastSocket
        private IPEndPoint epSend = null;  //UDP, 目的地址
        private ConcurrentQueue<byte[]> recvQueue = new ConcurrentQueue<byte[]>();  //接收bytes队列
        private byte[] sendByte = new byte[CmdOperation.CmdCountConst];  //发送bytes数组
        private AutoResetEvent sendWaitHandler = new AutoResetEvent(false);  //事件信号，线程同步：处理线程通知发送线程；发送线程处于阻塞状态；默认非终止，阻塞状态
        private ulong recvCount;  //接收计数
        private ulong sendCount;  //发送计数
        private Sqlite sqlite = new Sqlite("db_scop.db3");  //SQLite3数据库
        private ConcurrentQueue<SendModel> sendQueue = new ConcurrentQueue<SendModel>();  //发送存储
        //private DateTime dtStorage = DateTime.Now.AddMinutes(5);  //数据定时存储时刻
        private TimerWait sysTimer;  //系统时间
        private string sysTime;  //系统时间
        private string netState;  //网络状态
        #endregion

        #region Property
        public string SysTime
        {
            get { return sysTime; }
            set
            {
                sysTime = value;
                RaisePropertyChanged("SysTime");
            }
        }
        public ulong RecvCount
        {
            get { return recvCount; }
            set
            {
                recvCount = value;
                RaisePropertyChanged("RecvCount");
            }
        }
        public ulong SendCount
        {
            get { return sendCount; }
            set
            {
                sendCount = value;
                RaisePropertyChanged("SendCount");
            }
        }
        public string NetState
        {
            get { return netState; }
            set 
            { 
                netState = value;
                RaisePropertyChanged("NetState");
            }
        }
        #endregion


        #region Command
        private bool CanLoadedExecute()
        {
            return true;
        }
        private void LoadedExecute()
        {
            Messenger.Default.Send<string>("Loaded", "Main");
            InitSysTimer();  //初始化系统时间
            Init();  //初始化
        }
        public ICommand LoadedCommand { get { return new RelayCommand(LoadedExecute, CanLoadedExecute); } }

        private bool CanDragMoveExecute()
        {
            return true;
        }
        private void DragMoveExecute()
        {
            Messenger.Default.Send<string>("DragMove", "Main");
        }
        public ICommand DragMoveCommand { get { return new RelayCommand(DragMoveExecute, CanDragMoveExecute); } }

        private bool CanClosedExecute()
        {
            return true;
        }
        private void ClosedExecute()
        {
            Destroy();  //清理资源
            Messenger.Default.Send<string>("Closed", "Main");
        }
        public ICommand ClosedCommand { get { return new RelayCommand(ClosedExecute, CanClosedExecute); } }

        private bool CanNormalExecute()
        {
            return true;
        }
        private void NormalExecute()
        {
            Messenger.Default.Send<string>("Normal", "Main");
        }
        public ICommand NormalCommand { get { return new RelayCommand(NormalExecute, CanNormalExecute); } }

        private bool CanMaxExecute()
        {
            return true;
        }
        private void MaxExecute()
        {
            Messenger.Default.Send<string>("Max", "Main");
        }
        public ICommand MaxCommand { get { return new RelayCommand(MaxExecute, CanMaxExecute); } }

        private bool CanMinExecute()
        {
            return true;
        }
        private void MinExecute()
        {
            Messenger.Default.Send<string>("Min", "Main");
        }
        public ICommand MinCommand { get { return new RelayCommand(MinExecute, CanMinExecute); } }

        private bool CanMenuExecute()
        {
            return true;
        }
        private void MenuExecute()
        {
            Messenger.Default.Send<string>("Menu", "Main");
        }
        public ICommand MenuCommand { get { return new RelayCommand(MenuExecute, CanMenuExecute); } }

        private bool CanSeeExecute()
        {
            return true;
        }
        private void SeeExecute()
        {
            Messenger.Default.Send<string>("See", "Main");
        }
        public ICommand SeeCommand { get { return new RelayCommand(SeeExecute, CanSeeExecute); } }

        private bool CanNaviExecute(string item)
        {
            return true;
        }
        private void NaviExecute(string item)
        {
            Messenger.Default.Send<string>(item, "Navi");
        }
        public ICommand NaviCommand { get { return new RelayCommand<string>((item) => NaviExecute(item), CanNaviExecute); } }
        #endregion

        #region Method
        /// <summary>
        /// 初始化系统时间
        /// </summary>
        private void InitSysTimer()
        {
            this.SysTime = DateTime.Now.ToString("yyyy/MM/dd  HH:mm:ss");   //系统时间：yyyy年MM月dd日HH时mm分ss秒
            sysTimer = new TimerWait();  //初始化定时器
            sysTimer.Elapsed += new EventHandler(ShowSysTimer);  //一直获取当前时间
            sysTimer.MyTimer.Enabled = true;  //启动定时器
        }

        private void ShowSysTimer(object sender, EventArgs e)
        {
            this.SysTime = DateTime.Now.ToString("yyyy/MM/dd  HH:mm:ss");   //系统时间：yyyy年MM月dd日HH时mm分ss秒
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            List<IPAddress> groups = new List<IPAddress>();
            using (IDbConnection connection = new SQLiteConnection(strConnection))
            {
                List<UdpModel> sendModelList = connection.Query<UdpModel>("select * from t_udp where type = @type", new { type = "send" }).AsList<UdpModel>();  //获取发送端的IP地址和端口
                if (sendModelList.Count > 0)
                    this.epSend = new IPEndPoint(IPAddress.Parse(sendModelList[0].Ip), sendModelList[0].Port);

                List<UdpModel> recvModelList = connection.Query<UdpModel>("select * from t_udp where type = @type", new { type = "recv" }).AsList<UdpModel>();  //获取接收端的IP地址和端口
                if (recvModelList.Count > 0)
                {
                    this.udp = new UdpServer<UdpMessage>(recvModelList[0].Port, new UdpProtocol(), new UdpService());  //UDP, FastSocket
                    for (int i = 0; i < recvModelList.Count; i++)
                        groups.Add(IPAddress.Parse(recvModelList[i].Ip));
                }
                
                List<UdpModel> replyModelList = connection.Query<UdpModel>("select * from t_udp where type = @type", new { type = "reply" }).AsList<UdpModel>();  //获取应答端的IP地址和端口
                for (int i = 0; i < replyModelList.Count; i++)
                    groups.Add(IPAddress.Parse(replyModelList[i].Ip));

            } 
            this.udp.Start(groups.ToArray());  //启动接收:UDP

            this.SendCount = this.RecvCount = 0;  //每次重新开始计数
            this.NetState = "UDP已连接";  //网络状态：UDP组播

            //this.udp.SendTo(this.epSend, new byte[] { 0xEB, 0x90, 0xAA, 0xBB });

            //this.Replying = Resend;  //校验错误重发

            Task.Factory.StartNew(TaskRecv);  //启动接收(处理)任务
            Task.Factory.StartNew(TaskSend);  //启动发送任务
            //Task.Factory.StartNew(TaskResend);  //启动超时重发任务
            //Task.Factory.StartNew(TaskTimeout);  //启动超时检测任务
            Task.Factory.StartNew(TaskStorage);  //启动数据库存储任务
        }

        /// <summary>
        /// 清理资源
        /// </summary>
        private void Destroy()
        {
            if (this.udp != null)
                this.udp.Stop();

            //Task.Factory.StartNew(() => 
            //{
            //    if (this.sendDataList.Count > 0)
            //    {
            //        lock (lockStorage)
            //        {
            //            SendStorage.Insert(this.sqlite, this.sendDataList);
            //        }
            //    }
            //});
            
        }

        /// <summary>
        /// 接收(处理)任务
        /// </summary>
        private void TaskRecv()
        {
            while (true)
            {
                if (recvQueue.IsEmpty)  
                {
                    System.Threading.Thread.Sleep(1);  //挂起1ms，避免一直占用CPU
                }
                else  //接收数据队列中存在待处理的数据
                {
                    byte[] data = null;
                    if (recvQueue.TryDequeue(out data))
                    {
                        //logger.Log(LogLevel.Info, string.Format("SCOP: UDP收到数据:{0}", ByteHelper.Bytes2HexStr(data)));

                        if (data.Length < 13)  //帧长必须 >= 13 (包含应答帧)
                            continue;

                        switch (data[4])  //类型字
                        {
                            case 0x01:  //遥控
                                break;
                            case 0x02:  //遥测
                                ProcessTlm(data);
                                break;
                            case 0x03:  //遥控应答
                                break;
                            case 0x04:  //遥测应答
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 遥测数据处理
        /// </summary>
        /// <param name="data"></param>
        private void ProcessTlm(byte[] data)
        {
            byte flagMain = data[6];
            if (flagMain == 0x4)  //GPS - 422
            { 
            }
            else if (flagMain == 0x5)  //GPS - CAN
            {
                analyzeGps(data);
            }
            else if (flagMain == 0x6)  //星敏1
            { 
            }
            else if (flagMain == 0x7)  //星敏2
            {
            }
            else if (flagMain == 0x8)  //星敏3
            {
            }
            else if (flagMain == 0x9)  //模拟太阳敏
            {
            }
            else if (flagMain == 0xA)  //陀螺
            {
            }
            else if (flagMain == 0xB)  //反作用轮X
            {
            }
            else if (flagMain == 0xC)  //反作用轮Y
            {
            }
            else if (flagMain == 0xD)  //反作用轮Z
            {
            }
            else if (flagMain == 0xE)  //反作用轮S
            {
            }
            else if (flagMain == 0x10)  //遥感相机
            {
            }
            else if (flagMain == 0x11)  //磁强计1
            {
            }
            else if (flagMain == 0x12)  //磁强计2
            {
            }
            else if (flagMain == 0x13)  //实验磁强计
            {
            }
            else if (flagMain == 0x14)  //磁力矩器1
            {
            }
            else if (flagMain == 0x15)  //磁力矩器2
            {
            }
            else if (flagMain == 0x16)  //磁力矩器3
            {
            }
            else if (flagMain == 0x18)  //ADCS
            {
            }
            else
            {
            }
        }

        /// <summary>
        /// GPS数据解析
        /// </summary>
        /// <param name="data"></param>
        private void analyzeGps(byte[] data)
        {
            if (data.Length < 66)  //GPS: 51byte, header: 13byte, tail: 2byte
                return;

            Dictionary<int, string> workState = new Dictionary<int, string>();
            workState.Add(0x0, "不定位");
            workState.Add(0x1, "快捕");
            workState.Add(0x2, "定位");
            workState.Add(0x3, "重补");

            List<GpsShowModel> list = new List<GpsShowModel>();
            list.Add(new GpsShowModel() { Num = 0, Name = "工作状态", Hex = data[tlmHeader + 8].ToString("X2"), Cal = workState[data[tlmHeader + 8] % 4] });
            list.Add(new GpsShowModel() { Num = 1, Name = "定位模式", Hex = data[tlmHeader + 9].ToString("X2"), Cal = "BD/GPS" });
            list.Add(new GpsShowModel() { Num = 2, Name = "定位星数", Hex = ByteHelper.Bytes2HexStr(data, tlmHeader + 10, 2), Cal = BitConverter.ToUInt16(data, tlmHeader + 10).ToString() });
            list.Add(new GpsShowModel() { Num = 3, Name = "历元时间", Hex = ByteHelper.Bytes2HexStr(data, tlmHeader + 14), Cal = string.Format("{0}-{1}-{2} {3}:{4}:{5}.{6}", BitConverter.ToUInt16(data, tlmHeader + 14), data[tlmHeader + 16], data[tlmHeader + 17], data[tlmHeader + 18], data[tlmHeader + 19], data[tlmHeader + 20], BitConverter.ToUInt16(data, tlmHeader + 21)) });
            list.Add(new GpsShowModel() { Num = 4, Name = "卫星位置X方向(km)", Hex = ByteHelper.Bytes2HexStr(data, tlmHeader + 23), Cal = BitConverter.ToInt32(data, tlmHeader + 23).ToString() });
            list.Add(new GpsShowModel() { Num = 5, Name = "卫星位置Y方向(km)", Hex = ByteHelper.Bytes2HexStr(data, tlmHeader + 27), Cal = BitConverter.ToInt32(data, tlmHeader + 27).ToString() });
            list.Add(new GpsShowModel() { Num = 6, Name = "卫星位置Z方向(km)", Hex = ByteHelper.Bytes2HexStr(data, tlmHeader + 31), Cal = BitConverter.ToInt32(data, tlmHeader + 31).ToString() });
            list.Add(new GpsShowModel() { Num = 7, Name = "卫星速度X方向(km/s)", Hex = ByteHelper.Bytes2HexStr(data, tlmHeader + 35), Cal = BitConverter.ToInt32(data, tlmHeader + 35).ToString() });
            list.Add(new GpsShowModel() { Num = 8, Name = "卫星速度Y方向(km/s)", Hex = ByteHelper.Bytes2HexStr(data, tlmHeader + 39), Cal = BitConverter.ToInt32(data, tlmHeader + 39).ToString() });
            list.Add(new GpsShowModel() { Num = 9, Name = "卫星速度Z方向(km/s)", Hex = ByteHelper.Bytes2HexStr(data, tlmHeader + 43), Cal = BitConverter.ToInt32(data, tlmHeader + 43).ToString() });

            Messenger.Default.Send<List<GpsShowModel>>(list, "GpsShow");

        }

        /// <summary>
        /// 发送任务
        /// </summary>
        private void TaskSend()
        {
            while (true)
            {
                sendWaitHandler.WaitOne();  //阻塞当前线程，直到WaitHandler收到信号

                lock (lockSend)
                {
                    this.udp.SendTo(this.epSend, this.sendByte);  //UDP组播发送遥控指令
                    GalaSoft.MvvmLight.Threading.DispatcherHelper.CheckBeginInvokeOnUI(new Action(() => { SendCount += Convert.ToUInt16(CmdOperation.CmdCountConst); }));  //刷新发送计数
                }
            }
        }

        /// <summary>
        /// 数据库存储任务
        /// </summary>
        private void TaskStorage()
        {
            while (true)
            {
                if (sendQueue.IsEmpty)
                {
                    System.Threading.Thread.Sleep(1);  //挂起1ms，避免一直占用CPU
                }
                else
                {
                    SendModel model = null;
                    if (sendQueue.TryDequeue(out model))
                    {
                        using (IDbConnection connection = new SQLiteConnection(strConnection))
                        {
                            //指令发送记录插入数据库
                            connection.Execute("insert into t_send(sendtime, cmdid, cmdname, cmdparam) values(@sendtime, @cmdid, @cmdname, @cmdparam)", model);
                        }
                    }
                }


                //if (DateTime.Compare(DateTime.Now, this.dtStorage) < 0)  //当前时间 < 存储时刻
                //{
                //    System.Threading.Thread.Sleep(this.dtStorage - DateTime.Now);  //挂起，避免一直占用CPU
                //}
                //else  //进行存储
                //{
                //    if (this.sendDataList.Count > 0)
                //    {
                //        lock (lockStorage)
                //        {
                //            SendStorage.Insert(this.sqlite, this.sendDataList);
                //            this.sendDataList.Clear();
                //        }
                //    }

                //    dtStorage = dtStorage.AddMinutes(2);  //2分钟存储1次
                //}
            }
        }
        #endregion

















    }
}