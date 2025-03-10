using rand7.Views.Windows;
using rand7.ViewModels.Pages;
using rand7.ViewModels.Pages.Settings;
using Wpf.Ui.Controls;
using Wpf.Ui.Appearance;
using Wpf.Ui.Abstractions.Controls;


namespace rand7.Views.Pages.Settings
{
    public partial class PersonalizationPage : INavigableView<PersonalizationViewModel>
    {
        public int test = 0;
        public PersonalizationViewModel ViewModel { get; }
        Window _mainWindow = Application.Current.MainWindow as MainWindow;
        public PersonalizationPage(PersonalizationViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            Personalization_Theme.SelectedIndex = int.Parse(v.x[1].value);

            //Personalization_Theme2.SelectedIndex = int.Parse(v.x[1].value);


            Personalization_One_Digit_Number.SelectedIndex = int.Parse(v.x[23].value);
            Personalization_Two_Character_Name.SelectedIndex = int.Parse(v.x[24].value);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SystemThemeWatcher.Watch(_mainWindow, WindowBackdropType.Mica, true);
            ApplicationThemeManager.ApplySystemTheme();
        }



        private void Personalization_Theme2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            

        }

        private void Personalization_One_Digit_Number_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Personalization_Two_Character_Name == null) return;
            v.x[23].value = Personalization_One_Digit_Number.SelectedIndex.ToString();
            App.writeini();
        }

        private void Personalization_Two_Character_Name_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Personalization_Two_Character_Name == null) return;
            v.x[24].value = Personalization_Two_Character_Name.SelectedIndex.ToString();
            App.writeini();
        }

        private void Personalization_Theme_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(test == 0)
            {
                test++;
                return;
            }
            if (Personalization_Theme == null) return;
            if (Personalization_Theme.SelectedIndex == 0)
            {
                SystemThemeWatcher.Watch(_mainWindow, WindowBackdropType.Mica, true);
                ApplicationThemeManager.ApplySystemTheme();
            }
            else if (Personalization_Theme.SelectedIndex == 1)
            {
                SystemThemeWatcher.UnWatch(_mainWindow);
                ApplicationThemeManager.Apply(ApplicationTheme.Light);
            }
            else if (Personalization_Theme.SelectedIndex == 2)
            {
                SystemThemeWatcher.UnWatch(_mainWindow);
                ApplicationThemeManager.Apply(ApplicationTheme.Dark);
            }
            else if (Personalization_Theme.SelectedIndex == 3)
            {
                SystemThemeWatcher.UnWatch(_mainWindow);
                ApplicationThemeManager.Apply(ApplicationTheme.HighContrast);
            }
            v.x[1].value = Personalization_Theme.SelectedIndex.ToString();
            App.writeini();
        }
    }
}