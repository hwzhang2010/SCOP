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
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();

            Messenger.Default.Register<string>(this, "Login", HandleLogin);

            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }

        #region Message Handler
        private void HandleLogin(string info)
        {
            switch (info)
            {
                case "Loaded":
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
                case "Closed":
                    this.Close();
                    break;
                case "Min":
                    this.WindowState = System.Windows.WindowState.Minimized;
                    break;
                case "Set":
                    SetView set = new SetView();
                    set.ShowDialog();
                    break;
                case "Login":
                    MainView main = new MainView();
                    main.Show();
                    this.Close();
                    break;
                default:
                    break;
            }
        }
        #endregion

    }
}
