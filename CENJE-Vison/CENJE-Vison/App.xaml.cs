using CENJE_Vison.Models.UserSection;
using CENJE_Vison.ViewModel;
using CENJE_Vison.Views;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CENJE_Vison
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static new App Current=>(App)Application.Current;
        ManualResetEvent ResetSplashCreated;
        Thread splashThread;
        private static StartWindow Splash;
        StartWindowViewModel startWindowModel = new StartWindowViewModel();
        public Configuration Cfg { get;  }
        public App()
        {
            Cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (Cfg.GetSection("MainViewSection") == null)
            {
                Cfg.Sections.Add("MainViewSection", new MainViewSection());
                Cfg.Save();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string pName = Process.GetCurrentProcess().ProcessName;
            if(Process.GetProcessesByName(pName).Length >1)
            {
                MessageBox.Show("程序已启动，请勿重复打开！！");
                Environment.Exit(0);
            }
            //OpenSplash();

            //ClosedSplash();

            
            //Views.MainWindow mainView = new Views.MainWindow();
            //App.Current.MainWindow = mainView;
            //Current.StartupUri = new Uri("Views/MainWindow.xaml", UriKind.Relative);
            //Current.Run();
            //mainView.Show();
            //mainView.Activate();
        }
    }
}
