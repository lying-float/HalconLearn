using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using CENJE_Vison.Models;
using CENJE_Vison.ViewModel;
using CENJE_Vison.Common.HalconFunc;
using HalconDotNet;
using System.Windows.Forms;
using GalaSoft.MvvmLight.Ioc;
using CENJE_Vison.Views;
using System.Collections.ObjectModel;

namespace CENJE_Vison.ViewModel
{

    public class SettingNegativeViewModel:ViewModelBase
    {
        #region 字段

        private HSmartWindowControlWPF hSmart;
        HWindow hWindow;
        ObservableCollection<HDrawingObjectInfo> hDrawingObjects;
        #endregion

        #region 属性
        //public MembraneDetectionModel N_membraneDetectionModel_B { get; set; } = new MembraneDetectionModel();//黑箔时使用的Model
        //public MembraneDetectionModel N_membraneDetectionModel_L { get; set; } = new MembraneDetectionModel();//小银箔时使用的Model
        //public MembraneDetectionModel N_membraneDetectionModel_S { get; set; } = new MembraneDetectionModel();//银箔时使用的Model
        //public CameraAndCalibrationModel N_cameraAndCalibrationModel { get; set; } = new CameraAndCalibrationModel();

        public RelayCommand SaveSetting_Command { get; set; }//保存设定命令
        public RelayCommand ReadOK_Command { get; set; }//读取OK图像命令
        public RelayCommand ReadNG_Command { get; set; }//读取NG图像命令
        public RelayCommand<string> DrawFlowerRoi_Command { get; set; }//画Roi
        public RelayCommand Start_Command { get; set; }//执行测试
        public RelayCommand Stop_Command { get; set; }//停止测试

        private HObject image;

        public HObject Image
        {
            get { return image; }
            set
            {
                image = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        public SettingNegativeViewModel()
        {
            ReadOK_Command = new RelayCommand(ReadOkPicture);
            ReadNG_Command = new RelayCommand(ReadNgPicture);
            SaveSetting_Command=new RelayCommand(Btn_SaveSetting);
            DrawFlowerRoi_Command=new RelayCommand<string>(new Action<string>(DrawRoi));
        }

        #region 方法
        /// <summary>
        /// 按键保存参数修改
        /// </summary>
        private void Btn_SaveSetting()
        {
            MainViewModel.SetDetectionSetting();
        }

        /// <summary>
        /// 读取良品图片
        /// </summary>
        private void ReadOkPicture()
        {
            hWindow = SimpleIoc.Default.GetInstance<SettingNegativeWindow>().HwindowTest1.HalconWindow;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "D:\\CENJE-Vison\\Image\\Location1\\OK";
            //ofd.Filter = "图片|*.png *.jpg|所有文件|*.*";  //显示的文件类型
            ofd.Filter = "图片|*.png;*.jpg;*.bmp|(*.JPG)|*.jpg";
            ofd.RestoreDirectory = true;
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                HTuple fileName = ofd.FileName;
                //HObject himage = new HObject();
                HTuple hv_height = null; HTuple hv_width = null;
                //HOperatorSet.ReadImage(out Himage, fileName);
                var img=new HImage(fileName);
                Image=img;
                HOperatorSet.GetImageSize(Image, out hv_width, out hv_height);
                HOperatorSet.SetPart(hWindow, 0, 0, hv_height - 1, hv_width - 1);
                HOperatorSet.DispObj(Image, hWindow);
                fileName.Dispose(); //Himage.Dispose();
                hv_height.Dispose(); hv_width.Dispose();
                img.Dispose();
            }
            //imageRead = true;
            //Messenger.Default.Send(new NotificationMessage("ShowNegativeOK"));
        }

        /// <summary>
        /// 读取不良图像
        /// </summary>
        private void ReadNgPicture()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "D:\\CENJE-Vison\\Image\\Location1\\OK";
            //ofd.Filter = "图片|*.png *.jpg|所有文件|*.*";  //显示的文件类型
            ofd.Filter = "图片|*.png;*.jpg;*.bmp|(*.JPG)|*.jpg";
            ofd.RestoreDirectory = true;
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                HTuple fileName = ofd.FileName;
                //HObject himage = new HObject();
                HTuple hv_height = null; HTuple hv_width = null;
                //HOperatorSet.ReadImage(out Himage, fileName);
                var img = new HImage(fileName);
                Image = img;
                HOperatorSet.GetImageSize(Image, out hv_width, out hv_height);
                HOperatorSet.SetPart(hWindow, 0, 0, hv_height - 1, hv_width - 1);
                HOperatorSet.DispObj(Image, hWindow);
                fileName.Dispose(); img.Dispose();
                hv_height.Dispose(); hv_width.Dispose();
            }
            //Messenger.Default.Send(new NotificationMessage("ShowNegativeNG"));
        }

        private void DrawRoi(string drawType)
        {
            if(drawType != null)
            {
                switch (drawType)
                {
                    case "btn_pictureROI_N":
                        DrawShape();
                        break;

                    case "btn_pinROI_N": break;

                    case "btn_flowerROI_N": break;

                    case "btn_aluminiumTongueROI_N": break;

                }
            }
        }

        /// <summary>
        /// 读取花瓣模板
        /// </summary>
        private void ReadFlowersModel()
        {
            Messenger.Default.Send(new NotificationMessage("ShowNegativeNG"));
        }

        /// <summary>
        /// 调试窗口开始检测
        /// </summary>
        private void StartDetection()
        {
            Messenger.Default.Send(new NotificationMessage("TestWindowStartN"));
        }

        /// <summary>
        /// 调试窗口停止检测
        /// </summary>
        private void StopDetection()
        {
            Messenger.Default.Send(new NotificationMessage("TestWindowStopN"));
        }

        async private void DrawShape() 
        {
            await Task.Run(() =>
            {
                HObject drawObj;
                HOperatorSet.GenEmptyObj(out drawObj);
                HOperatorSet.SetColor(hWindow, "green");
                HTuple row = new HTuple();
                HTuple col = new HTuple();
                HTuple phi = new HTuple();
                HTuple Length1 = new HTuple();
                HTuple Length2 = new HTuple();
                HOperatorSet.DrawRectangle2(hWindow, out row, out col, out phi, out Length1, out Length2);
                HOperatorSet.GenRectangle2(out drawObj, row, col, phi, Length1, Length2);
                hWindow.DispObj(drawObj);
                //drawObj = hTuples.GenRectangle2();

                drawObj.Dispose();
                row.Dispose();
                col.Dispose();
                phi.Dispose();
                Length1.Dispose();
                Length2.Dispose();
            });
        }
        #endregion
    }
}
