using rand7.Views.Windows;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace rand7.ViewModels.Pages.Settings
{
    public partial class PersonalizationViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly Window _mainWindow;

        public PersonalizationViewModel(INavigationWindow navigationWindow, INavigationService navigationService)
        {
            _mainWindow = (navigationWindow as MainWindow)!;
            _navigationService = navigationService;
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
        void OnHighContrastThemeClick()
        {
            SystemThemeWatcher.UnWatch(_mainWindow);
            ApplicationThemeManager.Apply(ApplicationTheme.HighContrast);
        }

        [RelayCommand]
        void OnAutoThemeClick()
        {
            SystemThemeWatcher.Watch(_mainWindow, WindowBackdropType.Mica, true);
            ApplicationThemeManager.ApplySystemTheme();
        }
    }
}
