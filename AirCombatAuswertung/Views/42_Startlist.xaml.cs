using AirCombatAuswertung.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace AirCombatAuswertung.Views
{
    public sealed partial class _42_Startlist : Page
    {
        public _42_Startlist()
        {
            this.InitializeComponent();
        }
        public _42_StartlistViewModel ViewModel { get; } = (Application.Current as App).Container.GetService<_42_StartlistViewModel>();
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.InitializeStartlistDataAsync();
        }
    }
}
