using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;

namespace CENJE_Vison.ViewModel
{
    public class SettingPositiveViewModel:ObservableObject
    {
        #region 属性
        //public MembraneDetectionModel N_membraneDetectionModel_B { get; set; } = new MembraneDetectionModel();//黑箔时使用的Model
        //public MembraneDetectionModel N_membraneDetectionModel_L { get; set; } = new MembraneDetectionModel();//小银箔时使用的Model
        //public MembraneDetectionModel N_membraneDetectionModel_S { get; set; } = new MembraneDetectionModel();//银箔时使用的Model
        //public CameraAndCalibrationModel N_cameraAndCalibrationModel { get; set; } = new CameraAndCalibrationModel();

        public RelayCommand SaveSetting_Command { get; set; }//保存设定命令
        public RelayCommand ReadOK_Command { get; set; }//读取OK图像命令
        public RelayCommand ReadNG_Command { get; set; }//读取NG图像命令
        public RelayCommand Start_Command { get; set; }//执行测试
        public RelayCommand Stop_Command { get; set; }//停止测试

        #endregion

        public SettingPositiveViewModel()
        {
            ReadOK_Command = new RelayCommand(ReadOkPicture);
            ReadNG_Command = new RelayCommand(ReadNgPicture);
            SaveSetting_Command = new RelayCommand(Btn_SaveSetting);
            Start_Command = new RelayCommand(StartDetection);
            Stop_Command = new RelayCommand(StopDetection);
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
        /// 读取参数
        /// </summary>
        private void ReadSetting()
        {
            
        }

        /// <summary>
        /// 读取良品图片
        /// </summary>
        private void ReadOkPicture()
        {
            Messenger.Default.Send(new NotificationMessage("ShowPositiveOK"));
        }

        /// <summary>
        /// 读取不良图像
        /// </summary>
        private void ReadNgPicture()
        {
            Messenger.Default.Send(new NotificationMessage("ShowPositiveNG"));
        }

        /// <summary>
        /// 读取花瓣模板
        /// </summary>
        private void ReadFlowersModel()
        {
            Messenger.Default.Send(new NotificationMessage("ShowPositiveNG"));
        }

        /// <summary>
        /// 调试窗口开始检测
        /// </summary>
        private void StartDetection()
        {
            Messenger.Default.Send(new NotificationMessage("TestWindowStartP"));
        }

        /// <summary>
        /// 调试窗口停止检测
        /// </summary>
        private void StopDetection()
        {
            Messenger.Default.Send(new NotificationMessage("TestWindowStopP"));
        }

        #endregion
    }
}
