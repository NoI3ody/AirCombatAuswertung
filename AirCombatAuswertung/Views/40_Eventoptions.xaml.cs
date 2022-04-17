using AirCombatAuswertung.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace AirCombatAuswertung.Views
{
    public sealed partial class _40_Eventoptions : Page
    {
        public _40_Eventoptions()
        {
            this.InitializeComponent();
        }
        public _40_EventoptionsViewModel ViewModel { get; } = (Application.Current as App).Container.GetService<_40_EventoptionsViewModel>();
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.InitializeEventoptionsDataAsync();
        }
    }
}
