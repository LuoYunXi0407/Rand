using rand7.Models;
using System.Windows.Media;
using Wpf.Ui.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace rand7.ViewModels.Pages
{
    public partial class SingleViewModel : ObservableObject, INavigationAware
    {

        private bool _isInitialized = false;

        [ObservableProperty]
        private IEnumerable<DataColor> _colors;

        public Task OnNavigatedToAsync()
        {
            if (!_isInitialized)
                InitializeViewModel();
            return Task.CompletedTask;

        }

        public Task OnNavigatedFromAsync() 
        {
            return Task.CompletedTask;
        }

        private void InitializeViewModel()
        {
            

            _isInitialized = true;
        }
    }
}
