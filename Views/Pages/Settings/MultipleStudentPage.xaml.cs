using rand7.ViewModels.Pages;
using rand7.ViewModels.Pages.Settings;
using Wpf.Ui.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace rand7.Views.Pages.Settings
{
    public partial class MultipleStudentPage : INavigableView<MultipleStudentViewModel>
    {
        public MultipleStudentViewModel ViewModel { get; }

        public MultipleStudentPage(MultipleStudentViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            if (v.x[12].value == "0" || ((v.x[18].value == "3" || v.x[18].value == "4") && (v.x[19].value == "1" || v.x[19].value == "3" || v.x[19].value == "4")))
            {
                Multiple_Student_Roll_In_Order.IsEnabled = false;
                Multiple_Student_Roll_Expander.IsExpanded = false;

            }
            Multiple_Student_Show_In_Order.IsChecked = v.x[11].value == "1" ? true : false;
            Multiple_Student_Roll.IsChecked = v.x[12].value == "1" ? true : false;
            Multiple_Student_Roll_In_Order.IsChecked = v.x[13].value == "1" ? true : false;
            Multiple_Student_Show_Name.IsChecked = v.x[14].value == "1" ? true : false;
            Multiple_Student_Show_Num.IsChecked = v.x[15].value == "1" ? true : false;
            Multiple_Student_Voice_Name.IsChecked = v.x[16].value == "1" ? true : false;
            Multiple_Student_Voice_Num.IsChecked = v.x[17].value == "1" ? true : false;

        }

        private void Multiple_Student_Show_In_Order_Checked(object sender, RoutedEventArgs e)
        {
            v.x[11].value = "1";
            App.writeini();
        }

        private void Multiple_Student_Show_In_Order_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[11].value = "0";
            App.writeini();
        }

        private void Multiple_Student_Roll_Checked(object sender, RoutedEventArgs e)
        {
            v.x[12].value = "1";
            App.writeini();
            Multiple_Student_Roll_In_Order.IsEnabled = true;
            Multiple_Student_Roll_Expander.IsExpanded = true;
        }

        private void Multiple_Student_Roll_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[12].value = "0";
            App.writeini();
            Multiple_Student_Roll_In_Order.IsEnabled = false;
            Multiple_Student_Roll_Expander.IsExpanded = false;
        }

        private void Multiple_Student_Roll_In_Order_Checked(object sender, RoutedEventArgs e)
        {
            v.x[13].value = "1";
            App.writeini();
        }

        private void Multiple_Student_Roll_In_Order_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[13].value = "0";
            App.writeini();
        }

        private void Multiple_Student_Show_Name_Checked(object sender, RoutedEventArgs e)
        {
            v.x[14].value = "1";
            App.writeini();
        }

        private void Multiple_Student_Show_Name_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[14].value = "0";
            App.writeini();
        }

        private void Multiple_Student_Show_Num_Checked(object sender, RoutedEventArgs e)
        {
            v.x[15].value = "1";
            App.writeini();
        }

        private void Multiple_Student_Show_Num_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[15].value = "0";
            App.writeini();
        }

        private void Multiple_Student_Voice_Name_Checked(object sender, RoutedEventArgs e)
        {
            v.x[16].value = "1";
            App.writeini();
        }

        private void Multiple_Student_Voice_Name_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[16].value = "0";
            App.writeini();
        }


        private void Multiple_Student_Voice_Num_Checked(object sender, RoutedEventArgs e)
        {
            v.x[17].value = "1";
            App.writeini();
        }

        private void Multiple_Student_Voice_Num_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[17].value = "0";
            App.writeini();
        }
    }
}