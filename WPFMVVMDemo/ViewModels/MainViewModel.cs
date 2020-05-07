using GalaSoft.MvvmLight;
using Tracker_Software.Model;
using Tracker_Software.ViewModels;
using Tracker_Software.Views;

namespace Tracker_Software.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private object _currentView;
        private object _currentViewModel;

        #endregion

        #region Properties

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                RaisePropertyChanged(() => CurrentView);
            }
        }      

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            CurrentView = new LoginUserControl();
            
        }

        #endregion
    }
}