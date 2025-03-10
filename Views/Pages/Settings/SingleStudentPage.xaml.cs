using rand7.ViewModels.Pages;
using rand7.ViewModels.Pages.Settings;
using Wpf.Ui.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace rand7.Views.Pages.Settings
{
    public partial class SingleStudentPage : INavigableView<SingleStudentViewModel>
    {
        public SingleStudentViewModel ViewModel { get; }

        public SingleStudentPage(SingleStudentViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();



            if (v.x[5].value == "0" || ((v.x[18].value == "3" || v.x[18].value == "4")&& (v.x[19].value == "0" || v.x[19].value == "3" || v.x[19].value == "4")))
            { 
                Single_Student_Roll_In_Order.IsEnabled = false;
                Single_Student_Roll_Expander.IsExpanded = false;
            }
            Single_Student_Roll.IsChecked = v.x[5].value == "1" ? true : false;
            Single_Student_Roll_In_Order.IsChecked = v.x[6].value == "1" ? true : false;
            Single_Student_Show_Name.IsChecked = v.x[7].value == "1" ? true : false;
            Single_Student_Show_Num.IsChecked = v.x[8].value == "1" ? true : false;
            Single_Student_Voice_Name.IsChecked = v.x[9].value == "1" ? true : false;
            Single_Student_Voice_Num.IsChecked = v.x[10].value == "1" ? true : false;

        }

        private void Single_Student_Roll_Checked(object sender, RoutedEventArgs e)
        {
            v.x[5].value = "1";
            App.writeini();
            Single_Student_Roll_In_Order.IsEnabled = true;
            Single_Student_Roll_Expander.IsExpanded = true;
        }

        private void Single_Student_Roll_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[5].value = "0";
            App.writeini();
            Single_Student_Roll_In_Order.IsEnabled = false;
            Single_Student_Roll_Expander.IsExpanded = false;
        }

        private void Single_Student_Roll_In_Order_Checked(object sender, RoutedEventArgs e)
        {
            v.x[6].value = "1";
            App.writeini();
        }

        private void Single_Student_Roll_In_Order_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[6].value = "0";
            App.writeini();
        }

        private void Single_Student_Show_Name_Checked(object sender, RoutedEventArgs e)
        {
            v.x[7].value = "1";
            App.writeini();
        }

        private void Single_Student_Show_Name_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[7].value = "0";
            App.writeini();
        }

        private void Single_Student_Show_Num_Checked(object sender, RoutedEventArgs e)
        {
            v.x[8].value = "1";
            App.writeini();
        }

        private void Single_Student_Show_Num_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[8].value = "0";
            App.writeini();
        }

        private void Single_Student_Voice_Name_Checked(object sender, RoutedEventArgs e)
        {
            v.x[9].value = "1";
            App.writeini();
        }

        private void Single_Student_Voice_Name_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[9].value = "0";
            App.writeini();
        }

        private void Single_Student_Voice_Num_Checked(object sender, RoutedEventArgs e)
        {
            v.x[10].value = "1";
            App.writeini();
        }

        private void Single_Student_Voice_Num_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[10].value = "0";
            App.writeini();
        }
    }
}