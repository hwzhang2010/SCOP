using System;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using TSFCS.SCOP.Model;


namespace TSFCS.SCOP.ViewModel
{
    public class TempViewModel : ViewModelBase
    {
        #region Field
        private string tempTtc;
        private string tempObc1;
        private string tempObc2;
        private string tempGps;
        private string tempMm1;
        private string tempMm2;
        private string tempTmr;
        private string tempBoard1;
        private string tempBoard2;
        private string tempBoard3;
        private string tempBoard4;
        private string tempBoard5;
        private string tempBoard6;
        private string tempBoard7;
        private string tempBatt1;
        private string tempBatt2;
        private string tempStru1;
        private string tempStru2;
        private string tempStru3;
        private string tempStru4;
        private string tempStru5;
        private int tempCycle;
        private ObservableCollection<TempModel> temps;
        private TempModel temp;
        #endregion

        #region Property
        public string TempTtc
        {
            get { return tempTtc; }
            set 
            { 
                tempTtc = value;
                RaisePropertyChanged("TempTtc");
            }
        }

        public string TempObc1
        {
            get { return tempObc1; }
            set 
            { 
                tempObc1 = value;
                RaisePropertyChanged("TempObc1");
            }
        }

        public string TempObc2
        {
            get { return tempObc2; }
            set
            { 
                tempObc2 = value;
                RaisePropertyChanged("TempObc2");
            }
        }

        public string TempGps
        {
            get { return tempGps; }
            set 
            { 
                tempGps = value;
                RaisePropertyChanged("TempGps");
            }
        }

        public string TempMm1
        {
            get { return tempMm1; }
            set
            { 
                tempMm1 = value;
                RaisePropertyChanged("TempMm1");
            }
        }

        public string TempMm2
        {
            get { return tempMm2; }
            set
            { 
                tempMm2 = value;
                RaisePropertyChanged("TempMm2");
            }
        }

        public string TempTmr
        {
            get { return tempTmr; }
            set 
            {
                tempTmr = value;
                RaisePropertyChanged("TempTmr");
            }
        }

        public string TempBoard1
        {
            get { return tempBoard1; }
            set 
            { 
                tempBoard1 = value;
                RaisePropertyChanged("TempBoard1");
            }
        }

        public string TempBoard2
        {
            get { return tempBoard2; }
            set 
            {
                tempBoard2 = value;
                RaisePropertyChanged("TempBoard2");
            }
        }

        public string TempBoard3
        {
            get { return tempBoard3; }
            set 
            { 
                tempBoard3 = value;
                RaisePropertyChanged("TempBoard3");
            }
        }

        public string TempBoard4
        {
            get { return tempBoard4; }
            set 
            { 
                tempBoard4 = value;
                RaisePropertyChanged("TempBoard4");
            }
        }

        public string TempBoard5
        {
            get { return tempBoard5; }
            set 
            { 
                tempBoard5 = value;
                RaisePropertyChanged("TempBoard5");
            }
        }

        public string TempBoard6
        {
            get { return tempBoard6; }
            set 
            { 
                tempBoard6 = value;
                RaisePropertyChanged("TempBoard6");
            }
        }

        public string TempBoard7
        {
            get { return tempBoard7; }
            set 
            {
                tempBoard7 = value;
                RaisePropertyChanged("TempBoard7");
            }
        }

        public string TempBatt1
        {
            get { return tempBatt1; }
            set
            { 
                tempBatt1 = value;
                RaisePropertyChanged("TempBatt1");
            }
        }

        public string TempBatt2
        {
            get { return tempBatt2; }
            set
            { 
                tempBatt2 = value;
                RaisePropertyChanged("TempBatt2");
            }
        }

        public string TempStru1
        {
            get { return tempStru1; }
            set
            { 
                tempStru1 = value;
                RaisePropertyChanged("TempStru1");
            }
        }

        public string TempStru2
        {
            get { return tempStru2; }
            set 
            { 
                tempStru2 = value;
                RaisePropertyChanged("TempStru2");
            }
        }

        public string TempStru3
        {
            get { return tempStru3; }
            set 
            { 
                tempStru3 = value;
                RaisePropertyChanged("TempStru3");
            }
        }

        public string TempStru4
        {
            get { return tempStru4; }
            set 
            { 
                tempStru4 = value;
                RaisePropertyChanged("TempStru4");
            }
        }

        public string TempStru5
        {
            get { return tempStru5; }
            set
            { 
                tempStru5 = value;
                RaisePropertyChanged("TempStru5");
            }
        }

        public int TempCycle
        {
            get { return tempCycle; }
            set 
            { 
                tempCycle = value;
                RaisePropertyChanged("TempCycle");
            }
        }

        public ObservableCollection<TempModel> Temps
        {
            get { return temps; }
            set 
            { 
                temps = value;
                RaisePropertyChanged("Temps");
            }
        }

        public TempModel Temp
        {
            get { return temp; }
            set
            {
                temp = value;
                RaisePropertyChanged("Temp");
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
            //初始化温控周期为1
            TempCycle = 1;  
            //温度项
            Temps = TempModel.GetTemps();
            if (Temps.Count > 0)
              Temp = Temps[0];  //默认选中第0个
        }
        public ICommand LoadedCommand { get { return new RelayCommand(LoadedExecute, CanLoadedExecute); } }

        private bool CanTempCycleExecute()
        {
            return true;
        }
        private void TempCycleExecute()
        {
        }
        public ICommand TempCycleCommand { get { return new RelayCommand(TempCycleExecute, CanTempCycleExecute); } }

        private bool CanTempChangedExecute(int id)
        {
            return true;
        }
        private void TempChangedExecute(int id)
        {
        }
        public ICommand TempChangedCommand { get { return new RelayCommand<int>((id) => TempChangedExecute(id), CanTempChangedExecute); } }

        private bool CanTempDataExecute()
        {
            return true;
        }
        private void TempDataExecute()
        {
        }
        public ICommand TempDataCommand { get { return new RelayCommand(TempDataExecute, CanTempDataExecute); } }
        #endregion

        #region Constructor
        public TempViewModel()
        { 
        }
        #endregion

        #region Method
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
