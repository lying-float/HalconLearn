using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace CENJE_Vison.Views
{
    /// <summary>
    /// KeyBoardWindow.xaml 的交互逻辑
    /// </summary>
    public partial class KeyBoardWindow : Window
    {
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nindex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 GetWindowLong(IntPtr hWnd, int index);
        public KeyBoardWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            
        }
        


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper windowInteropHelper = new WindowInteropHelper(this);

            IntPtr intPtr = windowInteropHelper.Handle;

            int value = -20;

            SetWindowLong(intPtr, value, (IntPtr)0x8000000);
        }

        private void MainWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
