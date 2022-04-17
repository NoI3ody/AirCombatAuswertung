using AirCombatAuswertung.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace AirCombatAuswertung.Views
{
    public sealed partial class _50_Scoreboards : Page
    {
        public _50_Scoreboards()
        {
            this.InitializeComponent();
        }
        public _50_ScoreboardsViewModel ViewModel { get; } = (Application.Current as App).Container.GetService<_50_ScoreboardsViewModel>();
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.InitializeScoreboardsDataAsync();
        }
    }
}
