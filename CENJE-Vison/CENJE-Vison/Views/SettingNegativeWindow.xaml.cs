using CENJE_Vison.Common;
using GalaSoft.MvvmLight.Messaging;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CENJE_Vison.ViewModel;
using System.IO;
using System.Threading;
using GalaSoft.MvvmLight.Ioc;

namespace CENJE_Vison.Views
{
    /// <summary>
    /// SettingUpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingNegativeWindow : Window
    {
        public SettingNegativeWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                switch (message.Notification)
                {
                    case "ShowNegativeOK": DisplayImage("OK"); break;
                    case "ShowNegativeNG": DisplayImage("NG"); break;
                    default: break;
                }
            });
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (message.Notification == "TestWindowStartN")
                {
                    if (testThread == null)
                    {
                        testThread = new Thread(() => { NegativeReadAndDetection(); });
                        testThread.Start();
                    }
                    else
                    {
                        //testThread.Start();
                        mre.Set();
                    }
                }
            });
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (message.Notification == "TestWindowStopN")
                {
                    if (testThread != null && testThread.IsAlive)
                    {
                        //testThread.Interrupt();
                        mre.Reset();
                    }
                }
            });
        }
        #region 字段
        int width = 0;
        int height = 0;
        //HDrawingObject drawingObject = new HDrawingObject();
        HTuple paramName = new HTuple();
        string[] paramlist = { "row", "column", "phi", "length1","length2" };
        HImage Himage = new HImage();
        bool isSettingRoi = false;
        bool imageRead=false;
        Thread testThread = null;
        ManualResetEvent mre = new ManualResetEvent(true); 
        enum RoiDrawType
        {
            defaultValue,
            pictureRoi,
            flowerRoi,
            //flowerCenterRoi,
            tongueRoi,
            pinRoi
        }
        RoiDrawType roiDrawType= RoiDrawType.defaultValue;
        HalFunc halFunc = new HalFunc();
        #endregion

        #region 方法
        #region 读取并显示Roi
        /// <summary>
        /// 显示图像
        /// </summary>
        /// <param name="path"></param>
        public void DisplayImage(string path)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "D:\\CENJE-Vison\\Image\\Location1\\"+path;
            //ofd.Filter = "图片|*.png *.jpg|所有文件|*.*";  //显示的文件类型
            ofd.Filter = "图片|*.png;*.jpg;*.bmp|(*.JPG)|*.jpg";
            ofd.RestoreDirectory = true;
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                HTuple fileName = ofd.FileName;
                //HObject himage = new HObject();
                HTuple hv_height=null; HTuple hv_width=null;
                //HOperatorSet.ReadImage(out Himage, fileName);
                Himage.ReadImage(fileName);
                HOperatorSet.GetImageSize(Himage, out hv_width,out hv_height);
                HOperatorSet.SetPart(HwindowTest1.HalconWindow, 0, 0, hv_height - 1, hv_width - 1);
                HOperatorSet.DispObj(Himage, HwindowTest1.HalconWindow);
                fileName.Dispose(); //Himage.Dispose();
                hv_height.Dispose(); hv_width.Dispose();
            }
            imageRead = true;
        }
        #endregion

        #region 绘制ROI按键
        private void btn_pictureROI_N_Click(object sender, RoutedEventArgs e)
        {
            if (roiDrawType == RoiDrawType.defaultValue)
            {
                if (!isSettingRoi)
                {
                    isSettingRoi = true;
                    roiDrawType = RoiDrawType.pictureRoi;
                    btn_pictureROI_N.Content = "点击确认";
                    DrawRoi();

                }
            }
            else if (roiDrawType == RoiDrawType.pictureRoi)
            {
                roiDrawType = RoiDrawType.defaultValue;
                btn_pictureROI_N.Content = "图像有效区域ROI";
                isSettingRoi = false;
                //保存roi信息
                SaveRoi("pictureRoi");
            }

        }

        private void btn_pinROI_N_Click(object sender, RoutedEventArgs e)
        {
            if (roiDrawType == RoiDrawType.defaultValue)
            {
                if (!isSettingRoi)
                {
                    isSettingRoi = true;
                    roiDrawType = RoiDrawType.pinRoi;
                    btn_pinROI_N.Content = "点击确认";
                    DrawRoi();
                }
            }
            else if(roiDrawType == RoiDrawType.pinRoi)
            {
                roiDrawType = RoiDrawType.defaultValue;
                btn_pinROI_N.Content = "针ROI";
                isSettingRoi = false;
                //保存roi信息
                SaveRoi("PinRoi");
            }
        }

        private void btn_flowerROI_N_Click(object sender, RoutedEventArgs e)
        {
            if (roiDrawType == RoiDrawType.defaultValue)
            {
                if (!isSettingRoi)
                {
                    isSettingRoi = true;
                    roiDrawType = RoiDrawType.flowerRoi;
                    btn_flowerROI_N.Content = "点击确认";
                    DrawRoi();

                }
            }
            else if(roiDrawType== RoiDrawType.flowerRoi)
            {
                roiDrawType = RoiDrawType.defaultValue;
                btn_flowerROI_N.Content = "花瓣对角ROI";
                isSettingRoi = false;
                //保存roi信息
                SaveRoi("FlowerRoi");
            }
        }

        private void btn_aluminiumTongueROI_N_Click(object sender, RoutedEventArgs e)
        {
            if (roiDrawType == RoiDrawType.defaultValue)
            {
                if (!isSettingRoi)
                {
                    isSettingRoi = true;
                    roiDrawType = RoiDrawType.tongueRoi;
                    btn_aluminiumTongueROI_N.Content = "点击确认";
                    DrawRoi();

                }
            }
            else if(roiDrawType == RoiDrawType.tongueRoi)
            {
                roiDrawType = RoiDrawType.defaultValue;
                btn_aluminiumTongueROI_N.Content = "铝舌区域ROI";
                isSettingRoi = false;
                //保存roi信息
                SaveRoi("TongueRoi");
            }
        }
        #endregion

        #region 绘制与保存Roi数据
        private void DrawRoi()
        {
            //if (imageRead)
            //{
            //    Himage.GetImageSize(out width, out height);
            //    drawingObject = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.RECTANGLE2, height / 2, width / 2,0,
            //        height/4, width/4);
            //    HwindowTest1.HalconWindow.AttachDrawingObjectToWindow(drawingObject);
               
            //}
            //else
            //    System.Windows.MessageBox.Show("未读取图像");
            
        }

        private void SaveRoi(string roiType)
        {
            //paramName = paramlist;
            //HTuple param = drawingObject.GetDrawingObjectParams(paramName);
            //double[] RoiParam = { param.DArr[0], param.DArr[1], param.DArr[2], param.DArr[3], param.DArr[4] };
            //switch (roiType)
            //{
            //    case"PictureRoi":
            //        MainViewModel.N_cameraAndCalibrationModel.PictureRoi = RoiParam;
            //        MainViewModel.NegativeDetection.PictureRoi = RoiParam;
            //        break;
            //    case "FlowerRoi":
            //        MainViewModel.N_cameraAndCalibrationModel.FlowerRoi = RoiParam;
            //        MainViewModel.NegativeDetection.FlowerRoi = RoiParam;
            //        break;
            //    case "PinRoi":
            //        MainViewModel.N_cameraAndCalibrationModel.PinRoi = RoiParam;
            //        MainViewModel.NegativeDetection.PinRoi = RoiParam;
            //        break;
            //    case "TongueRoi":
            //        MainViewModel.N_cameraAndCalibrationModel.TongueRoi = RoiParam;
            //        MainViewModel.NegativeDetection.TongueRoi = RoiParam;
            //        break;
            //    case "LineRoi":
            //        MainViewModel.N_cameraAndCalibrationModel.LineRoi = RoiParam;
            //        MainViewModel.NegativeDetection.LineRoi = RoiParam;
            //        break;
            //    default:break;
            //}
        }
        #endregion

        #region 关闭窗口
        private void Window_Closed(object sender, EventArgs e)
        {
            //Himage.Dispose();
            //drawingObject.Dispose();
            //if (testThread != null && testThread.IsAlive)
            //{
            //    testThread.Interrupt();
            //    testThread.Abort();
            //}
            SimpleIoc.Default.Unregister<SettingNegativeWindow>();
            SimpleIoc.Default.Register<SettingNegativeWindow>();
        }
        #endregion

        #region 调试窗口开始检测
        public void NegativeReadAndDetection()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(0);
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
                        halFunc.HNegativeDetection(himage, HwindowTest1.HalconWindow, MainViewModel.NegativeTolerance, MainViewModel.NegativeDetection, out error);
                        //Thread.Sleep(500);
                        //HOperatorSet.DispObj(himage, location2HWindow.HalconWindow);
                        fileName.Dispose(); himage.Dispose();
                        //DetectionThreads[0].Join(new TimeSpan(2000));
                    }
                    
                }
            }
            catch (ThreadInterruptedException)
            {

                return;
            }

        }
        #endregion

        #endregion


    }
}
