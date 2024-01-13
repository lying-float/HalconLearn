using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using CENJE_Vison.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using CENJE_Vison.Views;
using GalaSoft.MvvmLight.Ioc;

namespace CENJE_Vison.ViewModel
{
    public class LoginViewModel:ViewModelBase
    {
        #region 属性
        public LoginModel loginModel { get; set; } = new LoginModel();
        public RelayCommand<string> Login_Command { get; set; }//登录按键命令
        public RelayCommand Back_Command { get; set; }//退出按键命令
        public RelayCommand ChangePassword_Command { get; set; }//修改密码

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginViewModel()
        {
            loginModel.Password = "123456";
            Login_Command = new RelayCommand<string>(new Action<string>(Btn_Login));
            Back_Command = new RelayCommand(new Action(Btn_Back));
            ChangePassword_Command = new RelayCommand(new Action(Btn_Change));
            
        }

        #region 方法
        /// <summary>
        /// 密码输入确认
        /// </summary>
        /// <param name="password"></param>
        private void Btn_Login(string password)
        {
            if (password == loginModel.Password)
            {
                MainViewModel._passWordEnter = true;
                switch (MainViewModel._WhichSetting)
                {
                    case MainViewModel._whichSettingToOpen.OpenSettingNegative :
                        var negativeSettingWindow = SimpleIoc.Default.GetInstance<SettingNegativeWindow>();
                        negativeSettingWindow.Show(); break;
                    case MainViewModel._whichSettingToOpen.OpenSettingPositive:
                        var positiveSettingWindow = SimpleIoc.Default.GetInstance<SettingPositiveWindow>();
                        positiveSettingWindow.Show(); break;
                    case MainViewModel._whichSettingToOpen.OpenSettingFinish:
                        var finishSettingWindow = SimpleIoc.Default.GetInstance<SettingFinishWindow>();
                        finishSettingWindow.Show(); break;

                }
                Messenger.Default.Send(new NotificationMessage("close"));
            }
            
        }

        /// <summary>
        /// 退出
        /// </summary>
        private void Btn_Back()
        {
            Messenger.Default.Send(new NotificationMessage("close"));

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public void Btn_Change()
        {

        }

        #endregion
    }
}
