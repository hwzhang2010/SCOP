using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using TSFCS.SCOP.Model;
using TSFCS.SCOP.Udp;
using TSFCS.SCOP.DAL;
using System.Collections.Generic;

namespace TSFCS.SCOP.ViewModel
{
    public class AdcsViewModel : ViewModelBase
    {
        #region Field Adcs
        private ObservableCollection<AdcsItemModel> adcsSoftOnOff;
        private byte currAdcsSoftOnOff = 0x0;  //星上软件开关，默认关
        private ObservableCollection<AdcsItemModel> adcsWorkMode;
        private byte currAdcsWorkMode = 0x0;  //星上软件工作模式，默认1
        private ObservableCollection<AdcsCtrlModeModel> adcsCtrlMode;
        private AdcsCtrlModeModel currAdcsCtrlMode;  //控制模式
        private int adcsCtrlT;  //控制周期
        private double adcsAngleMax;  //最大机动角
        private double adcsAngleRateMax;  //最大机动角速度
        private ObservableCollection<AdcsItemModel> adcsMwSelect;
        private byte currAdcsMwSelect = 0x0;  //反作用轮选择，默认反作用轮X
        private int adcsMwSpeedNormal;  //反作用轮额定转速
        private int adcsMwSpeedMax;  //反作用轮最高转速
        private int adcsMwSpeedStep;  //反作用轮最大增速步长

        private ObservableCollection<AdcsDataModel> adcsData;

        #endregion

        #region Property Adcs
        public ObservableCollection<AdcsItemModel> AdcsSoftOnOff
        {
            get { return adcsSoftOnOff; }
            set 
            { 
                adcsSoftOnOff = value;
                RaisePropertyChanged("AdcsSoftOnOff");
            }
        }
        public ObservableCollection<AdcsItemModel> AdcsWorkMode
        {
            get { return adcsWorkMode; }
            set
            { 
                adcsWorkMode = value;
                RaisePropertyChanged("AdcsWorkMode");
            }
        }

        public ObservableCollection<AdcsCtrlModeModel> AdcsCtrlMode
        {
            get { return adcsCtrlMode; }
            set
            { 
                adcsCtrlMode = value;
                RaisePropertyChanged("AdcsCtrlMode");
            }
        }

        public AdcsCtrlModeModel CurrAdcsCtrlMode
        {
            get { return currAdcsCtrlMode; }
            set 
            {
                currAdcsCtrlMode = value;
                RaisePropertyChanged("CurrAdcsCtrlMode");
            }
        }

        public int AdcsCtrlT
        {
            get { return adcsCtrlT; }
            set 
            { 
                adcsCtrlT = value;
                RaisePropertyChanged("AdcsCtrlT");
            }
        }

        public double AdcsAngleMax
        {
            get { return adcsAngleMax; }
            set
            { 
                adcsAngleMax = value;
                RaisePropertyChanged("AdcsAngleMax");
            }
        }

        public double AdcsAngleRateMax
        {
            get { return adcsAngleRateMax; }
            set 
            { 
                adcsAngleRateMax = value;
                RaisePropertyChanged("AdcsAngleRateMax");
            }
        }

        public ObservableCollection<AdcsItemModel> AdcsMwSelect
        {
            get { return adcsMwSelect; }
            set
            { 
                adcsMwSelect = value;
                RaisePropertyChanged("AdcsMwSelect");
            }
        }

        public int AdcsMwSpeedNormal
        {
            get { return adcsMwSpeedNormal; }
            set
            { 
                adcsMwSpeedNormal = value;
                RaisePropertyChanged("AdcsMwSpeedNormal");
            }
        }

        public int AdcsMwSpeedMax
        {
            get { return adcsMwSpeedMax; }
            set 
            { 
                adcsMwSpeedMax = value;
                RaisePropertyChanged("AdcsMwSpeedMax");
            }
        }

