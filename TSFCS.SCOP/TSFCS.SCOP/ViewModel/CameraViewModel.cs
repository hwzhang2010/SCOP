using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using TSFCS.SCOP.Model;
using TSFCS.SCOP.Udp;
using TSFCS.SCOP.DAL;

namespace TSFCS.SCOP.ViewModel
{
    public class CameraViewModel : ViewModelBase
    {
        #region Field
        private ObservableCollection<CameraParamModel> cameraParam;  //工程参数
        private ObservableCollection<CameraResponseModel> cameraResponse;  //指令应答
        private ObservableCollection<CameraResponseModel> cameraFileResponse;  //文件应答
        private ObservableCollection<CameraItemModel> cameraPictMode;  //成像模式
        private byte currCameraPictMode = 0x0;  //33H：推扫相机TDI成像, 55H：推扫相机前端自校
        private ObservableCollection<CameraItemModel> cameraDigitMode;  //数传模式
        private byte currCameraDigitMode = 0x2;  //11H：STD有损压缩图像下传, 22H：STD无损压缩图像下传
        private ObservableCollection<CameraItemModel> cameraPict;  //拍照指令
        private byte currCameraPict = 0x4;
        private byte cameraPictStart;  //原始图像文件号：范围：01H～FFH
        private ObservableCollection<CameraItemModel> cameraDigit;  //数传指令
        private byte currCameraDigit = 0x6;
        private byte cameraDigitStart;  //数传文件号：范围：01H～FFH
        private byte cameraDigitOffset;  //数传文件偏移：范围：00H～FFH

        private ObservableCollection<CameraItemModel> cameraIntegralDirection;  //积分方向
        private byte currCameraIntegralDirection = 0xB;

        private byte cameraFullIntegral;  //全色谱段积分级数：范围：00H～FFH
        private byte cameraMultiIntegral;  //多光谱段积分级数：范围：00H～FFH
        private byte cameraFullGain;  //全色谱段数字增益：范围：00H～05H
        private byte cameraMultiGain;  //多光谱段数字增益：范围：00H～05H

        private Dictionary<int, string> cameraAnalogGain;  //模拟增益
        private byte currAnalogGain;  //
        private ushort cameraLine;  //行周期

        private ObservableCollection<CameraItemModel> cameraBus;  //总线测试
        private byte currCameraBus = 0x0;

        private ushort cameraSpectralFullLine;  //全色谱段开窗行数设置: 推扫相机P谱段：范围：0000H～0FFFH
        private ushort cameraSpectralLineR;  //多光谱段开窗行数设置: 推扫相机R谱段：范围：0000H～0FFFH
        private ushort cameraSpectralLineG;  //多光谱段开窗行数设置: 推扫相机G谱段：范围：0000H～0FFFH
        private ushort cameraSpectralLineB;  //多光谱段开窗行数设置: 推扫相机B谱段：范围：0000H～0FFFH
        private ushort cameraSpectralLineIR;  //多光谱段开窗行数设置: 推扫相机IR谱段：范围：0000H～0FFFH
        private ushort cameraSpectralFullStart;  //全色谱段开窗起始行设置: 推扫相机P谱段：范围：0000H～0FFFH
        private ushort cameraSpectralStartR;  //多光谱段开窗起始行设置: 推扫相机R谱段：范围：0000H～0FFFH
        private ushort cameraSpectralStartG;  //多光谱段开窗起始行设置: 推扫相机G谱段：范围：0000H～0FFFH
        private ushort cameraSpectralStartB;  //多光谱段开窗起始行设置: 推扫相机B谱段：范围：0000H～0FFFH
        private ushort cameraSpectralStartIR;  //多光谱段开窗起始行设置: 推扫相机IR谱段：范围：0000H～0FFFH

        #endregion

