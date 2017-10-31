using MVVC_Binding.Utilities;
using MVVC_Binding.ViewModels;

namespace MVVC_Binding
{
    public class MainViewModel : BindableBase
    {
        public MyICommand<string> NavCommand { get; private set; }
        private Page1ViewModel page1 = new Page1ViewModel();
        private Page2ViewModel page2 = new Page2ViewModel();

        public MainViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = page1;
        }

        private BindableBase _CurrentViewModel;

        public BindableBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set
            {
                SetProperty(ref _CurrentViewModel, value);
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "page2":
                    CurrentViewModel = page2;
                    break;
                default:
                    CurrentViewModel = page1;
                    break;
            }
        }
    }
}
