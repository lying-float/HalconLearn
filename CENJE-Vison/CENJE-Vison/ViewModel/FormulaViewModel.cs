using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using CENJE_Vison.Models;
using CENJE_Vison.Common;
using CENJE_Vison.Views;
using System.IO;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;

namespace CENJE_Vison.ViewModel
{
    public class FormulaViewModel:ViewModelBase
    {
        #region 字段
        List<string> FormulaList = null;
        #endregion

        #region 属性
        public RelayCommand Load_Command { get; set; }//页面加载处理命令
        public RelayCommand Closed_Command { get; set; }//关闭页面时的处理命令
        public RelayCommand Create_Command { get; set; }//创建新配方命令
        public RelayCommand Delete_Command { get; set; }//删除配方命令
        public RelayCommand Select_Comand { get; set; }//选择配方命令
        public RelayCommand<string> Save_Command { get; set; }//保存配方命令
        public static RelayCommand<string> ClickFormula_Command { get; set; }//点击左侧选项栏切换配方显示命令
        public Window wHandle { get; set; }
        private string ClickFormulaName = MainViewModel.FM_name;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public FormulaViewModel()
        {
            Load_Command = new RelayCommand(new Action(View_Loaded));
            Closed_Command = new RelayCommand(ClosedFormulaWindow);
            Create_Command = new RelayCommand(new Action(CreateFormula));
            Select_Comand = new RelayCommand(new Action(SelectFormula));
            ClickFormula_Command = new RelayCommand<string>(new Action<string>(ClickFormula));
            Save_Command = new RelayCommand<string>(new Action<string>(SaveFormula));
            Delete_Command = new RelayCommand(new Action(DeleteFormula));
        }
        #region 方法
        /// <summary>
        /// 
        /// </summary>
        private void ClosedFormulaWindow()
        {
            SimpleIoc.Default.Unregister<FormulaWindow>();
            SimpleIoc.Default.Register<FormulaWindow>();
        }
        /// <summary>
        /// 新建配方
        /// </summary>
        private void CreateFormula()
        {
            try
            {
                string fileName = MainViewModel.FM.FormulaType;
                MainViewModel.FM_name= MainViewModel.FM.FormulaType;
                //创建新xml文件存储新配方
                if (File.Exists($"{Environment.CurrentDirectory}\\Config\\" + fileName + ".xml"))
                {
                    MessageBox.Show("已存在同名配方！！");
                    return;
                }
                if (!fileName.All(char.IsDigit))
                {
                    WriteFormula(fileName + ".xml");
                    Messenger.Default.Send(new NotificationMessage(fileName), "AddFormulaView");
                    FormulaList.Add(fileName);
                }
                else MessageBox.Show("配方名不能为纯数字!");
                

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 删除配方
        /// </summary>
        private void DeleteFormula()
        {
            string path = $"{Environment.CurrentDirectory}\\Config";
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles();
            if (files.Length > 2)
            {
                for(int i = 0; i <= files.Length - 1; i++)
                {
                    string fileName = files[i].Name;
                    if (files[i].Name != "config.xml" && ClickFormulaName + ".xml" == fileName)
                    {
                        //string fileName = files[i].Name;
                        if (files.Length == 2)
                        {
                            MessageBox.Show("必须保留一个以上配方！！");
                        }

                        else
                        {

                            int a = i < files.Length - 1 ? (i + 1) : (i - 1);
                            fileName = files[a].Name;
                            GetFormula(fileName);
                            if (ClickFormulaName == MainViewModel.FM_name) SetBackendValue();
                            files[i].Delete();
                            Messenger.Default.Send(new NotificationMessage(files[i].Name), "DeleteFormulaView");
                            return;
                        }
                        //else
                        //{
                        //    int a = i < files.Length - 1 ? (i + 1) : (i - 1);
                        //    fileName = files[a].Name;
                        //    GetFormula(fileName);
                        //    files[i].Delete();
                        //    fileName = files[i].Name;
                        //    Messenger.Default.Send(new NotificationMessage(fileName), "DeleteFormulaView");
                        //    return;
                        //}
                        
                    }
                }
            }
        }

        /// <summary>
        /// 选择配方
        /// </summary>
        private void SelectFormula()
        {
            MainViewModel.SetFormulaValue();
            SetBackendValue();
        }

        /// <summary>
        /// 保存配方
        /// </summary>
        /// <param name="saveFile"></param>
        private void SaveFormula(string saveFile)
        {
            WriteFormula(saveFile + ".xml");
        }

        /// <summary>
        /// 切换配方显示
        /// </summary>
        /// <param name="formulaName"></param>
        private void ClickFormula(string formulaName)
        {
            //MessageBox.Show("添加新配方成功");
            //WriteFormula(formulaName);
            GetFormula(formulaName+".xml");
            ClickFormulaName = formulaName;
            //MessageBox.Show(formulaList.Index);
            
        }

        /// <summary>
        /// 页面加载处理
        /// </summary>
        public void View_Loaded()
        {
            FormulaList = GetFormulaList();
            AddFormulaToView(FormulaList);
        }


        #endregion

        //修改指定xml文件内容，若不存在则新建
        private void WriteFormula(string fileName)
        {
            MainViewModel.FM_name = fileName;
            //写入当前使用配方名 
            MainViewModel.SetConfigValue("Config.xml", "FormulaName", $"{fileName}");

            MainViewModel.SetConfigValue(fileName, "FormulaType", $"{MainViewModel.FM.FormulaType}");
            MainViewModel.SetConfigValue(fileName, "FlowerCount", $"{MainViewModel.FM.FlowerCount}");
            MainViewModel.SetConfigValue(fileName, "Warming", $"{MainViewModel.FM.Warming}");
            //负极配方参数
            MainViewModel.SetConfigValue(fileName, "AreaMaxN", $"{MainViewModel.FM.AreaMaxN}");
            MainViewModel.SetConfigValue(fileName, "AreaMinN", $"{MainViewModel.FM.AreaMinN}");
            MainViewModel.SetConfigValue(fileName, "FoilCrackMaxN", $"{MainViewModel.FM.FoilCrackMaxN}");
            MainViewModel.SetConfigValue(fileName, "FoilCrackMinN", $"{MainViewModel.FM.FoilCrackMinN}");
            MainViewModel.SetConfigValue(fileName, "AngleMaxN", $"{MainViewModel.FM.AngleMaxN}");
            MainViewModel.SetConfigValue(fileName, "AngleMinN", $"{MainViewModel.FM.AngleMinN}");
            MainViewModel.SetConfigValue(fileName, "L2MaxN", $"{MainViewModel.FM.L2MaxN}");
            MainViewModel.SetConfigValue(fileName, "L2MinN", $"{MainViewModel.FM.L2MinN}");
            MainViewModel.SetConfigValue(fileName, "RivetOffsrtMaxN", $"{MainViewModel.FM.RivetOffsrtMaxN}");
            MainViewModel.SetConfigValue(fileName, "RivetOffsrtMinN", $"{MainViewModel.FM.RivetOffsrtMinN}");
            MainViewModel.SetConfigValue(fileName, "ColorN", $"{MainViewModel.FM.ColorN}");
            //正极配方参数 
            MainViewModel.SetConfigValue(fileName, "AreaMaxP", $"{MainViewModel.FM.AreaMaxP}");
            MainViewModel.SetConfigValue(fileName, "AreaMinP", $"{MainViewModel.FM.AreaMinP}");
            MainViewModel.SetConfigValue(fileName, "FoilCrackMaxP", $"{MainViewModel.FM.FoilCrackMaxP}");
            MainViewModel.SetConfigValue(fileName, "FoilCrackMinP", $"{MainViewModel.FM.FoilCrackMinP}");
            MainViewModel.SetConfigValue(fileName, "AngleMaxP", $"{MainViewModel.FM.AngleMaxP}");
            MainViewModel.SetConfigValue(fileName, "AngleMinP", $"{MainViewModel.FM.AngleMinP}");
            MainViewModel.SetConfigValue(fileName, "L2MaxP", $"{MainViewModel.FM.L2MaxP}");
            MainViewModel.SetConfigValue(fileName, "L2MinP", $"{MainViewModel.FM.L2MinP}");
            MainViewModel.SetConfigValue(fileName, "RivetOffsrtMaxP", $"{MainViewModel.FM.RivetOffsrtMaxP}");
            MainViewModel.SetConfigValue(fileName, "RivetOffsrtMinP", $"{MainViewModel.FM.RivetOffsrtMinP}");
            //芯包配方参数 
            MainViewModel.SetConfigValue(fileName, "HeightMax", $"{MainViewModel.FM.HeightMax}");
            MainViewModel.SetConfigValue(fileName, "HeightMin", $"{MainViewModel.FM.HeightMin}");
            MainViewModel.SetConfigValue(fileName, "ODMax", $"{MainViewModel.FM.ODMax}");
            MainViewModel.SetConfigValue(fileName, "ODMin", $"{MainViewModel.FM.ODMin}");
            MainViewModel.SetConfigValue(fileName, "PinPitchMax", $"{MainViewModel.FM.PinPitchMax}");//脚距
            MainViewModel.SetConfigValue(fileName, "PinPitchMin", $"{MainViewModel.FM.PinPitchMin}");
            MainViewModel.SetConfigValue(fileName, "DifferenceOfHeightMax", $"{MainViewModel.FM.DifferenceOfHeightMax}");//高低脚
            MainViewModel.SetConfigValue(fileName, "DifferenceOfHeightMin", $"{MainViewModel.FM.DifferenceOfHeightMin}");
            MainViewModel.SetConfigValue(fileName, "CPlineUpMax", $"{MainViewModel.FM.CPlineUpMax}");
            MainViewModel.SetConfigValue(fileName, "CPlineUpMin", $"{MainViewModel.FM.CPlineUpMin}");
            MainViewModel.SetConfigValue(fileName, "CPlineBottomMax", $"{MainViewModel.FM.CPlineBottomMax}");
            MainViewModel.SetConfigValue(fileName, "CPlineBottomMin", $"{MainViewModel.FM.CPlineBottomMin}");

            //MainViewModel.helper = null;
        }

        //读取xml文件内容
        private void GetFormula(string fileName)
        {
            //string s_true = $"true";
            #region 配方参数读取
            //读取配方参数
            if (fileName.Equals("")) fileName = "Formula.xml";
            MainViewModel.FM_name = fileName;
            MainViewModel.GetConfig(true);
            #endregion
        }
        /// <summary>
        /// 修改后台配方检测参数
        /// </summary>
        private void SetBackendValue()
        {
            MainViewModel.SetBackendValue();
        }

        //读取一个路径下的所有文件,返回配方列表
        private List<string> GetFormulaList()
        {
            string path = $"{Environment.CurrentDirectory}\\Config";
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles();
            List<string> fl = new List<string>();
            for(int i = 0; i < files.Length; i++)
            {
                if (files[i].Name != "Config.xml") fl.Add(files[i].Name.Substring(0,files[i].Name.Length-4).ToString());
            }
            return fl;
        }

        //发送信息在页面添加新配方选项
        private void AddFormulaToView(List<string> fl)
        {
            foreach(string file in fl)
            {
                Messenger.Default.Send(new NotificationMessage(file),"AddFormulaView");
            }
        }

        private void DeleteFormulaToView(List<string> fl)
        {
            foreach (string file in fl)
            {
                Messenger.Default.Send(new NotificationMessage(file),"DeleteFormulaView");
            }
        }


    }
}
