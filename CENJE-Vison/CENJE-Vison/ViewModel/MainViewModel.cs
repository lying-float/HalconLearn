using CENJE_Vison.Models;
using CENJE_Vison.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using CENJE_Vison.Views;
using HalconDotNet;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using CENJE_Vison.Models.UserSection;
using GalaSoft.MvvmLight.Ioc;
using static CENJE_Vison.Common.DetectionSetting;
using System.Xml.Serialization;

namespace CENJE_Vison.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region 字段
        /// <summary>
        /// 定义用于Messenger区分的密文
        /// </summary>
        //public static readonly Guid Token_OpenFormula = Guid.NewGuid();
        //public static readonly Guid Token_Setting_Negative = Guid.NewGuid();
        //public static readonly Guid Token_Setting_Positive = Guid.NewGuid();
        //public static readonly Guid Token_Setting_Finish = Guid.NewGuid();
        //xml处理
        public static XmlHelper helper = null;
        //是否已经输入过正确密码的状态标志
        public static bool _passWordEnter = false;
        //判定是要调试的是负极，正极还是成品
        public enum _whichSettingToOpen
        {
            OpenSettingNegative = 0,
            OpenSettingPositive = 1,
            OpenSettingFinish = 2,
            defult = 0
        };
        public static _whichSettingToOpen _WhichSetting = _whichSettingToOpen.defult;
        private static readonly object syncXml = new object();//用于线程锁
        //线程相关
        public static CancellationTokenSource cts = new CancellationTokenSource();
        public static ManualResetEvent mreN = new ManualResetEvent(true);
        public static ManualResetEvent mreP = new ManualResetEvent(false);
        public static ManualResetEvent mreF = new ManualResetEvent(false);
        public static AutoResetEvent areN = new AutoResetEvent(false);
        public static AutoResetEvent areP = new AutoResetEvent(false);
        public static AutoResetEvent areF = new AutoResetEvent(false);
        //启动过程状态
        private enum InitIndex
        {
            Error = -1,
            InitComplete,
            InitBegin,
            ValueSetting,
            ReadConfig,
            CarmeraInit,
            IOInit,
        }
        #endregion

        #region 属性
        private static MainViewModel _Instance;

        public static MainViewModel Instance
        {
            get 
            { 
                if(_Instance == null)
                {
                    _Instance=new MainViewModel();
                }
                return _Instance; 
            }
            set { _Instance = value; }
        }

        public static MainModel mainModel { get; set; } = new MainModel() { Run_Flag=true};
        //相机属性
        public HikVisionSDK Ncamera { get; set; } = new HikVisionSDK();//负极相机
        public HikVisionSDK Pcamera { get; set; } = new HikVisionSDK();//正极相机
        public HikVisionSDK Fcamera { get; set; } = new HikVisionSDK();//成品相机
        public MainViewSection MainViewSection { get; set; }=new MainViewSection();
        #region 命令
        public RelayCommand Start_Command { get; set; }//启动
        public RelayCommand Stop_Command { get; set; }//启动
        public RelayCommand<string> Record_Command { get; set; }//打开记录
        public RelayCommand Load_Command { get; set; }//加载
        public RelayCommand Formula_Command { get; set; }//打开配方窗口命令
        public RelayCommand<string> SettingUp_Command { get; set; }//打开参数修改设置界面
        public RelayCommand Closed_Command { get; set; }//关闭主窗口后处理命令
        public RelayCommand MenuItem_Command { get; set; }//菜单MenuItem触发事件
        public RelayCommand<string> MenuItemMask_Command { get; set; }//菜单屏蔽信号MenuItem触发事件
        #endregion

        #region UI绑定的检测参数
        //主窗口属性
        public static DetectionResultModel DetectionResultModel { get; set; }= new DetectionResultModel();

        //配方Model属性
        public static string FM_name = null;//当前选择的配方名
        public static FormulaModel FM { get; set; } = new FormulaModel();

        //负极Model参数属性，程序启动后读取config文件内容赋值，打开设定界面时传递设定值给设定界面的ViewModel
        public static MembraneDetectionModel N_membraneDetectionModel_B { get; set; } = new MembraneDetectionModel();//黑膜负极参数
        public static MembraneDetectionModel N_membraneDetectionModel_S { get; set; } = new MembraneDetectionModel();//银膜负极参数
        public static MembraneDetectionModel N_membraneDetectionModel_L { get; set; } = new MembraneDetectionModel();//小银膜负极参数
        public static CameraAndCalibrationModel N_cameraAndCalibrationModel { get; set; } = new CameraAndCalibrationModel();//负极相机与标定参数

        //正极Model参数属性，程序启动后读取config文件内容赋值，打开设定界面时传递设定值给设定界面的ViewModel
        public static MembraneDetectionModel P_membraneDetectionModel { get; set; } = new MembraneDetectionModel();//正极参数
        public static CameraAndCalibrationModel P_cameraAndCalibrationModel { get; set; } = new CameraAndCalibrationModel();//正极相机与标定参数

        //芯包成品Model参数属性，程序启动后读取config文件内容赋值，打开设定界面时传递设定值给设定界面的ViewModel
        public static FinishDetectionModel F_FinishDetectionModel { get; set; } = new FinishDetectionModel();//芯包成品参数
        public static CameraAndCalibrationModel F_cameraAndCalibrationModel { get; set; } = new CameraAndCalibrationModel();//成品相机与标定参数
        #endregion

        #region 后台运行参数
        //后台检测配方上下限设定
        public static MinMaxSetting.PositiveTolerance PositiveTolerance;
        public static MinMaxSetting.NegativeTolerance NegativeTolerance;
        public static MinMaxSetting.FinishTolerance FinishTolerance;
        //后台相机参数，标定参数
        public static DetectionSetting.PositiveDetection PositiveDetection;
        public static DetectionSetting.NegativeDetection NegativeDetection;
        public static DetectionSetting.FinishDetection FinishDetection;
        #endregion

        

        #endregion


        public MainViewModel()
        {
             Start_Command = new RelayCommand(new Action(Btn_Start));
            Stop_Command = new RelayCommand(new Action(Btn_Stop));
            Record_Command = new RelayCommand<string>(new Action<string>(Record_Click));
            Load_Command = new RelayCommand(new Action(WindowLoad));
            Formula_Command = new RelayCommand(new Action(OpenFormula));
            SettingUp_Command = new RelayCommand<string>(new Action<string>(OpenSettingUp));
            Closed_Command = new RelayCommand(WindowClosed);
            MenuItem_Command = new RelayCommand(MenuItem_Set);
            MenuItemMask_Command = new RelayCommand<string>(new Action<string>(MaskMenuItem_Checked));
            //测试打开相机
            //string nCameraId = "";
            //Ncamera.connectCamera(nCameraId);
            //Ncamera.StartCamera();
            
            MainViewSection = (MainViewSection)App.Current.Cfg.GetSection("MainViewSection");
            
        }


        #region 方法
        
        #region 初始化函数
        /// <summary>
        /// 程序初始化函数
        /// </summary>
        public static void Init()
        {
            var startView=SimpleIoc.Default.GetInstance<StartWindow>();
            startView.Show();
            try
            {
                var iRet = InitIndex.InitBegin;
                bool bRet = true;
                while (bRet)
                {
                    Thread.Sleep(1);
                    switch (iRet)
                    {
                        case InitIndex.Error:
                            System.Windows.Forms.MessageBox.Show("程序启动失败！");
                            bRet = false;
                            break;

                        case InitIndex.InitComplete:
                            bRet =false;
                            break;

                        case InitIndex.InitBegin:
                            LogHelper.Info("初始化开始");
                            iRet = InitIndex.ReadConfig;
                            break;

                        case InitIndex.ReadConfig:
                            LogHelper.Info("读取配置参数......");
                            if (GetConfig(false) && SetBackendValue())
                            {
                                iRet = InitIndex.ValueSetting;
                                CreatFiledDiretory();
                                LogHelper.Info("读取配置参数成功！");
                            }
                            else
                            {
                                iRet=InitIndex.Error;
                                LogHelper.Error("读取配置参数错误！");
                            }
                            break;

                        case InitIndex.ValueSetting:
                            CreatFiledDiretory();
                            iRet = InitIndex.CarmeraInit;
                            LogHelper.Info("设置软件参数......");
                            break;

                        case InitIndex.CarmeraInit:
                            iRet=InitIndex.IOInit;
                            LogHelper.Info("相机初始化......");
                            break;

                        case InitIndex.IOInit:
                            iRet=InitIndex.InitComplete;
                            LogHelper.Info("通讯接口初始化......");
                            break;

                        default: break;
                    }
                }
                startView.Close();

            }
            catch (Exception e)
            {
                startView.Close();
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }
        #endregion

        #region 窗口加载后处理函数
        /// <summary>
        /// 窗口加载事件处理函数
        /// </summary>
        private void WindowLoad()
        {
            Messenger.Default.Send(new NotificationMessage("Start_Detection"));
        }
        #endregion

        #region 窗口关闭处理函数
        private void WindowClosed()
        {
            cts.Cancel();
            WriteConfig();
            cts.Cancel();
            App.Current.Cfg.Save();
            
        }
        #endregion

        /// <summary>
        /// 项目启动时检测文件目录是否存在并创建
        /// </summary>
        private static void CreatFiledDiretory()
        {
            try
            {
                string ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Image"}\\{"Location1"}\\{"OK"}\\{DateTime.Now.ToString("yy-MM-dd")}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Image"}\\{"Location2"}\\{"OK"}\\{DateTime.Now.ToString("yy-MM-dd")}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Image"}\\{"Location3"}\\{"OK"}\\{DateTime.Now.ToString("yy-MM-dd")}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Image"}\\{"Location1"}\\{"NG"}\\{DateTime.Now.ToString("yy-MM-dd")}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Image"}\\{"Location2"}\\{"NG"}\\{DateTime.Now.ToString("yy-MM-dd")}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Image"}\\{"Location3"}\\{"NG"}\\{DateTime.Now.ToString("yy-MM-dd")}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Image"}\\{"Location1"}\\{"All"}\\{DateTime.Now.ToString("yy-MM-dd")}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Image"}\\{"Location2"}\\{"All"}\\{DateTime.Now.ToString("yy-MM-dd")}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Image"}\\{"Location3"}\\{"All"}\\{DateTime.Now.ToString("yy-MM-dd")}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Image"}\\{"Location1"}\\{"Fault"}\\{DateTime.Now.ToString("yy-MM-dd")}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Image"}\\{"Location2"}\\{"Fault"}\\{DateTime.Now.ToString("yy-MM-dd")}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Image"}\\{"Location3"}\\{"Fault"}\\{DateTime.Now.ToString("yy-MM-dd")}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Data"}\\{"Location1"}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Data"}\\{"Location2"}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Data"}\\{"Location3"}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Log"}\\{"Debug"}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Log"}\\{"Warn"}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Log"}\\{"Error"}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Log"}\\{"Info"}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

                ConfigPath = $"D:\\{"CENJE-Vison"}\\{"Model"}";
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("读取路径失败并且无法创建！"+ex.Message);
            }
            
        }

        #region 点击启动
        public void Btn_Start()
        {
            if (!mainModel.Run_Flag)
            {
                mainModel.Run_Flag = true;
                mreF.Set();
                mreN.Set();
                mreP.Set();
            }
            LogHelper.Info("test");
            LogHelper.Error("error");
            LogHelper.Warn("warn");
        }
        #endregion

        #region 点击停止
        public void Btn_Stop()
        {
            if (mainModel.Run_Flag)
            {
                mainModel.Run_Flag = false;
                mreF.Reset();
                mreN.Reset();
                mreP.Reset();
            }  
        }
        #endregion

        #region 点击“开始所有检测”时设置三个检测相关MenuItem的被勾选状态
        private void MenuItem_Set()
        {
            if(MainViewSection.isOpenAllChecked==true)
            {
                MainViewSection.IsOpenNegativeChecked = true;
                MainViewSection.IsOpenPositiveChecked = true;
                MainViewSection.IsOpenFinishChecked = true;
            }
            else
            {
                MainViewSection.IsOpenNegativeChecked = false;
                MainViewSection.IsOpenPositiveChecked = false;
                MainViewSection.IsOpenFinishChecked = false;
            }
        }
        #endregion

        #region 点击“屏蔽所有”时设置相应的选项的被勾选状态
        private void MaskMenuItem_Checked(string location)
        {
            bool isMasked = true;
            switch (location)
            {
                 case "negativeAll":
                    isMasked = MainViewSection.NegativeAll;
                    MainViewSection.NegativeAngle = isMasked;
                    MainViewSection.NegativeFlower = isMasked;
                    MainViewSection.NegativeL2 = isMasked;
                    MainViewSection.NegativeNeedleHole = isMasked;
                    MainViewSection.NegativeSplit = isMasked;
                    break;
                case "positiveAll":
                    isMasked = MainViewSection.PositiveAll;
                    MainViewSection.PositiveAngle = isMasked;
                    MainViewSection.PositiveFlower = isMasked;
                    MainViewSection.PositiveL2 = isMasked;
                    MainViewSection.PositiveNeedleHole = isMasked;
                    MainViewSection.PositiveSplit = isMasked;
                    break;
                case "productionAll":
                    isMasked |= MainViewSection.ProductionAll;
                    MainViewSection.ProductionDiameter = isMasked;
                    MainViewSection.ProductionfootOffset = isMasked;
                    MainViewSection.ProductionHeight = isMasked;
                    MainViewSection.PproductionfootDistanceExternal = isMasked;
                    break;
            }
        }
        #endregion

        /// <summary>
        /// 打开记录的图像或文档
        /// </summary>
        /// <param name="recordName">传入控件名</param>
        public void Record_Click(string recordName)
        {
            string path0 = null;
            string path1 = null;
            string path_open=null;
            if (recordName.Equals("Log")) path_open = @"d:\CENJE-Vison\Log\";
            else if (recordName.Length > 5 && !recordName.Remove(recordName.Length - 9, 9).Equals("Data"))
            {
                path0 = recordName.Substring(recordName.Length - 9, 9);
                path1 = recordName.Remove(recordName.Length - 9, 9);
                path_open = @"d:\CENJE-Vison\Image\" + path0 + @"\" + path1 + @"\";
            }
            else if (recordName.Substring(0, 4) == "Data")
            {
                path0 = recordName.Remove(recordName.Length - 9, 9);
                path1 = recordName.Substring(recordName.Length - 9, 9);
                path_open = @"d:\CENJE-Vison\" + path0 + @"\" + path1 + @"\";
            }
            else if (recordName.Length == 5)
            {
                path_open = @"d:\CENJE-Vison\Image\Fault\";
            }
            else return;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = path_open;
            //ofd.Filter = "图片|*.png *.jpg|所有文件|*.*";  //显示的文件类型
            ofd.Filter = "图片|*.png|(*.JPG)|*.jpg";
            ofd.RestoreDirectory = true;
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                HTuple fileName = ofd.FileName;
                HObject himage = new HObject();
                HOperatorSet.ReadImage(out himage, fileName);
                //PImage = himage.Clone() as HObject;
                //Messenger.Default.Send(new NotificationMessage<HObject>(NImage, "Nimage"));
                fileName.Dispose();himage.Dispose();
            }
        }

        #region 打开配方窗口或打开设置窗口
        /// <summary>
        /// 打开配方窗口
        /// </summary>
        public void OpenFormula()
        {
            //Messenger.Default.Send(new NotificationMessage("OpenFormula"),Token_OpenFormula);
            var formulaWindow=SimpleIoc.Default.GetInstance<FormulaWindow>();
            formulaWindow.Show();
        }

        /// <summary>
        /// 打开设置窗口
        /// </summary>
        /// <param name="type"></param>
        public void OpenSettingUp(string type)
        {
            if (type == null) return;
            else if(_passWordEnter==false)
            {
                switch (type)
                {
                    case "debugLocation1": _WhichSetting = _whichSettingToOpen.OpenSettingNegative; break;
                    case "debugLocation2": _WhichSetting = _whichSettingToOpen.OpenSettingPositive; break;
                    case "debugLocation3": _WhichSetting = _whichSettingToOpen.OpenSettingFinish; break;
                }
                //Messenger.Default.Send(new NotificationMessage("LoginWindow"));
                var loginWindow= SimpleIoc.Default.GetInstance<LoginWindow>();
                loginWindow.Show();
            }
            else
            {
                switch (type)
                {
                    case "debugLocation1":
                        var negativeSettingWindow = SimpleIoc.Default.GetInstance<SettingNegativeWindow>();
                        negativeSettingWindow.Show(); 
                        //Messenger.Default.Send(new NotificationMessage("SettingUp_Negative"), Token_Setting_Negative); 
                        break;
                    case "debugLocation2":
                        var positiveSettingWindow = SimpleIoc.Default.GetInstance<SettingPositiveWindow>();
                        positiveSettingWindow.Show(); 
                        //Messenger.Default.Send(new NotificationMessage("SettingUp_Positive"), Token_Setting_Positive); 
                        break;
                    case "debugLocation3":
                        var finishSettingWindow = SimpleIoc.Default.GetInstance<SettingFinishWindow>();
                        finishSettingWindow.Show(); 
                        //Messenger.Default.Send(new NotificationMessage("SettingUp_Finish"), Token_Setting_Finish); 
                        break;
                }
            }    
        }
        #endregion

        #endregion


        #region 初始化相关方法及其他方法

        /// <summary>
        /// 启动时读取config文件并初始化参数
        /// </summary>
        public static bool GetConfig(bool isChangeFormula)
        {
            try
            {
                string s_true = $"true";
                #region 配方参数读取
                //读取配方参数
                if (isChangeFormula)
                {
                    SetConfigValue("Config.xml", "FormulaName", $"{FM_name}");//写入当前使用配方名
                }
                else
                    FM_name = Convert.ToString(GetConfigValue("Config.xml", "FormulaName"));//读取要打开的配方文件名
                if (FM_name.Equals("")) FM_name="Formula.xml";
                FM.FormulaType = Convert.ToString(GetConfigValue(FM_name, "FormulaType"));
                FM.FlowerCount = Convert.ToInt32(GetConfigValue(FM_name, "FlowerCount"));
                FM.Warming = Convert.ToInt32(GetConfigValue(FM_name, "Warming"));
                //读取配方参数--负极
                FM.AreaMaxN = Convert.ToDouble(GetConfigValue(FM_name, "AreaMaxN"));
                FM.AreaMinN = Convert.ToDouble(GetConfigValue(FM_name, "AreaMinN"));
                FM.FoilCrackMaxN = Convert.ToDouble(GetConfigValue(FM_name, "FoilCrackMaxN"));
                FM.FoilCrackMinN = Convert.ToDouble(GetConfigValue(FM_name, "FoilCrackMinN"));
                FM.AngleMaxN = Convert.ToDouble(GetConfigValue(FM_name, "AngleMaxN"));
                FM.AngleMinN = Convert.ToDouble(GetConfigValue(FM_name, "AngleMinN"));
                FM.L2MaxN = Convert.ToDouble(GetConfigValue(FM_name, "L2MaxN"));
                FM.L2MinN = Convert.ToDouble(GetConfigValue(FM_name, "L2MinN"));
                FM.RivetOffsrtMaxN = Convert.ToDouble(GetConfigValue(FM_name, "RivetOffsrtMaxN"));
                FM.RivetOffsrtMinN = Convert.ToDouble(GetConfigValue(FM_name, "RivetOffsrtMinN"));
                FM.ColorN = Convert.ToInt32(GetConfigValue(FM_name, "ColorN"));
                if (FM.ColorN < 1 || FM.ColorN > 3) FM.ColorN = 1;
                //读取配方参数--正极
                FM.AreaMaxP = Convert.ToDouble(GetConfigValue(FM_name, "AreaMaxP"));
                FM.AreaMinP = Convert.ToDouble(GetConfigValue(FM_name, "AreaMinP"));
                FM.FoilCrackMaxP = Convert.ToDouble(GetConfigValue(FM_name, "FoilCrackMaxP"));
                FM.FoilCrackMinP = Convert.ToDouble(GetConfigValue(FM_name, "FoilCrackMinP"));
                FM.AngleMaxP = Convert.ToDouble(GetConfigValue(FM_name, "AngleMaxP"));
                FM.AngleMinP = Convert.ToDouble(GetConfigValue(FM_name, "AngleMinP"));
                FM.L2MaxP = Convert.ToDouble(GetConfigValue(FM_name, "L2MaxP"));
                FM.L2MinP = Convert.ToDouble(GetConfigValue(FM_name, "L2MinP"));
                FM.RivetOffsrtMaxP = Convert.ToDouble(GetConfigValue(FM_name, "RivetOffsrtMaxP"));
                FM.RivetOffsrtMinP = Convert.ToDouble(GetConfigValue(FM_name, "RivetOffsrtMinP"));
                //读取配方参数--芯包
                FM.HeightMax = Convert.ToDouble(GetConfigValue(FM_name, "HeightMax"));
                FM.HeightMin = Convert.ToDouble(GetConfigValue(FM_name, "HeightMin"));
                FM.ODMax = Convert.ToDouble(GetConfigValue(FM_name, "ODMax"));
                FM.ODMin = Convert.ToDouble(GetConfigValue(FM_name, "ODMin"));
                FM.PinPitchMax = Convert.ToDouble(GetConfigValue(FM_name, "PinPitchMax"));
                FM.PinPitchMin = Convert.ToDouble(GetConfigValue(FM_name, "PinPitchMin"));
                FM.DifferenceOfHeightMax = Convert.ToDouble(GetConfigValue(FM_name, "DifferenceOfHeightMax"));
                FM.DifferenceOfHeightMin = Convert.ToDouble(GetConfigValue(FM_name, "DifferenceOfHeightMin"));
                FM.CPlineUpMax = Convert.ToDouble(GetConfigValue(FM_name, "CPlineUpMax"));
                FM.CPlineUpMin = Convert.ToDouble(GetConfigValue(FM_name, "CPlineUpMin"));
                FM.CPlineBottomMax = Convert.ToDouble(GetConfigValue(FM_name, "CPlineBottomMax"));
                FM.CPlineBottomMin = Convert.ToDouble(GetConfigValue(FM_name, "CPlineBottomMin"));
                #endregion

                #region 负极设置
                //负极相机参数
                N_cameraAndCalibrationModel.ImageHeight = Convert.ToDouble(GetConfigValue("Config.xml", "ImageHeight_N"));
                N_cameraAndCalibrationModel.ImageWidth = Convert.ToDouble(GetConfigValue("Config.xml", "ImageWidth_N"));
                N_cameraAndCalibrationModel.OffsetX = Convert.ToDouble(GetConfigValue("Config.xml", "OffsetX_N"));
                N_cameraAndCalibrationModel.OffsetY = Convert.ToDouble(GetConfigValue("Config.xml", "OffsetY_N"));
                N_cameraAndCalibrationModel.FPS = Convert.ToDouble(GetConfigValue("Config.xml", "FPS_N"));
                N_cameraAndCalibrationModel.Angel = Convert.ToDouble(GetConfigValue("Config.xml", "Angel_N"));
                N_cameraAndCalibrationModel.Light1 = Convert.ToDouble(GetConfigValue("Config.xml", "Light1_N"));
                N_cameraAndCalibrationModel.Light2 = Convert.ToDouble(GetConfigValue("Config.xml", "Light2_N"));
                N_cameraAndCalibrationModel.LightTime = Convert.ToDouble(GetConfigValue("Config.xml", "LightTime_N"));
                N_cameraAndCalibrationModel.LightTimeAdvance = Convert.ToDouble(GetConfigValue("Config.xml", "LightTimeAdvance_N"));
                //负极标定参数
                N_cameraAndCalibrationModel.CalibrationX = Convert.ToDouble(GetConfigValue("Config.xml", "CalibrationX_N"));
                N_cameraAndCalibrationModel.CalibrationY = Convert.ToDouble(GetConfigValue("Config.xml", "CalibrationY_N"));
                N_cameraAndCalibrationModel.AdjustX = Convert.ToDouble(GetConfigValue("Config.xml", "AdjustX_N"));
                N_cameraAndCalibrationModel.AdjustY = Convert.ToDouble(GetConfigValue("Config.xml", "AdjustY_N"));
                if (s_true == GetConfigValue("Config.xml", "CalibrationMod_N"))
                    N_cameraAndCalibrationModel.CalibrationMod = true;
                else
                    N_cameraAndCalibrationModel.CalibrationMod = false;

                //黑膜负极设置
                N_membraneDetectionModel_B.MembraneThreshold = Convert.ToInt32(GetConfigValue("Config.xml", "B_membraneLocation_TN"));
                N_membraneDetectionModel_B.FootThreshold = Convert.ToInt32(GetConfigValue("Config.xml", "B_footThreshold_TN"));
                N_membraneDetectionModel_B.FlowerThrehold = Convert.ToInt32(GetConfigValue("Config.xml", "B_flower_TN"));
                N_membraneDetectionModel_B.L2Threshold = Convert.ToInt32(GetConfigValue("Config.xml", "B_L2_TN"));
                N_membraneDetectionModel_B.MembraneDownLineThrehold = Convert.ToInt32(GetConfigValue("Config.xml", "B_membraneDown_TN"));
                N_membraneDetectionModel_B.FlowerCenterThrehold = Convert.ToInt32(GetConfigValue("Config.xml", "B_flowerCenter_TN"));
                N_membraneDetectionModel_B.TongueThreshold = Convert.ToInt32(GetConfigValue("Config.xml", "B_tongue_TN"));
                N_membraneDetectionModel_B.SplitThreshold = Convert.ToInt32(GetConfigValue("Config.xml", "B_split_TN"));
                N_membraneDetectionModel_B.LocationScore = Convert.ToDouble(GetConfigValue("Config.xml", "B_locationScore_TN"));
                N_membraneDetectionModel_B.FlowerLocationScore = Convert.ToDouble(GetConfigValue("Config.xml", "B_flowerScore_TN"));
                N_membraneDetectionModel_B.Gain = Convert.ToInt32(GetConfigValue("Config.xml", "B_gain"));
                N_membraneDetectionModel_B.ExposureTime = Convert.ToInt32(GetConfigValue("Config.xml", "B_exposureTime"));

                //银膜负极设置
                N_membraneDetectionModel_S.MembraneThreshold = Convert.ToInt32(GetConfigValue("Config.xml", "S_membraneLocation_TN"));
                N_membraneDetectionModel_S.FootThreshold = Convert.ToInt32(GetConfigValue("Config.xml", "S_footThreshold_TN"));
                N_membraneDetectionModel_S.FlowerThrehold = Convert.ToInt32(GetConfigValue("Config.xml", "S_flower_TN"));
                N_membraneDetectionModel_S.L2Threshold = Convert.ToInt32(GetConfigValue("Config.xml", "S_L2_TN"));
                N_membraneDetectionModel_S.MembraneDownLineThrehold = Convert.ToInt32(GetConfigValue("Config.xml", "S_membraneDown_TN"));
                N_membraneDetectionModel_S.FlowerCenterThrehold = Convert.ToInt32(GetConfigValue("Config.xml", "S_flowerCenter_TN"));
                N_membraneDetectionModel_S.TongueThreshold = Convert.ToInt32(GetConfigValue("Config.xml", "S_tongue_TN"));
                N_membraneDetectionModel_S.SplitThreshold = Convert.ToInt32(GetConfigValue("Config.xml", "S_split_TN"));
                N_membraneDetectionModel_S.LocationScore = Convert.ToDouble(GetConfigValue("Config.xml", "S_locationScore_TN"));
                N_membraneDetectionModel_S.FlowerLocationScore = Convert.ToDouble(GetConfigValue("Config.xml", "S_flowerScore_TN"));
                N_membraneDetectionModel_S.Gain = Convert.ToInt32(GetConfigValue("Config.xml", "S_gain"));
                N_membraneDetectionModel_S.ExposureTime = Convert.ToInt32(GetConfigValue("Config.xml", "S_exposureTime"));

                //小银膜
                N_membraneDetectionModel_L.MembraneThreshold = Convert.ToInt32(GetConfigValue("Config.xml", "L_membraneLocation_TN"));
                N_membraneDetectionModel_L.FootThreshold = Convert.ToInt32(GetConfigValue("Config.xml", "L_footThreshold_TN"));
                N_membraneDetectionModel_L.FlowerThrehold = Convert.ToInt32(GetConfigValue("Config.xml", "L_flower_TN"));
                N_membraneDetectionModel_L.L2Threshold = Convert.ToInt32(GetConfigValue("Config.xml", "L_L2_TN"));
                N_membraneDetectionModel_L.MembraneDownLineThrehold = Convert.ToInt32(GetConfigValue("Config.xml", "L_membraneDown_TN"));
                N_membraneDetectionModel_L.FlowerCenterThrehold = Convert.ToInt32(GetConfigValue("Config.xml", "L_flowerCenter_TN"));
                N_membraneDetectionModel_L.TongueThreshold = Convert.ToInt32(GetConfigValue("Config.xml", "L_tongue_TN"));
                N_membraneDetectionModel_L.SplitThreshold = Convert.ToInt32(GetConfigValue("Config.xml", "L_split_TN"));
                N_membraneDetectionModel_L.LocationScore = Convert.ToDouble(GetConfigValue("Config.xml", "L_locationScore_TN"));
                N_membraneDetectionModel_L.FlowerLocationScore = Convert.ToDouble(GetConfigValue("Config.xml", "L_flowerScore_TN"));
                N_membraneDetectionModel_L.Gain = Convert.ToInt32(GetConfigValue("Config.xml", "L_gain"));
                N_membraneDetectionModel_L.ExposureTime = Convert.ToInt32(GetConfigValue("Config.xml", "L_exposureTime"));
                #endregion

                #region 正极参数读取
                //正极相机参数
                P_cameraAndCalibrationModel.ImageHeight = Convert.ToDouble(GetConfigValue("Config.xml", "ImageHeight_P"));
                P_cameraAndCalibrationModel.ImageWidth = Convert.ToDouble(GetConfigValue("Config.xml", "ImageWidth_P"));
                P_cameraAndCalibrationModel.OffsetX = Convert.ToDouble(GetConfigValue("Config.xml", "OffsetX_P"));
                P_cameraAndCalibrationModel.OffsetY = Convert.ToDouble(GetConfigValue("Config.xml", "OffsetY_P"));
                P_cameraAndCalibrationModel.FPS = Convert.ToDouble(GetConfigValue("Config.xml", "FPS_P"));
                P_cameraAndCalibrationModel.Angel = Convert.ToDouble(GetConfigValue("Config.xml", "Angel_P"));
                P_cameraAndCalibrationModel.Light1 = Convert.ToDouble(GetConfigValue("Config.xml", "Light1_P"));
                P_cameraAndCalibrationModel.Light2 = Convert.ToDouble(GetConfigValue("Config.xml", "Light2_P"));
                P_cameraAndCalibrationModel.LightTime = Convert.ToDouble(GetConfigValue("Config.xml", "LightTime_P"));
                P_cameraAndCalibrationModel.LightTimeAdvance = Convert.ToDouble(GetConfigValue("Config.xml", "LightTimeAdvance_P"));
                //正极标定参数
                P_cameraAndCalibrationModel.CalibrationX = Convert.ToDouble(GetConfigValue("Config.xml", "CalibrationX_P"));
                P_cameraAndCalibrationModel.CalibrationY = Convert.ToDouble(GetConfigValue("Config.xml", "CalibrationY_P"));
                P_cameraAndCalibrationModel.AdjustX = Convert.ToDouble(GetConfigValue("Config.xml", "AdjustX_P"));
                P_cameraAndCalibrationModel.AdjustY = Convert.ToDouble(GetConfigValue("Config.xml", "AdjustY_P"));
                if (s_true == GetConfigValue("Config.xml", "CalibrationMod_P"))
                    P_cameraAndCalibrationModel.CalibrationMod = true;
                else
                    P_cameraAndCalibrationModel.CalibrationMod = false;
                //正极检测设定
                P_membraneDetectionModel.MembraneThreshold = Convert.ToInt32(GetConfigValue("Config.xml", "membraneLocation_TP"));
                P_membraneDetectionModel.FootThreshold  = Convert.ToInt32(GetConfigValue("Config.xml", "footThreshold_TP"));
                P_membraneDetectionModel.FlowerThrehold  = Convert.ToInt32(GetConfigValue("Config.xml", "flower_TP"));
                P_membraneDetectionModel.L2Threshold  = Convert.ToInt32(GetConfigValue("Config.xml", "L2_TP"));
                P_membraneDetectionModel.MembraneDownLineThrehold  = Convert.ToInt32(GetConfigValue("Config.xml", "membraneDown_TP"));
                P_membraneDetectionModel.FlowerCenterThrehold  = Convert.ToInt32(GetConfigValue("Config.xml", "flowerCenter_TP"));
                P_membraneDetectionModel.TongueThreshold  = Convert.ToInt32(GetConfigValue("Config.xml", "tongue_TP"));
                P_membraneDetectionModel.SplitThreshold  = Convert.ToInt32(GetConfigValue("Config.xml", "split_TP"));
                P_membraneDetectionModel.LocationScore = Convert.ToInt32(GetConfigValue("Config.xml", "locationScore_TP"));
                P_membraneDetectionModel.FlowerLocationScore = Convert.ToInt32(GetConfigValue("Config.xml", "flowerScore_TP"));
                P_membraneDetectionModel.Gain = Convert.ToInt32(GetConfigValue("Config.xml", "gain_P"));
                P_membraneDetectionModel.ExposureTime = Convert.ToInt32(GetConfigValue("Config.xml", "exposureTime_P"));
                #endregion

                #region 芯包成品参数读取
                //芯包相机参数
                F_cameraAndCalibrationModel.ImageHeight = Convert.ToDouble(GetConfigValue("Config.xml", "ImageHeight_F"));
                F_cameraAndCalibrationModel.ImageWidth = Convert.ToDouble(GetConfigValue("Config.xml", "ImageWidth_F"));
                F_cameraAndCalibrationModel.OffsetX = Convert.ToDouble(GetConfigValue("Config.xml", "OffsetX_F"));
                F_cameraAndCalibrationModel.OffsetY = Convert.ToDouble(GetConfigValue("Config.xml", "OffsetY_F"));
                F_cameraAndCalibrationModel.FPS = Convert.ToDouble(GetConfigValue("Config.xml", "FPS_F"));
                F_cameraAndCalibrationModel.Angel = Convert.ToDouble(GetConfigValue("Config.xml", "Angel_F"));
                F_cameraAndCalibrationModel.Light1 = Convert.ToDouble(GetConfigValue("Config.xml", "Light1_F"));
                F_cameraAndCalibrationModel.Light2 = Convert.ToDouble(GetConfigValue("Config.xml", "Light2_F"));
                F_cameraAndCalibrationModel.LightTime = Convert.ToDouble(GetConfigValue("Config.xml", "LightTime_F"));
                F_cameraAndCalibrationModel.LightTimeAdvance = Convert.ToDouble(GetConfigValue("Config.xml", "LightTimeAdvance_F"));
                //芯包标定参数
                F_cameraAndCalibrationModel.CalibrationX = Convert.ToDouble(GetConfigValue("Config.xml", "CalibrationX_F"));
                F_cameraAndCalibrationModel.CalibrationY = Convert.ToDouble(GetConfigValue("Config.xml", "CalibrationY_F"));
                F_cameraAndCalibrationModel.AdjustX = Convert.ToDouble(GetConfigValue("Config.xml", "AdjustX_F"));
                F_cameraAndCalibrationModel.AdjustY = Convert.ToDouble(GetConfigValue("Config.xml", "AdjustY_F"));
                if (s_true == GetConfigValue("Config.xml", "CalibrationMod_F"))
                    F_cameraAndCalibrationModel.CalibrationMod = true;
                else
                    F_cameraAndCalibrationModel.CalibrationMod = false;
                //芯包检测设定
                F_FinishDetectionModel.CellThreshold = Convert.ToInt32(GetConfigValue("Config.xml", "CellThreshold"));
                F_FinishDetectionModel.PinErosion1 = Convert.ToInt32(GetConfigValue("Config.xml", "PinErosion1"));
                F_FinishDetectionModel.PinErosion2 = Convert.ToInt32(GetConfigValue("Config.xml", "PinErosion2"));
                #endregion

                #region 相机序列号读取
                mainModel.NseriesStr = Convert.ToString(GetConfigValue("Config.xml","NseriesStr"));//读取负极相机序列号
                mainModel.PseriesStr = Convert.ToString(GetConfigValue("Config.xml", "PseriesStr"));//读取正极相机序列号
                mainModel.FseriesStr = Convert.ToString(GetConfigValue("Config.xml", "FseriesStr"));//读取芯包成品相机序列号
                #endregion

                #region ROI
                if(PositiveDetection.FlowerRoi==null)
                    PositiveDetection.FlowerRoi = new double[5];
                PositiveDetection.FlowerRoi[0] = Convert.ToDouble(GetConfigValue(FM_name, "PositiveFlowerRoiRow"));
                PositiveDetection.FlowerRoi[1] = Convert.ToDouble(GetConfigValue(FM_name, "PositiveFlowerRoiColumn"));
                PositiveDetection.FlowerRoi[2] = Convert.ToDouble(GetConfigValue(FM_name, "PositiveFlowerRoiPhi"));
                PositiveDetection.FlowerRoi[3] = Convert.ToDouble(GetConfigValue(FM_name, "PositiveFlowerRoiLength1"));
                PositiveDetection.FlowerRoi[4] = Convert.ToDouble(GetConfigValue(FM_name, "PositiveFlowerRoiLength2"));

                if (PositiveDetection.TongueRoi == null)
                    PositiveDetection.TongueRoi = new double[5];
                PositiveDetection.TongueRoi[0] = Convert.ToDouble(GetConfigValue(FM_name, "PositiveTongueRoiRow"));
                PositiveDetection.TongueRoi[1] = Convert.ToDouble(GetConfigValue(FM_name, "PositiveTongueRoiColumn"));
                PositiveDetection.TongueRoi[2] = Convert.ToDouble(GetConfigValue(FM_name, "PositiveTongueRoiPhi"));
                PositiveDetection.TongueRoi[3] = Convert.ToDouble(GetConfigValue(FM_name, "PositiveTongueRoiLength1"));
                PositiveDetection.TongueRoi[4] = Convert.ToDouble(GetConfigValue(FM_name, "PositiveTongueRoiLength2"));

                if (PositiveDetection.PictureRoi == null)
                    PositiveDetection.PictureRoi = new double[5];
                PositiveDetection.PictureRoi[0] = Convert.ToDouble(GetConfigValue(FM_name, "PositivePictureRoiRow"));
                PositiveDetection.PictureRoi[1] = Convert.ToDouble(GetConfigValue(FM_name, "PositivePictureRoiColumn"));
                PositiveDetection.PictureRoi[2] = Convert.ToDouble(GetConfigValue(FM_name, "PositivePictureRoiPhi"));
                PositiveDetection.PictureRoi[3] = Convert.ToDouble(GetConfigValue(FM_name, "PositivePictureRoiLength1"));
                PositiveDetection.PictureRoi[4] = Convert.ToDouble(GetConfigValue(FM_name, "PositivePictureRoiLength2"));

                if (PositiveDetection.PinRoi == null)
                    PositiveDetection.PinRoi = new double[5];
                PositiveDetection.PinRoi[0] = Convert.ToDouble(GetConfigValue(FM_name, "PositivePinRoiRow"));
                PositiveDetection.PinRoi[1] = Convert.ToDouble(GetConfigValue(FM_name, "PositivePinRoiColumn"));
                PositiveDetection.PinRoi[2] = Convert.ToDouble(GetConfigValue(FM_name, "PositivePinRoiPhi"));
                PositiveDetection.PinRoi[3] = Convert.ToDouble(GetConfigValue(FM_name, "PositivePinRoiLength1"));
                PositiveDetection.PinRoi[4] = Convert.ToDouble(GetConfigValue(FM_name, "PositivePinRoiLength2"));

                if (PositiveDetection.LineRoi == null)
                    PositiveDetection.LineRoi = new double[5];
                PositiveDetection.LineRoi[0] = Convert.ToDouble(GetConfigValue(FM_name, "PositiveLineRoiRow"));
                PositiveDetection.LineRoi[1] = Convert.ToDouble(GetConfigValue(FM_name, "PositiveLineRoiColumn"));
                PositiveDetection.LineRoi[2] = Convert.ToDouble(GetConfigValue(FM_name, "PositiveLineRoiPhi"));
                PositiveDetection.LineRoi[3] = Convert.ToDouble(GetConfigValue(FM_name, "PositiveLineRoiLength1"));
                PositiveDetection.LineRoi[4] = Convert.ToDouble(GetConfigValue(FM_name, "PositiveLineRoiLength2"));

                if (NegativeDetection.FlowerRoi == null)
                    NegativeDetection.FlowerRoi = new double[5];
                NegativeDetection.FlowerRoi[0] = Convert.ToDouble(GetConfigValue(FM_name, "NegativeFlowerRoiRow"));
                NegativeDetection.FlowerRoi[1] = Convert.ToDouble(GetConfigValue(FM_name, "NegativeFlowerRoiColumn"));
                NegativeDetection.FlowerRoi[2] = Convert.ToDouble(GetConfigValue(FM_name, "NegativeFlowerRoiPhi"));
                NegativeDetection.FlowerRoi[3] = Convert.ToDouble(GetConfigValue(FM_name, "NegativeFlowerRoiLength1"));
                NegativeDetection.FlowerRoi[4] = Convert.ToDouble(GetConfigValue(FM_name, "NegativeFlowerRoiLength2"));

                if (NegativeDetection.TongueRoi == null)
                    NegativeDetection.TongueRoi = new double[5];
                NegativeDetection.TongueRoi[0] = Convert.ToDouble(GetConfigValue(FM_name, "NegativeTongueRoiRow"));
                NegativeDetection.TongueRoi[1] = Convert.ToDouble(GetConfigValue(FM_name, "NegativeTongueRoiColumn"));
                NegativeDetection.TongueRoi[2] = Convert.ToDouble(GetConfigValue(FM_name, "NegativeTongueRoiPhi"));
                NegativeDetection.TongueRoi[3] = Convert.ToDouble(GetConfigValue(FM_name, "NegativeTongueRoiLength1"));
                NegativeDetection.TongueRoi[4] = Convert.ToDouble(GetConfigValue(FM_name, "NegativeTongueRoiLength2"));

                if (NegativeDetection.PictureRoi == null)
                    NegativeDetection.PictureRoi = new double[5];
                NegativeDetection.PictureRoi[0] = Convert.ToDouble(GetConfigValue(FM_name, "NegativePictureRoiRow"));
                NegativeDetection.PictureRoi[1] = Convert.ToDouble(GetConfigValue(FM_name, "NegativePictureRoiColumn"));
                NegativeDetection.PictureRoi[2] = Convert.ToDouble(GetConfigValue(FM_name, "NegativePictureRoiPhi"));
                NegativeDetection.PictureRoi[3] = Convert.ToDouble(GetConfigValue(FM_name, "NegativePictureRoiLength1"));
                NegativeDetection.PictureRoi[4] = Convert.ToDouble(GetConfigValue(FM_name, "NegativePictureRoiLength2"));

                if (NegativeDetection.PinRoi == null)
                    NegativeDetection.PinRoi = new double[5];
                NegativeDetection.PinRoi[0] = Convert.ToDouble(GetConfigValue(FM_name, "NegativePinRoiRow"));
                NegativeDetection.PinRoi[1] = Convert.ToDouble(GetConfigValue(FM_name, "NegativePinRoiColumn"));
                NegativeDetection.PinRoi[2] = Convert.ToDouble(GetConfigValue(FM_name, "NegativePinRoiPhi"));
                NegativeDetection.PinRoi[3] = Convert.ToDouble(GetConfigValue(FM_name, "NegativePinRoiLength1"));
                NegativeDetection.PinRoi[4] = Convert.ToDouble(GetConfigValue(FM_name, "NegativePinRoiLength2"));

                if (NegativeDetection.LineRoi == null)
                    NegativeDetection.LineRoi = new double[5];
                NegativeDetection.LineRoi[0] = Convert.ToDouble(GetConfigValue(FM_name, "NegativeLineRoiRow"));
                NegativeDetection.LineRoi[1] = Convert.ToDouble(GetConfigValue(FM_name, "NegativeLineRoiColumn"));
                NegativeDetection.LineRoi[2] = Convert.ToDouble(GetConfigValue(FM_name, "NegativeLineRoiPhi"));
                NegativeDetection.LineRoi[3] = Convert.ToDouble(GetConfigValue(FM_name, "NegativeLineRoiLength1"));
                NegativeDetection.LineRoi[4] = Convert.ToDouble(GetConfigValue(FM_name, "NegativeLineRoiLength2"));

                if (FinishDetection.PictureRoi == null)
                    FinishDetection.PictureRoi = new double[5];
                FinishDetection.PictureRoi[0] = Convert.ToDouble(GetConfigValue(FM_name, "FinishPictureRoiRow"));
                FinishDetection.PictureRoi[1] = Convert.ToDouble(GetConfigValue(FM_name, "FinishPictureRoiColumn"));
                FinishDetection.PictureRoi[2] = Convert.ToDouble(GetConfigValue(FM_name, "FinishPictureRoiPhi"));
                FinishDetection.PictureRoi[3] = Convert.ToDouble(GetConfigValue(FM_name, "FinishPictureRoiLength1"));
                FinishDetection.PictureRoi[4] = Convert.ToDouble(GetConfigValue(FM_name, "FinishPictureRoiLength2"));

                if (FinishDetection.PinRoi == null)
                    FinishDetection.PinRoi = new double[5];
                FinishDetection.PinRoi[0] = Convert.ToDouble(GetConfigValue(FM_name, "FinishPinRoiRow"));
                FinishDetection.PinRoi[1] = Convert.ToDouble(GetConfigValue(FM_name, "FinishPinRoiColumn"));
                FinishDetection.PinRoi[2] = Convert.ToDouble(GetConfigValue(FM_name, "FinishPinRoiPhi"));
                FinishDetection.PinRoi[3] = Convert.ToDouble(GetConfigValue(FM_name, "FinishPinRoiLength1"));
                FinishDetection.PinRoi[4] = Convert.ToDouble(GetConfigValue(FM_name, "FinishPinRoiLength2"));

                #endregion
                helper = null;
                //SetBackendValue();
            }

            catch (Exception)
            {

                return false;
            }
            return true;
        }

        /// <summary>
        /// 关闭程序时保存设定到config文件
        /// </summary>
        public static void WriteConfig()
        {
            string s_true = null;
            #region 写入配方参数并保存
            if (FM_name==null) FM_name = "Formula.xml";
            SetConfigValue("Config.xml", "FormulaName", $"{FM_name}");//写入当前使用配方名
            //负极配方参数
            SetConfigValue(FM_name, "FormulaType",$"{FM.FormulaType}");
            SetConfigValue(FM_name, "FlowerCount", $"{FM.FlowerCount}");
            SetConfigValue(FM_name, "Warming", $"{FM.Warming}");
            SetConfigValue(FM_name, "AreaMaxN", $"{FM.AreaMaxN}");
            SetConfigValue(FM_name, "AreaMinN", $"{FM.AreaMinN}");
            SetConfigValue(FM_name, "FoilCrackMaxN", $"{FM.FoilCrackMaxN}");
            SetConfigValue(FM_name, "FoilCrackMinN", $"{FM.FoilCrackMinN}");
            SetConfigValue(FM_name, "AngleMaxN", $"{FM.AngleMaxN}");
            SetConfigValue(FM_name, "AngleMinN", $"{FM.AngleMinN}");
            SetConfigValue(FM_name, "L2MaxN", $"{FM.L2MaxN}");
            SetConfigValue(FM_name, "L2MinN", $"{FM.L2MinN}");
            SetConfigValue(FM_name, "RivetOffsrtMaxN", $"{FM.RivetOffsrtMaxN}");
            SetConfigValue(FM_name, "RivetOffsrtMinN", $"{FM.RivetOffsrtMinN}");
            SetConfigValue(FM_name, "ColorN", $"{FM.ColorN}");
            //正极配方参数 FM_name
            SetConfigValue(FM_name, "AreaMaxP", $"{FM.AreaMaxP}");
            SetConfigValue(FM_name, "AreaMinP", $"{FM.AreaMinP}");
            SetConfigValue(FM_name, "FoilCrackMaxP", $"{FM.FoilCrackMaxP}");
            SetConfigValue(FM_name, "FoilCrackMinP", $"{FM.FoilCrackMinP}");
            SetConfigValue(FM_name, "AngleMaxP", $"{FM.AngleMaxP}");
            SetConfigValue(FM_name, "AngleMinP", $"{FM.AngleMinP}");
            SetConfigValue(FM_name, "L2MaxP", $"{FM.L2MaxP}");
            SetConfigValue(FM_name, "L2MinP", $"{FM.L2MinP}");
            SetConfigValue(FM_name, "RivetOffsrtMaxP", $"{FM.RivetOffsrtMaxP}");
            SetConfigValue(FM_name, "RivetOffsrtMinP", $"{FM.RivetOffsrtMinP}");
            //芯包配方参数 FM_name
            SetConfigValue(FM_name, "HeightMax", $"{FM.HeightMax}");
            SetConfigValue(FM_name, "HeightMin", $"{FM.HeightMin}");
            SetConfigValue(FM_name, "ODMax", $"{FM.ODMax}");
            SetConfigValue(FM_name, "ODMin", $"{FM.ODMin}");
            SetConfigValue(FM_name, "PinPitchMax", $"{FM.PinPitchMax}");
            SetConfigValue(FM_name, "PinPitchMin", $"{FM.PinPitchMin}");
            SetConfigValue(FM_name, "DifferenceOfHeightMax", $"{FM.DifferenceOfHeightMax}");
            SetConfigValue(FM_name, "DifferenceOfHeightMin", $"{FM.DifferenceOfHeightMin}");
            SetConfigValue(FM_name, "CPlineUpMax", $"{FM.CPlineUpMax}");
            SetConfigValue(FM_name, "CPlineUpMin", $"{FM.CPlineUpMin}");
            SetConfigValue(FM_name, "CPlineBottomMax", $"{FM.CPlineBottomMax}");
            SetConfigValue(FM_name, "CPlineBottomMin", $"{FM.CPlineBottomMin}");
            #endregion

            #region 负极检测设定保存
            //负极相机参数
            SetConfigValue("Config.xml", "ImageHeight_N", $"{N_cameraAndCalibrationModel.ImageHeight}");
            SetConfigValue("Config.xml", "ImageWidth_N", $"{N_cameraAndCalibrationModel.ImageWidth}");
            SetConfigValue("Config.xml", "OffsetX_N", $"{N_cameraAndCalibrationModel.OffsetX}");
            SetConfigValue("Config.xml", "OffsetY_N", $"{N_cameraAndCalibrationModel.OffsetY}");
            SetConfigValue("Config.xml", "FPS_N", $"{N_cameraAndCalibrationModel.FPS}");
            SetConfigValue("Config.xml", "Angel_N", $"{N_cameraAndCalibrationModel.Angel}");
            SetConfigValue("Config.xml", "Light1_N", $"{N_cameraAndCalibrationModel.Light1}");
            SetConfigValue("Config.xml", "Light2_N", $"{N_cameraAndCalibrationModel.Light2}");
            SetConfigValue("Config.xml", "LightTime_N", $"{N_cameraAndCalibrationModel.LightTime}");
            SetConfigValue("Config.xml", "LightTimeAdvance_N", $"{N_cameraAndCalibrationModel.LightTimeAdvance}");
            //负极标定参数
            SetConfigValue("Config.xml", "CalibrationX_N", $"{N_cameraAndCalibrationModel.CalibrationX}");
            SetConfigValue("Config.xml", "CalibrationY_N", $"{N_cameraAndCalibrationModel.CalibrationY}");
            SetConfigValue("Config.xml", "AdjustX_N", $"{N_cameraAndCalibrationModel.AdjustX}");
            SetConfigValue("Config.xml", "AdjustY_N", $"{N_cameraAndCalibrationModel.AdjustY}");
            try
            {
                SetConfigValue("Config.xml", "CalibrationMod_N", $"{N_cameraAndCalibrationModel.CalibrationMod}");
            }
            catch (Exception)
            {

                throw;
            }
            //黑膜负极检测参数
            SetConfigValue("Config.xml", "B_membraneLocation_TN", $"{N_membraneDetectionModel_B.MembraneThreshold}");
            SetConfigValue("Config.xml", "B_footThreshold_TN", $"{N_membraneDetectionModel_B.FootThreshold}");
            SetConfigValue("Config.xml", "B_flower_TN", $"{N_membraneDetectionModel_B.FlowerThrehold}");
            SetConfigValue("Config.xml", "B_L2_TN", $"{N_membraneDetectionModel_B.L2Threshold}");
            SetConfigValue("Config.xml", "B_membraneDown_TN", $"{N_membraneDetectionModel_B.MembraneDownLineThrehold}");
            SetConfigValue("Config.xml", "B_flowerCenter_TN", $"{N_membraneDetectionModel_B.FlowerCenterThrehold}");
            SetConfigValue("Config.xml", "B_tongue_TN", $"{N_membraneDetectionModel_B.TongueThreshold}");
            SetConfigValue("Config.xml", "B_split_TN", $"{N_membraneDetectionModel_B.SplitThreshold}");
            SetConfigValue("Config.xml", "B_locationScore_TN", $"{N_membraneDetectionModel_B.LocationScore}");
            SetConfigValue("Config.xml", "B_flowerScore_TN", $"{N_membraneDetectionModel_B.FlowerLocationScore}");
            SetConfigValue("Config.xml", "B_gain", $"{N_membraneDetectionModel_B.Gain}");
            SetConfigValue("Config.xml", "B_exposureTime", $"{N_membraneDetectionModel_B.ExposureTime}");
            //银膜负极检测参数
            SetConfigValue("Config.xml", "S_membraneLocation_TN", $"{N_membraneDetectionModel_S.MembraneThreshold}");
            SetConfigValue("Config.xml", "S_footThreshold_TN", $"{N_membraneDetectionModel_S.FootThreshold}");
            SetConfigValue("Config.xml", "S_flower_TN", $"{N_membraneDetectionModel_S.FlowerThrehold}");
            SetConfigValue("Config.xml", "S_L2_TN", $"{N_membraneDetectionModel_S.L2Threshold}");
            SetConfigValue("Config.xml", "S_membraneDown_TN", $"{N_membraneDetectionModel_S.MembraneDownLineThrehold}");
            SetConfigValue("Config.xml", "S_flowerCenter_TN", $"{N_membraneDetectionModel_S.FlowerCenterThrehold}");
            SetConfigValue("Config.xml", "S_tongue_TN", $"{N_membraneDetectionModel_S.TongueThreshold}");
            SetConfigValue("Config.xml", "S_split_TN", $"{N_membraneDetectionModel_S.SplitThreshold}");
            SetConfigValue("Config.xml", "S_locationScore_TN", $"{N_membraneDetectionModel_S.LocationScore}");
            SetConfigValue("Config.xml", "S_flowerScore_TN", $"{N_membraneDetectionModel_S.FlowerLocationScore}");
            SetConfigValue("Config.xml", "S_gain", $"{N_membraneDetectionModel_S.Gain}");
            SetConfigValue("Config.xml", "S_exposureTime", $"{N_membraneDetectionModel_S.ExposureTime}");
            //小银膜负极检测参数
            SetConfigValue("Config.xml", "L_membraneLocation_TN", $"{N_membraneDetectionModel_L.MembraneThreshold}");
            SetConfigValue("Config.xml", "L_footThreshold_TN", $"{N_membraneDetectionModel_L.FootThreshold}");
            SetConfigValue("Config.xml", "L_flower_TN", $"{N_membraneDetectionModel_L.FlowerThrehold}");
            SetConfigValue("Config.xml", "L_L2_TN", $"{N_membraneDetectionModel_L.L2Threshold}");
            SetConfigValue("Config.xml", "L_membraneDown_TN", $"{N_membraneDetectionModel_L.MembraneDownLineThrehold}");
            SetConfigValue("Config.xml", "L_flowerCenter_TN", $"{N_membraneDetectionModel_L.FlowerCenterThrehold}");
            SetConfigValue("Config.xml", "L_tongue_TN", $"{N_membraneDetectionModel_L.TongueThreshold}");
            SetConfigValue("Config.xml", "L_split_TN", $"{N_membraneDetectionModel_L.SplitThreshold}");
            SetConfigValue("Config.xml", "L_locationScore_TN", $"{N_membraneDetectionModel_L.LocationScore}");
            SetConfigValue("Config.xml", "L_flowerScore_TN", $"{N_membraneDetectionModel_L.FlowerLocationScore}");
            SetConfigValue("Config.xml", "L_gain", $"{N_membraneDetectionModel_L.Gain}");
            SetConfigValue("Config.xml", "L_exposureTime", $"{N_membraneDetectionModel_L.ExposureTime}");
            #endregion

            #region 正极检测设定参数保存
            //正极相机参数
            SetConfigValue("Config.xml", "ImageHeight_P", $"{P_cameraAndCalibrationModel.ImageHeight}");
            SetConfigValue("Config.xml", "ImageWidth_P", $"{P_cameraAndCalibrationModel.ImageWidth}");
            SetConfigValue("Config.xml", "OffsetX_P", $"{P_cameraAndCalibrationModel.OffsetX}");
            SetConfigValue("Config.xml", "OffsetY_P", $"{P_cameraAndCalibrationModel.OffsetY}");
            SetConfigValue("Config.xml", "FPS_P", $"{P_cameraAndCalibrationModel.FPS}");
            SetConfigValue("Config.xml", "Angel_P", $"{P_cameraAndCalibrationModel.Angel}");
            SetConfigValue("Config.xml", "Light1_P", $"{P_cameraAndCalibrationModel.Light1}");
            SetConfigValue("Config.xml", "Light2_P", $"{P_cameraAndCalibrationModel.Light2}");
            SetConfigValue("Config.xml", "LightTime_P", $"{P_cameraAndCalibrationModel.LightTime}");
            SetConfigValue("Config.xml", "LightTimeAdvance_P", $"{P_cameraAndCalibrationModel.LightTimeAdvance}");
            //正极标定参数
            SetConfigValue("Config.xml", "CalibrationX_P", $"{P_cameraAndCalibrationModel.CalibrationX}");
            SetConfigValue("Config.xml", "CalibrationY_P", $"{P_cameraAndCalibrationModel.CalibrationY}");
            SetConfigValue("Config.xml", "AdjustX_P", $"{P_cameraAndCalibrationModel.AdjustX}");
            SetConfigValue("Config.xml", "AdjustY_P", $"{P_cameraAndCalibrationModel.AdjustY}");
            //正极检测参数
            SetConfigValue("Config.xml", "membraneLocation_TP", $"{P_membraneDetectionModel.MembraneThreshold}");
            SetConfigValue("Config.xml", "footThreshold_TP", $"{P_membraneDetectionModel.FootThreshold}");
            SetConfigValue("Config.xml", "flower_TP", $"{P_membraneDetectionModel.FlowerThrehold}");
            SetConfigValue("Config.xml", "L2_TP", $"{P_membraneDetectionModel.L2Threshold}");
            SetConfigValue("Config.xml", "membraneDown_TP", $"{P_membraneDetectionModel.MembraneDownLineThrehold}");
            SetConfigValue("Config.xml", "flowerCenter_TP", $"{P_membraneDetectionModel.FlowerCenterThrehold}");
            SetConfigValue("Config.xml", "tongue_TP", $"{P_membraneDetectionModel.TongueThreshold}");
            SetConfigValue("Config.xml", "split_TP", $"{P_membraneDetectionModel.SplitThreshold}");
            SetConfigValue("Config.xml", "locationScore_TP", $"{P_membraneDetectionModel.LocationScore}");
            SetConfigValue("Config.xml", "flowerScore_TP", $"{P_membraneDetectionModel.FlowerLocationScore}");
            SetConfigValue("Config.xml", "gain_P", $"{P_membraneDetectionModel.Gain}");
            SetConfigValue("Config.xml", "exposureTime_P", $"{P_membraneDetectionModel.ExposureTime}");
            #endregion

            #region 芯包检测设定参数保存
            //芯包相机参数
            SetConfigValue("Config.xml", "ImageHeight_F", $"{F_cameraAndCalibrationModel.ImageHeight}");
            SetConfigValue("Config.xml", "ImageWidth_F", $"{F_cameraAndCalibrationModel.ImageWidth}");
            SetConfigValue("Config.xml", "OffsetX_F", $"{F_cameraAndCalibrationModel.OffsetX}");
            SetConfigValue("Config.xml", "OffsetY_F", $"{F_cameraAndCalibrationModel.OffsetY}");
            SetConfigValue("Config.xml", "FPS_F", $"{F_cameraAndCalibrationModel.FPS}");
            SetConfigValue("Config.xml", "Angel_F", $"{F_cameraAndCalibrationModel.Angel}");
            SetConfigValue("Config.xml", "Light1_F", $"{F_cameraAndCalibrationModel.Light1}");
            SetConfigValue("Config.xml", "Light2_F", $"{F_cameraAndCalibrationModel.Light2}");
            SetConfigValue("Config.xml", "LightTime_F", $"{F_cameraAndCalibrationModel.LightTime}");
            SetConfigValue("Config.xml", "LightTimeAdvance_F", $"{F_cameraAndCalibrationModel.LightTimeAdvance}");
            //芯包标定参数
            SetConfigValue("Config.xml", "CalibrationX_F", $"{F_cameraAndCalibrationModel.CalibrationX}");
            SetConfigValue("Config.xml", "CalibrationY_F", $"{F_cameraAndCalibrationModel.CalibrationY}");
            SetConfigValue("Config.xml", "AdjustX_F", $"{F_cameraAndCalibrationModel.AdjustX}");
            SetConfigValue("Config.xml", "AdjustY_F", $"{F_cameraAndCalibrationModel.AdjustY}");
            //芯包检测参数
            SetConfigValue("Config.xml", "CellThreshold", $"{F_FinishDetectionModel.CellThreshold}");
            SetConfigValue("Config.xml", "PinErosion1", $"{F_FinishDetectionModel.PinErosion1}");
            SetConfigValue("Config.xml", "PinErosion2", $"{F_FinishDetectionModel.PinErosion2}");
            
            #endregion

            #region 相机序列号
            SetConfigValue("Config.xml", "NseriesStr", mainModel.NseriesStr);//负极相机序列号
            SetConfigValue("Config.xml", "PseriesStr", mainModel.PseriesStr);//正极相机序列号
            SetConfigValue("Config.xml", "FseriesStr", mainModel.FseriesStr);//芯包成品相机序列号
            #endregion

            #region Roi
            SetConfigValue("Config.xml", "PositiveFlowerRoiRow", $"{PositiveDetection.FlowerRoi[0]}");
            SetConfigValue("Config.xml", "PositiveFlowerRoiColumn", $"{PositiveDetection.FlowerRoi[1]}");
            SetConfigValue("Config.xml", "PositiveFlowerRoiRow", $"{PositiveDetection.FlowerRoi[2]}");
            SetConfigValue("Config.xml", "PositiveFlowerRoiRow", $"{PositiveDetection.FlowerRoi[3]}");
            SetConfigValue("Config.xml", "PositiveFlowerRoiRow", $"{PositiveDetection.FlowerRoi[4]}");
            #endregion
            helper = null;

            ObjToXML(PositiveDetection);
            ObjToXML(NegativeDetection);
            ObjToXML(FinishDetection);
        }

        /// <summary>
        /// 将二值化、相机、标定等设定参数的config文件值读取到运行参数
        /// </summary>
        public static bool SetBackendValue()
        {
            return (SetFormulaValue() && SetDetectionSetting());
        }

        /// <summary>
        /// 将配方参数赋值给当前运行的后台上下限设定参数
        /// </summary>
        public static bool SetFormulaValue()
        {
            try
            {
                //正极
                PositiveTolerance.FlowerCountP = FM.FlowerCount;
                PositiveTolerance.AreaMaxP = FM.AreaMaxP;
                PositiveTolerance.AreaMinP = FM.AreaMinP;
                PositiveTolerance.FoilCrackMaxP = FM.FoilCrackMaxP;
                PositiveTolerance.FoilCrackMinP = FM.FoilCrackMinP;
                PositiveTolerance.AngleMaxP = FM.AngleMaxP;
                PositiveTolerance.AngleMinP = FM.AngleMinP;
                PositiveTolerance.L2MaxP = FM.L2MaxP;
                PositiveTolerance.L2MinP = FM.L2MinP;
                PositiveTolerance.RivetOffsetMaxP = FM.RivetOffsrtMaxP;
                PositiveTolerance.RivetOffsetMinP = FM.RivetOffsrtMinP;
                //负极
                NegativeTolerance.FlowerCountN = FM.FlowerCount;
                NegativeTolerance.AreaMaxN = FM.AreaMaxN;
                NegativeTolerance.AreaMinN = FM.AreaMinN;
                NegativeTolerance.FoilCrackMaxN = FM.FoilCrackMaxN;
                NegativeTolerance.FoilCrackMinN = FM.FoilCrackMinN;
                NegativeTolerance.AngleMaxN = FM.AngleMaxN;
                NegativeTolerance.AngleMinN = FM.AngleMinN;
                NegativeTolerance.L2MaxN = FM.L2MaxN;
                NegativeTolerance.L2MinN = FM.L2MinN;
                NegativeTolerance.RivetOffsetMaxN = FM.RivetOffsrtMaxN;
                NegativeTolerance.RivetOffsetMinN = FM.RivetOffsrtMinN;
                NegativeTolerance.ColorN = FM.ColorN;
                //芯包成品
                FinishTolerance.HeightMax = FM.HeightMax;
                FinishTolerance.HeightMin = FM.HeightMin;
                FinishTolerance.ODMax = FM.ODMax;
                FinishTolerance.ODMin = FM.ODMin;
                FinishTolerance.PinPitchMax = FM.PinPitchMax;
                FinishTolerance.PinPitchMin = FM.PinPitchMin;
                FinishTolerance.DifferenceOfHeightMax = FM.DifferenceOfHeightMax;
                FinishTolerance.DifferenceOfHeightMin = FM.DifferenceOfHeightMin;
                FinishTolerance.CPlineUpMax = FM.CPlineUpMax;
                FinishTolerance.CPlineUpMin = FM.CPlineUpMin;
                FinishTolerance.CPlineBottomMax = FM.CPlineBottomMax;
                FinishTolerance.CPlineBottomMin = FM.CPlineBottomMin;

            }
            catch (Exception)
            {

                return false;
            }
            return true;
            
        }

        /// <summary>
        /// 将图像检测设定参数赋值给后台运行的检测参数
        /// </summary>
        public static bool SetDetectionSetting()
        {
            try
            {
                #region 后台正极图像检测参数
                string s_true = $"true";
                //正极相机参数
                PositiveDetection.ImageHeight = P_cameraAndCalibrationModel.ImageHeight;
                PositiveDetection.ImageWidth = P_cameraAndCalibrationModel.ImageWidth;
                PositiveDetection.OffsetX = P_cameraAndCalibrationModel.OffsetX;
                PositiveDetection.OffsetY = P_cameraAndCalibrationModel.OffsetY;
                PositiveDetection.FPS = P_cameraAndCalibrationModel.FPS;
                //PositiveDetection.Angel = Convert.ToDouble(GetConfigValue("Config.xml", "Angel_P"));
                PositiveDetection.Light1 = P_cameraAndCalibrationModel.Light1;
                //PositiveDetection.Light2 = Convert.ToDouble(GetConfigValue("Config.xml", "Light2_P"));
                PositiveDetection.LightTime = P_cameraAndCalibrationModel.LightTime;
                //PositiveDetection.LightTimeAdvance = Convert.ToDouble(GetConfigValue("Config.xml", "LightTimeAdvance_P"));
                //正极标定参数
                PositiveDetection.CalibrationX = P_cameraAndCalibrationModel.CalibrationX;
                PositiveDetection.CalibrationY = P_cameraAndCalibrationModel.CalibrationY;
                PositiveDetection.AdjustX = P_cameraAndCalibrationModel.AdjustX;
                PositiveDetection.AdjustY = P_cameraAndCalibrationModel.AdjustY;
                if (s_true == GetConfigValue("Config.xml", "CalibrationMod_P"))
                    PositiveDetection.CalibrationMod = true;
                else
                    PositiveDetection.CalibrationMod = false;
                //正极检测设定
                PositiveDetection.MembraneThreshold = P_membraneDetectionModel.MembraneThreshold;
                PositiveDetection.FootThreshold = P_membraneDetectionModel.FootThreshold;
                PositiveDetection.FlowerThrehold = P_membraneDetectionModel.FlowerThrehold;
                PositiveDetection.L2Threshold = P_membraneDetectionModel.L2Threshold;
                PositiveDetection.MembraneDownLineThrehold = P_membraneDetectionModel.MembraneDownLineThrehold;
                PositiveDetection.FlowerCenterThrehold = P_membraneDetectionModel.FlowerCenterThrehold;
                PositiveDetection.TongueThreshold = P_membraneDetectionModel.TongueThreshold;
                PositiveDetection.SplitThreshold = P_membraneDetectionModel.SplitThreshold;
                PositiveDetection.LocationScore = P_membraneDetectionModel.LocationScore;
                PositiveDetection.FlowerLocationScore = P_membraneDetectionModel.FlowerLocationScore;
                PositiveDetection.Gain = P_membraneDetectionModel.Gain;
                //PositiveDetection.ExposureTime = Convert.ToInt32(GetConfigValue("Config.xml", "exposureTime_P"));

                #endregion

                #region 后台负极图像检测参数
                //负极相机参数
                NegativeDetection.ImageHeight = N_cameraAndCalibrationModel.ImageHeight;
                NegativeDetection.ImageWidth = N_cameraAndCalibrationModel.ImageWidth;
                NegativeDetection.OffsetX = N_cameraAndCalibrationModel.OffsetX;
                NegativeDetection.OffsetY = N_cameraAndCalibrationModel.OffsetY;
                NegativeDetection.FPS = N_cameraAndCalibrationModel.FPS;
                NegativeDetection.Angel = N_cameraAndCalibrationModel.Angel;
                NegativeDetection.Light1 = N_cameraAndCalibrationModel.Light1;
                NegativeDetection.Light2 = N_cameraAndCalibrationModel.Light2;
                NegativeDetection.LightTime = N_cameraAndCalibrationModel.LightTime;
                NegativeDetection.LightTimeAdvance = N_cameraAndCalibrationModel.LightTimeAdvance;
                //负极标定参数
                NegativeDetection.CalibrationX = N_cameraAndCalibrationModel.CalibrationX;
                NegativeDetection.CalibrationY = N_cameraAndCalibrationModel.CalibrationY;
                NegativeDetection.AdjustX = N_cameraAndCalibrationModel.AdjustX;
                NegativeDetection.AdjustY = N_cameraAndCalibrationModel.AdjustY;
                if (s_true == GetConfigValue("Config.xml", "CalibrationMod_N"))
                    NegativeDetection.CalibrationMod = true;
                else
                    NegativeDetection.CalibrationMod = false;

                //负极检测设置
                switch (NegativeTolerance.ColorN)
                {
                    case 1://小银
                        NegativeDetection.MembraneThreshold = N_membraneDetectionModel_L.MembraneThreshold;
                        NegativeDetection.FootThreshold = N_membraneDetectionModel_L.FootThreshold;
                        NegativeDetection.FlowerThrehold = N_membraneDetectionModel_L.FlowerThrehold;
                        NegativeDetection.L2Threshold = N_membraneDetectionModel_L.L2Threshold;
                        NegativeDetection.MembraneDownLineThrehold = N_membraneDetectionModel_L.MembraneDownLineThrehold;
                        NegativeDetection.FlowerCenterThrehold = N_membraneDetectionModel_L.FlowerCenterThrehold;
                        NegativeDetection.TongueThreshold = N_membraneDetectionModel_L.TongueThreshold;
                        NegativeDetection.SplitThreshold = N_membraneDetectionModel_L.SplitThreshold;
                        NegativeDetection.LocationScore = N_membraneDetectionModel_L.LocationScore;
                        NegativeDetection.FlowerLocationScore = N_membraneDetectionModel_L.FlowerLocationScore;
                        NegativeDetection.Gain = N_membraneDetectionModel_L.Gain;
                        NegativeDetection.ExposureTime = N_membraneDetectionModel_L.ExposureTime;
                        break;
                    case 2://亮银
                        NegativeDetection.MembraneThreshold = N_membraneDetectionModel_S.MembraneThreshold;
                        NegativeDetection.FootThreshold = N_membraneDetectionModel_S.FootThreshold;
                        NegativeDetection.FlowerThrehold = N_membraneDetectionModel_S.FlowerThrehold;
                        NegativeDetection.L2Threshold = N_membraneDetectionModel_S.L2Threshold;
                        NegativeDetection.MembraneDownLineThrehold = N_membraneDetectionModel_S.MembraneDownLineThrehold;
                        NegativeDetection.FlowerCenterThrehold = N_membraneDetectionModel_S.FlowerCenterThrehold;
                        NegativeDetection.TongueThreshold = N_membraneDetectionModel_S.TongueThreshold;
                        NegativeDetection.SplitThreshold = N_membraneDetectionModel_S.SplitThreshold;
                        NegativeDetection.LocationScore = N_membraneDetectionModel_S.LocationScore;
                        NegativeDetection.FlowerLocationScore = N_membraneDetectionModel_S.FlowerLocationScore;
                        NegativeDetection.Gain = N_membraneDetectionModel_S.Gain;
                        NegativeDetection.ExposureTime = N_membraneDetectionModel_S.ExposureTime;
                        break;
                    case 3://黑
                        NegativeDetection.MembraneThreshold = N_membraneDetectionModel_B.MembraneThreshold;
                        NegativeDetection.FootThreshold = N_membraneDetectionModel_B.FootThreshold;
                        NegativeDetection.FlowerThrehold = N_membraneDetectionModel_B.FlowerThrehold;
                        NegativeDetection.L2Threshold = N_membraneDetectionModel_B.L2Threshold;
                        NegativeDetection.MembraneDownLineThrehold = N_membraneDetectionModel_B.MembraneDownLineThrehold;
                        NegativeDetection.FlowerCenterThrehold = N_membraneDetectionModel_B.FlowerCenterThrehold;
                        NegativeDetection.TongueThreshold = N_membraneDetectionModel_B.TongueThreshold;
                        NegativeDetection.SplitThreshold = N_membraneDetectionModel_B.SplitThreshold;
                        NegativeDetection.LocationScore = N_membraneDetectionModel_B.LocationScore;
                        NegativeDetection.FlowerLocationScore = N_membraneDetectionModel_B.FlowerLocationScore;
                        NegativeDetection.Gain = N_membraneDetectionModel_B.Gain;
                        NegativeDetection.ExposureTime = N_membraneDetectionModel_B.ExposureTime;
                        break;

                    default: break;

                }
                #endregion

                #region 后台芯包成品图像检测参数
                //芯包相机参数
                FinishDetection.ImageHeight = F_cameraAndCalibrationModel.ImageHeight;
                FinishDetection.ImageWidth = F_cameraAndCalibrationModel.ImageWidth;
                FinishDetection.OffsetX = F_cameraAndCalibrationModel.OffsetX;
                FinishDetection.OffsetY = F_cameraAndCalibrationModel.OffsetY;
                FinishDetection.FPS = F_cameraAndCalibrationModel.FPS;
                FinishDetection.Angel = F_cameraAndCalibrationModel.Angel;
                FinishDetection.Light1 = F_cameraAndCalibrationModel.Light1;
                //FinishDetection.Light2 = F_cameraAndCalibrationModel.Light2;
                FinishDetection.LightTime = F_cameraAndCalibrationModel.LightTime;
                //FinishDetection.LightTimeAdvance = F_cameraAndCalibrationModel.LightTimeAdvance;
                //芯包标定参数
                FinishDetection.CalibrationX = F_cameraAndCalibrationModel.CalibrationX;
                FinishDetection.CalibrationY = F_cameraAndCalibrationModel.CalibrationY;
                FinishDetection.AdjustX = F_cameraAndCalibrationModel.AdjustX;
                FinishDetection.AdjustY = F_cameraAndCalibrationModel.AdjustY;
                if (s_true == GetConfigValue("Config.xml", "CalibrationMod_F"))
                    FinishDetection.CalibrationMod = true;
                else
                    FinishDetection.CalibrationMod = false;
                //芯包检测设定
                FinishDetection.CellThreshold = F_FinishDetectionModel.CellThreshold = Convert.ToInt32(GetConfigValue("Config.xml", "CellThreshold"));
                FinishDetection.PinErosion1 = F_FinishDetectionModel.PinErosion1 = Convert.ToInt32(GetConfigValue("Config.xml", "PinErosion1"));
                FinishDetection.PinErosion2 = F_FinishDetectionModel.PinErosion2 = Convert.ToInt32(GetConfigValue("Config.xml", "PinErosion2"));
                #endregion
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        #region XML文件读写
        /// <summary>
        /// 读取xml文件的值,此函数后面可优化为只输入节点名，在外部创建xmlhelper实例减少系统资源消耗
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <param name="Name">节点名</param>
        /// <returns></returns>
        public static string GetConfigValue(string FileName,string Name)
        {
            //XmlHelper helper = new XmlHelper("CENJE_Vison.xml");
            lock (syncXml)
            {
                //using(helper=new XmlHelper("")) { }
                string ConfigPath = $"{Environment.CurrentDirectory}\\Config";
                if (!Directory.Exists(ConfigPath))
                {
                    Directory.CreateDirectory(ConfigPath);
                }
                helper = new XmlHelper($"{ConfigPath}\\{FileName}");
                if (helper == null)
                {
                    helper = new XmlHelper($"{ConfigPath}\\{FileName}");
                }
                return helper.ReadXml(Name);
            }
            

        }

        /// <summary>
        /// 设定/修改xml文件的一个值
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        public static void SetConfigValue(string FileName,string Name, string Value)
        {
            lock (syncXml)
            {
                string ConfigPath = $"{Environment.CurrentDirectory}\\Config";
                if (!Directory.Exists(ConfigPath))
                {
                    Directory.CreateDirectory(ConfigPath);
                }

                if (helper == null || helper.XmlName != $"{ConfigPath}\\{FileName}")
                {
                    helper = new XmlHelper($"{ConfigPath}\\{FileName}");
                }
                else if (helper.XmlName != $"{ConfigPath}\\{FileName}")
                {
                    helper = null;
                    helper = new XmlHelper($"{ConfigPath}\\{FileName}");
                }

                helper.Modify(Name, Value);
            }
            
        }
        #endregion

        #region 文件清理
        /// <summary>
        /// 删除日期超过10日的文件夹
        /// </summary>
        /// <param name="strPath"></param>
        public void DeleteFiles(string strPath)
        {
            try
            {
                string path = strPath;
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] files = dir.GetFiles();
                foreach(FileInfo file in files)
                {
                    if (file.LastWriteTime < DateTime.Now.AddDays(-10))
                    {
                        file.Delete();
                    }
                }
            }
            catch 
            {

                
            }
        }

        /// <summary>
        /// 删除旧文件
        /// </summary>
        /// <param name="nDay"></param>
        private void DeleteFilesOld(int nDay)
        {
            DateTime dt = DateTime.Now;
            string strPath = null;
            try
            {
                strPath = $"D:\\CENJE-Vison\\Image\\Location1\\NG\\"+ dt.AddDays(-nDay).ToString("yy-MM-dd");
                DeleteFolder(strPath);
                strPath = $"D:\\CENJE-Vison\\Image\\Location2\\NG\\" + dt.AddDays(-nDay).ToString("yy-MM-dd");
                DeleteFolder(strPath);
                strPath = $"D:\\CENJE-Vison\\Image\\Location3\\NG\\" + dt.AddDays(-nDay).ToString("yy-MM-dd");
                DeleteFolder(strPath);
                strPath = $"D:\\CENJE-Vison\\Image\\Location1\\OK\\" + dt.AddDays(-nDay).ToString("yy-MM-dd");
                DeleteFolder(strPath);
                strPath = $"D:\\CENJE-Vison\\Image\\Location2\\OK\\" + dt.AddDays(-nDay).ToString("yy-MM-dd");
                DeleteFolder(strPath);
                strPath = $"D:\\CENJE-Vison\\Image\\Location3\\OK\\" + dt.AddDays(-nDay).ToString("yy-MM-dd");
                DeleteFolder(strPath);
                strPath = $"D:\\CENJE-Vison\\Image\\Location1\\All\\" + dt.AddDays(-nDay).ToString("yy-MM-dd");
                DeleteFolder(strPath);
                strPath = $"D:\\CENJE-Vison\\Image\\Location2\\All\\" + dt.AddDays(-nDay).ToString("yy-MM-dd");
                DeleteFolder(strPath);
                strPath = $"D:\\CENJE-Vison\\Image\\Location3\\All\\" + dt.AddDays(-nDay).ToString("yy-MM-dd");
                DeleteFolder(strPath);
                strPath = $"D:\\CENJE-Vison\\Image\\Location1\\Fault\\" + dt.AddDays(-nDay).ToString("yy-MM-dd");
                DeleteFolder(strPath);
                strPath = $"D:\\CENJE-Vison\\Image\\Location2\\Fault\\" + dt.AddDays(-nDay).ToString("yy-MM-dd");
                DeleteFolder(strPath);
                strPath = $"D:\\CENJE-Vison\\Image\\Location3\\Fault\\" + dt.AddDays(-nDay).ToString("yy-MM-dd");
                DeleteFolder(strPath);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="folderPath"></param>
        public static void DeleteFolder(string folderPath)
        {
            try
            {
                foreach(string dire in Directory.GetFileSystemEntries(folderPath))
                {
                    if (File.Exists(dire))
                    {
                        FileInfo fileInfo = new FileInfo(dire);
                        if (fileInfo.Attributes.ToString().IndexOf("ReadOnly") != -1)
                            fileInfo.Attributes = FileAttributes.Normal;
                        File.Delete(dire);//删除文件 
                    }
                    else
                    {
                        DirectoryInfo direInfo = new DirectoryInfo(dire);
                        if (direInfo.GetFiles().Length != 0)
                        {
                            DeleteFolder(direInfo.FullName); //删除子文件夹
                        }
                        Directory.Delete(dire);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #endregion
        public static void ObjToXML(object obj)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(obj.GetType());
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                string path = "C:\\Users\\W\\Desktop\\G\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileName = path + "Congfig.xml";

                TextWriter textWriter = new StreamWriter(fileName);
                xs.Serialize(textWriter, obj, ns);
                //xs.Serialize(Console.Out, obj);
                //Console.ReadLine();
                textWriter.Close();

            }
            catch (Exception)
            {

                Console.WriteLine("error");
            }

        }
    }
}