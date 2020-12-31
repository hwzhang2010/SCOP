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
    public class GpsViewModel : ViewModelBase
    {
        #region Field
        private ObservableCollection<GpsItemModel> gpsComm;
        private byte currGpsComm;  //GPS数据传输方式，默认CAN
        private ObservableCollection<GpsModel> gps;
        #endregion

        #region Property
        public ObservableCollection<GpsItemModel> GpsComm
        {
            get { return gpsComm; }
            set 
            {
                gpsComm = value;
                RaisePropertyChanged("GpsComm");
            }
        }
        public ObservableCollection<GpsModel> Gps
        {
            get { return gps; }
            set 
            { 
                gps = value;
                RaisePropertyChanged("Gps");
            }
        }
        #endregion

        #region Command
        private bool CanGpsCommChangedExecute(string comm)
        {
            return true;
        }
        private void GpsCommChangedExecute(string comm)
        {
            switch (comm)
            {
                case "CAN":
                    this.currGpsComm = 0x0;
                    break;
                case "422":
                    this.currGpsComm = 0x1;
                    break;
                default:
                    break;
            }
        }
        public ICommand GpsCommChangedCommand { get { return new RelayCommand<string>((string comm) => GpsCommChangedExecute(comm), CanGpsCommChangedExecute); } }

        private bool CanGpsDataExecute()
        {
            return true;
        }
        private void GpsDataExecute()
        {
            Messenger.Default.Send<string>(string.Format("K0{0:X}00", this.currGpsComm + 0x4), "Alert");
            List<byte> cmdList = CmdOperation.genCmdByte(string.Format("K0{0:X00}", this.currGpsComm + 0x4));

            CmdOperation.makeCmdByte(ref cmdList);

            UdpMessage cmd = new UdpMessage();
            cmd.Payload = new byte[CmdOperation.CmdCountConst];
            Buffer.BlockCopy(cmdList.ToArray(), 0, cmd.Payload, 0, CmdOperation.CmdCountConst);
            cmd.Length = CmdOperation.CmdCountConst;

            Messenger.Default.Send<UdpMessage>(cmd, "Send");
        }
        public ICommand GpsDataCommand { get { return new RelayCommand(GpsDataExecute, CanGpsDataExecute); } }
        #endregion

        #region Constructor
        public GpsViewModel()
        {
            this.gpsComm = GpsItemModel.GetGpsComm();
            this.gps = GpsModel.GetGps();

            Messenger.Default.Register<List<GpsShowModel>>(this, "GpsShow", HandleGpsShow);  //Gps解析数据显示
        }
        #endregion

        #region Override Method
        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
        }
        #endregion

        #region Messenger Handler
        private void HandleGpsShow(List<GpsShowModel> list)
        {
            if (list == null || list.Count < this.Gps.Count)
                return;

            for (int i = 0; i < list.Count; i++)
            {
                this.Gps[i].Hex = list[i].Hex;
                this.Gps[i].Cal = list[i].Cal;
            }
        }
        #endregion
    }
}
