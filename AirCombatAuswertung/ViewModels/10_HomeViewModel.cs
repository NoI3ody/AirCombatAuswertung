using AirCombatAuswertung.Interfaces;
using AirCombatAuswertung.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AirCombatAuswertung.ViewModels
{
    public class _10_HomeViewModel:BindableBase
    {
        private ObservableCollection<Class> classes = new ObservableCollection<Class>();
        public ObservableCollection<Class> Classes
        {
            get
            {
                return classes;
            }
            set
            {
                SetProperty(ref classes, value);
            }
        }
        public _10_HomeViewModel(IDataService dataService)
        {
            _dataService = dataService;

            PopulateDataAsync();
        }
        public async Task PopulateDataAsync()
        {
            classes.Clear();

            foreach(var c in await _dataService.GetClassesAsync())
            {
                classes.Add(c);
            }
        }
    }    
}
