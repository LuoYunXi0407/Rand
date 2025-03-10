using rand7.ViewModels.Pages;
using rand7.ViewModels.Pages.Settings;
using Wpf.Ui.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace rand7.Views.Pages.Settings
{
    public partial class DeveloperPage : INavigableView<DeveloperViewModel>
    {
        public DeveloperViewModel ViewModel { get; }

        public DeveloperPage(DeveloperViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();


            string t = "";
            for (int i = 1; i <= v.num; i++)
            {
                t += v.x[i].section + "." + v.x[i].key + ":" + v.x[i].value + "\n";
            }
            t += "\n";
            foreach (int i in Views.Pages.HomePage.num_list)
            {
                t += i.ToString() + "\n";
            }
            t += System.AppDomain.CurrentDomain.BaseDirectory;
            dev.Text = t;

        }
    }
}