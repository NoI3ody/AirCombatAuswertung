using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AirCombatAuswertung.Interfaces
{
    public interface IPrintService
    {
        void Print();
        void AddPrintContent(FrameworkElement content);
        FrameworkElement Header { set; }
    }
}
