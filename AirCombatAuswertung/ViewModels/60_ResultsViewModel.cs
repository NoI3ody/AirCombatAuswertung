using AirCombatAuswertung.Interfaces;
using AirCombatAuswertung.Model;
using System.Threading.Tasks;

namespace AirCombatAuswertung.ViewModels
{
    public class _60_ResultsViewModel : BindableBase
    {
        public _60_ResultsViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }
        private Class _SelClass;
        public Class SelClass
        {
            get => _SelClass;
            set
            {
                if (!SetProperty(ref _SelClass, value, nameof(SelClass)))
                    return;
            }
        }
        #region Commandbar Classes
        private Microsoft.UI.Xaml.Visibility _cbWW2 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbWW2
        {
            get => _cbWW2;
            set
            {
                if (!SetProperty(ref _cbWW2, value, nameof(CbWW2)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbWW1 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbWW1
        {
            get => _cbWW1;
            set
            {
                if (!SetProperty(ref _cbWW1, value, nameof(CbWW1)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbEPA = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbEPA
        {
            get => _cbEPA;
            set
            {
                if (!SetProperty(ref _cbEPA, value, nameof(CbEPA)))
                    return;
            }
        }
        private bool _WW2sel;
        public bool WW2sel
        {
            get => _WW2sel;
            set
            {
                if (!SetProperty(ref _WW2sel, value, nameof(WW2sel)))
                    return;
                if (value)
                {
                    WW1sel = false;
                    EPAsel = false;
                    WW2selen = false;
                }
                else { WW2selen = true; }
                GetClassbyName("WW2");
            }
        }
        private bool _WW1sel;
        public bool WW1sel
        {
            get => _WW1sel;
            set
            {
                if (!SetProperty(ref _WW1sel, value, nameof(WW1sel)))
                    return;
                if (value)
                {
                    WW2sel = false;
                    EPAsel = false;
                    WW1selen = false;
                }
                else { WW1selen = true; }
                GetClassbyName("WW1");
            }
        }
        private bool _EPAsel;
        public bool EPAsel
        {
            get => _EPAsel;
            set
            {
                if (!SetProperty(ref _EPAsel, value, nameof(EPAsel)))
                    return;
                if (value)
                {
                    WW2sel = false;
                    WW1sel = false;
                    EPAselen = false;
                }
                else { EPAselen = true; }
                GetClassbyName("EPA");
            }
        }
        private bool _WW2selen = true;
        public bool WW2selen
        {
            get => _WW2selen;
            set
            {
                if (!SetProperty(ref _WW2selen, value, nameof(WW2selen)))
                    return;
            }
        }
        private bool _WW1selen = true;
        public bool WW1selen
        {
            get => _WW1selen;
            set
            {
                if (!SetProperty(ref _WW1selen, value, nameof(WW1selen)))
                    return;
            }
        }
        private bool _EPAselen = true;
        public bool EPAselen
        {
            get => _EPAselen;
            set
            {
                if (!SetProperty(ref _EPAselen, value, nameof(EPAselen)))
                    return;
            }
        }
        #endregion
        public async Task InitializeResultsDataAsync()
        {
            //Classes visibility
            foreach (var c in await _dataService.GetClassesAsync())
            {
                var isavailable = await GetAvailableClassAsync(c);
                if (isavailable)
                {
                    switch (c.Nr)
                    {
                        case 1:
                            CbWW2 = Microsoft.UI.Xaml.Visibility.Visible;
                            break;
                        case 2:
                            CbWW1 = Microsoft.UI.Xaml.Visibility.Visible;
                            break;
                        case 3:
                            CbEPA = Microsoft.UI.Xaml.Visibility.Visible;
                            break;
                        default: break;
                    }
                }
            }
        }
        private async void GetClassbyName(string Classname)
        {
            SelClass = await _dataService.GetClassByNameAsync(Classname);
        }
        public async Task<bool> GetAvailableClassAsync(Class c)
        {
            var flownClass = await _dataService.GetOptionByNameAsync("AnzRnd" + c.Nr);
            if (int.Parse(flownClass.Value) > 0) return true;
            else return false;
        }
    }
}
