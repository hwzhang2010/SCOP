using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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
    ///     <MyNamespace:NumericBox/>
    ///
    /// </summary>
    public class NumericBox : Control
    {
        static NumericBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericBox), new FrameworkPropertyMetadata(typeof(NumericBox)));
        }

        #region 方法重写
        public override void OnApplyTemplate()
        {
            Button BtnReduce = Template.FindName("BtnReduce", this) as Button;
            BtnReduce.Click += delegate
            {
                if (Value <= MinValue)
                {
                    Value = MinValue;
                    return;
                }
                Value -= Step;
            };

            Button BtnAdd = Template.FindName("BtnAdd", this) as Button;
            BtnAdd.Click += delegate
            {
                if (Value >= MaxValue)
                {
                    Value = MaxValue;
                    return;
                }
                Value += Step;
            };
        }
        #endregion

        #region 依赖属性
        public string ShowText
        {
            get { return (string)GetValue(ShowTextProperty); }
            set { SetValue(ShowTextProperty, value); }
        }
        public static readonly DependencyProperty ShowTextProperty =
            DependencyProperty.Register("ShowText", typeof(string), typeof(NumericBox), new PropertyMetadata(string.Empty));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(NumericBox), new PropertyMetadata(0));

        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        //Using a DependencyProperty as the backing store for MinValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(int), typeof(NumericBox), new PropertyMetadata(0));

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(NumericBox), new PropertyMetadata(100));

        public int Step
        {
            get { return (int)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(int), typeof(NumericBox), new PropertyMetadata(1));
        #endregion

    }
}
