using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;

using TSFCS.SCOP.Helper;
using TSFCS.SCOP.Model;
using TSFCS.SCOP.Udp;
using TSFCS.SCOP.DAL;

namespace TSFCS.SCOP.ViewModel
{
    public class TtcViewModel : ViewModelBase
    {

        #region Field
        #endregion

        #region Property
        
        #endregion

        #region Command
        private bool CanLoadedExecute()
        {
            return true;
        }
        private void LoadedExecute()
        {
            Messenger.Default.Send<string>("Loaded", "Ttc");
        }
        public ICommand LoadedCommand { get { return new RelayCommand(LoadedExecute, CanLoadedExecute); } }


        private bool CanObcAOnBOffExecute()
        {
            return true;
        }
        private void ObcAOnBOffExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1800");

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand ObcAOnBOffCommand { get { return new RelayCommand(ObcAOnBOffExecute, CanObcAOnBOffExecute); } }

        private bool CanObcAOffBOnExecute()
        {
            return true;
        }
        private void ObcAOffBOnExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1800");

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand ObcAOffBOnCommand { get { return new RelayCommand(ObcAOffBOnExecute, CanObcAOffBOnExecute); } }

        private bool CanObcOffExecute()
        {
            return true;
        }
        private void ObcOffExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1800");

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand ObcOffCommand { get { return new RelayCommand(ObcOffExecute, CanObcOffExecute); } }

        private bool CanObcOnExecute()
        {
            return true;
        }
        private void ObcOnExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1800");

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand ObcOnCommand { get { return new RelayCommand(ObcOnExecute, CanObcOnExecute); } }

        private bool CanObcResetExecute()
        {
            return true;
        }
        private void ObcResetExecute()
        {
            List<byte> cmdList = CmdOperation.genCmdByte("K1800");

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand ObcResetCommand { get { return new RelayCommand(ObcResetExecute, CanObcResetExecute); } }
        #endregion


        #region Constructor
        public TtcViewModel()
        {
        }
        #endregion

        #region Method
        #endregion
    }
}
