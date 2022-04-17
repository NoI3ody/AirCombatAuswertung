using AirCombatAuswertung.Helpers;
using AirCombatAuswertung.Interfaces;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace AirCombatAuswertung.Services
{
    public class PrintService : IPrintService
    {
        private static Panel _printingContainer;

        private PrintHelper _printHelper;
        private List<FrameworkElement> _content = new List<FrameworkElement>();
        private FrameworkElement _header;
        private FrameworkElement _footer;
        private PageNumbering _pageNumbering = PageNumbering.None;

        public PrintService() { }
        public static Panel PrintingContainer
        {
            set { _printingContainer = value; }
        }
        public FrameworkElement Header
        {
            set { _header = value; }
        }
        public FrameworkElement Footer
        {
            set { _footer = value; }
        }
        public PageNumbering PageNumbering
        {
            set { _pageNumbering = value; }
        }
        public void AddPrintContent(FrameworkElement content)
        {
            _content.Add(content);
        }
        public async void Print()
        {
            _printHelper = new PrintHelper(_printingContainer);
            PrintPage.StartPageNumber = 1;
            foreach(var content in _content)
            {
                var page = new PrintPage(content,_header,_footer, _pageNumbering);
                _printHelper.AddFrameworkElementToPrint(page);
            }
            _printHelper.OnPrintFailed += printHelper_OnPrintFailed;
            _printHelper.OnPrintSucceeded += printHelper_OnPrintSucceeded;
            _printHelper.OnPrintCanceled += printHelper_OnPrintCanceled;

            await _printHelper.ShowPrintUIAsync("Print Sample");
        }
        private void printHelper_OnPrintCanceled()
        {
            ReleasePrintHelper();
        }

        private void printHelper_OnPrintSucceeded()
        {
            ReleasePrintHelper();
        }

        private void printHelper_OnPrintFailed()
        {
            ReleasePrintHelper();
        }

        private void ReleasePrintHelper()

        {
            _printHelper.Dispose();

            //if (!DirectPrintContainer.Children.Contains(PrintableContent))
            //{
            //    DirectPrintContainer.Children.Add(PrintableContent);
            //}
        }
    }
}
