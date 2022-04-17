using AirCombatAuswertung.Interfaces;
using AirCombatAuswertung.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AirCombatAuswertung.ViewModels
{
    public class _50_ScoreboardsViewModel : BindableBase
    {
        private ObservableCollection<Pilot> _pilots = new ObservableCollection<Pilot>();
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
                Class c = new Class { Nr = 0, Description = "", Name = "", Rules = "" };
                SetClassVisibility(c);
                GetPilotsforselClass(SelClass);
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
                SetPilotsVisibility(SelRound);
                SetClassVisibility(SelClass);
                SelPilot = null;
            }
        }
        private Pilot _SelPilot;
        public Pilot SelPilot
        {
            get => _SelPilot;
            set
            {
                if (!SetProperty(ref _SelPilot, value, nameof(SelPilot)))
                    return;
                if (value == null)
                {
                    EnableEntry = false;
                    return;
                }
                else
                {
                    switch (SelClass.Nr)
                    {
                        case 1: 
                            GetScoresWW2(SelClass, SelRound, SelPilot);
                            break;
                        case 2:
                            GetScoresWW1(SelClass, SelRound, SelPilot);
                            break;
                        case 3:
                            GetScoresEPA(SelClass, SelRound, SelPilot);
                            break;
                    }

                    EnableEntry = true;
                }

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
        #region Visibility
        private Microsoft.UI.Xaml.Visibility _svWW2 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility SvWW2
        {
            get => _svWW2;
            set
            {
                if (!SetProperty(ref _svWW2, value, nameof(SvWW2)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _svWW1 = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility SvWW1
        {
            get => _svWW1;
            set
            {
                if (!SetProperty(ref _svWW1, value, nameof(SvWW1)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _svEPA = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility SvEPA
        {
            get => _svEPA;
            set
            {
                if (!SetProperty(ref _svEPA, value, nameof(SvEPA)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _vPilots = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility VPilots
        {
            get => _vPilots;
            set
            {
                if (!SetProperty(ref _vPilots, value, nameof(VPilots)))
                    return;
            }
        }
        private Microsoft.UI.Xaml.Visibility _vScores = Microsoft.UI.Xaml.Visibility.Collapsed;
        public Microsoft.UI.Xaml.Visibility VScores
        {
            get => _vScores;
            set
            {
                if (!SetProperty(ref _vScores, value, nameof(VScores)))
                    return;
            }
        }
        private bool _EnableEntry;
        public bool EnableEntry
        {
            get => _EnableEntry;
            set
            {
                if (!SetProperty(ref _EnableEntry, value, nameof(EnableEntry)))
                    return;
            }
        }
        #endregion
        #region WW2
        private string _WW2Flighttime;
        public string WW2Flighttime
        {
            get => _WW2Flighttime;
            set
            {
                if (value == "_:__") return;
                if (!SetProperty(ref _WW2Flighttime, value, nameof(WW2Flighttime)))
                    return;
                UpdateFlighttime(SelClass, SelRound, SelPilot, value);
            }
        }
        private int _WW2Cuts;
        public int WW2Cuts
        {
            get => _WW2Cuts;
            set
            {
                if (!SetProperty(ref _WW2Cuts, value, nameof(WW2Cuts)))
                    return;
                UpdateNumberbox(SelClass, SelRound, SelPilot, value, "Cut");
            }
        }
        private bool _WW2StreamerOK;
        public bool WW2StreamerOK
        {
            get => _WW2StreamerOK;
            set
            {
                if (!SetProperty(ref _WW2StreamerOK, value, nameof(WW2StreamerOK)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "StreamerOK");
            }
        }
        private bool _WW2SafetyCross;
        public bool WW2SafetyCross
        {
            get => _WW2SafetyCross;
            set
            {
                if (!SetProperty(ref _WW2SafetyCross, value, nameof(WW2SafetyCross)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "Safetyline Cross");
            }
        }
        private bool _WW2NonEng;
        public bool WW2NonEng
        {
            get => _WW2NonEng;
            set
            {
                if (!SetProperty(ref _WW2NonEng, value, nameof(WW2NonEng)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "Non Engagement");
            }
        }
        private bool _WW22_5ccm;
        public bool WW22_5ccm
        {
            get => _WW22_5ccm;
            set
            {
                if (!SetProperty(ref _WW22_5ccm, value, nameof(WW22_5ccm)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "WW2 2,5ccm Engine");
            }
        }
        private int _WW2Sum;
        public int WW2Sum
        {
            get => _WW2Sum;
            set
            {
                if (!SetProperty(ref _WW2Sum, value, nameof(WW2Sum)))
                    return;
            }
        }

        #endregion
        #region WW1
        private bool _WW1Motor4Stroke;
        public bool WW1Motor4Stroke
        {
            get => _WW1Motor4Stroke;
            set
            {
                if (!SetProperty(ref _WW1Motor4Stroke, value, nameof(WW1Motor4Stroke)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "WW1 Four-Stroke");
            }
        }
        private bool _WW1Biplane;
        public bool WW1Biplane
        {
            get => _WW1Biplane;
            set
            {
                if (!SetProperty(ref _WW1Biplane, value, nameof(WW1Biplane)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "WW1 Biplane");
            }
        }
        private bool _WW1BalsaStructure;
        public bool WW1BalsaStructure
        {
            get => _WW1BalsaStructure;
            set
            {
                if (!SetProperty(ref _WW1BalsaStructure, value, nameof(WW1BalsaStructure)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "WW1 WingStructure");
            }
        }
        private bool _WW1WiresAndStruts;
        public bool WW1WiresAndStruts
        {
            get => _WW1WiresAndStruts;
            set
            {
                if (!SetProperty(ref _WW1WiresAndStruts, value, nameof(WW1WiresAndStruts)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "WW1 Wires and Struts");
            }
        }
        private string _WW1Flighttime;
        public string WW1Flighttime
        {
            get => _WW1Flighttime;
            set
            {
                if (value == "_:__") return;
                if (!SetProperty(ref _WW1Flighttime, value, nameof(WW1Flighttime)))
                    return;
                UpdateFlighttime(SelClass, SelRound, SelPilot, value);
            }
        }
        private int _WW1Cuts;
        public int WW1Cuts
        {
            get => _WW1Cuts;
            set
            {
                if (!SetProperty(ref _WW1Cuts, value, nameof(WW1Cuts)))
                    return;
                UpdateNumberbox(SelClass, SelRound, SelPilot, value, "Cut");
            }
        }
        private int _WW1GroundTargets;
        public int WW1GroundTargets
        {
            get => _WW1GroundTargets;
            set
            {
                if (!SetProperty(ref _WW1GroundTargets, value, nameof(WW1GroundTargets)))
                    return;
                UpdateNumberbox(SelClass, SelRound, SelPilot, value, "WW1 Ground Targets");
            }
        }
        private bool _WW1StreamerOK;
        public bool WW1StreamerOK
        {
            get => _WW1StreamerOK;
            set
            {
                if (!SetProperty(ref _WW1StreamerOK, value, nameof(WW1StreamerOK)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "StreamerOK");
            }
        }
        private bool _WW1ScaleStart;
        public bool WW1ScaleStart
        {
            get => _WW1ScaleStart;
            set
            {
                if (!SetProperty(ref _WW1ScaleStart, value, nameof(WW1ScaleStart)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "WW1 Scale Start");
            }
        }
        private bool _WW1ScaleLand;
        public bool WW1ScaleLand
        {
            get => _WW1ScaleLand;
            set
            {
                if (!SetProperty(ref _WW1ScaleLand, value, nameof(WW1ScaleLand)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "WW1 Scale Landing");
            }
        }
        private bool _WW1SafetyCross;
        public bool WW1SafetyCross
        {
            get => _WW1SafetyCross;
            set
            {
                if (!SetProperty(ref _WW1SafetyCross, value, nameof(WW1SafetyCross)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "Safetyline Cross");
            }
        }
        private bool _WW1NonEng;
        public bool WW1NonEng
        {
            get => _WW1NonEng;
            set
            {
                if (!SetProperty(ref _WW1NonEng, value, nameof(WW1NonEng)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "Non Engagement");
            }
        }
        private int _WW1SumModP;
        public int WW1SumModP
        {
            get => _WW1SumModP;
            set
            {
                if (!SetProperty(ref _WW1SumModP, value, nameof(WW1SumModP)))
                    return;
            }
        }
        private int _WW1Sum;
        public int WW1Sum
        {
            get => _WW1Sum;
            set
            {
                if (!SetProperty(ref _WW1Sum, value, nameof(WW1Sum)))
                    return;
            }
        }
        #endregion
        #region EPA
        private string _EPAFlighttime;
        public string EPAFlighttime
        {
            get => _EPAFlighttime;
            set
            {
                if (value == "_:__") return;
                if (!SetProperty(ref _EPAFlighttime, value, nameof(EPAFlighttime)))
                    return;
                UpdateFlighttime(SelClass, SelRound, SelPilot, value);
            }
        }
        private int _EPACuts;
        public int EPACuts
        {
            get => _EPACuts;
            set
            {
                if (!SetProperty(ref _EPACuts, value, nameof(EPACuts)))
                    return;
                UpdateNumberbox(SelClass, SelRound, SelPilot, value, "Cut");
            }
        }
        private bool _EPAStreamerOK;
        public bool EPAStreamerOK
        {
            get => _EPAStreamerOK;
            set
            {
                if (!SetProperty(ref _EPAStreamerOK, value, nameof(EPAStreamerOK)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "StreamerOK");
            }
        }
        private bool _EPANonEng;
        public bool EPANonEng
        {
            get => _EPANonEng;
            set
            {
                if (!SetProperty(ref _EPANonEng, value, nameof(EPANonEng)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "Non Engagement");
            }
        }
        private bool _EPASafetyCross;
        public bool EPASafetyCross
        {
            get => _EPASafetyCross;
            set
            {
                if (!SetProperty(ref _EPASafetyCross, value, nameof(EPASafetyCross)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "Safetyline Cross");
            }
        }
        private bool _EPADoubleEngine;
        public bool EPADoubleEngine
        {
            get => _EPADoubleEngine;
            set
            {
                if (!SetProperty(ref _EPADoubleEngine, value, nameof(EPADoubleEngine)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "EPA Double Engine");
            }
        }
        private bool _EPABiplane;
        public bool EPABiplane
        {
            get => _EPABiplane;
            set
            {
                if (!SetProperty(ref _EPABiplane, value, nameof(EPABiplane)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "EPA Biplane");
            }
        }
        private bool _EPAFlatFuselage;
        public bool EPAFlatFuselage
        {
            get => _EPAFlatFuselage;
            set
            {
                if (!SetProperty(ref _EPAFlatFuselage, value, nameof(EPAFlatFuselage)))
                    return;
                UpdateCheckbox(SelClass, SelRound, SelPilot, value, "EPA Flat Fuselage");
            }
        }
        private int _EPASumModP;
        public int EPASumModP
        {
            get => _EPASumModP;
            set
            {
                if (!SetProperty(ref _EPASumModP, value, nameof(EPASumModP)))
                    return;
            }
        }
        private int _EPASum;
        public int EPASum
        {
            get => _EPASum;
            set
            {
                if (!SetProperty(ref _EPASum, value, nameof(EPASum)))
                    return;
            }
        }
        #endregion
        public _50_ScoreboardsViewModel(IDataService dataService)
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
        public async Task InitializeScoreboardsDataAsync()
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
        private async void GetPilotsforselClass(Class c)
        {
            _pilots.Clear();
            foreach (var p in await _dataService.GetPilotsAsync())
            {
                if (c.Nr == 1 && p.FliesWW2)
                {
                    _pilots.Add(p);
                }
                else if (c.Nr == 2 && p.FliesWW1)
                {
                    _pilots.Add(p);
                }
                else if (c.Nr == 3 && p.FliesEPA)
                {
                    _pilots.Add(p);
                }
            }
        }
        private void SetClassVisibility(Class c)
        {
            switch (c.Nr)
            {
                case 0:
                    VScores = Microsoft.UI.Xaml.Visibility.Collapsed;
                    SvWW2 = Microsoft.UI.Xaml.Visibility.Collapsed;
                    SvWW1 = Microsoft.UI.Xaml.Visibility.Collapsed;
                    SvEPA = Microsoft.UI.Xaml.Visibility.Collapsed;
                    break;
                case 1:
                    VScores = Microsoft.UI.Xaml.Visibility.Visible;
                    SvWW2 = Microsoft.UI.Xaml.Visibility.Visible;
                    SvWW1 = Microsoft.UI.Xaml.Visibility.Collapsed;
                    SvEPA = Microsoft.UI.Xaml.Visibility.Collapsed;
                    break;
                case 2:
                    VScores = Microsoft.UI.Xaml.Visibility.Visible;
                    SvWW2 = Microsoft.UI.Xaml.Visibility.Collapsed;
                    SvWW1 = Microsoft.UI.Xaml.Visibility.Visible;
                    SvEPA = Microsoft.UI.Xaml.Visibility.Collapsed;
                    break;
                case 3:
                    VScores = Microsoft.UI.Xaml.Visibility.Visible;
                    SvWW2 = Microsoft.UI.Xaml.Visibility.Collapsed;
                    SvWW1 = Microsoft.UI.Xaml.Visibility.Collapsed;
                    SvEPA = Microsoft.UI.Xaml.Visibility.Visible;
                    break;
            }

        }
        private void SetPilotsVisibility(int r)
        {
            if (r != 0)
            {
                VPilots = Microsoft.UI.Xaml.Visibility.Visible;
            }
            else
            {
                VPilots = Microsoft.UI.Xaml.Visibility.Collapsed;
            }
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
        private async void GetScoresWW2(Class c, int r, Pilot p)
        {
            //Lade Score für Flugzeit
            var score = await _dataService.GetScoreAsync(c, r, p, "Time");
            int min = Convert.ToInt32(Math.Floor(score.Value / (double)60));
            int sec = score.Value % 60;
            WW2Flighttime = (min.ToString("0") + sec.ToString("00")).ToString().PadLeft(3).Insert(1, ":").Replace(" ", "0");
            //Lade Score für Cuts
            score = await _dataService.GetScoreAsync(c, r, p, "Cut");
            WW2Cuts = score.Value;
            //Lade Score für StreamerOK
            score = await _dataService.GetScoreAsync(c, r, p, "StreamerOK");
            WW2StreamerOK = Convert.ToBoolean(score.Value);
            //Lade Score für SafetylineCross
            score = await _dataService.GetScoreAsync(c, r, p, "Safetyline Cross");
            WW2SafetyCross = Convert.ToBoolean(score.Value);
            //Lade Score für SafetylineCross
            score = await _dataService.GetScoreAsync(c, r, p, "Non Engagement");
            WW2NonEng = Convert.ToBoolean(score.Value);
            //Lade Score für 2,5ccm Engine
            score = await _dataService.GetScoreAsync(c, r, p, "WW2 2,5ccm Engine");
            WW22_5ccm = Convert.ToBoolean(score.Value);
            //Lade Summe der Runde
            await UpdateSum(c, r, p);
        }
        private async void GetScoresWW1(Class c, int r, Pilot p)
        {
            //Lade Score für 4-Takt Motor
            var score = await _dataService.GetScoreAsync(c, r, p, "WW1 Four-Stroke");
            WW1Motor4Stroke = Convert.ToBoolean(score.Value);
            //Lade Score für Mehrdecker
            score = await _dataService.GetScoreAsync(c, r, p, "WW1 Biplane");
            WW1Biplane = Convert.ToBoolean(score.Value);
            //Lade Score für Wing Structure
            score = await _dataService.GetScoreAsync(c, r, p, "WW1 WingStructure");
            WW1BalsaStructure = Convert.ToBoolean(score.Value);
            //Lade Score für Wires and Struts
            score = await _dataService.GetScoreAsync(c, r, p, "WW1 Wires and Struts");
            WW1WiresAndStruts=Convert.ToBoolean(score.Value);
            //Lade Score für Flugzeit
            score = await _dataService.GetScoreAsync(c, r, p, "Time");
            int min = Convert.ToInt32(Math.Floor(score.Value / (double)60));
            int sec = score.Value % 60;
            WW1Flighttime = (min.ToString() + sec.ToString()).ToString().PadLeft(3).Insert(1, ":").Replace(" ", "0");
            //Lade Score für Cuts
            score = await _dataService.GetScoreAsync(c, r, p, "Cut");
            WW1Cuts = score.Value;
            //Lade Score für Ground Targets
            score = await _dataService.GetScoreAsync(c, r, p, "WW1 Ground Targets");
            WW1GroundTargets=score.Value;
            //Lade Score für StreamerOK
            score = await _dataService.GetScoreAsync(c, r, p, "StreamerOK");
            WW1StreamerOK = Convert.ToBoolean(score.Value);
            //Lade Score für ScaleStart
            score = await _dataService.GetScoreAsync(c, r, p, "WW1 Scale Start");
            WW1ScaleStart = Convert.ToBoolean(score.Value);
            //Lade Score für ScaleLand
            score = await _dataService.GetScoreAsync(c, r, p, "WW1 Scale Landing");
            WW1ScaleLand = Convert.ToBoolean(score.Value);
            //Lade Score für Non Engagement
            score = await _dataService.GetScoreAsync(c, r, p, "Non Engagement");
            WW1NonEng = Convert.ToBoolean(score.Value);
            //Lade Score für SafetylineCross
            score = await _dataService.GetScoreAsync(c, r, p, "Safetyline Cross");
            WW1SafetyCross = Convert.ToBoolean(score.Value);            
            //Lade Summe der Runde
            await UpdateSum(c, r, p);
        }
        private async void GetScoresEPA(Class c, int r, Pilot p)
        {
            //Lade Score für Flugzeit
            var score = await _dataService.GetScoreAsync(c, r, p, "Time");
            int min = Convert.ToInt32(Math.Floor(score.Value / (double)60));
            int sec = score.Value % 60;
            EPAFlighttime = (min.ToString() + sec.ToString()).ToString().PadLeft(3).Insert(1, ":").Replace(" ", "0");
            //Lade Score für Cuts
            score = await _dataService.GetScoreAsync(c, r, p, "Cut");
            EPACuts = score.Value;
            //Lade Score für StreamerOK
            score = await _dataService.GetScoreAsync(c, r, p, "StreamerOK");
            EPAStreamerOK = Convert.ToBoolean(score.Value);
            //Lade Score für Non Engagement
            score = await _dataService.GetScoreAsync(c, r, p, "Non Engagement");
            EPANonEng = Convert.ToBoolean(score.Value);
            //Lade Score für SafetylineCross
            score = await _dataService.GetScoreAsync(c, r, p, "Safetyline Cross");
            EPASafetyCross = Convert.ToBoolean(score.Value);
            //Lade Score für Double Engine
            score = await _dataService.GetScoreAsync(c, r, p, "EPA Double Engine");
            EPADoubleEngine = Convert.ToBoolean(score.Value);
            //Lade Score für Mehrdecker
            score = await _dataService.GetScoreAsync(c, r, p, "EPA Biplane");
            EPABiplane = Convert.ToBoolean(score.Value);
            //Lade Score für Wing Structure
            score = await _dataService.GetScoreAsync(c, r, p, "EPA Flat Fuselage");
            EPAFlatFuselage = Convert.ToBoolean(score.Value);            
            //Lade Summe der Runde
            await UpdateSum(c, r, p);
        }
        private async Task UpdateSum(Class c, int r, Pilot p)
        {
            var result = await _dataService.GetResultAsync(c, r, p);
            switch (c.Nr)
            {
                case 1:                   
                    WW2Sum = result.Sum;
                    break;
                case 2:
                    WW1SumModP = result.SumModP;
                    WW1Sum = result.Sum;
                    break;
                case 3:
                    EPASumModP = result.SumModP;
                    EPASum = result.Sum;
                    break;
            }
            
        }
        private async Task UpdateScore(Class c, int r, Pilot p, string category, int value)
        {
            await _dataService.UpdateScoreAsync(c, r, p, category, value);
        }
        private async Task UpdateFlighttime(Class c, int r, Pilot p, string value)
        {
            if (SelClass != null && SelRound != 0 && SelPilot != null)
            {
                int ts = (int)TimeSpan.Parse(value.Replace("_", "0")).TotalMinutes;
                await UpdateScore(SelClass, SelRound, SelPilot, "Time", ts);
                await UpdateSum(SelClass, SelRound, SelPilot);
            }
        }
        private async Task UpdateNumberbox(Class c, int r, Pilot p, int value, string name)
        {
            if (SelClass != null && SelRound != 0 && SelPilot != null)
            {
                await UpdateScore(SelClass, SelRound, SelPilot, name, value);
                await UpdateSum(SelClass, SelRound, SelPilot);
            }
        }
        private async Task UpdateCheckbox(Class c, int r, Pilot p, bool value, string name)
        {
            if (SelClass != null && SelRound != 0 && SelPilot != null)
            {
                await UpdateScore(SelClass, SelRound, SelPilot, name, Convert.ToInt32(value));
                await UpdateSum(SelClass, SelRound, SelPilot);
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