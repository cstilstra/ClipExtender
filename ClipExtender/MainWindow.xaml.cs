//This file is part of ClipExtender.

//ClipExtender is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//ClipExtender is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with ClipExtender.  If not, see <http://www.gnu.org/licenses/>.

using ClipExtender.ViewModels;
using System;
using System.Windows;
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