        #region Property
        public ObservableCollection<CameraParamModel> CameraParam
        {
            get { return cameraParam; }
            set 
            { 
                cameraParam = value;
                RaisePropertyChanged("CameraParam");
            }
        }
        public ObservableCollection<CameraResponseModel> CameraResponse
        {
            get { return cameraResponse; }
            set 
            { 
                cameraResponse = value;
                RaisePropertyChanged("CameraResponse");
            }
        }
        public ObservableCollection<CameraResponseModel> CameraFileResponse
        {
            get { return cameraFileResponse; }
            set
            { 
                cameraFileResponse = value;
                RaisePropertyChanged("CameraFileResponse");
            }
        }
        public ObservableCollection<CameraItemModel> CameraPictMode
        {
            get { return cameraPictMode; }
            set
            {
                cameraPictMode = value;
                RaisePropertyChanged("CameraPictMode");
            }
        }
        public ObservableCollection<CameraItemModel> CameraDigitMode
        {
            get { return cameraDigitMode; }
            set 
            { 
                cameraDigitMode = value;
                RaisePropertyChanged("CameraDigitMode");
            }
        }
        public ObservableCollection<CameraItemModel> CameraPict
        {
            get { return cameraPict; }
            set 
            { 
                cameraPict = value;
                RaisePropertyChanged("CameraPict");
            }
        }
        public byte CameraPictStart
        {
            get { return cameraPictStart; }
            set 
            { 
                cameraPictStart = value;
                RaisePropertyChanged("CameraPictStart");
            }
        }
        public ObservableCollection<CameraItemModel> CameraDigit
        {
            get { return cameraDigit; }
            set 
            { 
                cameraDigit = value;
                RaisePropertyChanged("CameraDigit");
            }
        }
        public byte CameraDigitStart
        {
            get { return cameraDigitStart; }
            set
            { 
                cameraDigitStart = value;
                RaisePropertyChanged("CameraDigitStart");
            }
        }

        public byte CameraDigitOffset
        {
            get { return cameraDigitOffset; }
            set 
            { 
                cameraDigitOffset = value;
                RaisePropertyChanged("CameraDigitOffset");
            }
        }

        public ObservableCollection<CameraItemModel> CameraIntegralDirection
        {
            get { return cameraIntegralDirection; }
            set 
            { 
                cameraIntegralDirection = value;
                RaisePropertyChanged("CameraIntegralDirection");
            }
        }

        public byte CameraFullIntegral
        {
            get { return cameraFullIntegral; }
            set
            {
                cameraFullIntegral = value;
                RaisePropertyChanged("CameraFullIntegral");
            }
        }

        public byte CameraMultiIntegral
        {
            get { return cameraMultiIntegral; }
            set 
            {
                cameraMultiIntegral = value;
                RaisePropertyChanged("CameraMultiIntegral");
            }
        }

        public byte CameraFullGain
        {
            get { return cameraFullGain; }
            set
            {
                cameraFullGain = value;
                RaisePropertyChanged("CameraFullGain");
            }
        }

        public byte CameraMultiGain
        {
            get { return cameraMultiGain; }
            set
            {
                cameraMultiGain = value;
                RaisePropertyChanged("CameraMultiGain");
            }
        }


        public Dictionary<int, string> CameraAnalogGain
        {
            get { return cameraAnalogGain; }
            set 
            {
                cameraAnalogGain = value;
                RaisePropertyChanged("CameraAnalogGain");
            }
        }
        public byte CurrAnalogGain
        {
            get { return currAnalogGain; }
            set 
            {
                currAnalogGain = value;
                RaisePropertyChanged("CurrAnalogGain");
            }
        }

        public ushort CameraLine
        {
            get { return cameraLine; }
            set 
            {
                cameraLine = value;
                RaisePropertyChanged("CameraLine");
            }
        }

        public ObservableCollection<CameraItemModel> CameraBus
        {
            get { return cameraBus; }
            set 
            { 
                cameraBus = value;
                RaisePropertyChanged("CameraBus");
            }
        }

