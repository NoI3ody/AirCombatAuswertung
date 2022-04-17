using AirCombatAuswertung.Interfaces;
using AirCombatAuswertung.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;

namespace AirCombatAuswertung.ViewModels
{
    public class _42_StartlistViewModel : BindableBase
    {
        private ObservableCollection<StartlistShow> _startlist = new ObservableCollection<StartlistShow>();

        private Class _SelClass;
        public Class SelClass
        {
            get => _SelClass;
            set
            {
                if (!SetProperty(ref _SelClass, value, nameof(SelClass)))
                    return;
                GetPilotsPerClass(_SelClass);
            }
        }
        private double _pilotsfound = double.NaN;
        public double PilotsFound
        {
            get => _pilotsfound;
            set
            {
                if (!SetProperty(ref _pilotsfound, value, nameof(PilotsFound)))
                    return;
                GetRoundsToFly(_SelClass);
            }
        }
        private string _roundstofly;
        public string RoundsToFly
        {
            get => _roundstofly;
            set
            {
                if (!SetProperty(ref _roundstofly, value, nameof(RoundsToFly)))
                    return;
                GetHeats(_SelClass);
                GetIgnoredoubleFrequency(_SelClass);
                GetGeneratewithoutJudges(_SelClass);
                SetGenerateEnable();
            }
        }
        private double _numberofHeats = double.NaN;
        public double NumberofHeats
        {
            get => _numberofHeats;
            set
            {
                if (value == _numberofHeats)
                {
                    UpdatePilotsperHeat();
                    SetClassTitleText(_SelClass);
                    FillStartlist(_SelClass);
                }

                if (!SetProperty(ref _numberofHeats, value, nameof(NumberofHeats)))
                    return;
                if (value > 0)
                {
                    UpdateOption("AnzHeats" + SelClass.Nr, value.ToString());
                    UpdatePilotsperHeat();
                }
            }
        }
        private int _numberofHeatsMin;
        public int NumberofHeatsMin
        {
            get => _numberofHeatsMin;
            set
            {
                if (!SetProperty(ref _numberofHeatsMin, value, nameof(NumberofHeatsMin)))
                    return;
            }
        }
        private int _numberofHeatsMax;
        public int NumberofHeatsMax
        {
            get => _numberofHeatsMax;
            set
            {
                if (!SetProperty(ref _numberofHeatsMax, value, nameof(NumberofHeatsMax)))
                    return;
            }
        }
        private bool _numberofHeatsEnable;
        public bool NumberofHeatsEnable
        {
            get => _numberofHeatsEnable;
            set
            {
                if (!SetProperty(ref _numberofHeatsEnable, value, nameof(NumberofHeatsEnable)))
                    return;
            }
        }
        private string _pilotsperHeat;
        public string PilotsperHeat
        {
            get => _pilotsperHeat;
            set
            {
                if (!SetProperty(ref _pilotsperHeat, value, nameof(PilotsperHeat)))
                    return;
            }
        }
        private List<int> _pilotsperHeatList = new List<int>();
        public List<int> PilotsperHeatList
        {
            get => _pilotsperHeatList;
            set
            {
                if (!SetProperty(ref _pilotsperHeatList, value, nameof(PilotsperHeatList)))
                    return;
            }
        }
        private bool _ignoredoubleFrequency;
        public bool IgnoreDoubleFrequency
        {
            get => _ignoredoubleFrequency;
            set
            {
                if (!SetProperty(ref _ignoredoubleFrequency, value, nameof(IgnoreDoubleFrequency)))
                    return;
                UpdateOption("IgnoredoubleFrequency" + SelClass.Nr, value.ToString());
            }
        }
        private bool _ignoredoubleFrequencyEnable;
        public bool IgnoreDoubleFrequencyEnable
        {
            get => _ignoredoubleFrequencyEnable;
            set
            {
                if (!SetProperty(ref _ignoredoubleFrequencyEnable, value, nameof(IgnoreDoubleFrequencyEnable)))
                    return;
            }
        }
        private bool _generatewithoutJudges;
        public bool GeneratewithoutJudges
        {
            get => _generatewithoutJudges;
            set
            {
                if (!SetProperty(ref _generatewithoutJudges, value, nameof(GeneratewithoutJudges)))
                    return;
                UpdateOption("GeneratewithoutJudges" + SelClass.Nr, value.ToString());
            }
        }
        private bool _generatewithoutJudgesEnable;
        public bool GeneratewithoutJudgesEnable
        {
            get => _generatewithoutJudgesEnable;
            set
            {
                if (!SetProperty(ref _generatewithoutJudgesEnable, value, nameof(GeneratewithoutJudgesEnable)))
                    return;
            }
        }
        private bool _generateEnable;
        public bool GenerateEnable
        {
            get => _generateEnable;
            set
            {
                if (!SetProperty(ref _generateEnable, value, nameof(GenerateEnable)))
                    return;
            }
        }
        private string _classTitle;
        public string ClassTitle
        {
            get => _classTitle;
            set
            {
                if (!SetProperty(ref _classTitle, value, nameof(ClassTitle)))
                    return;
            }
        }
        #region Commandbar Buttons
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
        private bool _printenable;
        public bool PrintEnable
        {
            get => _printenable;
            set
            {
                if (!SetProperty(ref _printenable, value, nameof(PrintEnable)))
                    return;
            }
        }

