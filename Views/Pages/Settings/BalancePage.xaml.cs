using rand7.ViewModels.Pages;
using rand7.ViewModels.Pages.Settings;
using Wpf.Ui.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace rand7.Views.Pages.Settings
{
    public partial class BalancePage : INavigableView<BalanceViewModel>
    {
        public BalanceViewModel ViewModel { get; }

        public BalancePage(BalanceViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            var _singleStudentViewModel = new SingleStudentViewModel();
            var _singleStudentPage = new SingleStudentPage(_singleStudentViewModel);

            InitializeComponent();
            if (int.Parse(v.x[18].value) == 0)
            {
                Student_Avoid_Duplication_Setting.SelectedIndex = 0;
            }
            else if (int.Parse(v.x[18].value) <= 2)
            {
                Student_Avoid_Duplication_Setting.SelectedIndex = 1;
            }
            else if (int.Parse(v.x[18].value) <= 4)
            {
                Student_Avoid_Duplication_Setting.SelectedIndex = 2;
            }
            if(int.Parse(v.x[18].value) % 2 == 1)
            {
                Student_Avoid_Duplication_Setting_Save.IsChecked = false;
            }
            if (int.Parse(v.x[18].value) % 2 == 0)
            {
                Student_Avoid_Duplication_Setting_Save.IsChecked = true;
            }
            //Student_Avoid_Duplication_Setting.SelectedIndex = int.Parse(v.x[18].value);
            Student_Avoid_Duplication_Mode.SelectedIndex = int.Parse(v.x[19].value);
            if (Student_Avoid_Duplication_Setting.SelectedIndex != 0)
            {
                Student_Avoid_Duplication_Mode_Menu.IsEnabled = true;
                Student_Avoid_Duplication_Setting_Expander.IsExpanded = true;

            }
        }

        private void Student_Avoid_Duplication_Mode_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Student_Avoid_Duplication_Mode_Menu == null) return;
            if (Student_Avoid_Duplication_Setting.SelectedIndex != 0)
            {
                Student_Avoid_Duplication_Mode_Menu.IsEnabled = true;
                Student_Avoid_Duplication_Setting_Expander.IsExpanded = true;
            }
            else if (Student_Avoid_Duplication_Setting.SelectedIndex == 0)
            {
                Student_Avoid_Duplication_Mode_Menu.IsEnabled = false;
                Student_Avoid_Duplication_Setting_Expander.IsExpanded = false;
            }

            if (Student_Avoid_Duplication_Setting.SelectedIndex == 2)
            {

            }

            if (Student_Avoid_Duplication_Setting.SelectedIndex == 0) v.x[18].value = "0";
            else if (Student_Avoid_Duplication_Setting.SelectedIndex == 1 && Student_Avoid_Duplication_Setting_Save.IsChecked == false) v.x[18].value = "1";
            else if (Student_Avoid_Duplication_Setting.SelectedIndex == 1 && Student_Avoid_Duplication_Setting_Save.IsChecked == true) v.x[18].value = "2";
            else if (Student_Avoid_Duplication_Setting.SelectedIndex == 2 && Student_Avoid_Duplication_Setting_Save.IsChecked == false) v.x[18].value = "3";
            else if (Student_Avoid_Duplication_Setting.SelectedIndex == 2 && Student_Avoid_Duplication_Setting_Save.IsChecked == true) v.x[18].value = "4";

            


            //v.x[18].value = Student_Avoid_Duplication_Setting.SelectedIndex.ToString();
            //MessageBox.Show("?1");
            App.writeini();
        }

        private void Student_Avoid_Duplication_Setting_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void Single_Student_Show_Num_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Single_Student_Show_Num_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Single_Student_Show_Name_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Single_Student_Show_Name_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}