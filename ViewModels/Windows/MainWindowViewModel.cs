using System.Collections.ObjectModel;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace rand7.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {

        public ISnackbarService SnackbarService { get; set; }
        public IContentDialogService contentDialogService { get; set; }

        public MainWindowViewModel()
        {
            SnackbarService = new SnackbarService();
            contentDialogService = new ContentDialogService();
        }

        public void ShowSnackbar(string title, string message, ControlAppearance appearance, SymbolIcon icon, TimeSpan duration)
        {
            SnackbarService.Show(
                title,
                message,
                appearance,
                icon,
                duration
            );
        }

        [ObservableProperty]
        private string _applicationTitle = "抽号器";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "主页",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.HomePage)
            },
            new NavigationViewItem()
            {
                Content = "单人",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Person24 },
                TargetPageType = typeof(Views.Pages.SinglePage)
            },
            new NavigationViewItem()
            {
                Content = "多人",
                Icon = new SymbolIcon { Symbol = SymbolRegular.PeopleTeam24 },
                TargetPageType = typeof(Views.Pages.MultiplePage)
            }
            /*,
            new NavigationViewItem()
            {
                Content = "成就",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Reward24 },
                TargetPageType = typeof(Views.Pages.RewardPage)
            }*/
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "设置",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };
    }
}
