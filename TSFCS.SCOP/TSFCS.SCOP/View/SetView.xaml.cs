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

using TSFCS.UI;

namespace TSFCS.SCOP.View
{
    /// <summary>
    /// SetView.xaml 的交互逻辑
    /// </summary>
    public partial class SetView : Window
    {
        public SetView()
        {
            InitializeComponent();

            Messenger.Default.Register<string>(this, "Set", HandleSet);
            Messenger.Default.Register<bool>(this, "Set", HandleSet);

            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }

        #region Messenger Handler
        private void HandleSet(string command)
        {
            switch (command)
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
                default:
                    break;
            }
        }

        private void HandleSet(bool isSave)
        {
            if (isSave)
            {
                TSFCS.UI.MessageBox.Show("配置文件保存成功", "提示");
            }

            this.Close();  //关闭窗口
        }
        #endregion

    }
}