        public int AdcsMwSpeedStep
        {
            get { return adcsMwSpeedStep; }
            set 
            { 
                adcsMwSpeedStep = value;
                RaisePropertyChanged("AdcsMwSpeedStep");
            }
        }

        public ObservableCollection<AdcsDataModel> AdcsData
        {
            get { return adcsData; }
            set 
            { 
                adcsData = value;
                RaisePropertyChanged("AdcsData");
            }
        }

        
        #endregion

        #region Command Adcs
        private bool CanAdcsSoftOnOffChangedExecute(string onOff)
        {
            return true;
        }
        private void AdcsSoftOnOffChangedExecute(string onOff)
        {
            switch (onOff)
            {
                case "关":
                    this.currAdcsSoftOnOff = 0x0;
                    break;
                case "开":
                    this.currAdcsSoftOnOff = 0x1;
                    break;
                default:
                    break;
            }
        }
        public ICommand AdcsSoftOnOffChangedCommand { get { return new RelayCommand<string>((string onOff) => AdcsSoftOnOffChangedExecute(onOff), CanAdcsSoftOnOffChangedExecute); } }

        private bool CanAdcsSoftOnOffExecute()
        {
            return true;
        }
        private void AdcsSoftOnOffExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1800");
            cmdList.Add(this.currAdcsSoftOnOff);

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand AdcsSoftOnOffCommand { get { return new RelayCommand(AdcsSoftOnOffExecute, CanAdcsSoftOnOffExecute); } }

        private bool CanAdcsConfigUpdateExecute()
        {
            return true;
        }
        private void AdcsConfigUpdateExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1801");

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand AdcsConfigUpdateCommand { get { return new RelayCommand(AdcsConfigUpdateExecute, CanAdcsConfigUpdateExecute); } }

        private bool CanAdcsOrbitUpdateExecute()
        {
            return true;
        }
        private void AdcsOrbitUpdateExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1802");

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand AdcsOrbitUpdateCommand { get { return new RelayCommand(AdcsOrbitUpdateExecute, CanAdcsOrbitUpdateExecute); } }

        private bool CanAdcsWorkModeChangedExecute(string workMode)
        {
            return true;
        }
        private void AdcsWorkModeChangedExecute(string workMode)
        {
            switch (workMode)
            {
                case "0":
                    this.currAdcsWorkMode = 0x0;
                    break;
                case "1":
                    this.currAdcsWorkMode = 0x1;
                    break;
                case "2":
                    this.currAdcsWorkMode = 0x2;
                    break;
                default:
                    break;
            }
        }
        public ICommand AdcsWorkModeChangedCommand { get { return new RelayCommand<string>((string workMode) => AdcsWorkModeChangedExecute(workMode), CanAdcsWorkModeChangedExecute); } }

        private bool CanAdcsWorkModeExecute()
        {
            return true;
        }
        private void AdcsWorkModeExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1803");
            cmdList.Add(this.currAdcsWorkMode);

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand AdcsWorkModeCommand { get { return new RelayCommand(AdcsWorkModeExecute, CanAdcsWorkModeExecute); } }


        private bool CanAdcsCtrlModeChangedExecute(int id)
        {
            return true;
        }
        private void AdcsCtrlModeChangedExecute(int id)
        {
            //Messenger.Default.Send<string>(id.ToString(), "Alert");
        }
        public ICommand AdcsCtrlModeChangedCommand { get { return new RelayCommand<int>((id) => AdcsCtrlModeChangedExecute(id), CanAdcsCtrlModeChangedExecute); } }

        private bool CanAdcsCtrlModeExecute()
        {
            return true;
        }
        private void AdcsCtrlModeExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1804");
            cmdList.Add((byte)this.currAdcsCtrlMode.Id);

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand AdcsCtrlModeCommand { get { return new RelayCommand(AdcsCtrlModeExecute, CanAdcsCtrlModeExecute); } }


