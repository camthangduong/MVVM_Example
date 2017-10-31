using MVVC_Binding.Utilities;

namespace MVVC_Binding.ViewModels
{
    public class Page1ViewModel : BindableBase
    {
        public Page1ViewModel()
        {
            Page1Text = "This is page 1";
        }

        private string _page1Text;

        public string Page1Text
        {
            get { return _page1Text; }
            set
            {
                _page1Text = value;
                SetProperty(ref _page1Text, value);
            }
        }
    }
}
