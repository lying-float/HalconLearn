using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using HalconDotNet;
using System.IO;
using System.Windows.Forms;
using CENJE_Vison.ViewModel;
using CENJE_Vison.Common;
using GalaSoft.MvvmLight.Messaging;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace CENJE_Vison.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel.Init();
            //注册Messenger接受打开新窗口信息，若接收到则打开新窗口。
            
            #region 旧方法，通过Messenger发送信息在View层打开窗口
            //接收信息后打开配方窗口
            //Messenger.Default.Register< NotificationMessage > (this,MainViewModel.Token_OpenFormula, message =>
            //{
            //    var famulaWindow = new FormulaWindow();
            //    famulaWindow.Show();
            //});
            ////接收信息后打开负极参数调试窗口
            //Messenger.Default.Register<NotificationMessage>(this, MainViewModel.Token_Setting_Negative, message =>
            //{
            //    var settingWindow = new SettingNegativeWindow();
            //    settingWindow.Show();
            //});
            ////接收信息后打开正极参数调试窗口
            //Messenger.Default.Register<NotificationMessage>(this, MainViewModel.Token_Setting_Positive, message =>
            //{
            //    var settingWindow = new SettingPositiveWindow();
            //    settingWindow.Show();
            //});
            ////接收信息后打开成品参数调试窗口
            //Messenger.Default.Register<NotificationMessage>(this, MainViewModel.Token_Setting_Finish, message =>
            //{
            //    var settingWindow = new SettingFinishWindow();
            //    settingWindow.Show();
            //});

            //接收到信息后打开登录界面
            //Messenger.Default.Register<NotificationMessage>(this, message =>
            //{
            //    if (message.Notification == "LoginWindow")
            //    {
            //        var loginWindow = new LoginWindow();
            //        loginWindow.Show();
            //    }
            //});
            #endregion

            //halcon窗口显示图像
            //Messenger.Default.Register<NotificationMessage<HObject>>(this, message => { ShowImageWnd((HObject)message.Content, message.Notification); });
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (message.Notification == "Start_Detection")
                {
                    Detection();
                }
            });
        }

        #region 字段
        bool p = true;
        bool Flag = true;
        //public static Thread[] DetectionThreads = new Thread[3];
        StartWindow startWindow = new StartWindow();
        //CancellationTokenSource cts = new CancellationTokenSource();
        #endregion

        #region 属性
        //MainViewModel Vm { get; set; } = new MainViewModel();
        #endregion

        #region 方法
        private void Detection()
        {
            //DetectionThreads[0] = new Thread(NegativeReadAndDetection);
            //DetectionThreads[1] = new Thread(PositiveReadAndDetection);
            //DetectionThreads[2] = new Thread(FinishReadAndDetection);

            //DetectionThreads[0].Name = "N_DetectionThread";
            //DetectionThreads[1].Name = "P_DetectionThread";
            //DetectionThreads[2].Name = "F_DetectionThread";

            //DetectionThreads[0].IsBackground = true;
            //DetectionThreads[1].IsBackground = true;
            //DetectionThreads[2].IsBackground = true;

            //DetectionThreads[0].Start();
            //DetectionThreads[1].Start();
            //DetectionThreads[2].Start();
            var token=MainViewModel.cts.Token;
            Task.Factory.StartNew(() => { NegativeReadAndDetection(); }, TaskCreationOptions.LongRunning);
            Task.Factory.StartNew(() => { PositiveReadAndDetection(); }, TaskCreationOptions.LongRunning);
            Task.Factory.StartNew(() => { FinishReadAndDetection(); },TaskCreationOptions.LongRunning);
        }

        public void NegativeReadAndDetection()
        {
            //MainViewModel.mreN.WaitOne();
            try
            {
                while (false)
                {
                    
                    if(MainViewModel.cts.Token.IsCancellationRequested)throw new OperationCanceledException();
                    //while (true) { Debug.WriteLine("running"); MainViewModel.mreN.WaitOne(); }
                    MainViewModel.mreN.WaitOne();
                    //int ImageCount;
                    //List<string> ImagePaths=new List<string>();
                    //int nowCount;
                    foreach (string Path in Directory.GetFiles(@"C:\Users\W\Desktop\20230923"))
                    {
                        //ImagePaths.Add(Path);
                        bool error = false;
                        HTuple fileName = Path;
                        HObject himage = new HObject();
                        HOperatorSet.ReadImage(out himage, fileName);
                        //NImage = himage.Clone() as HObject;
                        //Messenger.Default.Send(new NotificationMessage<HObject>(NImage, "Nimage"));
                        HalFunc halFunc = new HalFunc();
                        halFunc.HNegativeDetection(himage, location1HWindow.HalconWindow, MainViewModel.NegativeTolerance, MainViewModel.NegativeDetection, out error);
                        //Thread.Sleep(500);
                        //HOperatorSet.DispObj(himage, location2HWindow.HalconWindow);
                        fileName.Dispose(); himage.Dispose();
                        //DetectionThreads[0].Join(new TimeSpan(2000));
                    }
                    Thread.Sleep(0);
                }
            }
            catch (OperationCanceledException)
            {

                return;
            }
            
        }
        public void PositiveReadAndDetection()
        {
            try
            {
                while (false)
                {
                    MainViewModel.mreP.WaitOne();
                    p = false;
                    //int ImageCount;
                    //List<string> ImagePaths=new List<string>();
                    //int nowCount;
                    foreach (string Path in Directory.GetFiles(@"C:\Users\W\Desktop\picture\negativeNG"))
                    {
                        bool error = false;
                        HTuple fileName = Path;
                        HObject himage = new HObject();
                        HTuple hv_width, hv_height;
                        HOperatorSet.ReadImage(out himage, fileName);
                        //PImage = himage.Clone() as HObject;
                        //Messenger.Default.Send(new NotificationMessage<HObject>(NImage, "Pimage"));

                        HalFunc halFunc = new HalFunc();
                        //HOperatorSet.GetImageSize(himage,out hv_width,out hv_height);
                        //HOperatorSet.SetPart(location2HWindow.HalconWindow, 0, 0, hv_height, hv_width);
                        //HOperatorSet.DispObj(himage, location2HWindow.HalconWindow);
                        //halFunc.HPositiveDetection(himage, location2HWindow.HalconWindow, MainViewModel.PositiveTolerance, MainViewModel.PositiveDetection, out error);

                        HalconHelper halconHelper = new HalconHelper();
                        //halconHelper.ShowImageWnd(himage, location2HWindow.HalconWindow);
                        //HOperatorSet.DispObj(himage, location2HWindow.HalconWindow);
                        fileName.Dispose(); himage.Dispose();
                        //hv_width.Dispose(); hv_height.Dispose();
                    }
                    Thread.Sleep(0);
                }
            }
            catch (ThreadInterruptedException)
            {

                return;
            }
            
        }
        public void FinishReadAndDetection()
        {
            try
            {
                //while (true) { Debug.WriteLine("runing"); }
                while (false)
                {
                    MainViewModel.mreF.WaitOne();
                    //int ImageCount;
                    //List<string> ImagePaths=new List<string>();
                    //int nowCount;
                    foreach (string Path in Directory.GetFiles(@"C:\Users\W\Desktop\picture\finish"))
                    {
                        //ImagePaths.Add(Path);
                        bool error = false;
                        HTuple fileName = Path;
                        HObject himage = new HObject();
                        HTuple hv_width, hv_height;
                        HOperatorSet.ReadImage(out himage, fileName);
                        HalFunc halFunc = new HalFunc();
                        HOperatorSet.GetImageSize(himage, out hv_width, out hv_height);
                        HOperatorSet.SetPart(location2HWindow.HalconWindow, 0, 0, hv_height, hv_width);
                        //HOperatorSet.DispObj(himage, location2HWindow.HalconWindow);
                        //halFunc.HFinishDetection(himage, location3HWindow.HalconWindow, MainViewModel.FinishTolerance, MainViewModel.FinishDetection, out error);
                        //halFunc.HPositiveDetection(himage, location2HWindow.HalconWindow, MainViewModel.PositiveTolerance, MainViewModel.PositiveDetection, out error);
                        fileName.Dispose(); himage.Dispose();
                        fileName.Dispose(); himage.Dispose();
                        //hv_width.Dispose(); hv_height.Dispose();
                    }
                    Thread.Sleep(0);
                }
            }
            catch (ThreadInterruptedException)
            {

                return;
            }
           
        }

        #endregion

    }
}