        public _42_StartlistViewModel(IDataService dataService, IPrintService printService)
        {
            _dataService = dataService;
            _printService = printService;
            GenerateCommand = new RelayCommand(GenerateStartlist);
            PrintCommand = new RelayCommand(PrintStartlist);
        }
        public async Task InitializeStartlistDataAsync()
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
        public async Task<bool> GetAvailableClassAsync(Class c)
        {
            var flownClass = await _dataService.GetOptionByNameAsync("AnzRnd" + c.Nr);
            if (int.Parse(flownClass.Value) > 0) return true;
            else return false;
        }
        private async void GetClassbyName(string Classname)
        {
            SelClass = await _dataService.GetClassByNameAsync(Classname);
        }
        private async void GetPilotsPerClass(Class c)
        {
            PilotsFound = await _dataService.GetPilotsPerClassAsync(c);
        }
        private async void GetRoundsToFly(Class c)
        {
            var option = await _dataService.GetOptionByNameAsync("AnzRnd" + c.Nr);
            string text = string.Empty;
            text = option.Value;
            ResourceLoader loader = new ResourceLoader();
            if (text != "")
            {
                var semi = await _dataService.GetOptionByNameAsync("Semi" + c.Nr);
                if (semi.Value == "True")
                {
                    text += " + " + loader.GetString("42_Startlist_semifinal");
                }
                var final = await _dataService.GetOptionByNameAsync("Finale" + c.Nr);
                if (final.Value == "True")
                {
                    text += " + " + loader.GetString("42_Startlist_final");
                }
            }
            RoundsToFly = text;
        }
        private async void GetHeats(Class c)
        {
            var heats = await _dataService.GetOptionByNameAsync("AnzHeats" + c.Nr);
            NumberofHeatsMin = heats.Min;
            NumberofHeatsMax = (int)PilotsFound;
            NumberofHeatsEnable = true;
            double.TryParse(heats.Value, out double value);
            NumberofHeats = value;
        }
        private async void UpdateOption(string Optionname, string value)
        {
            Option newOption = new Option();
            newOption.Name = Optionname;
            newOption.Value = value;
            await _dataService.UpdateOptionByNameAsync(newOption);
        }
        private void UpdatePilotsperHeat(string output = "")
        {
            int rest;
            if (!double.IsNaN(NumberofHeats) && !(NumberofHeats == 0) && !double.IsNaN(PilotsFound) && !(PilotsFound == 0))
            {
                int pph = Math.DivRem((int)PilotsFound, (int)NumberofHeats, out rest);
                int i = (int)PilotsFound;
                PilotsperHeatList.Clear();
                int actHeatcount;
                do
                {
                    if (pph < i)
                    {
                        actHeatcount = pph;
                        if (rest > 0)
                        {
                            actHeatcount++;
                            rest--;
                        }
                    }
                    else
                    {
                        actHeatcount = i;
                    }
                    output += (output == "" ? "" : " | ") + actHeatcount.ToString();
                    i = i - actHeatcount;
                    PilotsperHeatList.Add(actHeatcount);
                } while (i > 0);
            }
            PilotsperHeat = output;
        }
        private async void GetIgnoredoubleFrequency(Class c)
        {
            var option = await _dataService.GetOptionByNameAsync("IgnoredoubleFrequency" + c.Nr);
            IgnoreDoubleFrequency = bool.Parse(option.Value);
            IgnoreDoubleFrequencyEnable = true;
        }
        private async void GetGeneratewithoutJudges(Class c)
        {
            var option = await _dataService.GetOptionByNameAsync("GeneratewithoutJudges" + c.Nr);
            GeneratewithoutJudges = bool.Parse(option.Value);
            GeneratewithoutJudgesEnable = true;
        }
        private void SetGenerateEnable()
        {
            GenerateEnable = true;
        }
        public ICommand GenerateCommand { get; set; }
        private async void GenerateStartlist()
        {
            //Lösche alle Startlisteneinträge der aktuellen Klasse
            await _dataService.ClearStartlistAsync(_SelClass);
            float freeFreq = 2.4f;
            Random Generator = new Random();
            var AnzRnd = await _dataService.GetOptionByNameAsync("AnzRnd" + _SelClass.Nr);
            for (int Runde = 1; Runde <= int.Parse(AnzRnd.Value); Runde++)
            {
                Dictionary<float, List<int>> freqList = new Dictionary<float, List<int>>();
                Dictionary<float, List<int>> heatList = new Dictionary<float, List<int>>();
                Dictionary<float, List<int>> judgList = new Dictionary<float, List<int>>();
                List<int> jul = new List<int>();
                var pilotslist = await _dataService.GetPilotsAsync();
                foreach (Pilot pilot in pilotslist)
                {
                    if ((pilot.FliesWW2 && _SelClass.Nr == 1) || (pilot.FliesWW1 && _SelClass.Nr == 2) || (pilot.FliesEPA && _SelClass.Nr == 3))
                    {
                        if (freqList.ContainsKey(pilot.Channel))
                        {
                            //Kanal vorhanden -> pilot hinzufügen
                            freqList[pilot.Channel].Add((int)pilot.Startnr);
                        }
                        else
                        {
                            //Kanal unbekannt -> neu anlegen
                            freqList.Add(pilot.Channel, new List<int> { (int)pilot.Startnr });
                        }
                        if (pilot.IsJudge) jul.Add((int)pilot.Startnr);
                    }
                }
                bool ok = true;
                if (!IgnoreDoubleFrequency)
                {
                    foreach (float freq in freqList.Keys)
                    {
                        if (freeFreq != freq && (freqList[freq].Count > NumberofHeats))
                        {
                            ok = false;
                            //MSG: Konflikt bei Frequenzen!!!
                        }
                    }
                }
                if (ok)
                {
                    //Befüllen der Heats mit leeren Listen
                    for (int i = 1; i <= _pilotsperHeatList.Count; i++) heatList.Add(i, new List<int>());
                    if (!IgnoreDoubleFrequency)
                    {
                        foreach (float freq in freqList.Keys)
                        {
                            if (freeFreq != freq)
                            {
                                //Zuweisung der gemeinsamen Frequenzen
                                foreach (int snum in freqList[freq])
                                {
                                    int r = Generator.Next(1, _pilotsperHeatList.Count + 1);
                                    int heat = r;
                                    bool done = false;
                                    do
                                    {
                                        bool isInList = false;
                                        foreach (int snum2 in freqList[freq])
                                        {
                                            if (heatList[heat].Contains(snum2))
                                            {
                                                isInList = true;
                                                break;
                                            }
                                        }
                                        if (heatList[heat].Count < _pilotsperHeatList[heat - 1] && !isInList)
                                        {
                                            heatList[heat].Add(snum);
                                            done = true;
                                        }
                                        else
                                        {
                                            //nur Auswerten wenn nicht letzer Pilot in dieser Frequenz!
                                            heat++;
                                            if (heat > _pilotsperHeatList.Count) heat = 1;
                                            if (heat == r)
                                            {
                                                //MSG Pilot nicht zuweisbar
                                                break;
                                            }
                                        }
                                    } while (!done);
                                }
                            }
                            else
                            {
                                //Freie Zuweisung auf Restplätze -> nächster For-Loop
                            }
                        }
                    }
                    foreach (float freq in freqList.Keys)
                    {
                        if (freeFreq == freq || IgnoreDoubleFrequency)
                        {
                            foreach (int snum in freqList[freq]) //durch die Piloten laufen
                            {
                                //Freie Zuweisung auf Restplätze
                                int r = Generator.Next(1, _pilotsperHeatList.Count + 1);
                                int heat = r;
                                bool done = false;
                                do
                                {
                                    //wenn die Anzahl der Piloten in der Liste kleiner ist als die maximale Pilotenanzahl für diesen Heat -> hinzufügen
                                    if (heatList[heat].Count < _pilotsperHeatList[heat - 1])
                                    {
                                        heatList[heat].Add(snum);
                                        done = true;
                                    }
                                    else
                                    {
                                        heat++;
                                        if (heat > _pilotsperHeatList.Count) heat = 1;
                                        if (heat == r)
                                        {
                                            //MSG Pilot nicht zuweisbar
                                            break;
                                        }
                                    }
                                } while (!done);
                            }
                        }
                    }
                    //Judges finden
                    if (heatList.Count > 1)
                    {
                        for (int heat = 1; heat <= heatList.Count; heat++)
                        {
                            //Muss angepasst werden auf die Datenbanksuche
                            var judgeslist = new List<Judge>();
                            List<int> l = new List<int>(); //Pilotenliste
                            List<int> pj = new List<int>();//Pilotenjudges
                            List<int> ej = new List<int>();//externe Judges
                            List<int> j = new List<int>();//Judgeliste
                            l.AddRange(heatList[heat].ToArray());
                            //Nachsehen ob externe Judges vorhanden sind
                            if (judgeslist.Count > 0 && !GeneratewithoutJudges)
                            {
                                foreach (Judge ejrow in judgeslist) ej.Add((int)ejrow.Startnr);
                                int ran = Generator.Next(0, ej.Count);
                                for (int i = ej.Count; i > 1; i--)
                                {
                                    ran = Generator.Next(0, ej.Count - 1);
                                    j.Add(ej[ran]);
                                    ej.RemoveAt(ran);
                                }
                            }
                            if (l.Count > j.Count && !GeneratewithoutJudges) //nicht genug externe Judges vorhanden mit Judges des Vorheats auffüllen
                            {
                                int jh = heat - 1; // Berechne Judgeheat
                                if (jh <= 0) jh = heatList.Count;
                                pj.AddRange(heatList[jh].ToArray());
                                int ran = Generator.Next(0, pj.Count - 1);
                                do
                                {
                                    ran = Generator.Next(0, pj.Count - 1);
                                    var pil = await _dataService.GetPilotAsync(pj[ran]);
                                    if (pil.IsJudge) j.Add(pj[ran]);
                                    pj.RemoveAt(ran);

                                } while (!(l.Count <= j.Count) && (pj.Count != 0));
                            }
                            if ((l.Count > j.Count) && !GeneratewithoutJudges) //nicht genug externe Judges und Pilotjudges vorhanden mit zufälligem Pilot auffüllen
                            {
                                int ran = Generator.Next(1, jul.Count);
                                do
                                {
                                    if (jul.Count == (l.Count + j.Count))
                                    {
                                        j.Add(0);
                                    }
                                    else
                                    {
                                        List<int> tjul = new List<int>();
                                        tjul.AddRange(jul);
                                        while (tjul.Count > 0 && (l.Contains(tjul[ran]) || j.Contains(tjul[ran])))
                                        {
                                            tjul.RemoveAt(ran);
                                            if (tjul.Count > 0) ran = Generator.Next(0, tjul.Count - 1);
                                        }
                                        if (tjul.Count > 0) j.Add(tjul[ran]);
                                        else j.Add(0);
                                    }
                                } while (!(l.Count <= j.Count));
                            }
                            judgList.Add(heat, j);
                        }
                    }
                    //Ausgabe pro Runde
                    for (int heat = 1; heat <= heatList.Count; heat++)
                    {
                        for (int i = 0; i < heatList[heat].Count; i++)
                        {
                            Startlist sl = new Startlist();
                            sl.Classnr = _SelClass.Nr;
                            sl.Round = Runde;
                            sl.Heat = heat;
                            sl.Pilotnr = heatList[heat][i];
                            if (judgList.Count > 0 && !GeneratewithoutJudges)
                            {
                                sl.Judgenr = judgList[heat][i];
                            }
                            else sl.Judgenr = 0;
                            await _dataService.CreateStartlistEntry(sl);
                        }
                    }
                }
            }
            FillStartlist(_SelClass);
        }
        private void SetClassTitleText(Class c)
        {
            ClassTitle = _SelClass.Name;
        }