        public ushort CameraSpectralFullLine
        {
            get { return cameraSpectralFullLine; }
            set 
            { 
                cameraSpectralFullLine = value;
                RaisePropertyChanged("CameraSpectralFullLine");
            }
        }

        public ushort CameraSpectralLineR
        {
            get { return cameraSpectralLineR; }
            set 
            { 
                cameraSpectralLineR = value;
                RaisePropertyChanged("CameraSpectralLineR");
            }
        }

        public ushort CameraSpectralLineG
        {
            get { return cameraSpectralLineG; }
            set 
            { 
                cameraSpectralLineG = value;
                RaisePropertyChanged("CameraSpectralLineG");
            }
        }

        public ushort CameraSpectralLineB
        {
            get { return cameraSpectralLineB; }
            set 
            { 
                cameraSpectralLineB = value;
                RaisePropertyChanged("CameraSpectralLineB");
            }
        }

        public ushort CameraSpectralLineIR
        {
            get { return cameraSpectralLineIR; }
            set 
            { 
                cameraSpectralLineIR = value;
                RaisePropertyChanged("CameraSpectralLineIR");
            }
        }

        public ushort CameraSpectralFullStart
        {
            get { return cameraSpectralFullStart; }
            set 
            { 
                cameraSpectralFullStart = value;
                RaisePropertyChanged("CameraSpectralFullStart");
            }
        }

        public ushort CameraSpectralStartR
        {
            get { return cameraSpectralStartR; }
            set
            { 
                cameraSpectralStartR = value;
                RaisePropertyChanged("CameraSpectralStartR");
            }
        }

        public ushort CameraSpectralStartG
        {
            get { return cameraSpectralStartG; }
            set 
            { 
                cameraSpectralStartG = value;
                RaisePropertyChanged("CameraSpectralStartG");
            }
        }

        public ushort CameraSpectralStartB
        {
            get { return cameraSpectralStartB; }
            set 
            { 
                cameraSpectralStartB = value;
                RaisePropertyChanged("CameraSpectralStartB");
            }
        }

        public ushort CameraSpectralStartIR
        {
            get { return cameraSpectralStartIR; }
            set 
            { 
                cameraSpectralStartIR = value;
                RaisePropertyChanged("CameraSpectralStartIR");
            }
        }
        #endregion

        #region Command
        private bool CanCameraPictModeChangedExecute(string mode)
        {
            return true;
        }
        private void CameraPictModeChangedExecute(string mode)
        {
            switch (mode)
            {
                case "推扫相机TDI成像":
                    this.currCameraPictMode = 0x0;
                    break;
                case "推扫相机前端自校":
                    this.currCameraPictMode = 0x1;
                    break;
                default:
                    break;
            }
        }
        public ICommand CameraPictModeChangedCommand { get { return new RelayCommand<string>((string mode) => CameraPictModeChangedExecute(mode), CanCameraPictModeChangedExecute); } }

