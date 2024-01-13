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
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using CENJE_Vison.ViewModel;

namespace CENJE_Vison.Views
{
    /// <summary>
    /// FormulaWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FormulaWindow : Window
    {
        public FormulaWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this,"AddFormulaView" ,message =>
            {
                Button button = new Button();
                string  strbtn = message.Notification.ToString();
                button.Name = message.Notification.ToString();
                //button.Name = "btn12";
                button.Content = message.Notification.ToString();
                button.Command = FormulaViewModel.ClickFormula_Command;
                button.CommandParameter = button.Name;
                formulaList.Items.Add(button);
            });
            Messenger.Default.Register<NotificationMessage>(this, "DeleteFormulaView",DeleteFormulaView);
        }

        private void DeleteFormulaView(NotificationMessage message)
        {
            foreach(Button b in formulaList.Items)
            {
                if (b.Name+".xml" == message.Notification.ToString())
                {
                    formulaList.Items.Remove(b);
                    formulaList.UpdateLayout();
                    return;
                }
                    
                    
            }
            
        }
    }
}
