using rand7.ViewModels.Pages;
using rand7.ViewModels.Pages.Settings;
using Wpf.Ui.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace rand7.Views.Pages.Settings
{
    public partial class NumberPage : INavigableView<NumberViewModel>
    {
        public NumberViewModel ViewModel { get; }

        public NumberPage(NumberViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            if (v.x[2].value == "0")
            {
                Single_Number_Roll_In_Order.IsEnabled = false;
                Single_Number_Roll_Expander.IsExpanded = false;
            }
            Single_Number_Roll.IsChecked = v.x[2].value == "1" ? true : false;
            Single_Number_Roll_In_Order.IsChecked = v.x[3].value == "1" ? true : false;
            Single_Number_Voice.IsChecked = v.x[4].value == "1" ? true : false;
        }

        private void Single_Number_Roll_Checked(object sender, RoutedEventArgs e)
        {
            v.x[2].value = "1";
            App.writeini();
            Single_Number_Roll_In_Order.IsEnabled = true;
            Single_Number_Roll_Expander.IsExpanded = true;
        }

        private void Single_Number_Roll_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[2].value = "0";
            App.writeini();
            Single_Number_Roll_In_Order.IsEnabled = false;
            Single_Number_Roll_Expander.IsExpanded = false;
        }

        private void Single_Number_Roll_In_Order_Checked(object sender, RoutedEventArgs e)
        {
            v.x[3].value = "1";
            App.writeini();
        }

        private void Single_Number_Roll_In_Order_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[3].value = "0";
            App.writeini();
        }

        private void Single_Number_Voice_Checked(object sender, RoutedEventArgs e)
        {
            v.x[4].value = "1";
            App.writeini();
        }

        private void Single_Number_Voice_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[4].value = "0";
            App.writeini();
        }
    }
}