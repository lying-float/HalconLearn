using CENJE_Vison.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace CENJE_Vison.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            //ÈÝÆ÷³õÊ¼»¯
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            
            //×¢²áwindowµ½IOCÈÝÆ÷
            SimpleIoc.Default.Register<MainWindow>();
            SimpleIoc.Default.Register<StartWindow>();
            SimpleIoc.Default.Register<FormulaWindow>();
            SimpleIoc.Default.Register<SettingNegativeWindow>();
            SimpleIoc.Default.Register<SettingPositiveWindow>();
            SimpleIoc.Default.Register<SettingFinishWindow>();
            SimpleIoc.Default.Register<LoginWindow>();
            //×¢²áviewModleµ½IocÈÝÆ÷
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<FormulaViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<SettingNegativeViewModel>();
            SimpleIoc.Default.Register<SettingPositiveViewModel>();
            SimpleIoc.Default.Register<SettingFinishViewModel>();
            SimpleIoc.Default.Register<StartWindowViewModel>();
        }

        //ÊôÐÔ
        //Window
        public MainWindow MainWind
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindow>();
            }
        }

        public FormulaWindow FormulaWind
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FormulaWindow>();
            }
        }

        public SettingNegativeViewModel SettingNegativeWindow
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingNegativeViewModel>();
            }
        }

        public SettingPositiveViewModel SettingPositiveWindow
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingPositiveViewModel>();
            }
        }

        public SettingFinishViewModel SettingFinishWindow
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingFinishViewModel>();
            }
        }

        //ViewModel
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        
        public FormulaViewModel FormulaVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FormulaViewModel>();
            }
            
        }

        public LoginViewModel LoginVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }

        }

        public SettingNegativeViewModel SettingNVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingNegativeViewModel>();
            }

        }

        public SettingPositiveViewModel SettingPVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingPositiveViewModel> ();
            }
        }

        public SettingFinishViewModel SettingFVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingFinishViewModel>();
            }
        }

        public StartWindowViewModel StartVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StartWindowViewModel>();
            }
        }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
        
    }
}