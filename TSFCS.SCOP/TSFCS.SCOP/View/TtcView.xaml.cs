using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using GalaSoft.MvvmLight.Messaging;

namespace TSFCS.SCOP.View
{
    /// <summary>
    /// TtcView.xaml 的交互逻辑
    /// </summary>
    public partial class TtcView : Page
    {
        #region Resource
        private BitmapImage bmpOn = new BitmapImage(new Uri("/ImageSource/Sys/on.png", UriKind.Relative));
        private BitmapImage bmpOff = new BitmapImage(new Uri("/ImageSource/Sys/off.png", UriKind.Relative));
        #endregion

        public TtcView()
        {
            InitializeComponent();
            Messenger.Default.Register<string>(this, "Ttc", HandleTtc);

            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }

        #region Messenger Handler
        private void HandleTtc(string info)
        {
            switch (info)
            {
                case "Loaded":
                    this.imgObcA.Source = bmpOff;
                    this.imgObcB.Source = bmpOff;
                    break;
                default:
                    break;
            }
        }
        #endregion


    }
}
