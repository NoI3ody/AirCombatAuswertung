using AirCombatAuswertung.Interfaces;
using AirCombatAuswertung.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using Microsoft.Windows.ApplicationModel.Resources;

namespace AirCombatAuswertung.ViewModels
{
    public class _41_RegistrationViewModel : BindableBase
    {
        private ObservableCollection<Pilot> _pilots = new ObservableCollection<Pilot>();
        private ObservableCollection<Judge> _judges = new ObservableCollection<Judge>();
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (!SetProperty(ref _firstName, value, nameof(FirstName)))
                    return;
            }
        }
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (!SetProperty(ref _lastName, value, nameof(LastName)))
                    return;
            }
        }
        private string _nation;
        public string Nation
        {
            get => _nation;
            set
            {
                if (!SetProperty(ref _nation, value, nameof(Nation)))
                    return;
            }
        }
        private string _channel;
        public string Channel
        {
            get => _channel;
            set
            {
                if (!SetProperty(ref _channel, value, nameof(Channel)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _lClasses = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility LClasses
        {
            get => _lClasses;
            set
            {
                if (!SetProperty(ref _lClasses, value, nameof(LClasses)))
                    return;
            }
        }
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
        private bool _isJudge;
        public bool IsJudge
        {
            get => _isJudge;
            set
            {
                if (!SetProperty(ref _isJudge, value, nameof(IsJudge)))
                    return;
                var loader = new ResourceLoader();
                if (value) IsJudgeText = loader.GetString("Yes");
                else IsJudgeText = loader.GetString("No");
            }
        }
        private string _isJudgeText;
        public string IsJudgeText
        {
            get => _isJudgeText;
            set
            {
                if (!SetProperty(ref _isJudgeText, value, nameof(IsJudgeText)))
                    return;
            }
        }
        private bool _classWW2;
        public bool ClassWW2
        {
            get => _classWW2;
            set
            {
                if (!SetProperty(ref _classWW2, value, nameof(ClassWW2)))
                    return;
            }
        }
        private bool _classWW1;
        public bool ClassWW1
        {
            get => _classWW1;
            set
            {
                if (!SetProperty(ref _classWW1, value, nameof(ClassWW1)))
                    return;
            }
        }
        private bool _classEPA;
        public bool ClassEPA
        {
            get => _classEPA;
            set
            {
                if (!SetProperty(ref _classEPA, value, nameof(ClassEPA)))
                    return;
            }
        }
        private Pilot _selectedPilot;
        public Pilot SelectedPilot
        {
            get => _selectedPilot;
            set
            {
                if (!SetProperty(ref _selectedPilot, value, nameof(SelectedPilot)))
                    return;
                ((RelayCommand)DelPilotCommand).RaiseCanExecuteChanged();
            }
        }

        public _41_RegistrationViewModel(IDataService dataService)
        {
            _dataService = dataService;

            AddPilotCommand = new RelayCommand(AddPilot);
            DelPilotCommand = new RelayCommand(async () => await DelPilot(), CanDeletePilot);
            UpdPilotCommand = new RelayCommand(async () => await UpdPilot(SelectedPilot));
        }
        public async Task InitializeRegistrationDataAsync()
        {
            //Classes visibility
            foreach (var c in await _dataService.GetClassesAsync())
            {
                var isavailable = await GetAvailableClassAsync(c);
                if (isavailable)
                {
                    LClasses = Microsoft.UI.Xaml.Visibility.Visible;
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
            //Set IsJudge active
            IsJudge = true;

            _pilots.Clear();
            foreach (var p in await _dataService.GetPilotsAsync())
            {
                _pilots.Add(p);
            }
        }
        public async Task<bool> GetAvailableClassAsync(Class c)
        {
            var flownClass = await _dataService.GetOptionByNameAsync("AnzRnd" + c.Nr);
            if (int.Parse(flownClass.Value) > 0) return true;
            else return false;
        }
        public ObservableCollection<Pilot> Pilots
        {
            get
            {
                return _pilots;
            }
            set
            {
                SetProperty(ref _pilots, value);
            }
        }
        public ObservableCollection<Judge> Judges
        {
            get
            {
                return _judges;
            }
            set
            {
                SetProperty(ref _judges, value);
            }
        }
        public ICommand AddPilotCommand { get; set; }
        private async void AddPilot()
        {
            Pilot p = new Pilot
            {
                Firstname = FirstName,
                Lastname = LastName,
                Channel = ConvertStringtoNumber(Channel),
                Nation = Nation.ToUpper(),
                IsJudge = IsJudge,
                FliesWW2 = ClassWW2,
                FliesWW1 = ClassWW1,
                FliesEPA = ClassEPA
            };
            Pilot newPilot = await _dataService.AddPilotAsync(p);
            Pilots.Add(newPilot);

            FirstName = "";
            LastName = "";
            Channel = "";
            Nation = "";
            IsJudge = true;
            ClassWW2 = false;
            ClassWW1 = false;
            ClassEPA = false;
        }
        public ICommand DelPilotCommand { get; set; }
        private async Task DelPilot()
        {
            await _dataService.DeletePilotAsync(SelectedPilot);
            Pilots.Remove(SelectedPilot);
        }
        public ICommand UpdPilotCommand { get; set; }
        private async Task UpdPilot(Pilot p)
        {
            await _dataService.UpdatePilotAsync(p);
        }
        private float ConvertStringtoNumber(string text)
        {
            if (string.IsNullOrEmpty(text)) return -1;
            string sep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (text.Contains(".")) text = text.Replace(".", sep);
            else if (text.Contains(",")) text = text.Replace(",", sep);
            float num;
            float.TryParse(text, out num);
            return num;
        }
        private bool CanDeletePilot() => SelectedPilot != null;
    }
}
