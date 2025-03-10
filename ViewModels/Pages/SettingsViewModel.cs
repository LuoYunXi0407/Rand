using rand7.Views.Pages;
using rand7.Views.Pages.Settings;
using rand7.Views.Windows;
using System.Windows.Controls;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace rand7.ViewModels.Pages
{
    public partial class SettingsViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;

        private readonly INavigationService _navigationService;
        private readonly Window _mainWindow;

        public ISnackbarService snackbarService { get; set; }

        public SettingsViewModel(INavigationWindow navigationWindow, INavigationService navigationService)
        {
            _mainWindow = (navigationWindow as MainWindow)!;
            _navigationService = navigationService;

            snackbarService = new SnackbarService();


        }

        public void ShowSnackbar()
        {
            snackbarService.Show("操作成功", "数据已保存", ControlAppearance.Success, new SymbolIcon(SymbolRegular.Fluent24)
            {
                FontSize = 32
            }, TimeSpan.FromSeconds(2));
        }

        



        [ObservableProperty]
        private string appVersion = string.Empty;

        public Task OnNavigatedToAsync()
        {
            test();
            if (!_isInitialized)
                InitializeViewModel();
            return Task.CompletedTask;

        }

        public Task OnNavigatedFromAsync() 
        {
            return Task.CompletedTask;

        }

        private void test()
        {
            
        }

        private void InitializeViewModel()
        {
            AppVersion = $"UiDesktopApp1 - {GetAssemblyVersion()}";
            _isInitialized = true;
        }

        private string GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
                ?? String.Empty;
        }

        [RelayCommand]
        void OnLightThemeClick()
        {
            SystemThemeWatcher.UnWatch(_mainWindow);
            ApplicationThemeManager.Apply(ApplicationTheme.Light);
        }

        [RelayCommand]
        void OnDarkThemeClick()
        {
            SystemThemeWatcher.UnWatch(_mainWindow);
            ApplicationThemeManager.Apply(ApplicationTheme.Dark);
        }

        [RelayCommand]
        void OnAutoThemeClick()
        {
            SystemThemeWatcher.Watch(_mainWindow, WindowBackdropType.Mica, true);
            ApplicationThemeManager.ApplySystemTheme();
        }

        [RelayCommand]
        void OnNavigatePersonalizationPage()
        {
            _navigationService.NavigateWithHierarchy(typeof(PersonalizationPage));
        }

        [RelayCommand]
        void OnNavigateNumberPage()
        {
            _navigationService.NavigateWithHierarchy(typeof(NumberPage));
        }

        [RelayCommand]
        void OnNavigateBalancePage()
        {
            _navigationService.NavigateWithHierarchy(typeof(BalancePage));
        }

        [RelayCommand]
        void OnNavigateSingleStudentPage()
        {
            
            _navigationService.NavigateWithHierarchy(typeof(SingleStudentPage));
        }

        [RelayCommand]
        void OnNavigateMultipleStudentPage()
        {
            _navigationService.NavigateWithHierarchy(typeof(MultipleStudentPage));
        }

        [RelayCommand]
        void OnNavigateDeveloperPage()
        {
            _navigationService.NavigateWithHierarchy(typeof(DeveloperPage));
        }

        [RelayCommand]
        void OnNavigateAboutPage()
        {
            _navigationService.NavigateWithHierarchy(typeof(AboutPage));
        }
    }
}
