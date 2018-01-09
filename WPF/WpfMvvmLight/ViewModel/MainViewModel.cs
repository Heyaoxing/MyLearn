using GalaSoft.MvvmLight;
using System.Diagnostics;

namespace WpfMvvmLight.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _viewModel = new HomePageViewModel();
        }


        private ViewModelBase _viewModel;
        /// <summary>
        /// �󶨵� ContentControl
        /// </summary>
        public ViewModelBase ViewModel
        {
            get { return _viewModel; }
            set
            {
                if (_viewModel == value)
                {
                    return;
                }
                _viewModel = value;
                RaisePropertyChanged();
            }
        }

        public override void Cleanup()
        {
            Debug.Fail("��ͼģ���е����ͷ�");
            base.Cleanup();
        }
    }
}