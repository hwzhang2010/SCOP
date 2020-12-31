/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:TSFCS.SCOP"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;
//using Microsoft.Practices.ServiceLocation;

namespace TSFCS.SCOP.ViewModel
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
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<SetViewModel>();
            SimpleIoc.Default.Register<SeeViewModel>();
            SimpleIoc.Default.Register<TtcViewModel>();
            SimpleIoc.Default.Register<ObcViewModel>();
            SimpleIoc.Default.Register<TempViewModel>();
            SimpleIoc.Default.Register<PowerViewModel>();
            SimpleIoc.Default.Register<GpsViewModel>();
            SimpleIoc.Default.Register<AdcsViewModel>();
            SimpleIoc.Default.Register<CameraViewModel>();
            SimpleIoc.Default.Register<DigitViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public LoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public SetViewModel Set
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SetViewModel>();
            }
        }

        public SeeViewModel See
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SeeViewModel>();
            }
        }

        public TtcViewModel Ttc
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TtcViewModel>();
            }
        }

        public ObcViewModel Obc
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ObcViewModel>();
            }
        }

        public TempViewModel Temp
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TempViewModel>();
            }
        }

        public PowerViewModel Power
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PowerViewModel>();
            }
        }

        public GpsViewModel Gps
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GpsViewModel>();
            }
        }

        public AdcsViewModel Adcs
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AdcsViewModel>();
            }
        }

        public CameraViewModel Camera
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CameraViewModel>();
            }
        }

        public DigitViewModel Digit
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DigitViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}