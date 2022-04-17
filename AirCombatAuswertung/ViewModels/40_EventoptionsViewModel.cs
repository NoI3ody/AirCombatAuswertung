using AirCombatAuswertung.Interfaces;
using AirCombatAuswertung.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AirCombatAuswertung.ViewModels
{
    public class _40_EventoptionsViewModel : BindableBase
    {
        private bool noUpdate;
        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                if (!SetProperty(ref _location, value, nameof(Location)))
                    return;
                UpdateOption(nameof(Location), value);
            }
        }
        private int _location_MaxLength;
        public int Location_MaxLength
        {
            get => _location_MaxLength;
            set
            {
                if (!SetProperty(ref _location_MaxLength, value, nameof(Location_MaxLength)))
                    return;
            }
        }
        private DateTimeOffset _date = new DateTimeOffset(DateTime.Now.Year,DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0, TimeSpan.Zero); //DefaultDate = {01.01.1921 00:00:00}
        public DateTimeOffset Date
        {
            get => _date;
            set
            {
                if (!SetProperty(ref _date, value, nameof(Date)))
                    return;
                UpdateOption(nameof(Date), value.Date.ToShortDateString());
            }
        }
        private int _roundsWW2;
        public int RoundsWW2
        {
            get => _roundsWW2;
            set
            {
                if (!SetProperty(ref _roundsWW2, value, nameof(RoundsWW2)) || noUpdate)
                    return;
                UpdateOption("AnzRnd1", value.ToString());
                if (value > 0) CbFinalWW2 = Microsoft.UI.Xaml.Visibility.Visible;
                else
                {
                    CbFinalWW2 = Microsoft.UI.Xaml.Visibility.Collapsed;
                }
            }
        }
        private int _roundsWW1;
        public int RoundsWW1
        {
            get => _roundsWW1;
            set
            {
                if (!SetProperty(ref _roundsWW1, value, nameof(RoundsWW1)) || noUpdate)
                    return;
                UpdateOption("AnzRnd2", value.ToString());
                if (value > 0) CbFinalWW1 = Microsoft.UI.Xaml.Visibility.Visible;
                else
                {
                    CbFinalWW1 = Microsoft.UI.Xaml.Visibility.Collapsed;
                }
            }
        }
        private int _roundsEPA;
        public int RoundsEPA
        {
            get => _roundsEPA;
            set
            {
                if (!SetProperty(ref _roundsEPA, value, nameof(RoundsEPA)) || noUpdate)
                    return;
                UpdateOption("AnzRnd3", value.ToString());
                if (value > 0) CbFinalEPA = Microsoft.UI.Xaml.Visibility.Visible;
                else
                {
                    CbFinalEPA = Microsoft.UI.Xaml.Visibility.Collapsed;
                }
            }
        }
        private int _roundsWW2_min;
        public int RoundsWW2_min
        {
            get => _roundsWW2_min;
            set
            {
                if (!SetProperty(ref _roundsWW2_min, value, nameof(RoundsWW2_min)))
                    return;
            }
        }
        private int _roundsWW2_max;
        public int RoundsWW2_max
        {
            get => _roundsWW2_max;
            set
            {
                if (!SetProperty(ref _roundsWW2_max, value, nameof(RoundsWW2_max)))
                    return;
            }
        }
        private int _roundsWW1_min;
        public int RoundsWW1_min
        {
            get => _roundsWW1_min;
            set
            {
                if (!SetProperty(ref _roundsWW1_min, value, nameof(RoundsWW1_min)))
                    return;
            }
        }
        private int _roundsWW1_max;
        public int RoundsWW1_max
        {
            get => _roundsWW1_max;
            set
            {
                if (!SetProperty(ref _roundsWW1_max, value, nameof(RoundsWW1_max)))
                    return;
            }
        }
        private int _roundsEPA_min;
        public int RoundsEPA_min
        {
            get => _roundsEPA_min;
            set
            {
                if (!SetProperty(ref _roundsEPA_min, value, nameof(RoundsEPA_min)))
                    return;
            }
        }
        private int _roundsEPA_max;
        public int RoundsEPA_max
        {
            get => _roundsEPA_max;
            set
            {
                if (!SetProperty(ref _roundsEPA_max, value, nameof(RoundsEPA_max)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbFinalWW2 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbFinalWW2
        {
            get => _cbFinalWW2;
            set
            {
                if (!SetProperty(ref _cbFinalWW2, value, nameof(CbFinalWW2)))
                    return;
                if (value==Microsoft.UI.Xaml.Visibility.Collapsed)
                {
                    FinalWW2 = false;
                }
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbFinalWW1 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbFinalWW1
        {
            get => _cbFinalWW1;
            set
            {
                if (!SetProperty(ref _cbFinalWW1, value, nameof(CbFinalWW1)))
                    return;
                if (value == Microsoft.UI.Xaml.Visibility.Collapsed)
                {
                    FinalWW1 = false;
                }
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbFinalEPA = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbFinalEPA
        {
            get => _cbFinalEPA;
            set
            {
                if (!SetProperty(ref _cbFinalEPA, value, nameof(CbFinalEPA)))
                    return;
                if (value == Microsoft.UI.Xaml.Visibility.Collapsed)
                {
                    FinalEPA = false;
                }
            }
        }
        private bool _finalWW2;
        public bool FinalWW2
        {
            get => _finalWW2;
            set
            {
                if (!SetProperty(ref _finalWW2, value, nameof(FinalWW2)))
                    return;
                UpdateOption("Finale1", value.ToString());
                if (value)
                {
                    NbFinalPilotsWW2 = Microsoft.UI.Xaml.Visibility.Visible;
                    CbSemiFinalWW2 = Microsoft.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    NbFinalPilotsWW2 = Microsoft.UI.Xaml.Visibility.Collapsed;
                    CbSemiFinalWW2 = Microsoft.UI.Xaml.Visibility.Collapsed;
                }
            }
        }
        private bool _finalWW1;
        public bool FinalWW1
        {
            get => _finalWW1;
            set
            {
                if (!SetProperty(ref _finalWW1, value, nameof(FinalWW1)))
                    return;
                UpdateOption("Finale2", value.ToString());
                if (value)
                {
                    NbFinalPilotsWW1 = Microsoft.UI.Xaml.Visibility.Visible;
                    CbSemiFinalWW1 = Microsoft.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    NbFinalPilotsWW1 = Microsoft.UI.Xaml.Visibility.Collapsed;
                    CbSemiFinalWW1 = Microsoft.UI.Xaml.Visibility.Collapsed;
                }
            }
        }
        private bool _finalEPA;
        public bool FinalEPA
        {
            get => _finalEPA;
            set
            {
                if (!SetProperty(ref _finalEPA, value, nameof(FinalEPA)))
                    return;
                UpdateOption("Finale3", value.ToString());
                if (value)
                {
                    NbFinalPilotsEPA = Microsoft.UI.Xaml.Visibility.Visible;
                    CbSemiFinalEPA = Microsoft.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    NbFinalPilotsEPA = Microsoft.UI.Xaml.Visibility.Collapsed;
                    CbSemiFinalEPA = Microsoft.UI.Xaml.Visibility.Collapsed;
                }
            }
        }
        private int _finalPilotsWW2;
        public int FinalPilotsWW2
        {
            get => _finalPilotsWW2;
            set
            {
                if (!SetProperty(ref _finalPilotsWW2, value, nameof(FinalPilotsWW2))||noUpdate)
                    return;
                UpdateOption("AnzFin1", value.ToString());
            }
        }
        private int _finalPilotsWW1;
        public int FinalPilotsWW1
        {
            get => _finalPilotsWW1;
            set
            {
                if (!SetProperty(ref _finalPilotsWW1, value, nameof(FinalPilotsWW1)) || noUpdate)
                    return;
                UpdateOption("AnzFin2", value.ToString());
            }
        }
        private int _finalPilotsEPA;
        public int FinalPilotsEPA
        {
            get => _finalPilotsEPA;
            set
            {
                if (!SetProperty(ref _finalPilotsEPA, value, nameof(FinalPilotsEPA)) || noUpdate)
                    return;
                UpdateOption("AnzFin3", value.ToString());
            }
        }
        private int _finalPilotsWW2_min;
        public int FinalPilotsWW2_min
        {
            get => _finalPilotsWW2_min;
            set
            {
                if (!SetProperty(ref _finalPilotsWW2_min, value, nameof(FinalPilotsWW2_min)))
                    return;
            }
        }
        private int _finalPilotsWW2_max;
        public int FinalPilotsWW2_max
        {
            get => _finalPilotsWW2_max;
            set
            {
                if (!SetProperty(ref _finalPilotsWW2_max, value, nameof(FinalPilotsWW2_max)))
                    return;
            }
        }
        private int _finalPilotsWW1_min;
        public int FinalPilotsWW1_min
        {
            get => _finalPilotsWW1_min;
            set
            {
                if (!SetProperty(ref _finalPilotsWW1_min, value, nameof(FinalPilotsWW1_min)))
                    return;
            }
        }
        private int _finalPilotsWW1_max;
        public int FinalPilotsWW1_max
        {
            get => _finalPilotsWW1_max;
            set
            {
                if (!SetProperty(ref _finalPilotsWW1_max, value, nameof(FinalPilotsWW1_max)))
                    return;
            }
        }
        private int _finalPilotsEPA_min;
        public int FinalPilotsEPA_min
        {
            get => _finalPilotsEPA_min;
            set
            {
                if (!SetProperty(ref _finalPilotsEPA_min, value, nameof(FinalPilotsEPA_min)))
                    return;
            }
        }
        private int _finalPilotsEPA_max;
        public int FinalPilotsEPA_max
        {
            get => _finalPilotsEPA_max;
            set
            {
                if (!SetProperty(ref _finalPilotsEPA_max, value, nameof(FinalPilotsEPA_max)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _nbFinalPilotsWW2 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility NbFinalPilotsWW2
        {
            get => _nbFinalPilotsWW2;
            set
            {
                if (!SetProperty(ref _nbFinalPilotsWW2, value, nameof(NbFinalPilotsWW2)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _nbFinalPilotsWW1 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility NbFinalPilotsWW1
        {
            get => _nbFinalPilotsWW1;
            set
            {
                if (!SetProperty(ref _nbFinalPilotsWW1, value, nameof(NbFinalPilotsWW1)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _nbFinalPilotsEPA = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility NbFinalPilotsEPA
        {
            get => _nbFinalPilotsEPA;
            set
            {
                if (!SetProperty(ref _nbFinalPilotsEPA, value, nameof(NbFinalPilotsEPA)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbSemiFinalWW2 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbSemiFinalWW2
        {
            get => _cbSemiFinalWW2;
            set
            {
                if (!SetProperty(ref _cbSemiFinalWW2, value, nameof(CbSemiFinalWW2)))
                    return;
                if (value == Microsoft.UI.Xaml.Visibility.Collapsed)
                {
                    SemiFinalWW2 = false;
                }
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbSemiFinalWW1 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbSemiFinalWW1
        {
            get => _cbSemiFinalWW1;
            set
            {
                if (!SetProperty(ref _cbSemiFinalWW1, value, nameof(CbSemiFinalWW1)))
                    return;
                if (value == Microsoft.UI.Xaml.Visibility.Collapsed)
                {
                    SemiFinalWW1 = false;
                }
            }
        }
        private Microsoft.UI.Xaml.Visibility _cbSemiFinalEPA = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility CbSemiFinalEPA
        {
            get => _cbSemiFinalEPA;
            set
            {
                if (!SetProperty(ref _cbSemiFinalEPA, value, nameof(CbSemiFinalEPA)))
                    return;
                if (value == Microsoft.UI.Xaml.Visibility.Collapsed)
                {
                    SemiFinalEPA = false;
                }
            }
        }
        private bool _semifinalWW2;
        public bool SemiFinalWW2
        {
            get => _semifinalWW2;
            set
            {
                if (!SetProperty(ref _semifinalWW2, value, nameof(SemiFinalWW2)))
                    return;
                UpdateOption("Semi1", value.ToString());
                if (value)
                {
                    NbSemiFinalPilotsWW2 = Microsoft.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    NbSemiFinalPilotsWW2 = Microsoft.UI.Xaml.Visibility.Collapsed;
                }
            }
        }
        private bool _semifinalWW1;
        public bool SemiFinalWW1
        {
            get => _semifinalWW1;
            set
            {
                if (!SetProperty(ref _semifinalWW1, value, nameof(SemiFinalWW1)))
                    return;
                UpdateOption("Semi2", value.ToString());
                if (value)
                {
                    NbSemiFinalPilotsWW1 = Microsoft.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    NbSemiFinalPilotsWW1 = Microsoft.UI.Xaml.Visibility.Collapsed;
                }
            }
        }
        private bool _semifinalEPA;
        public bool SemiFinalEPA
        {
            get => _semifinalEPA;
            set
            {
                if (!SetProperty(ref _semifinalEPA, value, nameof(SemiFinalEPA)))
                    return;
                UpdateOption("Semi3", value.ToString());
                if (value)
                {
                    NbSemiFinalPilotsEPA = Microsoft.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    NbSemiFinalPilotsEPA = Microsoft.UI.Xaml.Visibility.Collapsed;
                }
            }
        }
        private int _semifinalPilotsWW2;
        public int SemiFinalPilotsWW2
        {
            get => _semifinalPilotsWW2;
            set
            {
                if (!SetProperty(ref _semifinalPilotsWW2, value, nameof(SemiFinalPilotsWW2)) || noUpdate)
                    return;
                UpdateOption("AnzSemi1", value.ToString());
            }
        }
        private int _semifinalPilotsWW1;
        public int SemiFinalPilotsWW1
        {
            get => _semifinalPilotsWW1;
            set
            {
                if (!SetProperty(ref _semifinalPilotsWW1, value, nameof(SemiFinalPilotsWW1)) || noUpdate)
                    return;
                UpdateOption("AnzSemi2", value.ToString());
            }
        }
        private int _semifinalPilotsEPA;
        public int SemiFinalPilotsEPA
        {
            get => _semifinalPilotsEPA;
            set
            {
                if (!SetProperty(ref _semifinalPilotsEPA, value, nameof(SemiFinalPilotsEPA)) || noUpdate)
                    return;
                UpdateOption("AnzSemi3", value.ToString());
            }
        }
        private int _semifinalPilotsWW2_min;
        public int SemiFinalPilotsWW2_min
        {
            get => _semifinalPilotsWW2_min;
            set
            {
                if (!SetProperty(ref _semifinalPilotsWW2_min, value, nameof(SemiFinalPilotsWW2_min)))
                    return;
            }
        }
        private int _semifinalPilotsWW2_max;
        public int SemiFinalPilotsWW2_max
        {
            get => _semifinalPilotsWW2_max;
            set
            {
                if (!SetProperty(ref _semifinalPilotsWW2_max, value, nameof(SemiFinalPilotsWW2_max)))
                    return;
            }
        }
        private int _semifinalPilotsWW1_min;
        public int SemiFinalPilotsWW1_min
        {
            get => _semifinalPilotsWW1_min;
            set
            {
                if (!SetProperty(ref _semifinalPilotsWW1_min, value, nameof(SemiFinalPilotsWW1_min)))
                    return;
            }
        }
        private int _semifinalPilotsWW1_max;
        public int SemiFinalPilotsWW1_max
        {
            get => _semifinalPilotsWW1_max;
            set
            {
                if (!SetProperty(ref _semifinalPilotsWW1_max, value, nameof(SemiFinalPilotsWW1_max)))
                    return;
            }
        }
        private int _semifinalPilotsEPA_min;
        public int SemiFinalPilotsEPA_min
        {
            get => _semifinalPilotsEPA_min;
            set
            {
                if (!SetProperty(ref _semifinalPilotsEPA_min, value, nameof(SemiFinalPilotsEPA_min)))
                    return;
            }
        }
        private int _semifinalPilotsEPA_max;
        public int SemiFinalPilotsEPA_max
        {
            get => _semifinalPilotsEPA_max;
            set
            {
                if (!SetProperty(ref _semifinalPilotsEPA_max, value, nameof(SemiFinalPilotsEPA_max)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _nbSemiFinalPilotsWW2 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility NbSemiFinalPilotsWW2
        {
            get => _nbSemiFinalPilotsWW2;
            set
            {
                if (!SetProperty(ref _nbSemiFinalPilotsWW2, value, nameof(NbSemiFinalPilotsWW2)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _nbSemiFinalPilotsWW1 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility NbSemiFinalPilotsWW1
        {
            get => _nbSemiFinalPilotsWW1;
            set
            {
                if (!SetProperty(ref _nbSemiFinalPilotsWW1, value, nameof(NbSemiFinalPilotsWW1)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _nbSemiFinalPilotsEPA = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility NbSemiFinalPilotsEPA
        {
            get => _nbSemiFinalPilotsEPA;
            set
            {
                if (!SetProperty(ref _nbSemiFinalPilotsEPA, value, nameof(NbSemiFinalPilotsEPA)))
                    return;
            }
        }

        public _40_EventoptionsViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }
        public async Task InitializeEventoptionsDataAsync()
        {
            GetLocation();
            GetDate();
            foreach (var c in await _dataService.GetClassesAsync())
            {
                GetRounds(c);
                GetFinal(c);
                GetFinalPilots(c);
                GetSemiFinal(c);
                GetSemiFinalPilots(c);
            }
        }
        private async void GetLocation()
        {
            var location = await _dataService.GetOptionByNameAsync("Location");
            Location = location.Value;
            Location_MaxLength = location.Max;
        }
        private async void GetDate()
        {
            var date = await _dataService.GetOptionByNameAsync("Date");
            Date = DateTime.Parse(date.Value);
        }
        private async void GetRounds(Class c)
        {
            var rounds = await _dataService.GetOptionByNameAsync("AnzRnd" + c.Nr);
            switch (c.Nr)
            {
                case 1:
                    noUpdate = true;
                    RoundsWW2_min = rounds.Min;
                    RoundsWW2_max = rounds.Max;
                    noUpdate = false;
                    RoundsWW2 = int.Parse(rounds.Value);
                    return;

                case 2:
                    noUpdate = true;
                    RoundsWW1_min = rounds.Min;
                    RoundsWW1_max = rounds.Max;
                    noUpdate = false;
                    RoundsWW1 = int.Parse(rounds.Value);
                    return;

                case 3:
                    noUpdate = true;
                    RoundsEPA_min = rounds.Min;
                    RoundsEPA_max = rounds.Max;
                    noUpdate = false;
                    RoundsEPA = int.Parse(rounds.Value);
                    return;

                default: return;
            }

        }
        private async void GetFinal(Class c)
        {
            var finals = await _dataService.GetOptionByNameAsync("Finale" + c.Nr);
            switch (c.Nr)
            {
                case 1:
                    FinalWW2 = bool.Parse(finals.Value);
                    return;

                case 2:
                    FinalWW1 = bool.Parse(finals.Value);
                    return;

                case 3:
                    FinalEPA = bool.Parse(finals.Value);
                    return;

                default: return;
            }
        }
        private async void GetFinalPilots(Class c)
        {
            var finalPilots = await _dataService.GetOptionByNameAsync("AnzFin" + c.Nr);
            switch (c.Nr)
            {
                case 1:
                    noUpdate = true;
                    FinalPilotsWW2_min = finalPilots.Min;
                    FinalPilotsWW2_max = finalPilots.Max;
                    noUpdate = false;
                    FinalPilotsWW2 = int.Parse(finalPilots.Value);
                    return;

                case 2:
                    noUpdate = true;
                    FinalPilotsWW1_min = finalPilots.Min;
                    FinalPilotsWW1_max = finalPilots.Max;
                    noUpdate = false;
                    FinalPilotsWW1 = int.Parse(finalPilots.Value);
                    return;

                case 3:
                    noUpdate = true;
                    FinalPilotsEPA_min = finalPilots.Min;
                    FinalPilotsEPA_max = finalPilots.Max;
                    noUpdate = false;
                    FinalPilotsEPA = int.Parse(finalPilots.Value);
                    return;

                default: return;
            }

        }
        private async void GetSemiFinal(Class c)
        {
            var semifinals = await _dataService.GetOptionByNameAsync("Semi" + c.Nr);
            switch (c.Nr)
            {
                case 1:
                    SemiFinalWW2 = bool.Parse(semifinals.Value);
                    return;

                case 2:
                    SemiFinalWW1 = bool.Parse(semifinals.Value);
                    return;

                case 3:
                    SemiFinalEPA = bool.Parse(semifinals.Value);
                    return;

                default: return;
            }
        }
        private async void GetSemiFinalPilots(Class c)
        {
            var semifinalPilots = await _dataService.GetOptionByNameAsync("AnzSemi" + c.Nr);
            switch (c.Nr)
            {
                case 1:
                    noUpdate = true;
                    SemiFinalPilotsWW2_min = semifinalPilots.Min;
                    SemiFinalPilotsWW2_max = semifinalPilots.Max;
                    noUpdate = false;
                    SemiFinalPilotsWW2 = int.Parse(semifinalPilots.Value);
                    return;

                case 2:
                    noUpdate = true;
                    SemiFinalPilotsWW1_min = semifinalPilots.Min;
                    SemiFinalPilotsWW1_max = semifinalPilots.Max;
                    noUpdate = false;
                    SemiFinalPilotsWW1 = int.Parse(semifinalPilots.Value);
                    return;

                case 3:
                    noUpdate = true;
                    SemiFinalPilotsEPA_min = semifinalPilots.Min;
                    SemiFinalPilotsEPA_max = semifinalPilots.Max;
                    noUpdate = false;
                    SemiFinalPilotsEPA = int.Parse(semifinalPilots.Value);
                    return;

                default: return;
            }

        }
        private async void UpdateOption(string Optionname, string value)
        {
            Option newOption = new Option();
            newOption.Name = Optionname;
            newOption.Value = value;
            await _dataService.UpdateOptionByNameAsync(newOption);
        }
    }
}
