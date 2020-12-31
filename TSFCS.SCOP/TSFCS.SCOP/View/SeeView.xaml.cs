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
using System.Windows.Shapes;

using GalaSoft.MvvmLight.Messaging;

namespace TSFCS.SCOP.View
{
    /// <summary>
    /// SeeView.xaml 的交互逻辑
    /// </summary>
    public partial class SeeView : Window
    {
        private static SeeView instance = default(SeeView);
        private static readonly object obj = new object();

        public static SeeView Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (obj)
                    {
                        if (instance == null)
                        {
                            instance = new SeeView();
                        }
                    }
                }

                return instance;
            }

            set
            {
                instance = value;
            }
        }

        public SeeView()
        {
            InitializeComponent();

            Messenger.Default.Register<string>(this, "See", HandleSee);

            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }

        #region Messenger Handler
        private void HandleSee(string command)
        {
            switch (command)
            {
                case "Loaded":
                    break;
                case "Closed":
                    SeeView.Instance = null;
                    this.Close();
                    break;
                case "DragMove":
                    try
                    {
                        this.DragMove();
                    }
                    catch
                    {
                    }
                    break;
                case "Seeing":
                    this.rtxtRecv.ScrollToEnd();  //滚动到最后
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
