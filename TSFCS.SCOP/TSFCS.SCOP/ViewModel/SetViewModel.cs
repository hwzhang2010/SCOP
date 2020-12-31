using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace TSFCS.SCOP.ViewModel
{
    public class SetViewModel : ViewModelBase
    {
        #region Field
        private string addrRecv;  //组播地址：遥测
        private string addrSend;  //组播地址：遥控
        private int port;  //组播端口
        #endregion

        #region Property
        public string AddrRecv
        {
            get { return addrRecv; }
            set
            {
                addrRecv = value;
                RaisePropertyChanged("AddrRecv");
            }
        }
        public string AddrSend
        {
            get { return addrSend; }
            set
            {
                addrSend = value;
                RaisePropertyChanged("AddrSend");
            }
        }
        public int Port
        {
            get { return port; }
            set
            {
                port = value;
                RaisePropertyChanged("Port");
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
            LoadConfig();  //加载配置信息
        }
        public ICommand LoadedCommand { get { return new RelayCommand(LoadedExecute, CanLoadedExecute); } }

        private bool CanDragMoveExecute()
        {
            return true;
        }
        private void DragMoveExecute()
        {
            Messenger.Default.Send<string>("DragMove", "Set");
        }
        public ICommand DragMoveCommand { get { return new RelayCommand(DragMoveExecute, CanDragMoveExecute); } }

        private bool CanClosedExecute()
        {
            return true;
        }
        private void ClosedExecute()
        {
            Messenger.Default.Send<string>("Closed", "Set");
        }
        public ICommand ClosedCommand { get { return new RelayCommand(ClosedExecute, CanClosedExecute); } }

        public bool CanOkExecute()
        {
            return true;
        }
        public void OkExecute()
        {
            SaveConfig();  //保存配置信息
            Messenger.Default.Send<bool>(true, "Set");
        }
        public ICommand OkCommand { get { return new RelayCommand(OkExecute, CanOkExecute); } }

        public bool CanCancelExecute()
        {
            return true;
        }
        public void CancelExecute()
        {
            Messenger.Default.Send<bool>(false, "Set");
        }
        public ICommand CancelCommand { get { return new RelayCommand(CancelExecute, CanCancelExecute); } }
        #endregion

        #region Constructor
        public SetViewModel()
        {
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

        #region 配置文件
        // 
        // 保存的配置信息如下：
        //
        // 0. 组播地址(遥测)
        // 1. 组播地址(遥控)
        // 2. 组播端口
        //
        /// <summary>
        /// 保存配置信息
        /// </summary>
        private void SaveConfig()
        {
            Configuration config = new Configuration();  //配置对象实例
            config.Add("AddrRecv", this.AddrRecv);  //保存组播地址(遥测)
            config.Add("AddrSend", this.AddrSend);  //保存组播地址(遥控)
            config.Add("Port", this.Port);  //保存组播端口
            //Configuration.Save(config, @"Config\config.json");  //保存配置信息到磁盘中
            Configuration.Save(config);  //保存配置信息到磁盘中
        }

        /// <summary>
        /// 加载配置信息
        /// </summary>
        private bool LoadConfig()
        {
            //Configuration config = Configuration.Read(@"Config\config.json");
            Configuration config = Configuration.Read();
            if (config == null)
                return false;

            this.AddrRecv = config.GetString("AddrRecv");  //获取组播地址(遥测)
            this.AddrSend = config.GetString("AddrSend");  //获取组播地址(遥控)
            this.Port = config.GetInt("Port");  //获取组播端口

            return true;
        }
        #endregion
    }
}