        private bool CanAdcsCtrlTExecute()
        {
            return true;
        }
        private void AdcsCtrlTExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1805");
            cmdList.Add((byte)this.AdcsCtrlT);

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand AdcsCtrlTCommand { get { return new RelayCommand(AdcsCtrlTExecute, CanAdcsCtrlTExecute); } }

        private bool CanAdcsAngleMaxExecute()
        {
            return true;
        }
        private void AdcsAngleMaxExecute()
        {
            Messenger.Default.Send<string>(this.AdcsAngleMax.ToString(), "Alert");
            List<byte> cmdList = CmdOperation.genCmdByte("K1806");
            cmdList.AddRange(BitConverter.GetBytes(this.AdcsAngleMax));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand AdcsAngleMaxCommand { get { return new RelayCommand(AdcsAngleMaxExecute, CanAdcsAngleMaxExecute); } }

        private bool CanAdcsAngleRateMaxExecute()
        {
            return true;
        }
        private void AdcsAngleRateMaxExecute()
        {
            Messenger.Default.Send<string>(this.AdcsAngleRateMax.ToString(), "Alert");
            List<byte> cmdList = CmdOperation.genCmdByte("K1807");
            cmdList.AddRange(BitConverter.GetBytes(this.AdcsAngleRateMax));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand AdcsAngleRateMaxCommand { get { return new RelayCommand(AdcsAngleRateMaxExecute, CanAdcsAngleRateMaxExecute); } }



        private bool CanAdcsMwSelectChangedExecute(string mwSelect)
        {
            return true;
        }
        private void AdcsMwSelectChangedExecute(string mwSelect)
        {
            switch (mwSelect)
            {
                case "X":
                    this.currAdcsMwSelect = 0x0;
                    break;
                case "Y":
                    this.currAdcsMwSelect = 0x1;
                    break;
                case "Z":
                    this.currAdcsMwSelect = 0x2;
                    break;
                case "S":
                    this.currAdcsMwSelect = 0x3;
                    break;
                default:
                    break;
            }
        }
        public ICommand AdcsMwSelectChangedCommand { get { return new RelayCommand<string>((string mwSelect) => AdcsMwSelectChangedExecute(mwSelect), CanAdcsMwSelectChangedExecute); } }

        private bool CanAdcsMwSpeedNormalExecute()
        {
            return true;
        }
        private void AdcsMwSpeedNormalExecute()
        {
            Messenger.Default.Send<string>(string.Format("K180{0:X}", this.currAdcsMwSelect + 0x8), "Alert");
            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K180{0:X}", this.currAdcsMwSelect + 0x8));
            cmdList.AddRange(BitConverter.GetBytes(this.AdcsMwSpeedNormal));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand AdcsMwSpeedNormalCommand { get { return new RelayCommand(AdcsMwSpeedNormalExecute, CanAdcsMwSpeedNormalExecute); } }

        private bool CanAdcsMwSpeedMaxExecute()
        {
            return true;
        }
        private void AdcsMwSpeedMaxExecute()
        {
            //Messenger.Default.Send<string>(string.Format("K180{0:X}", this.currAdcsMwSelect + 0xC), "Alert");
            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K180{0:X}", this.currAdcsMwSelect + 0xC));
            cmdList.AddRange(BitConverter.GetBytes(this.AdcsMwSpeedMax));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand AdcsMwSpeedMaxCommand { get { return new RelayCommand(AdcsMwSpeedMaxExecute, CanAdcsMwSpeedMaxExecute); } }

        private bool CanAdcsMwSpeedStepExecute()
        {
            return true;
        }
        private void AdcsMwSpeedStepExecute()
        {
            //Messenger.Default.Send<string>(string.Format("K181{0:X}", this.currAdcsMwSelect), "Alert");
            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K181{0:X}", this.currAdcsMwSelect));
            cmdList.AddRange(BitConverter.GetBytes(this.AdcsMwSpeedStep));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand AdcsMwSpeedStepCommand { get { return new RelayCommand(AdcsMwSpeedStepExecute, CanAdcsMwSpeedStepExecute); } }

        private bool CanAdcsDataExecute()
        {
            return true;
        }
        private void AdcsDataExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1814");

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand AdcsDataCommand { get { return new RelayCommand(AdcsDataExecute, CanAdcsDataExecute); } }
        #endregion

        #region Field Mw
        private ObservableCollection<MwItemModel> mwSelect;
        private byte currMwSelect = 0x0;  //反作用轮哪个
        private ObservableCollection<MwItemModel> mwMode;
        private byte mwMod = 0x0;  //速度/力矩模式选择，默认速度
        private int mwSpeed;  //速度
        private int mwMoment;  //力矩
        private ObservableCollection<MwDataModel> mwData;
        #endregion

        #region Property Mw
        public ObservableCollection<MwItemModel> MwSelect
        {
            get { return mwSelect; }
            set
            {
                mwSelect = value;
                RaisePropertyChanged("MwSelect");
            }
        }
        public ObservableCollection<MwItemModel> MwMode
        {
            get { return mwMode; }
            set
            {
                mwMode = value;
                RaisePropertyChanged("MwMode");
            }
        }
        public int MwSpeed
        {
            get { return mwSpeed; }
            set 
            { 
                mwSpeed = value;
                RaisePropertyChanged("MwSpeed");
            }
        }
        public int MwMoment
        {
            get { return mwMoment; }
            set 
            {
                mwMoment = value;
                RaisePropertyChanged("MwMoment");
            }
        }
        public ObservableCollection<MwDataModel> MwData
        {
            get { return mwData; }
            set
            {
                mwData = value;
                RaisePropertyChanged("MwData");
            }
        }
        #endregion

        #region Command Mw
        private bool CanMwSelectChangedExecute(string select)
        {
            return true;
        }
        private void MwSelectChangedExecute(string select)
        {
            switch (select)
            {
                case "X":
                    this.currMwSelect = 0x0;
                    break;
                case "Y":
                    this.currMwSelect = 0x1;
                    break;
                case "Z":
                    this.currMwSelect = 0x2;
                    break;
                case "S":
                    this.currMwSelect = 0x3;
                    break;
                default:
                    break;
            }
        }
        public ICommand MwSelectChangedCommand { get { return new RelayCommand<string>((string select) => MwSelectChangedExecute(select), CanMwSelectChangedExecute); } }

        private bool CanMwModeExecute(string mode)
        {
            return true;
        }
        private void MwModeExecute(string mode)
        {
            switch (mode)
            {
                case "速度":
                    this.mwMod = 0x0;
                    break;
                case "力矩":
                    this.mwMod = 0x1;
                    break;
                default:
                    break;
            }
        }
        public ICommand MwModeCommand { get { return new RelayCommand<string>((string mode) => MwModeExecute(mode), CanMwModeExecute); } }


        private bool CanMwExecute()
        {
            return true;
        }
        private void MwExecute()
        {
            Messenger.Default.Send<string>(string.Format("K0{0:X}0{1:X}", this.currMwSelect + 0xB, this.mwMod), "Alert");
            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K0{0:X}0{1:X}", this.currMwSelect + 0xB, this.mwMod));
            cmdList.AddRange(BitConverter.GetBytes(this.MwSpeed));
            cmdList.AddRange(BitConverter.GetBytes(this.MwMoment));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand MwCommand { get { return new RelayCommand(MwExecute, CanMwExecute); } }
        #endregion

        #region Field Mm
        private ObservableCollection<MmItemModel> mmSelect;
        private byte currMmSelect = 0x0;  //磁强计哪个
        private ObservableCollection<MmDataModel> mmData;
        private ObservableCollection<MmDataModel> tmrData;
        #endregion

        #region Property Mm
        public ObservableCollection<MmItemModel> MmSelect
        {
            get { return mmSelect; }
            set 
            {
                mmSelect = value;
                RaisePropertyChanged("MmSelect");
            }
        }
        public ObservableCollection<MmDataModel> MmData
        {
            get { return mmData; }
            set
            {
                mmData = value;
                RaisePropertyChanged("MmData");
            }
        }
        public ObservableCollection<MmDataModel> TmrData
        {
            get { return tmrData; }
            set
            {
                tmrData = value;
                RaisePropertyChanged("TmrData");
            }
        }
        #endregion

        #region Command Mm
        private bool CanMmSelectChangedExecute(string select)
        {
            return true;
        }
        private void MmSelectChangedExecute(string select)
        {
            Messenger.Default.Send<string>(select, "Alert");
            switch (select)
            {
                case "磁强计1":
                    this.currMmSelect = 0x0;
                    break;
                case "磁强计2":
                    this.currMmSelect = 0x1;
                    break;
                default:
                    break;
            }
        }
        public ICommand MmSelectChangedCommand { get { return new RelayCommand<string>((string select) => MmSelectChangedExecute(select), CanMmSelectChangedExecute); } }

        private bool CanMmDataExecute()
        {
            return true;
        }
        private void MmDataExecute()
        {
            Messenger.Default.Send<string>(string.Format("K1{0:X}00", this.currMmSelect + 0x1), "Alert");
            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K0{0:X}00", this.currMmSelect + 0x1));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand MmDataCommand { get { return new RelayCommand(MmDataExecute, CanMmDataExecute); } }

        private bool CanTmrDataExecute()
        {
            return true;
        }
        private void TmrDataExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1300");

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand TmrDataCommand { get { return new RelayCommand(TmrDataExecute, CanTmrDataExecute); } }
        #endregion

        #region Field Mt
        private ObservableCollection<MtItemModel> mtAxis;
        private byte currMtAx = 0x0;  //磁力矩器轴向，默认+X
        private int mtPwm;  //占空比
        #endregion

        #region Property Mt
        public ObservableCollection<MtItemModel> MtAxis
        {
            get { return mtAxis; }
            set
            {
                mtAxis = value;
                RaisePropertyChanged("MtAxis");
            }
        }
        public int MtPwm
        {
            get { return mtPwm; }
            set 
            { 
                mtPwm = value;
                RaisePropertyChanged("MtPwm");
            }
        }
        #endregion

        #region Command Mt
        private bool CanMtAxisChangedExecute(string axis)
        {
            return true;
        }
        private void MtAxisChangedExecute(string axis)
        {
            switch (axis)
            {
                case "X轴":
                    this.currMtAx = 0x0;
                    break;
                case "Y轴":
                    this.currMtAx = 0x1;
                    break;
                case "Z轴":
                    this.currMtAx = 0x2;
                    break;
                default:
                    break;
            }
        }
        public ICommand MtAxisChangedCommand { get { return new RelayCommand<string>((string axis) => MtAxisChangedExecute(axis), CanMtAxisChangedExecute); } }

        private bool CanMtExecute()
        {
            return true;
        }
        private void MtExecute()
        {
            //Messenger.Default.Send<string>(pwm.ToString(), "Alert");
            Messenger.Default.Send<string>(string.Format("K1{0:X}00", this.currMtAx + 0x4), "Alert");
            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K1{0:X}00", this.currMtAx + 0x4));
            cmdList.Add((byte)this.MtPwm); Messenger.Default.Send<string>(this.MtPwm.ToString(), "Alert");

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand MtCommand { get { return new RelayCommand(MtExecute, CanMtExecute); } }
        #endregion




        #region Field Star
        private ObservableCollection<StarItemModel> starSelect;
        private byte currStarSelect = 0x0;  //星敏选择，默认星敏1
        private ObservableCollection<StarItemModel> starWorkMode;
        private byte currStarWorkMode = 0x0;  //工作模式，默认自检
        private ObservableCollection<StarItemModel> starFilter;
        private byte currStarFilter = 0x0;  //四元数滤波
        private ObservableCollection<StarItemModel> starPict;
        private byte currStarPict = 0x0;  //图像传输
        private ObservableCollection<StarDataModel> starData;
        #endregion

        #region Property Star
        public ObservableCollection<StarItemModel> StarSelect
        {
            get { return starSelect; }
            set
            { 
                starSelect = value;
                RaisePropertyChanged("StarSelect");
            }
        }
        
        public ObservableCollection<StarItemModel> StarWorkMode
        {
            get { return starWorkMode; }
            set
            { 
                starWorkMode = value;
                RaisePropertyChanged("StarWorkMode");
            }
        }
        public ObservableCollection<StarItemModel> StarFilter
        {
            get { return starFilter; }
            set 
            { 
                starFilter = value;
                RaisePropertyChanged("StarFilter");
            }
        }
        public ObservableCollection<StarItemModel> StarPict
        {
            get { return starPict; }
            set 
            { 
                starPict = value;
                RaisePropertyChanged("StarPict");
            }
        }
        public ObservableCollection<StarDataModel> StarData
        {
            get { return starData; }
            set
            {
                starData = value;
                RaisePropertyChanged("StarData");
            }
        }
        #endregion

        #region Command Star
        private bool CanStarSelectChangedExecute(string select)
        {
            return true;
        }
        private void StarSelectChangedExecute(string select)
        {
            switch (select)
            {
                case "1":
                    this.currStarSelect = 0x0;
                    break;
                case "2":
                    this.currStarSelect = 0x1;
                    break;
                case "3":
                    this.currStarSelect = 0x2;
                    break;
                default:
                    break;
            }
        }
        public ICommand StarSelectChangedCommand { get { return new RelayCommand<string>((string select) => StarSelectChangedExecute(select), CanStarSelectChangedExecute); } }

        private bool CanStarDataExecute()
        {
            return true;
        }
        private void StarDataExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K0{0:X}0B", this.currStarSelect + 0x6));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");

        }
        public ICommand StarDataCommand { get { return new RelayCommand(StarDataExecute, CanStarDataExecute); } }

        private bool CanStarWorkModeChangedExecute(string workMode)
        {
            return true;
        }
        private void StarWorkModeChangedExecute(string workMode)
        {
            switch (workMode)
            {
                case "自检":
                    this.currStarWorkMode = 0x0;
                    break;
                case "正常":
                    this.currStarWorkMode = 0x1;
                    break;
                case "自适应":
                    this.currStarWorkMode = 0x2;
                    break;
                default:
                    break;
            }
        }
        public ICommand StarWorkModeChangedCommand { get { return new RelayCommand<string>((workMode) => StarWorkModeChangedExecute(workMode), CanStarWorkModeChangedExecute); } }

        private bool CanStarWorkModeExecute()
        {
            return true;
        }
        private void StarWorkModeExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K0{0:X}00", this.currStarSelect + 0x6));
            cmdList.Add(this.currStarWorkMode);

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");

        }
        public ICommand StarWorkModeCommand { get { return new RelayCommand(StarWorkModeExecute, CanStarWorkModeExecute); } }

        private bool CanStarSecondExecute(string second)
        {
            return true;
        }
        private void StarSecondExecute(string second)
        {
            int sec = default(int);
            int.TryParse(second, out sec);

            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K0{0:X}01", this.currStarSelect + 0x6));
            cmdList.AddRange(BitConverter.GetBytes(sec));
            
            CmdOperation.makeCmdByte(ref cmdList);
            
            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst; 

            Messenger.Default.Send<UdpMessage>(cmd, "Send");

            SendModel sendModel = CmdOperation.genSendByCmd(string.Format("K0{0:X}01", this.currStarSelect + 0x6));
            sendModel.CmdParam = second;
            Messenger.Default.Send<SendModel>(sendModel, "Storage");
        }
        public ICommand StarSecondCommand { get { return new RelayCommand<string>((second) => StarSecondExecute(second), CanStarSecondExecute); } }

        private bool CanStarResetExecute()
        {
            return true;
        }
        private void StarResetExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K0{0:X}0A", this.currStarSelect + 0x6));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand StarResetCommand { get { return new RelayCommand(StarResetExecute, CanStarResetExecute); } }

        private bool CanStarNaviExecute(string navi)
        {
            return true;
        }
        private void StarNaviExecute(string navi)
        {
            //Messenger.Default.Send<string>(navi, "Alert");

            int n = default(int);
            int.TryParse(navi, out n);

            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K0{0:X}02", this.currStarSelect + 0x6));
            cmdList.AddRange(BitConverter.GetBytes(n));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand StarNaviCommand { get { return new RelayCommand<string>((navi) => StarNaviExecute(navi), CanStarNaviExecute); } }

        private bool CanStarSeekExecute(string seek)
        {
            return true;
        }
        private void StarSeekExecute(string seek)
        {
            //Messenger.Default.Send<string>(seek, "Alert");

            int s = default(int);
            int.TryParse(seek, out s);

            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K0{0:X}03", this.currStarSelect + 0x6));
            cmdList.AddRange(BitConverter.GetBytes(s));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand StarSeekCommand { get { return new RelayCommand<string>((seek) => StarSeekExecute(seek), CanStarSeekExecute); } }

        private bool CanStarFilterChangedExecute(string filter)
        {
            return true;
        }
        private void StarFilterChangedExecute(string filter)
        {
            switch (filter)
            {
                case "关闭":
                    this.currStarFilter = 0x0;
                    break;
                case "打开":
                    this.currStarFilter = 0x1;
                    break;
                default:
                    break;
            }
        }
        public ICommand StarFilterChangedCommand { get { return new RelayCommand<string>((filter) => StarFilterChangedExecute(filter), CanStarFilterChangedExecute); } }

        private bool CanStarFilterExecute()
        {
            return true;
        }
        private void StarFilterExecute()
        {
            //Messenger.Default.Send<string>(this.currStarFilter.ToString(), "Alert");

            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K0{0:X}04", this.currStarSelect + 0x6));
            cmdList.Add(this.currStarFilter);

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand StarFilterCommand { get { return new RelayCommand(StarFilterExecute, CanStarFilterExecute); } }

        private bool CanStarFollowExecute(string follow)
        {
            return true;
        }
        private void StarFollowExecute(string follow)
        {
            //Messenger.Default.Send<string>(follow, "Alert");

            int f = default(int);
            int.TryParse(follow, out f);

            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K0{0:X}05", this.currStarSelect + 0x6));
            cmdList.AddRange(BitConverter.GetBytes(f));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand StarFollowCommand { get { return new RelayCommand<string>((follow) => StarFollowExecute(follow), CanStarFollowExecute); } }

        private bool CanStarExpoExecute(string expo)
        {
            return true;
        }
        private void StarExpoExecute(string expo)
        {
            //Messenger.Default.Send<string>(expo, "Alert");

            int e = default(int);
            int.TryParse(expo, out e);

            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K0{0:X}08", this.currStarSelect + 0x6));
            cmdList.AddRange(BitConverter.GetBytes(e));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand StarExpoCommand { get { return new RelayCommand<string>((expo) => StarExpoExecute(expo), CanStarExpoExecute); } }

        private bool CanStarFixExecute(string fix)
        {
            return true;
        }
        private void StarFixExecute(string fix)
        {
            //Messenger.Default.Send<string>(fix, "Alert");

            int f = default(int);
            int.TryParse(fix, out f);

            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K0{0:X}09", this.currStarSelect + 0x6));
            cmdList.AddRange(BitConverter.GetBytes(f));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand StarFixCommand { get { return new RelayCommand<string>((fix) => StarFixExecute(fix), CanStarFixExecute); } }

        private bool CanStarFormationExecute(string formation)
        {
            return true;
        }
        private void StarFormationExecute(string formation)
        {
            //Messenger.Default.Send<string>(formation, "Alert");

            int f = default(int);
            int.TryParse(formation, out f);

            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K0{0:X}07", this.currStarSelect + 0x6));
            cmdList.AddRange(BitConverter.GetBytes(f));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand StarFormationCommand { get { return new RelayCommand<string>((formation) => StarFormationExecute(formation), CanStarFormationExecute); } }

        private bool CanStarPictChangedExecute(string pict)
        {
            return true;
        }
        private void StarPictChangedExecute(string pict)
        {
            switch (pict)
            {
                case "关闭":
                    this.currStarPict = 0x0;
                    break;
                case "打开":
                    this.currStarPict = 0x1;
                    break;
                default:
                    break;
            }
        }
        public ICommand StarPictChangedCommand { get { return new RelayCommand<string>((pict) => StarPictChangedExecute(pict), CanStarPictChangedExecute); } }

        private bool CanStarPictExecute()
        {
            return true;
        }
        private void StarPictExecute()
        {
            //Messenger.Default.Send<string>(this.stPict.ToString(), "Alert");

            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K0{0:X}06", this.currStarSelect + 0x6));
            cmdList.Add(this.currStarPict);

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand StarPictCommand { get { return new RelayCommand(StarPictExecute, CanStarPictExecute); } }
        #endregion

        #region Field Ass
        private ObservableCollection<AssDataModel> assData;
        #endregion

        #region Property Ass
        public ObservableCollection<AssDataModel> AssData
        {
            get { return assData; }
            set
            {
                assData = value;
                RaisePropertyChanged("AssData");
            }
        }
        #endregion

        #region Command Ass
        private bool CanAssDataExecute()
        {
            return true;
        }
        private void AssDataExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K0900");

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand AssDataCommand { get { return new RelayCommand(AssDataExecute, CanAssDataExecute); } }
        #endregion

 

        #region Field Gyro
        private ObservableCollection<GyroModel> gyroData;
        #endregion

        #region Property Gyro
        public ObservableCollection<GyroModel> GyroData
        {
            get { return gyroData; }
            set 
            { 
                gyroData = value;
                RaisePropertyChanged("GyroData");
            }
        }
        #endregion

        #region Command Gyro
        private bool CanGyroResetExecute()
        {
            return true;
        }
        private void GyroResetExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K0A01");

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand GyroResetCommand { get { return new RelayCommand(GyroResetExecute, CanGyroResetExecute); } }

        private bool CanGyroQueryExecute()
        {
            return true;
        }
        private void GyroQueryExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K0A00");

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand GyroQueryCommand { get { return new RelayCommand(GyroQueryExecute, CanGyroQueryExecute); } }
        #endregion

     

        #region Constructor
        public AdcsViewModel()
        {
            this.adcsSoftOnOff = AdcsItemModel.GetAdcsSoftOnOff();
            this.adcsWorkMode = AdcsItemModel.GetAdcsWorkMode();
            this.adcsCtrlMode = AdcsCtrlModeModel.GetAdcsCtrlMode();
            if (this.adcsCtrlMode.Count > 0)
                this.currAdcsCtrlMode = this.adcsCtrlMode[0];
            this.adcsMwSelect = AdcsItemModel.GetAdcsMwSelect();
            AdcsData = AdcsDataModel.GetAdcsData();

            MwSelect = MwItemModel.GetMwSelect();
            MwMode = MwItemModel.GetMwMode();
            MwData = MwDataModel.GetMwData();

            MmSelect = MmItemModel.GetMmSelect();
            MmData = MmDataModel.GetMmData();
            TmrData = MmDataModel.GetTmrData();

            MtAxis = MtItemModel.GetMtAxis();

            this.starSelect = StarItemModel.GetStarSelect();
            this.starWorkMode = StarItemModel.GetStarWorkMode();
            this.starFilter = StarItemModel.GetStarFilter();
            this.starPict = StarItemModel.GetStarPict();
            this.starData = StarDataModel.GetStarData();

            this.assData = AssDataModel.GetAssData();

            this.gyroData = GyroModel.GetGyroData();

            
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
