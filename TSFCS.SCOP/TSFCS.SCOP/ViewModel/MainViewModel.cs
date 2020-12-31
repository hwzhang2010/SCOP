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
            Messenger.Default.Register<SendModel>(this, "Storage", HandleStorage);  //Storage, Sqlite3�洢
        }
        #endregion

        #region Messenger Handler
        private void HandleRecv(UdpMessage message)
        {
            if (message != null)
            {
                int length = message.Length;
                byte[] payload = new byte[length];  //��������
                Buffer.BlockCopy(message.Payload, 0, payload, 0, length);  //ȡ����Ч����
                recvQueue.Enqueue(payload);  //��Ч���ݷ������

                GalaSoft.MvvmLight.Threading.DispatcherHelper.CheckBeginInvokeOnUI(new Action(() => { RecvCount += Convert.ToUInt16(length); }));  //ˢ�½��ռ���
            }
        }

        private void HandleSend(UdpMessage message)
        {
            //Messenger.Default.Send<string>(ByteHelper.Bytes2HexStr(message.Payload), "Alert");
            lock (lockSend)
            { 
                //���ͻ�����
                Buffer.BlockCopy(message.Payload, 0, sendByte, 0, CmdOperation.CmdCountConst);  
            }

            sendWaitHandler.Set();  //���¼�״̬��Ϊ��ֹ״̬������ȴ��̼߳���ִ��
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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();  //NLog��־��¼
        private readonly string strConnection = "Data Source=db_scop.db3";
        private readonly int tlmHeader = 13;  //ң��֡ͷ����
        private readonly object lockSend = new object();  //����ʱ��������
        private UdpServer<UdpMessage> udp = null;  //UDP, FastSocket
        private IPEndPoint epSend = null;  //UDP, Ŀ�ĵ�ַ
        private ConcurrentQueue<byte[]> recvQueue = new ConcurrentQueue<byte[]>();  //����bytes����
        private byte[] sendByte = new byte[CmdOperation.CmdCountConst];  //����bytes����
        private AutoResetEvent sendWaitHandler = new AutoResetEvent(false);  //�¼��źţ��߳�ͬ���������߳�֪ͨ�����̣߳������̴߳�������״̬��Ĭ�Ϸ���ֹ������״̬
        private ulong recvCount;  //���ռ���
        private ulong sendCount;  //���ͼ���
        private Sqlite sqlite = new Sqlite("db_scop.db3");  //SQLite3���ݿ�
        private ConcurrentQueue<SendModel> sendQueue = new ConcurrentQueue<SendModel>();  //���ʹ洢
        //private DateTime dtStorage = DateTime.Now.AddMinutes(5);  //���ݶ�ʱ�洢ʱ��
        private TimerWait sysTimer;  //ϵͳʱ��
        private string sysTime;  //ϵͳʱ��
        private string netState;  //����״̬
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
            InitSysTimer();  //��ʼ��ϵͳʱ��
            Init();  //��ʼ��
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
            Destroy();  //������Դ
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
        /// ��ʼ��ϵͳʱ��
        /// </summary>
        private void InitSysTimer()
        {
            this.SysTime = DateTime.Now.ToString("yyyy/MM/dd  HH:mm:ss");   //ϵͳʱ�䣺yyyy��MM��dd��HHʱmm��ss��
            sysTimer = new TimerWait();  //��ʼ����ʱ��
            sysTimer.Elapsed += new EventHandler(ShowSysTimer);  //һֱ��ȡ��ǰʱ��
            sysTimer.MyTimer.Enabled = true;  //������ʱ��
        }

        private void ShowSysTimer(object sender, EventArgs e)
        {
            this.SysTime = DateTime.Now.ToString("yyyy/MM/dd  HH:mm:ss");   //ϵͳʱ�䣺yyyy��MM��dd��HHʱmm��ss��
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            List<IPAddress> groups = new List<IPAddress>();
            using (IDbConnection connection = new SQLiteConnection(strConnection))
            {
                List<UdpModel> sendModelList = connection.Query<UdpModel>("select * from t_udp where type = @type", new { type = "send" }).AsList<UdpModel>();  //��ȡ���Ͷ˵�IP��ַ�Ͷ˿�
                if (sendModelList.Count > 0)
                    this.epSend = new IPEndPoint(IPAddress.Parse(sendModelList[0].Ip), sendModelList[0].Port);

                List<UdpModel> recvModelList = connection.Query<UdpModel>("select * from t_udp where type = @type", new { type = "recv" }).AsList<UdpModel>();  //��ȡ���ն˵�IP��ַ�Ͷ˿�
                if (recvModelList.Count > 0)
                {
                    this.udp = new UdpServer<UdpMessage>(recvModelList[0].Port, new UdpProtocol(), new UdpService());  //UDP, FastSocket
                    for (int i = 0; i < recvModelList.Count; i++)
                        groups.Add(IPAddress.Parse(recvModelList[i].Ip));
                }
                
                List<UdpModel> replyModelList = connection.Query<UdpModel>("select * from t_udp where type = @type", new { type = "reply" }).AsList<UdpModel>();  //��ȡӦ��˵�IP��ַ�Ͷ˿�
                for (int i = 0; i < replyModelList.Count; i++)
                    groups.Add(IPAddress.Parse(replyModelList[i].Ip));

            } 
            this.udp.Start(groups.ToArray());  //��������:UDP

            this.SendCount = this.RecvCount = 0;  //ÿ�����¿�ʼ����
            this.NetState = "UDP������";  //����״̬��UDP�鲥

            //this.udp.SendTo(this.epSend, new byte[] { 0xEB, 0x90, 0xAA, 0xBB });

            //this.Replying = Resend;  //У������ط�

            Task.Factory.StartNew(TaskRecv);  //��������(����)����
            Task.Factory.StartNew(TaskSend);  //������������
            //Task.Factory.StartNew(TaskResend);  //������ʱ�ط�����
            //Task.Factory.StartNew(TaskTimeout);  //������ʱ�������
            Task.Factory.StartNew(TaskStorage);  //�������ݿ�洢����
        }

        /// <summary>
        /// ������Դ
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
        /// ����(����)����
        /// </summary>
        private void TaskRecv()
        {
            while (true)
            {
                if (recvQueue.IsEmpty)  
                {
                    System.Threading.Thread.Sleep(1);  //����1ms������һֱռ��CPU
                }
                else  //�������ݶ����д��ڴ����������
                {
                    byte[] data = null;
                    if (recvQueue.TryDequeue(out data))
                    {
                        //logger.Log(LogLevel.Info, string.Format("SCOP: UDP�յ�����:{0}", ByteHelper.Bytes2HexStr(data)));

                        if (data.Length < 13)  //֡������ >= 13 (����Ӧ��֡)
                            continue;

                        switch (data[4])  //������
                        {
                            case 0x01:  //ң��
                                break;
                            case 0x02:  //ң��
                                ProcessTlm(data);
                                break;
                            case 0x03:  //ң��Ӧ��
                                break;
                            case 0x04:  //ң��Ӧ��
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ң�����ݴ���
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
            else if (flagMain == 0x6)  //����1
            { 
            }
            else if (flagMain == 0x7)  //����2
            {
            }
            else if (flagMain == 0x8)  //����3
            {
            }
            else if (flagMain == 0x9)  //ģ��̫����
            {
            }
            else if (flagMain == 0xA)  //����
            {
            }
            else if (flagMain == 0xB)  //��������X
            {
            }
            else if (flagMain == 0xC)  //��������Y
            {
            }
            else if (flagMain == 0xD)  //��������Z
            {
            }
            else if (flagMain == 0xE)  //��������S
            {
            }
            else if (flagMain == 0x10)  //ң�����
            {
            }
            else if (flagMain == 0x11)  //��ǿ��1
            {
            }
            else if (flagMain == 0x12)  //��ǿ��2
            {
            }
            else if (flagMain == 0x13)  //ʵ���ǿ��
            {
            }
            else if (flagMain == 0x14)  //��������1
            {
            }
            else if (flagMain == 0x15)  //��������2
            {
            }
            else if (flagMain == 0x16)  //��������3
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
        /// GPS���ݽ���
        /// </summary>
        /// <param name="data"></param>
        private void analyzeGps(byte[] data)
        {
            if (data.Length < 66)  //GPS: 51byte, header: 13byte, tail: 2byte
                return;

            Dictionary<int, string> workState = new Dictionary<int, string>();
            workState.Add(0x0, "����λ");
            workState.Add(0x1, "�첶");
            workState.Add(0x2, "��λ");
            workState.Add(0x3, "�ز�");

            List<GpsShowModel> list = new List<GpsShowModel>();
            list.Add(new GpsShowModel() { Num = 0, Name = "����״̬", Hex = data[tlmHeader + 8].ToString("X2"), Cal = workState[data[tlmHeader + 8] % 4] });
            list.Add(new GpsShowModel() { Num = 1, Name = "��λģʽ", Hex = data[tlmHeader + 9].ToString("X2"), Cal = "BD/GPS" });
            list.Add(new GpsShowModel() { Num = 2, Name = "��λ����", Hex = ByteHelper.Bytes2HexStr(data, tlmHeader + 10, 2), Cal = BitConverter.ToUInt16(data, tlmHeader + 10).ToString() });
            list.Add(new GpsShowModel() { Num = 3, Name = "��Ԫʱ��", Hex = ByteHelper.Bytes2HexStr(data, tlmHeader + 14), Cal = string.Format("{0}-{1}-{2} {3}:{4}:{5}.{6}", BitConverter.ToUInt16(data, tlmHeader + 14), data[tlmHeader + 16], data[tlmHeader + 17], data[tlmHeader + 18], data[tlmHeader + 19], data[tlmHeader + 20], BitConverter.ToUInt16(data, tlmHeader + 21)) });
            list.Add(new GpsShowModel() { Num = 4, Name = "����λ��X����(km)", Hex = ByteHelper.Bytes2HexStr(data, tlmHeader + 23), Cal = BitConverter.ToInt32(data, tlmHeader + 23).ToString() });
            list.Add(new GpsShowModel() { Num = 5, Name = "����λ��Y����(km)", Hex = ByteHelper.Bytes2HexStr(data, tlmHeader + 27), Cal = BitConverter.ToInt32(data, tlmHeader + 27).ToString() });
            list.Add(new GpsShowModel() { Num = 6, Name = "����λ��Z����(km)", Hex = ByteHelper.Bytes2HexStr(data, tlmHeader + 31), Cal = BitConverter.ToInt32(data, tlmHeader + 31).ToString() });
            list.Add(new GpsShowModel() { Num = 7, Name = "�����ٶ�X����(km/s)", Hex = ByteHelper.Bytes2HexStr(data, tlmHeader + 35), Cal = BitConverter.ToInt32(data, tlmHeader + 35).ToString() });
            list.Add(new GpsShowModel() { Num = 8, Name = "�����ٶ�Y����(km/s)", Hex = ByteHelper.Bytes2HexStr(data, tlmHeader + 39), Cal = BitConverter.ToInt32(data, tlmHeader + 39).ToString() });
            list.Add(new GpsShowModel() { Num = 9, Name = "�����ٶ�Z����(km/s)", Hex = ByteHelper.Bytes2HexStr(data, tlmHeader + 43), Cal = BitConverter.ToInt32(data, tlmHeader + 43).ToString() });

            Messenger.Default.Send<List<GpsShowModel>>(list, "GpsShow");

        }

        /// <summary>
        /// ��������
        /// </summary>
        private void TaskSend()
        {
            while (true)
            {
                sendWaitHandler.WaitOne();  //������ǰ�̣߳�ֱ��WaitHandler�յ��ź�

                lock (lockSend)
                {
                    this.udp.SendTo(this.epSend, this.sendByte);  //UDP�鲥����ң��ָ��
                    GalaSoft.MvvmLight.Threading.DispatcherHelper.CheckBeginInvokeOnUI(new Action(() => { SendCount += Convert.ToUInt16(CmdOperation.CmdCountConst); }));  //ˢ�·��ͼ���
                }
            }
        }

        /// <summary>
        /// ���ݿ�洢����
        /// </summary>
        private void TaskStorage()
        {
            while (true)
            {
                if (sendQueue.IsEmpty)
                {
                    System.Threading.Thread.Sleep(1);  //����1ms������һֱռ��CPU
                }
                else
                {
                    SendModel model = null;
                    if (sendQueue.TryDequeue(out model))
                    {
                        using (IDbConnection connection = new SQLiteConnection(strConnection))
                        {
                            //ָ��ͼ�¼�������ݿ�
                            connection.Execute("insert into t_send(sendtime, cmdid, cmdname, cmdparam) values(@sendtime, @cmdid, @cmdname, @cmdparam)", model);
                        }
                    }
                }


                //if (DateTime.Compare(DateTime.Now, this.dtStorage) < 0)  //��ǰʱ�� < �洢ʱ��
                //{
                //    System.Threading.Thread.Sleep(this.dtStorage - DateTime.Now);  //���𣬱���һֱռ��CPU
                //}
                //else  //���д洢
                //{
                //    if (this.sendDataList.Count > 0)
                //    {
                //        lock (lockStorage)
                //        {
                //            SendStorage.Insert(this.sqlite, this.sendDataList);
                //            this.sendDataList.Clear();
                //        }
                //    }

                //    dtStorage = dtStorage.AddMinutes(2);  //2���Ӵ洢1��
                //}
            }
        }
        #endregion

















    }
}