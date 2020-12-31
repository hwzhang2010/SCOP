using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using TSFCS.SCOP.Helper;
using TSFCS.SCOP.Udp;

namespace TSFCS.SCOP.ViewModel
{
    public class SeeViewModel : ViewModelBase
    {
        #region Field
        private string strData;
        #endregion

        #region Property
        public string StrData
        {
            get { return strData; }
            set
            {
                strData = value;
                RaisePropertyChanged("StrData");
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
            this.StrData = string.Empty;
        }
        public ICommand LoadedCommand { get { return new RelayCommand(LoadedExecute, CanLoadedExecute); } }

        private bool CanClosedExecute()
        {
            return true;
        }
        private void ClosedExecute()
        {
            Messenger.Default.Send<string>("Closed", "See");
        }
        public ICommand ClosedCommand { get { return new RelayCommand(ClosedExecute, CanClosedExecute); } }

        private bool CanDragMoveExecute()
        {
            return true;
        }
        private void DragMoveExecute()
        {
            Messenger.Default.Send<string>("DragMove", "See");
        }
        public ICommand DragMoveCommand { get { return new RelayCommand(DragMoveExecute, CanDragMoveExecute); } }

        public bool CanSeeingExecute()
        {
            return true;
        }
        public void SeeingExecute()
        {
            Messenger.Default.Send<string>("Seeing", "See");
        }
        public ICommand SeeingCommand { get { return new RelayCommand(SeeingExecute, CanSeeingExecute); } }

        public bool CanClearExecute()
        {
            return true;
        }
        public void ClearExecute()
        {
            //Messenger.Default.Send<string>("Clear", "See");
            this.StrData = string.Empty;
        }
        public ICommand ClearCommand { get { return new RelayCommand(ClearExecute, CanClearExecute); } }

        public bool CanSaveExecute()
        {
            return true;
        }
        public void SaveExecute()
        {
            //Messenger.Default.Send<string>("save", "see");
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = "Txt files(*.txt)|*.txt|All files(*.*)|*.*";
            sfd.DefaultExt = "txt";  //设置默认文件extension
            sfd.RestoreDirectory = true;  //是否记忆上次打开目录
            sfd.FileName = "data" + System.DateTime.Now.ToString("yyyyMMHHhhmmss") + ".txt";
            bool? result = sfd.ShowDialog();
            if (result == true)
            {
                System.IO.File.WriteAllText(sfd.FileName, this.StrData);  //创建写文件流，文件名含有路径,注意是错误的。
                //using (FileStream stream = File.OpenWrite(sfd.FileName))
                //{
                //    TextRange documentTextRange = new TextRange(this.rtxtRecv.Document.ContentStart, this.rtxtRecv.Document.ContentEnd);
                //    documentTextRange.Save(stream, DataFormats.Text);
                //}
            }
        }
        public ICommand SaveCommand { get { return new RelayCommand(SaveExecute, CanSaveExecute); } }
        #endregion

        #region Constructor
        public SeeViewModel()
        {
            Messenger.Default.Register<byte[]>(this, "Recv", HandleRecv);  //有效接收数据
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

        #region Messenger Handler
        private void HandleRecv(byte[] data)
        {
            this.StrData += ByteHelper.Bytes2HexStr(data) + "\r\n";

            if (this.StrData.Length > 65536)  //string的长度<=65536
                this.StrData = string.Empty;
        }
        #endregion
    }
}
