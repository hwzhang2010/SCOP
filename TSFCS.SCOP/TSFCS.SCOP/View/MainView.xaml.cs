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
using System.Windows.Threading;

using GalaSoft.MvvmLight.Messaging;


namespace TSFCS.SCOP.View
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        #region Window
        private SeeView see = null;  //查看下行数据
        #endregion

        #region Page
        private TtcView ttc = new TtcView();  //TTC
        private ObcView obc = new ObcView();  //OBC
        private TempView temp = new TempView();  //Temp
        private PowerView power = new PowerView();  //Power
        private GpsView gps = new GpsView();  //GPS
        private AdcsView adcs = new AdcsView();  //ADCS
        private CameraView camera = new CameraView();  //Camera
        private DigitView digit = new DigitView();  //DC
        #endregion

        #region Field
        #endregion

        public MainView()
        {
            InitializeComponent();

            FullScreenManager.RepairWpfWindowFullScreenBehavior(this);  //最大化不遮挡任务栏

            Messenger.Default.Register<string>(this, "Alert", HandleAlert);
            Messenger.Default.Register<string>(this, "Main", HandleMain);
            Messenger.Default.Register<string>(this, "Navi", HandleNavi);

            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }

        #region Method
        #endregion

        #region Messenger Handler
        private void HandleAlert(string info)
        {
            TSFCS.UI.MessageBox.Show(info, "提示");
        }

        private void HandleMain(string info)
        {
            switch (info)
            {
                case "Loaded":
                    this.main.Content = new Frame { Content = ttc };  //默认
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
                case "Normal":
                    this.max.Visibility = System.Windows.Visibility.Visible;
                    this.normal.Visibility = System.Windows.Visibility.Collapsed;
                    this.WindowState = System.Windows.WindowState.Normal;
                    break;
                case "Max":
                    this.normal.Visibility = System.Windows.Visibility.Visible;
                    this.max.Visibility = System.Windows.Visibility.Collapsed;
                    this.WindowState =  System.Windows.WindowState.Maximized;
                    break;
                case "Min":
                    this.WindowState = System.Windows.WindowState.Minimized; 
                    break;
                case "Menu":
                    this.SysMenuItem.IsOpen = true;
                    break;
                case "See":
                    see = SeeView.Instance;
                    see.Show();
                    //see.Activate();
                    break;
                default:
                    break;
            }
        }

        private void HandleNavi(string item)
        {
            switch (item)
            {
                case "TTC":
                    this.main.Content = new Frame { Content = ttc };
                    break;
                case "OBC":
                    this.main.Content = new Frame { Content = obc };
                    break;
                case "温度":
                    this.main.Content = new Frame { Content = temp };
                    break;
                case "电源":
                    this.main.Content = new Frame { Content = power };
                    break;
                case "GPS":
                    this.main.Content = new Frame { Content = gps };
                    break;
                case "ADCS":
                    this.main.Content = new Frame { Content = adcs };
                    break;
                case "相机":
                    this.main.Content = new Frame { Content = camera };
                    break;
                case "数传":
                    this.main.Content = new Frame { Content = digit };
                    break;
                default:
                    break;
            }
        }
        #endregion


    }
}
