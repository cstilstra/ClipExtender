using ClipExtender.ViewModels;
using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace ClipExtender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static MainWindowViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += ViewLoaded;
        }

        private void ViewLoaded(object sender, RoutedEventArgs e)
        {
            IntPtr handle = GetWindowHandle();
            _vm = new MainWindowViewModel(handle);
            this.DataContext = _vm;
            HwndSource source = HwndSource.FromHwnd(handle);
            source.AddHook(new HwndSourceHook(WndProc));
        }

        private IntPtr GetWindowHandle()
        {
            var window = Window.GetWindow(this);
            if (window == null) return IntPtr.Zero;

            return new WindowInteropHelper(window).Handle;
        }

        // handles the message that is sent when the clipboard updates
        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            _vm.HandleClipboardMessage(msg);

            return IntPtr.Zero;
        }
    }
}
