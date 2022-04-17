using AirCombatAuswertung.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AirCombatAuswertung.Views
{
    public sealed partial class _10_Home : Page
    {
        public _10_Home()
        {
            this.InitializeComponent();

        }
        public _10_HomeViewModel ViewModel { get; } = (Application.Current as App).Container.GetService<_10_HomeViewModel>();
    }
}
