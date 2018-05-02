using Silent_Update.Utilities;
using System.IO;

namespace Silent_Update.ViewModels
{
    class SettingHealthCheckViewModel : BindableBase
    {
        public SettingHealthCheckViewModel()
        {
            if (Directory.Exists(EnviSetting.Instance.SourcePath))
            {
                // Source = EnviSetting.Instance.SourcePath;
            }
        }
    }
}
