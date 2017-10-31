using MVVC_Binding.Utilities;

namespace MVVC_Binding.ViewModels
{
    public class Page2ViewModel : BindableBase
    {
        public MyICommand<string> BtnCommand { get; private set; }

        public Page2ViewModel()
        {
            BtnCommand = new MyICommand<string>(OnBtnClick);
            Page2Text = "Just a test";
        }

        private void OnBtnClick(string obj)
        {
            _page2Text = "Changing by button click";
        }

        private string _page2Text;

        public string Page2Text
        {
            get { return _page2Text; }
            set
            {
                _page2Text = value;
                SetProperty(ref _page2Text, value);
            }
        }
    }
}
