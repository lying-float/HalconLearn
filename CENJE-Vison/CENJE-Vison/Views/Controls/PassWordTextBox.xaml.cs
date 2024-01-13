using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
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

namespace CENJE_Vison.Views.Controls
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class PassWordTextBox : UserControl
    {

        public static readonly DependencyProperty ShowTitleProperty = DependencyProperty.Register(
            "ShowTitle", typeof(Visibility), typeof(PassWordTextBox), new PropertyMetadata(System.Windows.Visibility.Collapsed));

        public Visibility ShowTitle
        {
            get { return (Visibility)GetValue(ShowTitleProperty); }
            set { SetValue(ShowTitleProperty, value); }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public static readonly DependencyProperty StrTitleProperty = DependencyProperty.Register(
            "StrTitle", typeof(string), typeof(PassWordTextBox), new PropertyMetadata(string.Empty));

        public string StrTitle
        {
            get { return (string)GetValue(StrTitleProperty); }
            set { SetValue(StrTitleProperty, value); }
        }
        /// <summary>
        /// 按钮字体图标编码
        /// </summary>
        public string PassWordText
        {
            get { return (string)GetValue(PassWordTextProperty); }
            set { SetValue(PassWordTextProperty, value); }
        }
        public static readonly DependencyProperty PassWordTextProperty =
            DependencyProperty.Register("PassWordText", typeof(string), typeof(PassWordTextBox), new PropertyMetadata(string.Empty));

        /// <summary>
        /// 密码框线
        /// </summary>
        public Thickness PwdThickness
        {
            get { return (Thickness)GetValue(PwdThicknessProperty); }
            set { SetValue(PwdThicknessProperty, value); }
        }

        public static readonly DependencyProperty PwdThicknessProperty =
            DependencyProperty.Register("PwdThickness", typeof(Thickness), typeof(PassWordTextBox), new PropertyMetadata(new Thickness(1)));

        #region

        public static readonly DependencyProperty CapsProperty = DependencyProperty.Register(
            "Caps", typeof(bool), typeof(PassWordTextBox), new PropertyMetadata(default(bool)));

        public bool Caps
        {
            get { return (bool)GetValue(CapsProperty); }
            set { SetValue(CapsProperty, value); }
        }

        public static readonly DependencyProperty CtrlProperty = DependencyProperty.Register(
            "Ctrl", typeof(bool), typeof(PassWordTextBox), new PropertyMetadata(default(bool)));

        public bool Ctrl
        {
            get { return (bool)GetValue(CtrlProperty); }
            set { SetValue(CtrlProperty, value); }
        }

        public static readonly DependencyProperty AltProperty = DependencyProperty.Register(
            "Alt", typeof(bool), typeof(PassWordTextBox), new PropertyMetadata(default(bool)));

        public bool Alt
        {
            get { return (bool)GetValue(AltProperty); }
            set { SetValue(AltProperty, value); }
        }

        public static readonly DependencyProperty ShiftProperty = DependencyProperty.Register(
            "Shift", typeof(bool), typeof(PassWordTextBox), new PropertyMetadata(default(bool)));

        public bool Shift
        {
            get { return (bool)GetValue(ShiftProperty); }
            set { SetValue(ShiftProperty, value); }
        }

        #endregion

        public ICommand KeyCommand { get; set; }


        public PassWordTextBox()
        {
            InitializeComponent();

            KeyCommand = new RoutedCommand();
            CommandBinding cbinding = new CommandBinding(KeyCommand, KeyCommand_Excuted, KeyCommand_CanExcute);
            this.CommandBindings.Add(cbinding);
        }
        private void KeyCommand_CanExcute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void SetDisPlayPwd(string p)
        {
            passwordBox.Password = p;
        }

        private void KeyCommand_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter != null)
            {
                string code = e.Parameter.ToString();
                if (code == "Back")
                {
                    if (passwordBox.Password.Length > 0)
                    {
                        passwordBox.Password = passwordBox.Password.Substring(0, passwordBox.Password.Length - 1);
                    }
                }
                else if (code == "Space")
                {
                    passwordBox.Password = passwordBox.Password + " ";
                }
                else if (code == "Tab")
                {
                    passwordBox.Password = passwordBox.Password + "\t";
                }
                else if (code == "Enter")
                {
                    VisibleKeyBoard = false;
                    passwordBox.KeyDown -= PasswordBox_OnKeyDown;
                }
                else
                {
                    passwordBox.Password = passwordBox.Password + code;
                }
            }

        }



        public static readonly DependencyProperty VisibleKeyBoardProperty = DependencyProperty.Register(
            "VisibleKeyBoard", typeof(bool), typeof(PassWordTextBox), new PropertyMetadata(false));

        public bool VisibleKeyBoard
        {
            get { return (bool)GetValue(VisibleKeyBoardProperty); }
            set { SetValue(VisibleKeyBoardProperty, value); }
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            PassWordText = passwordBox.Password;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            //if (e.Property.Name == "PassWordText")
            //{
            //    if (PassWordText == null)
            //    {
            //        return;
            //    }
            //    passwordBox.Password = PassWordText;
            //    SetSelection(passwordBox, passwordBox.Password.Length, PassWordText.Length);

            //}
        }

        private void SetSelection(PasswordBox passwordBox, int start, int length)
        {
            passwordBox.GetType()
                       .GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic)
                       .Invoke(passwordBox, new object[] { start, length });
        }

        private void PasswordBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void OpenOrCloseKeyBoard(object sender, RoutedEventArgs e)
        {
            if (VisibleKeyBoard)
            {
                VisibleKeyBoard = false;
                passwordBox.KeyDown -= PasswordBox_OnKeyDown;
            }
            else
            {
                VisibleKeyBoard = true;
                passwordBox.KeyDown += PasswordBox_OnKeyDown;
            }
        }

        private void PasswordBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            VisibleKeyBoard = false;
            passwordBox.KeyDown -= PasswordBox_OnKeyDown;
        }

        public void ClearPassWord()
        {
            passwordBox.Password = "";
        }
    }

    public class UperConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                return "";
            }

            if (value == null)
            {
                if (parameter != null)
                {
                    return parameter.ToString();
                }
                else
                {
                    return "";
                }
            }

            bool isuper = (bool)value;

            if (isuper)
            {
                return parameter.ToString().ToUpper();
            }
            else
            {
                return parameter.ToString().ToLower();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ShiftConvert : IValueConverter
    {
        Dictionary<string, string> dic = new Dictionary<string, string>()
        {
            {"`","~" },
            {"1","!" },
            {"2","@" },
            {"3","#" },
            {"4","$" },
            {"5","%" },
            {"6","^" },
            {"7","&" },
            {"8","*" },
            {"9","(" },
            {"0",")" },
            {"-","_" },
            {"=","+" },
            {"[","{" },
            {"]","}" },
            {";",":" },
            {"'","\"" },
            {",","<" },
            {".",">" },
            {"/","?" },
            {@"\","|" },
        };

        //Dictionary<string, string> spdic = new Dictionary<string, string>()
        //{
        //    {"douhao","<" },
        //    {"juhao",">" },
        //    {"fanxiegang","?" },
        //};

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (parameter == null)
                {
                    return "";
                }

                if (value == null)
                {
                    if (parameter != null)
                    {
                        return parameter.ToString();
                    }
                    else
                    {
                        return "";
                    }
                }

                bool shift = (bool)value;

                if (shift)
                {

                    var cond = parameter.ToString();
                    if (dic.ContainsKey(cond))
                    {
                        return dic[cond];
                    }

                    return string.Empty;
                }
                else
                {
                    return parameter.ToString();


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
