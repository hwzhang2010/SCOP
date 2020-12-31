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
    ///     <MyNamespace:TopTabItem/>
    ///
    /// </summary>
    public class TopTabItem : TabItem
    {
        static TopTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TopTabItem), new FrameworkPropertyMetadata(typeof(TopTabItem)));
        }

        #region 依赖属性
        public SolidColorBrush SelectedColor
        {
            get { return (SolidColorBrush)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(SolidColorBrush), typeof(TopTabItem), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 124, 125, 133))));


        public SolidColorBrush SelectForeground
        {
            get { return (SolidColorBrush)GetValue(SelectForegroundProperty); }
            set { SetValue(SelectForegroundProperty, value); }
        }
        public static readonly DependencyProperty SelectForegroundProperty =
            DependencyProperty.Register("SelectForeground", typeof(SolidColorBrush), typeof(TopTabItem), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 229, 229, 231))));


        public TabItemType TabItemType
        {
            get { return (TabItemType)GetValue(TabItemTypeProperty); }
            set
            {
                SetValue(TabItemTypeProperty, value);
            }
        }
        public static readonly DependencyProperty TabItemTypeProperty =
            DependencyProperty.Register("TabItemType", typeof(TabItemType), typeof(TopTabItem), new PropertyMetadata(TabItemType.Middle));

        #endregion
    }

    public enum TabItemType
    {
        Left, Middle, Right
    }
}