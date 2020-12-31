using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using TSFCS.SCOP.Model;
using TSFCS.SCOP.Udp;
using TSFCS.SCOP.DAL;


namespace TSFCS.SCOP.ViewModel
{
    public class DigitViewModel : ViewModelBase
    {

        #region Field
        private ObservableCollection<DigitItemModel> digitSelect;
        private byte currDigitSelect = 0x0;  //数传选择，默认1
        private ObservableCollection<DigitItemModel> digitTransmit;
        private byte currDigitTransmit = 0x0;  //发射机开/关机指令，默认0
        private ObservableCollection<DigitItemModel> digitMode;
        private byte currDigitMode = 0x0;  //数传模式/遥测模式，默认0
        private ObservableCollection<DigitItemModel> digitRefresh;
        private byte currDigitRefresh = 0x0;  //刷新使能/禁止，默认0
        #endregion

        #region Property
        public ObservableCollection<DigitItemModel> DigitSelect
        {
            get { return digitSelect; }
            set 
            { 
                digitSelect = value;
                RaisePropertyChanged("DigitSelect");
            }
        }

        public ObservableCollection<DigitItemModel> DigitTransmit
        {
            get { return digitTransmit; }
            set 
            { 
                digitTransmit = value;
                RaisePropertyChanged("DigitTransmit");
            }
        }

        public ObservableCollection<DigitItemModel> DigitMode
        {
            get { return digitMode; }
            set 
            { 
                digitMode = value;
                RaisePropertyChanged("DigitMode");
            }
        }

        public ObservableCollection<DigitItemModel> DigitRefresh
        {
            get { return digitRefresh; }
            set 
            { 
                digitRefresh = value;
                RaisePropertyChanged("DigitRefresh");
            }
        }
        #endregion

        #region Command
        private bool CanDigitSelectChangedExecute(string select)
        {
            return true;
        }
        private void DigitSelectChangedExecute(string select)
        {
            switch (select)
            {
                case "1":
                    this.currDigitSelect = 0x1B;
                    break;
                case "2":
                    this.currDigitSelect = 0x1C;
                    break;
                default:
                    break;
            }
        }
        public ICommand DigitSelectChangedCommand { get { return new RelayCommand<string>((string select) => DigitSelectChangedExecute(select), CanDigitSelectChangedExecute); } }

        private bool CanDigitDataExecute()
        {
            return true;
        }
        private void DigitDataExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K{0:2X}06", this.currDigitSelect + 0x1B));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand DigitDataCommand { get { return new RelayCommand(DigitDataExecute, CanDigitDataExecute); } }

        private bool CanDigitTransmitChangedExecute(string transmit)
        {
            return true;
        }
        private void DigitTransmitChangedExecute(string transmit)
        {
            switch (transmit)
            {
                case "开机":
                    this.currDigitTransmit = 0x0;
                    break;
                case "关机":
                    this.currDigitTransmit = 0x1;
                    break;
                default:
                    break;
            }
        }
        public ICommand DigitTransmitChangedCommand { get { return new RelayCommand<string>((string transmit) => DigitTransmitChangedExecute(transmit), CanDigitTransmitChangedExecute); } }

        private bool CanDigitTransmitExecute()
        {
            return true;
        }
        private void DigitTransmitExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K{0:2X}0{1:X}", this.currDigitSelect, this.currDigitTransmit));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand DigitTransmitCommand { get { return new RelayCommand(DigitTransmitExecute, CanDigitTransmitExecute); } }


        private bool CanDigitModeChangedExecute(string mode)
        {
            return true;
        }
        private void DigitModeChangedExecute(string mode)
        {
            switch (mode)
            {
                case "遥测":
                    this.currDigitMode = 0x2;
                    break;
                case "数传":
                    this.currDigitMode = 0x3;
                    break;
                default:
                    break;
            }
        }
        public ICommand DigitModeChangedCommand { get { return new RelayCommand<string>((string mode) => DigitModeChangedExecute(mode), CanDigitModeChangedExecute); } }

        private bool CanDigitModeExecute()
        {
            return true;
        }
        private void DigitModeExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K{0:2X}0{1:X}", this.currDigitSelect, this.currDigitMode));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand DigitModeCommand { get { return new RelayCommand(DigitModeExecute, CanDigitModeExecute); } }

        private bool CanDigitRefreshChangedExecute(string refresh)
        {
            return true;
        }
        private void DigitRefreshChangedExecute(string refresh)
        {
            switch (refresh)
            {
                case "使能":
                    this.currDigitRefresh = 0x4;
                    break;
                case "禁止":
                    this.currDigitRefresh = 0x5;
                    break;
                default:
                    break;
            }
        }
        public ICommand DigitRefreshChangedCommand { get { return new RelayCommand<string>((string refresh) => DigitRefreshChangedExecute(refresh), CanDigitRefreshChangedExecute); } }

        private bool CanDigitRefreshExecute()
        {
            return true;
        }
        private void DigitRefreshExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K{0:2X}0{1:X}", this.currDigitSelect, this.currDigitRefresh));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand DigitRefreshCommand { get { return new RelayCommand(DigitRefreshExecute, CanDigitRefreshExecute); } }

        #endregion

        #region Constructor
        public DigitViewModel()
        {
            this.digitSelect = DigitItemModel.GetDigitSelect();
            this.digitTransmit = DigitItemModel.GetDigitTransmit();
            this.digitMode = DigitItemModel.GetDigitMode();
            this.digitRefresh = DigitItemModel.GetDigitRefresh();
        }
        #endregion

        #region Override Method
        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
        }
        #endregion

        #region Messenger Handler
        #endregion
    }
}
