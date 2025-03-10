using System.Reflection;
using Wpf.Ui.Appearance;

namespace rand7.ViewModels.Pages.Settings
{
    public partial class AboutViewModel : ObservableObject
    {

        private bool _isInitialized = false;

        [ObservableProperty]
        //private string appVersion = "抽号器 - 9.2.2.0";
        private string _appVersion = $"抽号器 - {GetAssemblyVersion()}";

        public Task OnNavigatedToAsync()
        {
            if (!_isInitialized)
                InitializeViewModel();

            return Task.CompletedTask;
        }

        public Task OnNavigatedFromAsync() => Task.CompletedTask;

        private void InitializeViewModel()
        {
            AppVersion = $"{GetAssemblyName()} - {GetAssemblyVersion()}";

            _isInitialized = true;
        }

        private static string _cachedVersion = null;
        private static string _cachedName = null;


        /// <summary>
        /// 获取程序集的版本信息。
        /// </summary>
        /// <returns>程序集的版本字符串。</returns>
        public static string GetAssemblyVersion()
        {
            try
            {
                // 使用缓存避免重复计算
                if (_cachedVersion != null)
                {
                    return _cachedVersion;
                }

                // 获取当前程序集的版本信息
                Assembly assembly = Assembly.GetExecutingAssembly();
                Version version = assembly.GetName().Version;
                _cachedVersion = version.ToString();

                return _cachedVersion;
            }
            catch (Exception ex)
            {
                // 记录异常信息（可以使用日志库）
                Console.WriteLine($"获取程序集版本时发生错误: {ex.Message}");
                // 返回默认值或空字符串，具体根据业务需求决定
                return "0.0.0.0";
            }
        }
        public static string GetAssemblyName()
        {
            try
            {
                // 使用缓存避免重复计算
                if (_cachedName != null)
                {
                    return _cachedName;
                }

                // 获取当前程序集的版本信息
                Assembly assembly = Assembly.GetExecutingAssembly();
                string version = assembly.GetName().Name;
                _cachedName = version.ToString();

                return _cachedName;
            }
            catch (Exception ex)
            {
                // 记录异常信息（可以使用日志库）
                Console.WriteLine($"获取程序集名字时发生错误: {ex.Message}");
                // 返回默认值或空字符串，具体根据业务需求决定
                return "未命名";
            }
        }

    }
}
