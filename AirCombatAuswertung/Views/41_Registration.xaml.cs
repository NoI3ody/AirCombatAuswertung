using AirCombatAuswertung.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace AirCombatAuswertung.Views
{
    public sealed partial class _41_Registration : Page
    {
        public _41_Registration()
        {
            this.InitializeComponent();
        }
        public _41_RegistrationViewModel ViewModel { get; } = (Application.Current as App).Container.GetService<_41_RegistrationViewModel>();
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.InitializeRegistrationDataAsync();
        }
    }
}