        private bool CanCameraPictModeExecute()
        {
            return true;
        }
        private void CameraPictModeExecute()
        {
            Dictionary<byte, byte> dict = new Dictionary<byte, byte>();
            dict.Add(0x0, 0x33);
            dict.Add(0x1, 0x55);

            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K100{0:X}", this.currCameraPictMode));
            cmdList.Add(dict[this.currCameraPictMode]);
            cmdList.AddRange(new byte[] { 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55 });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraPictModeCommand { get { return new RelayCommand(CameraPictModeExecute, CanCameraPictModeExecute); } }


        private bool CanCameraDigitModeChangedExecute(string mode)
        {
            return true;
        }
        private void CameraDigitModeChangedExecute(string mode)
        {
            switch (mode)
            {
                case "STD有损压缩图像下传":
                    this.currCameraDigitMode = 0x2;
                    break;
                case "STD无损压缩图像下传":
                    this.currCameraDigitMode = 0x3;
                    break;
                default:
                    break;
            }
        }
        public ICommand CameraDigitModeChangedCommand { get { return new RelayCommand<string>((string mode) => CameraDigitModeChangedExecute(mode), CanCameraDigitModeChangedExecute); } }

        private bool CanCameraDigitModeExecute()
        {
            return true;
        }
        private void CameraDigitModeExecute()
        {
            Dictionary<byte, byte> dict = new Dictionary<byte, byte>();
            dict.Add(0x2, 0x11);
            dict.Add(0x3, 0x22);

            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K100{0:X}", this.currCameraDigitMode));
            cmdList.Add(dict[this.currCameraDigitMode]);
            cmdList.AddRange(new byte[] { 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55 });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraDigitModeCommand { get { return new RelayCommand(CameraDigitModeExecute, CanCameraDigitModeExecute); } }

        private bool CanCameraPictChangedExecute(string pict)
        {
            return true;
        }
        private void CameraPictChangedExecute(string pict)
        {
            switch (pict)
            {
                case "开始拍照":
                    this.currCameraPict = 0x4;
                    break;
                case "停止拍照":
                    this.currCameraPict = 0x5;
                    break;
                default:
                    break;
            }
        }
        public ICommand CameraPictChangedCommand { get { return new RelayCommand<string>((string pict) => CameraPictChangedExecute(pict), CanCameraPictChangedExecute); } }

        private bool CanCameraPictExecute()
        {
            return true;
        }
        private void CameraPictExecute()
        {
            Dictionary<byte, byte> dict = new Dictionary<byte, byte>();
            dict.Add(0x4, this.CameraPictStart);
            dict.Add(0x5, 0x55);

            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K100{0:X}", this.currCameraPict));
            cmdList.Add(dict[this.currCameraPict]);
            cmdList.AddRange(new byte[] { 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55 });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraPictCommand { get { return new RelayCommand(CameraPictExecute, CanCameraPictExecute); } }


        private bool CanCameraDigitChangedExecute(string digit)
        {
            return true;
        }
        private void CameraDigitChangedExecute(string digit)
        {
            switch (digit)
            {
                case "开始数传":
                    this.currCameraDigit = 0x6;
                    break;
                case "停止数传":
                    this.currCameraDigit = 0x7;
                    break;
                default:
                    break;
            }
        }
        public ICommand CameraDigitChangedCommand { get { return new RelayCommand<string>((string digit) => CameraDigitChangedExecute(digit), CanCameraDigitChangedExecute); } }

        private bool CanCameraDigitExecute()
        {
            return true;
        }
        private void CameraDigitExecute()
        {
            Dictionary<byte, byte[]> dict = new Dictionary<byte, byte[]>();
            dict.Add(0x6, new byte[] {this.CameraDigitStart, this.CameraDigitOffset });
            dict.Add(0x7, new byte[] {0x55, 0x55});

            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K100{0:X}", this.currCameraDigit));
            cmdList.AddRange(dict[this.currCameraDigit]);
            cmdList.AddRange(new byte[] { 0x55, 0x55, 0x55, 0x55, 0x55, 0x55 });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraDigitCommand { get { return new RelayCommand(CameraDigitExecute, CanCameraDigitExecute); } }


        private bool CanCameraFileSearchExecute(string number)
        {
            return true;
        }
        private void CameraFileSearchExecute(string number)
        {
            int num = default(int);
            int.TryParse(number, out num);

            List<byte> cmdList = CmdOperation.genCmdByte("K1008");
            cmdList.Add((byte)(num & 0xFF));
            cmdList.AddRange(new byte[] { 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55 });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraFileSearchCommand { get { return new RelayCommand<string>((number) => CameraFileSearchExecute(number), CanCameraFileSearchExecute); } }

        private bool CanCameraFileDeleteExecute(string number)
        {
            return true;
        }
        private void CameraFileDeleteExecute(string number)
        {
            int num = default(int);
            int.TryParse(number, out num);

            List<byte> cmdList = CmdOperation.genCmdByte("K1009");
            cmdList.Add((byte)(num & 0xFF));
            cmdList.AddRange(new byte[] { 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55 });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraFileDeleteCommand { get { return new RelayCommand<string>((number) => CameraFileDeleteExecute(number), CanCameraFileDeleteExecute); } }

        private bool CanCameraFileFormatExecute()
        {
            return true;
        }
        private void CameraFileFormatExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K100A");
            cmdList.AddRange(new byte[] { 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55 });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraFileFormatCommand { get { return new RelayCommand(CameraFileFormatExecute, CanCameraFileFormatExecute); } }

        private bool CanCameraIntegralDirectionChangedExecute(string direction)
        {
            return true;
        }
        private void CameraIntegralDirectionChangedExecute(string direction)
        {
            switch (direction)
            {
                case "由小到大":
                    this.currCameraIntegralDirection = 0xB;
                    break;
                case "由大到小":
                    this.currCameraIntegralDirection = 0xC;
                    break;
                default:
                    break;
            }
        }
        public ICommand CameraIntegralDirectionChangedCommand { get { return new RelayCommand<string>((string direction) => CameraIntegralDirectionChangedExecute(direction), CanCameraIntegralDirectionChangedExecute); } }

        private bool CanCamerIntegralDirectionExecute()
        {
            return true;
        }
        private void CameraIntegralDirectionExecute()
        {
            Dictionary<byte, byte> dict = new Dictionary<byte, byte>();
            dict.Add(0xB, 0x0A);
            dict.Add(0xC, 0xA0);

            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K100{0:X}", this.currCameraIntegralDirection));
            cmdList.Add(dict[this.currCameraIntegralDirection]);
            cmdList.AddRange(new byte[] { 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55 });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraIntegralDirectionCommand { get { return new RelayCommand(CameraIntegralDirectionExecute, CanCamerIntegralDirectionExecute); } }

        private bool CanCameraZipFactorExecute(string factor)
        {
            return true;
        }
        private void CameraZipFactorExecute(string factor)
        {
            //Messenger.Default.Send<string>(navi, "Alert");

            byte f = default(byte);
            byte.TryParse(factor, out f);

            List<byte> cmdList = CmdOperation.genCmdByte("K100B");
            cmdList.Add(f);

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraZipFactorCommand { get { return new RelayCommand<string>((factor) => CameraZipFactorExecute(factor), CanCameraZipFactorExecute); } }

        private bool CanCameraIntegralNumberExecute()
        {
            return true;
        }
        private void CameraIntegralNumberExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K100E");
            cmdList.AddRange(new byte[] { this.CameraFullIntegral, this.CameraMultiIntegral });
            cmdList.AddRange(new byte[] { 0x55, 0x55, 0x55, 0x55, 0x55, 0x55 });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraIntegralNumberCommand { get { return new RelayCommand(CameraIntegralNumberExecute, CanCameraIntegralNumberExecute); } }

        private bool CanCameraDigitGainExecute()
        {
            return true;
        }
        private void CameraDigitGainExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1011");
            cmdList.AddRange(new byte[] { this.CameraFullGain, this.CameraMultiGain });
            cmdList.AddRange(new byte[] { 0x55, 0x55, 0x55, 0x55, 0x55, 0x55 });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraDigitGainCommand { get { return new RelayCommand(CameraDigitGainExecute, CanCameraDigitGainExecute); } }

        private bool CanCameraAnalogGainExecute()
        {
            return true;
        }
        private void CameraAnalogGainExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K100F");
            cmdList.Add(this.CurrAnalogGain);
            cmdList.AddRange(new byte[] { 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55 });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraAnalogGainCommand { get { return new RelayCommand(CameraAnalogGainExecute, CanCameraAnalogGainExecute); } }


        private bool CanCameraLineExecute(string line)
        {
            return true;
        }
        private void CameraLineExecute(string line)
        {
            ushort l = default(ushort);
            ushort.TryParse(line, out l);

            List<byte> cmdList = CmdOperation.genCmdByte("K1010");
            cmdList.AddRange(BitConverter.GetBytes(l));
            cmdList.AddRange(new byte[] { 0x55, 0x55, 0x55, 0x55, 0x55, 0x55 });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraLineCommand { get { return new RelayCommand<string>((line) => CameraLineExecute(line), CanCameraLineExecute); } }

        private bool CanCameraTimeExecute(string time)
        {
            return true;
        }
        private void CameraTimeExecute(string time)
        {
            uint t = default(uint);
            uint.TryParse(time, out t);

            List<byte> cmdList = CmdOperation.genCmdByte("K1053");
            cmdList.AddRange(BitConverter.GetBytes(t));
            cmdList.AddRange(new byte[] { 0x55, 0x55, 0x55, 0x55 });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraTimeCommand { get { return new RelayCommand<string>((time) => CameraTimeExecute(time), CanCameraTimeExecute); } }

        private bool CanCameraTimeEnableExecute()
        {
            return true;
        }
        private void CameraTimeEnableExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1052");
            cmdList.AddRange(new byte[] { 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55 });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraTimeEnableCommand { get { return new RelayCommand(CameraTimeEnableExecute, CanCameraTimeEnableExecute); } }

        private bool CanCameraBusChangedExecute(string bus)
        {
            return true;
        }
        private void CameraBusChangedExecute(string bus)
        {
            switch (bus)
            {
                case "自主":
                    this.currCameraBus = 0x0;
                    break;
                case "A":
                    this.currCameraBus = 0xA0;
                    break;
                case "B":
                    this.currCameraBus = 0x0A;
                    break;
                default:
                    break;
            }
        }
        public ICommand CameraBusChangedCommand { get { return new RelayCommand<string>((string bus) => CameraBusChangedExecute(bus), CanCameraBusChangedExecute); } }

        private bool CanCameraBusExecute()
        {
            return true;
        }
        private void CameraBusExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1058");
            cmdList.Add(this.currCameraBus);
            cmdList.AddRange(new byte[] { 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraBusCommand { get { return new RelayCommand(CameraBusExecute, CanCameraBusExecute); } }

        private bool CanCameraSpectralFullLineExecute()
        {
            return true;
        }
        private void CameraSpectralFullLineExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1054");
            cmdList.AddRange(BitConverter.GetBytes(this.CameraSpectralFullLine));
            cmdList.AddRange(new byte[] { 0x55, 0x55, 0x55, 0x55, 0x55, 0x55 });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraSpectralFullLineCommand { get { return new RelayCommand(CameraSpectralFullLineExecute, CanCameraSpectralFullLineExecute); } }

        private bool CanCameraSpectralMultiLineExecute()
        {
            return true;
        }
        private void CameraSpectralMultiLineExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1055");
            cmdList.AddRange(BitConverter.GetBytes(CameraSpectralLineR));
            cmdList.AddRange(BitConverter.GetBytes(CameraSpectralLineG));
            cmdList.AddRange(BitConverter.GetBytes(CameraSpectralLineB));
            cmdList.AddRange(BitConverter.GetBytes(CameraSpectralLineIR));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraSpectralMultiLineCommand { get { return new RelayCommand(CameraSpectralMultiLineExecute, CanCameraSpectralMultiLineExecute); } }

        private bool CanCameraSpectralFullStartExecute()
        {
            return true;
        }
        private void CameraSpectralFullStartExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1056");
            cmdList.AddRange(BitConverter.GetBytes(this.CameraSpectralFullStart));
            cmdList.AddRange(new byte[] { 0x55, 0x55, 0x55, 0x55, 0x55, 0x55 });

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraSpectralFullStartCommand { get { return new RelayCommand(CameraSpectralFullStartExecute, CanCameraSpectralFullStartExecute); } }

        private bool CanCameraSpectralMultiStartExecute()
        {
            return true;
        }
        private void CameraSpectralMultiStartExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1057");
            cmdList.AddRange(BitConverter.GetBytes(CameraSpectralStartR));
            cmdList.AddRange(BitConverter.GetBytes(CameraSpectralStartG));
            cmdList.AddRange(BitConverter.GetBytes(CameraSpectralStartB));
            cmdList.AddRange(BitConverter.GetBytes(CameraSpectralStartIR));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand CameraSpectralMultiStartCommand { get { return new RelayCommand(CameraSpectralMultiStartExecute, CanCameraSpectralMultiStartExecute); } }

        #endregion

        #region Constructor
        public CameraViewModel()
        {
            this.cameraParam = CameraParamModel.GetCameraParam();  //工程参数
            this.cameraResponse = CameraResponseModel.GetCameraResponse();  //指令应答
            this.cameraFileResponse = CameraResponseModel.GetCameraFileResponse();  //文件应答
            this.cameraPictMode = CameraItemModel.GetCameraPictMode();  //成像模式
            this.cameraDigitMode = CameraItemModel.GetCameraDigitMode();  //数传模式
            this.cameraPict = CameraItemModel.GetCameraPict();  //拍照指令
            this.cameraDigit = CameraItemModel.GetCameraDigit();  //数传指令
            this.cameraIntegralDirection = CameraItemModel.GetCameraIntegralDirection();  //积分方向
            this.cameraBus = CameraItemModel.GetCameraBus();  //总线测试
            this.cameraAnalogGain = new Dictionary<int, string>();  //模拟增益
            InitAnalogGain();  //初始化模拟增益数据
        }
        #endregion

        #region Method
        private void InitAnalogGain()
        {
            this.cameraAnalogGain.Add(1, "0.4x");
            this.cameraAnalogGain.Add(3, "0.6x");
            this.cameraAnalogGain.Add(5, "0.8x");
            this.cameraAnalogGain.Add(7, "1.0x");
            this.cameraAnalogGain.Add(9, "1.2x");
            this.cameraAnalogGain.Add(11, "1.4x");
            this.cameraAnalogGain.Add(13, "1.6x");
            this.cameraAnalogGain.Add(15, "1.8x");
            this.cameraAnalogGain.Add(17, "2.0x");
            this.cameraAnalogGain.Add(19, "2.2x");
            this.cameraAnalogGain.Add(21, "2.4x");
            this.cameraAnalogGain.Add(23, "2.6x");
            this.cameraAnalogGain.Add(25, "2.8x");
            this.cameraAnalogGain.Add(27, "3.0x");
            this.cameraAnalogGain.Add(29, "3.2x");
            this.cameraAnalogGain.Add(31, "3.4x");
            this.cameraAnalogGain.Add(33, "3.6x");
            this.cameraAnalogGain.Add(35, "3.8x");
            this.cameraAnalogGain.Add(37, "4.0x");
            this.cameraAnalogGain.Add(39, "4.2x");
            this.cameraAnalogGain.Add(41, "4.4x");
            this.cameraAnalogGain.Add(43, "4.6x");
            this.cameraAnalogGain.Add(45, "4.8x");
            this.cameraAnalogGain.Add(47, "5.0x");
            this.cameraAnalogGain.Add(49, "5.2x");
            this.cameraAnalogGain.Add(51, "5.4x");
            this.cameraAnalogGain.Add(53, "5.6x");
            this.cameraAnalogGain.Add(55, "5.8x");
            this.cameraAnalogGain.Add(57, "6.0x");
            this.cameraAnalogGain.Add(59, "6.2x");
            this.cameraAnalogGain.Add(61, "6.4x");
            this.cameraAnalogGain.Add(63, "6.6x");
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
