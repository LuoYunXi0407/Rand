using rand7.ViewModels.Windows;
using System.ComponentModel;
using System.Windows.Interop;
using System.Windows.Media;
using Wpf.Ui;
using Wpf.Ui.Abstractions;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace rand7.Views.Windows
{
    public partial class MainWindow : INavigationWindow
    {
        private bool _allowClose = false;
        public MainWindowViewModel ViewModel { get; }

        public MainWindow(
            MainWindowViewModel viewModel,
            INavigationViewPageProvider pageService,
            INavigationService navigationService
            )
        {
            ViewModel = viewModel;
            DataContext = this;



            SystemThemeWatcher.Watch(this);

            InitializeComponent();
            Closing += MainWindow_Closing;

            RootNavigation.IsBackButtonVisible = NavigationViewBackButtonVisible.Auto;

            SetPageService(pageService);

            navigationService.SetNavigationControl(RootNavigation);

            ViewModel.SnackbarService.SetSnackbarPresenter(SnackbarPresenter);
            ViewModel.contentDialogService.SetDialogHost(RootContentDialog);


        }
        private void MainWindow_Closing(object? sender, CancelEventArgs e)
        {
            if (v.x[26].value == "0" && !_allowClose)
            {
                e.Cancel = true;
                Hide();
            }
        }
        #region INavigationWindow methods

        public INavigationView GetNavigation() => RootNavigation;

        public bool Navigate(Type pageType) => RootNavigation.Navigate(pageType);

        public void SetPageService(INavigationViewPageProvider pageService) => RootNavigation.SetPageProviderService(pageService);

        public void ShowWindow() => Show();

        public void CloseWindow() => Close();

        #endregion INavigationWindow methods
        const int WM_SHOWME = 0x8001; // WM_APP + 1
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_SHOWME)
            {
                Dispatcher.Invoke(() =>
                {
                    Show();
                    WindowState = WindowState.Normal;
                    Activate();
                });

                handled = true;
            }

            return IntPtr.Zero;
        }
        /// <summary>
        /// Raises the closed event.
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Make sure that closing this window will begin the process of closing the application.
            Application.Current.Shutdown();
        }

        INavigationView INavigationWindow.GetNavigation()
        {
            throw new NotImplementedException();
        }

        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }

        private void RootNavigation_Navigated(NavigationView sender, NavigatedEventArgs args)
        {
            var scrollViewers = FindControls<DynamicScrollViewer>(sender);
            scrollViewers!.Last().ScrollToTop();
        }

        private IEnumerable<T> FindControls<T>(DependencyObject parent) where T : DependencyObject
        {
            var childCount = VisualTreeHelper.GetChildrenCount(parent);

            for (var i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T tChild)
                {
                    yield return tChild;
                }

                foreach (var descendant in FindControls<T>(child))
                {
                    yield return descendant;
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            _allowClose = true;
            Close();

        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            Show();
            Activate();
        }
    }
}
