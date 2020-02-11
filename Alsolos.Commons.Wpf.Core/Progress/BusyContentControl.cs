using System.Windows;
using System.Windows.Controls;

namespace Alsolos.Commons.Wpf.Core.Progress
{
    public class BusyContentControl : ContentControl
    {
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(
            "IsBusy", typeof(bool), typeof(BusyContentControl), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(string), typeof(BusyContentControl), new PropertyMetadata(default(string)));

        static BusyContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyContentControl), new FrameworkPropertyMetadata(typeof(BusyContentControl)));
        }

        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }
    }
}