        public ObservableCollection<StartlistShow> Startlist
        {
            get
            {
                return _startlist;
            }
            set
            {
                SetProperty(ref _startlist, value);
            }
        }
        private async void FillStartlist(Class c)
        {
            _startlist.Clear();
            var allrounds = await _dataService.GetAllRoundsfromStartlist(c);
            foreach (var r in allrounds)
            {
                var allheats = await _dataService.GetAllHeatsfromStartlist(c, r);
                foreach (var h in allheats)
                {
                    StartlistShow sls = new StartlistShow();
                    sls.Round = r;
                    sls.Heat = h;
                    sls.Frequency = (List<string>)await _dataService.GetAllFrequencyfromStartlist(c, r, h);
                    sls.Pilots = (List<string>)await _dataService.GetAllPilotsfromStartlist(c, r, h);
                    var option = await _dataService.GetOptionByNameAsync("GeneratewithoutJudges" + c.Nr);
                    GeneratewithoutJudges = bool.Parse(option.Value);
                    if (!GeneratewithoutJudges)
                    {
                        sls.Judges = (List<string>)await _dataService.GetAllJudgesfromStartlist(c, r, h);
                    }
                    _startlist.Add(sls);
                }
            }
            //Enable Printbutton
            if (_startlist.Count > 0)
            {
                // Enable to print
                //PrintEnable = true;
            }
            else
            {
                PrintEnable = false;
            }
        }
        public ICommand PrintCommand { get; set; }
        private void PrintStartlist()
        {
            _printService.Header = new TextBlock { Text = "Testtestetst" };
            foreach (var sl in _startlist)
            {
                var cont = new ContentControl();
                var rd = new ResourceDictionary();
                rd.Source = new Uri("ms-appx:///Styles/_XamlResources.xaml");
                cont.ContentTemplate = rd["StartlistTemplate"] as DataTemplate;
                cont.DataContext = sl;
                _printService.AddPrintContent(cont);
            }
            _printService.Print();
        }        
    }
}