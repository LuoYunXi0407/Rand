using rand7.ViewModels.Windows;
using System.Windows.Media;
using Wpf.Ui;
using Wpf.Ui.Abstractions;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace rand7.Views.Windows
{
    public partial class MainWindow : INavigationWindow
    {
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
            RootNavigation.IsBackButtonVisible = NavigationViewBackButtonVisible.Auto;

            SetPageService(pageService);

            navigationService.SetNavigationControl(RootNavigation);

            ViewModel.SnackbarService.SetSnackbarPresenter(SnackbarPresenter);
            ViewModel.contentDialogService.SetDialogHost(RootContentDialog);


        }

        #region INavigationWindow methods

        public INavigationView GetNavigation() => RootNavigation;

        public bool Navigate(Type pageType) => RootNavigation.Navigate(pageType);

        public void SetPageService(INavigationViewPageProvider pageService) => RootNavigation.SetPageProviderService(pageService);

        public void ShowWindow() => Show();

        public void CloseWindow() => Close();

        #endregion INavigationWindow methods

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
    }
}
