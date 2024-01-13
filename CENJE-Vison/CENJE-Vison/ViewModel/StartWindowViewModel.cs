using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CENJE_Vison.ViewModel
{
    public class StartWindowViewModel:ViewModelBase
    {
        public StartWindowViewModel()
        {
            ProcessValueText = "正在初始化.....";
            ProgressBar = 1;
        }

        private string _NameText = "诚捷智能";

        public string NameText
        {
            get { return _NameText; }
            set
            {
                _NameText= value;
                RaisePropertyChanged();
            }
        }


        private string _logText="CenJe";

        public string LogText
        {
            get { return _logText; }
            set
            {
                _logText = value;
                RaisePropertyChanged();
            }
        }

        private string _processValueText;

        public string ProcessValueText
        {
            get { return _processValueText; }
            set
            {
                _processValueText = value;
                RaisePropertyChanged();
            }
        }

        private int _progressBar;

        public int ProgressBar
        {
            get { return _progressBar; }
            set
            {
                _progressBar = value;
                RaisePropertyChanged();
            }
        }

        private bool ReadSolutionConfig()
        {
            try
            {
                
            }
            catch (Exception ex)
            {

            }

            return true;
        }

    }
}
