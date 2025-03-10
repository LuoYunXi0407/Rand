using rand7.ViewModels.Pages;
using Wpf.Ui.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace rand7.Views.Pages
{
    public partial class RewardPage : INavigableView<RewardViewModel>
    {
        public RewardViewModel ViewModel { get; }

        public RewardPage(RewardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}