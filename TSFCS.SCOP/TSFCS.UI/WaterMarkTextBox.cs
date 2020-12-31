using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TSFCS.UI
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TSFCS.UI"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TSFCS.UI;assembly=TSFCS.UI"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:WaterMarkTextBox/>
    ///
    /// </summary>
    public class WaterMarkTextBox : TextBox
    {
        static WaterMarkTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WaterMarkTextBox), new FrameworkPropertyMetadata(typeof(WaterMarkTextBox)));
        }

        #region 依赖属性
        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }
        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(WaterMarkTextBox), new PropertyMetadata(""));
        
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(WaterMarkTextBox), new PropertyMetadata(new CornerRadius(2)));
        
        public Geometry Icon
        {
            get { return (Geometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(Geometry), typeof(WaterMarkTextBox), new PropertyMetadata(null));
        
        public bool ShowIcon
        {
            get { return (bool)GetValue(ShowIconProperty); }
            set { SetValue(ShowIconProperty, value); }
        }
        public static readonly DependencyProperty ShowIconProperty =
            DependencyProperty.Register("ShowIcon", typeof(bool), typeof(WaterMarkTextBox), new PropertyMetadata(false));

        /// <summary>
        /// 图标宽度
        /// </summary>
        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(WaterMarkTextBox), new PropertyMetadata(15.0));

        /// <summary>
        /// 图标高度
        /// </summary>
        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(WaterMarkTextBox), new PropertyMetadata(15.0));

        //public double ShadowBlurRadius
        //{
        //    get { return (double)GetValue(ShadowBlurRadiusProperty); }
        //    set { SetValue(ShadowBlurRadiusProperty, value); }
        //}
        //public static readonly DependencyProperty ShadowBlurRadiusProperty =
        //    DependencyProperty.Register("ShadowBlurRadius", typeof(double), typeof(WaterMarkTextBox), new PropertyMetadata(0.0));
        ///// <summary>
        ///// 是否显示阴影
        ///// </summary>
        //public bool ShowShadow
        //{
        //    get { return (bool)GetValue(ShowShadowProperty); }
        //    set { SetValue(ShowShadowProperty, value); }
        //}
        //public static readonly DependencyProperty ShowShadowProperty =
        //    DependencyProperty.Register("ShowShadow", typeof(bool), typeof(WaterMarkTextBox), new PropertyMetadata(false));

        public SolidColorBrush SelectedColor
        {
            get { return (SolidColorBrush)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(SolidColorBrush), typeof(WaterMarkTextBox), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 124, 125, 133))));
        #endregion

    }
}
