using AirCombatAuswertung.Interfaces;
using AirCombatAuswertung.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AirCombatAuswertung.ViewModels
{
    public class _60_ResultsViewModel : BindableBase
    {
        private ObservableCollection<ResultShow> _results = new ObservableCollection<ResultShow>();
        public _60_ResultsViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Rnd1Command = new RelayCommand(SetRnd1);
            Rnd2Command = new RelayCommand(SetRnd2);
            Rnd3Command = new RelayCommand(SetRnd3);
            Rnd4Command = new RelayCommand(SetRnd4);
            Rnd5Command = new RelayCommand(SetRnd5);
            Rnd6Command = new RelayCommand(SetRnd6);
            Rnd7Command = new RelayCommand(SetRnd7);
            Rnd8Command = new RelayCommand(SetRnd8);
            Rnd9Command = new RelayCommand(SetRnd9);
            Rnd10Command = new RelayCommand(SetRnd10);
            RndSCommand = new RelayCommand(SetRndS);
            RndFCommand = new RelayCommand(SetRndF);
        }
        private Class _SelClass;
        public Class SelClass
        {
            get => _SelClass;
            set
            {
                if (!SetProperty(ref _SelClass, value, nameof(SelClass)))
                    return;
                EnableRoundButtons(SelClass);
                UnselectRoundButtons();
            }
        }
        private int _SelRound;
        public int SelRound
        {
            get => _SelRound;
            set
            {
                if (!SetProperty(ref _SelRound, value, nameof(SelRound)))
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
        #region Commandbar Rounds
        #region Commandbar Rounds Visibility
        private Microsoft.UI.Xaml.Visibility _cbRnd1 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbRnd1
        {
            get => _cbRnd1;
            set
            {
                if (!SetProperty(ref _cbRnd1, value, nameof(CbRnd1)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbRnd2 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbRnd2
        {
            get => _cbRnd2;
            set
            {
                if (!SetProperty(ref _cbRnd2, value, nameof(CbRnd2)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbRnd3 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbRnd3
        {
            get => _cbRnd3;
            set
            {
                if (!SetProperty(ref _cbRnd3, value, nameof(CbRnd3)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbRnd4 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbRnd4
        {
            get => _cbRnd4;
            set
            {
                if (!SetProperty(ref _cbRnd4, value, nameof(CbRnd4)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbRnd5 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbRnd5
        {
            get => _cbRnd5;
            set
            {
                if (!SetProperty(ref _cbRnd5, value, nameof(CbRnd5)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbRnd6 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbRnd6
        {
            get => _cbRnd6;
            set
            {
                if (!SetProperty(ref _cbRnd6, value, nameof(CbRnd6)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbRnd7 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbRnd7
        {
            get => _cbRnd7;
            set
            {
                if (!SetProperty(ref _cbRnd7, value, nameof(CbRnd7)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbRnd8 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbRnd8
        {
            get => _cbRnd8;
            set
            {
                if (!SetProperty(ref _cbRnd8, value, nameof(CbRnd8)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbRnd9 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbRnd9
        {
            get => _cbRnd9;
            set
            {
                if (!SetProperty(ref _cbRnd9, value, nameof(CbRnd9)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbRnd10 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbRnd10
        {
            get => _cbRnd10;
            set
            {
                if (!SetProperty(ref _cbRnd10, value, nameof(CbRnd10)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbRndSemi = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbRndSemi
        {
            get => _cbRndSemi;
            set
            {
                if (!SetProperty(ref _cbRndSemi, value, nameof(CbRndSemi)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbRndFinal = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbRndFinal
        {
            get => _cbRndFinal;
            set
            {
                if (!SetProperty(ref _cbRndFinal, value, nameof(CbRndFinal)))
                    return;
            }
        }
        #endregion
        #region Commandbar Rounds Checked
        private bool _CbRnd1sel;
        public bool CbRnd1sel
        {
            get => _CbRnd1sel;
            set
            {
                if (!SetProperty(ref _CbRnd1sel, value, nameof(CbRnd1sel)))
                    return;
                if (value)
                {
                    CbRnd2sel = CbRnd3sel = CbRnd4sel = CbRnd5sel = CbRnd6sel = CbRnd7sel = CbRnd8sel = CbRnd9sel = CbRnd10sel = CbRndSemisel = CbRndFinalsel = false;
                    CbRnd1selen = false;
                }
                else { CbRnd1selen = true; }
            }
        }
        private bool _CbRnd2sel;
        public bool CbRnd2sel
        {
            get => _CbRnd2sel;
            set
            {
                if (!SetProperty(ref _CbRnd2sel, value, nameof(CbRnd2sel)))
                    return;
                if (value)
                {
                    CbRnd1sel = CbRnd3sel = CbRnd4sel = CbRnd5sel = CbRnd6sel = CbRnd7sel = CbRnd8sel = CbRnd9sel = CbRnd10sel = CbRndSemisel = CbRndFinalsel = false;
                    CbRnd2selen = false;
                }
                else { CbRnd2selen = true; }
            }
        }
        private bool _CbRnd3sel;
        public bool CbRnd3sel
        {
            get => _CbRnd3sel;
            set
            {
                if (!SetProperty(ref _CbRnd3sel, value, nameof(CbRnd3sel)))
                    return;
                if (value)
                {
                    CbRnd1sel = CbRnd2sel = CbRnd4sel = CbRnd5sel = CbRnd6sel = CbRnd7sel = CbRnd8sel = CbRnd9sel = CbRnd10sel = CbRndSemisel = CbRndFinalsel = false;
                    CbRnd3selen = false;
                }
                else { CbRnd3selen = true; }
            }
        }
        private bool _CbRnd4sel;
        public bool CbRnd4sel
        {
            get => _CbRnd4sel;
            set
            {
                if (!SetProperty(ref _CbRnd4sel, value, nameof(CbRnd4sel)))
                    return;
                if (value)
                {
                    CbRnd1sel = CbRnd2sel = CbRnd3sel = CbRnd5sel = CbRnd6sel = CbRnd7sel = CbRnd8sel = CbRnd9sel = CbRnd10sel = CbRndSemisel = CbRndFinalsel = false;
                    CbRnd4selen = false;
                }
                else { CbRnd4selen = true; }
            }
        }
        private bool _CbRnd5sel;
        public bool CbRnd5sel
        {
            get => _CbRnd5sel;
            set
            {
                if (!SetProperty(ref _CbRnd5sel, value, nameof(CbRnd5sel)))
                    return;
                if (value)
                {
                    CbRnd1sel = CbRnd2sel = CbRnd3sel = CbRnd4sel = CbRnd6sel = CbRnd7sel = CbRnd8sel = CbRnd9sel = CbRnd10sel = CbRndSemisel = CbRndFinalsel = false;
                    CbRnd5selen = false;
                }
                else { CbRnd5selen = true; }
            }
        }
        private bool _CbRnd6sel;
        public bool CbRnd6sel
        {
            get => _CbRnd6sel;
            set
            {
                if (!SetProperty(ref _CbRnd6sel, value, nameof(CbRnd6sel)))
                    return;
                if (value)
                {
                    CbRnd1sel = CbRnd2sel = CbRnd3sel = CbRnd4sel = CbRnd5sel = CbRnd7sel = CbRnd8sel = CbRnd9sel = CbRnd10sel = CbRndSemisel = CbRndFinalsel = false;
                    CbRnd6selen = false;
                }
                else { CbRnd6selen = true; }
            }
        }
        private bool _CbRnd7sel;
        public bool CbRnd7sel
        {
            get => _CbRnd7sel;
            set
            {
                if (!SetProperty(ref _CbRnd7sel, value, nameof(CbRnd7sel)))
                    return;
                if (value)
                {
                    CbRnd1sel = CbRnd2sel = CbRnd3sel = CbRnd4sel = CbRnd5sel = CbRnd6sel = CbRnd8sel = CbRnd9sel = CbRnd10sel = CbRndSemisel = CbRndFinalsel = false;
                    CbRnd7selen = false;
                }
                else { CbRnd7selen = true; }
            }
        }
        private bool _CbRnd8sel;
        public bool CbRnd8sel
        {
            get => _CbRnd8sel;
            set
            {
                if (!SetProperty(ref _CbRnd8sel, value, nameof(CbRnd8sel)))
                    return;
                if (value)
                {
                    CbRnd1sel = CbRnd2sel = CbRnd3sel = CbRnd4sel = CbRnd5sel = CbRnd6sel = CbRnd7sel = CbRnd9sel = CbRnd10sel = CbRndSemisel = CbRndFinalsel = false;
                    CbRnd8selen = false;
                }
                else { CbRnd8selen = true; }
            }
        }
        private bool _CbRnd9sel;
        public bool CbRnd9sel
        {
            get => _CbRnd9sel;
            set
            {
                if (!SetProperty(ref _CbRnd9sel, value, nameof(CbRnd9sel)))
                    return;
                if (value)
                {
                    CbRnd1sel = CbRnd2sel = CbRnd3sel = CbRnd4sel = CbRnd5sel = CbRnd6sel = CbRnd7sel = CbRnd8sel = CbRnd10sel = CbRndSemisel = CbRndFinalsel = false;
                    CbRnd9selen = false;
                }
                else { CbRnd9selen = true; }
            }
        }
        private bool _CbRnd10sel;
        public bool CbRnd10sel
        {
            get => _CbRnd10sel;
            set
            {
                if (!SetProperty(ref _CbRnd10sel, value, nameof(CbRnd10sel)))
                    return;
                if (value)
                {
                    CbRnd1sel = CbRnd2sel = CbRnd3sel = CbRnd4sel = CbRnd5sel = CbRnd6sel = CbRnd7sel = CbRnd8sel = CbRnd9sel = CbRndSemisel = CbRndFinalsel = false;
                    CbRnd10selen = false;
                }
                else { CbRnd10selen = true; }
            }
        }
        private bool _CbRndSemisel;
        public bool CbRndSemisel
        {
            get => _CbRndSemisel;
            set
            {
                if (!SetProperty(ref _CbRndSemisel, value, nameof(CbRndSemisel)))
                    return;
                if (value)
                {
                    CbRnd1sel = CbRnd2sel = CbRnd3sel = CbRnd4sel = CbRnd5sel = CbRnd6sel = CbRnd7sel = CbRnd8sel = CbRnd9sel = CbRnd10sel = CbRndFinalsel = false;
                    CbRndSemiselen = false;
                }
                else { CbRndSemiselen = true; }
            }
        }
        private bool _CbRndFinalsel;
        public bool CbRndFinalsel
        {
            get => _CbRndFinalsel;
            set
            {
                if (!SetProperty(ref _CbRndFinalsel, value, nameof(CbRndFinalsel)))
                    return;
                if (value)
                {
                    CbRnd1sel = CbRnd2sel = CbRnd3sel = CbRnd4sel = CbRnd5sel = CbRnd6sel = CbRnd7sel = CbRnd8sel = CbRnd9sel = CbRnd10sel = CbRndSemisel = false;
                    CbRndFinalselen = false;
                }
                else { CbRndFinalselen = true; }
            }
        }
        #endregion
        #region Commandbar Rounds Enable
        private bool _CbRnd1selen = true;
        public bool CbRnd1selen
        {
            get => _CbRnd1selen;
            set
            {
                if (!SetProperty(ref _CbRnd1selen, value, nameof(CbRnd1selen)))
                    return;
            }
        }
        private bool _CbRnd2selen = true;
        public bool CbRnd2selen
        {
            get => _CbRnd2selen;
            set
            {
                if (!SetProperty(ref _CbRnd2selen, value, nameof(CbRnd2selen)))
                    return;
            }
        }
        private bool _CbRnd3selen = true;
        public bool CbRnd3selen
        {
            get => _CbRnd3selen;
            set
            {
                if (!SetProperty(ref _CbRnd3selen, value, nameof(CbRnd3selen)))
                    return;
            }
        }
        private bool _CbRnd4selen = true;
        public bool CbRnd4selen
        {
            get => _CbRnd4selen;
            set
            {
                if (!SetProperty(ref _CbRnd4selen, value, nameof(CbRnd4selen)))
                    return;
            }
        }
        private bool _CbRnd5selen = true;
        public bool CbRnd5selen
        {
            get => _CbRnd5selen;
            set
            {
                if (!SetProperty(ref _CbRnd5selen, value, nameof(CbRnd5selen)))
                    return;
            }
        }
        private bool _CbRnd6selen = true;
        public bool CbRnd6selen
        {
            get => _CbRnd6selen;
            set
            {
                if (!SetProperty(ref _CbRnd6selen, value, nameof(CbRnd6selen)))
                    return;
            }
        }
        private bool _CbRnd7selen = true;
        public bool CbRnd7selen
        {
            get => _CbRnd7selen;
            set
            {
                if (!SetProperty(ref _CbRnd7selen, value, nameof(CbRnd7selen)))
                    return;
            }
        }
        private bool _CbRnd8selen = true;
        public bool CbRnd8selen
        {
            get => _CbRnd8selen;
            set
            {
                if (!SetProperty(ref _CbRnd8selen, value, nameof(CbRnd8selen)))
                    return;
            }
        }
        private bool _CbRnd9selen = true;
        public bool CbRnd9selen
        {
            get => _CbRnd9selen;
            set
            {
                if (!SetProperty(ref _CbRnd9selen, value, nameof(CbRnd9selen)))
                    return;
            }
        }
        private bool _CbRnd10selen = true;
        public bool CbRnd10selen
        {
            get => _CbRnd10selen;
            set
            {
                if (!SetProperty(ref _CbRnd10selen, value, nameof(CbRnd10selen)))
                    return;
            }
        }
        private bool _CbRndSemiselen = true;
        public bool CbRndSemiselen
        {
            get => _CbRndSemiselen;
            set
            {
                if (!SetProperty(ref _CbRndSemiselen, value, nameof(CbRndSemiselen)))
                    return;
            }
        }
        private bool _CbRndFinalselen = true;
        public bool CbRndFinalselen
        {
            get => _CbRndFinalselen;
            set
            {
                if (!SetProperty(ref _CbRndFinalselen, value, nameof(CbRndFinalselen)))
                    return;
            }
        }
        #endregion
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
        private async void EnableRoundButtons(Class c)
        {
            var AnzRounds = await GetRoundsToFly(c);
            var MaxRounds = await GetMaxRoundsToFly(c);
            var SemiFinal = await GetSemiFinalToFly(c);
            var Final = await GetFinalToFly(c);
            CbRnd1 = CbRnd2 = CbRnd3 = CbRnd4 = CbRnd5 = CbRnd6 = CbRnd7 = CbRnd8 = CbRnd9 = CbRnd10 = CbRndSemi = CbRndFinal = Microsoft.UI.Xaml.Visibility.Visible;
            for (int i = AnzRounds + 1; i <= MaxRounds; i++)
            {
                switch (i)
                {
                    case 0:
                        break;
                    case 1:
                        CbRnd1 = Microsoft.UI.Xaml.Visibility.Collapsed;
                        break;
                    case 2:
                        CbRnd2 = Microsoft.UI.Xaml.Visibility.Collapsed;
                        break;
                    case 3:
                        CbRnd3 = Microsoft.UI.Xaml.Visibility.Collapsed;
                        break;
                    case 4:
                        CbRnd4 = Microsoft.UI.Xaml.Visibility.Collapsed;
                        break;
                    case 5:
                        CbRnd5 = Microsoft.UI.Xaml.Visibility.Collapsed;
                        break;
                    case 6:
                        CbRnd6 = Microsoft.UI.Xaml.Visibility.Collapsed;
                        break;
                    case 7:
                        CbRnd7 = Microsoft.UI.Xaml.Visibility.Collapsed;
                        break;
                    case 8:
                        CbRnd8 = Microsoft.UI.Xaml.Visibility.Collapsed;
                        break;
                    case 9:
                        CbRnd9 = Microsoft.UI.Xaml.Visibility.Collapsed;
                        break;
                    case 10:
                        CbRnd10 = Microsoft.UI.Xaml.Visibility.Collapsed;
                        break;
                    default:
                        break;
                }
            }
            if (!SemiFinal) { CbRndSemi = Microsoft.UI.Xaml.Visibility.Collapsed; }
            if (!Final) { CbRndFinal = Microsoft.UI.Xaml.Visibility.Collapsed; }
        }
        private async Task<int> GetRoundsToFly(Class c)
        {
            var option = await _dataService.GetOptionByNameAsync("AnzRnd" + c.Nr);
            int.TryParse(option.Value, out int retval);
            return retval;
        }
        private async Task<int> GetMaxRoundsToFly(Class c)
        {
            var option = await _dataService.GetOptionByNameAsync("AnzRnd" + c.Nr);
            return option.Max;
        }
        private async Task<bool> GetSemiFinalToFly(Class c)
        {
            var semi = await _dataService.GetOptionByNameAsync("Semi" + c.Nr);
            if (semi.Value == "True") return true;
            else return false;
        }
        private async Task<bool> GetFinalToFly(Class c)
        {
            var final = await _dataService.GetOptionByNameAsync("Finale" + c.Nr);
            if (final.Value == "True") return true;
            else return false;
        }
        private void UnselectRoundButtons()
        {
            SelRound = 0;
            CbRnd1sel = false;
            CbRnd2sel = false;
            CbRnd3sel = false;
            CbRnd4sel = false;
            CbRnd5sel = false;
            CbRnd6sel = false;
            CbRnd7sel = false;
            CbRnd8sel = false;
            CbRnd9sel = false;
            CbRnd10sel = false;
            CbRndSemisel = false;
            CbRndFinalsel = false;
        }
        public ObservableCollection<ResultShow> Results
        {
            get
            {
                return _results;
            }
            set
            {
                SetProperty(ref _results, value);
            }
        }
        #region Commands
        public ICommand Rnd1Command { get; set; }
        public ICommand Rnd2Command { get; set; }
        public ICommand Rnd3Command { get; set; }
        public ICommand Rnd4Command { get; set; }
        public ICommand Rnd5Command { get; set; }
        public ICommand Rnd6Command { get; set; }
        public ICommand Rnd7Command { get; set; }
        public ICommand Rnd8Command { get; set; }
        public ICommand Rnd9Command { get; set; }
        public ICommand Rnd10Command { get; set; }
        public ICommand RndSCommand { get; set; }
        public ICommand RndFCommand { get; set; }

        private void SetRnd1()
        {
            SelRound = 1;
        }
        private void SetRnd2()
        {
            SelRound = 2;
        }
        private void SetRnd3()
        {
            SelRound = 3;
        }
        private void SetRnd4()
        {
            SelRound = 4;
        }
        private void SetRnd5()
        {
            SelRound = 5;
        }
        private void SetRnd6()
        {
            SelRound = 6;
        }
        private void SetRnd7()
        {
            SelRound = 7;
        }
        private void SetRnd8()
        {
            SelRound = 8;
        }
        private void SetRnd9()
        {
            SelRound = 9;
        }
        private void SetRnd10()
        {
            SelRound = 10;
        }
        private void SetRndS()
        {
            SelRound = 11;
        }
        private void SetRndF()
        {
            SelRound = 12;
        }
        #endregion
    }
}
