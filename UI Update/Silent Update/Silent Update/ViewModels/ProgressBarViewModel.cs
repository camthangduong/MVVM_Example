using Silent_Update.Utilities;
using Silent_Update.Views;
using System.Windows;

namespace Silent_Update.ViewModels
{
    public class ProgressBarViewModel : BindableBase
    {
        private string _workingMsg, _fileNameMsg;
        private ProgressBarView viewBar;

        public string WorkingMsg
        {
            get { return _workingMsg; }
            set { _workingMsg = value; SetProperty(ref _workingMsg, value); }
        }

        public string FileNameMsg
        {
            get { return _fileNameMsg; }
            set { _fileNameMsg = value; SetProperty(ref _fileNameMsg, value); }
        }

        /// <summary>
        /// Navigation control command
        /// </summary>
        public MyICommand<string> CancelCommand { get; private set; }

        public ProgressBarViewModel()
        {
            CancelCommand = new MyICommand<string>(ButtonCancelEventHandler);
            viewBar = new ProgressBarView();
        }

        private void ButtonCancelEventHandler(string obj)
        {
            MessageBox.Show("Please wait for current process to be finished");
            IsCancel = true;
        }

        public bool IsCancel { get; set; }

        public void ShowDialog()
        {
            viewBar = new ProgressBarView();
            viewBar.Show();
        }

        public void CloseDialog()
        {
            if (viewBar != null)
            {
                viewBar.Close();
            }
        }
    }
}
