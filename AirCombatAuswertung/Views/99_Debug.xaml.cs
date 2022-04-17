using AirCombatAuswertung.Services;
using Microsoft.UI.Xaml.Controls;
using System.IO;
using Windows.Storage;

namespace AirCombatAuswertung.Views
{
    public sealed partial class _99_Debug : Page
    {
        public _99_Debug()
        {
            this.InitializeComponent();

            tbDBPath.Text ="Datenbank-Speicherort: " + Path.Combine(ApplicationData.Current.LocalFolder.Path, SqliteDataService.DbName);
        }
    }
}
