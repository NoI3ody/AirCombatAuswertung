using AirCombatAuswertung.Services;
using Microsoft.UI.Xaml.Controls;

namespace AirCombatAuswertung
{
    public sealed partial class MainWindow : Page
    {        
        public MainWindow()
        {
            this.InitializeComponent();
            PrintService.PrintingContainer = printingContainer;   
            
        }
    }
}
