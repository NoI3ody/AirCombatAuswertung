using AirCombatAuswertung.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace AirCombatAuswertung.Views
{
    public sealed partial class _60_Results : Page
    {
        public _60_Results()
        {
            this.InitializeComponent();
        }
        public _60_ResultsViewModel ViewModel { get; } = (Application.Current as App).Container.GetService<_60_ResultsViewModel>();
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.InitializeResultsDataAsync();
        }
    }
}