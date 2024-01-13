using CENJE_Vison.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CENJE_Vison.Views
{
    /// <summary>
    /// StartWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
            //DataContext=new StartWindowViewModel();
        }
        //public void LoadComplete()
        //{
        //    base.Dispatcher.InvokeShutdown();
        //}
        public void ShowProcessValueText(string processValueText)
        {
            if(processValueText != null)
            {
                Application.Current.Dispatcher.Invoke(new Action(() => { ProcessValueText.Text = processValueText; }));
                Thread.Sleep(100);
            }
        }

        public void LoadCpmpleta()
        {
            base.Dispatcher.InvokeShutdown();
        }

        //public void ShowProgressBar(int progress)
        //{
        //    if(progress==0)
        //    {
        //        return;
        //    }
        //    Application.Current.Dispatcher.Invoke(new Action(() => { }));
        //}
    }
}
