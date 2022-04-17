using AirCombatAuswertung.Helpers;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using Windows.UI.ViewManagement;

namespace AirCombatAuswertung.Views
{
    public sealed partial class _90_Settings : Page
    {
        public _90_Settings()
        {
            this.InitializeComponent();
            Loaded += OnSettingsPageLoaded;
        }
        private void OnSettingsPageLoaded(object sender, RoutedEventArgs e)
        {
            var currentTheme = ThemeHelper.RootTheme.ToString();
            ((RadioButton)ThemePanel.Children.FirstOrDefault(c => (c as RadioButton)?.Tag?.ToString() == currentTheme)).IsChecked = true;
        }
        private void OnThemeRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            var selectedTheme = ((RadioButton)sender)?.Tag?.ToString();
#if UNIVERSAL
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            Action<Windows.UI.Color> SetTitleBarButtonForegroundColor = (Windows.UI.Color color) => { titleBar.ButtonForegroundColor = color; };
#else
            Action<Windows.UI.Color> SetTitleBarButtonForegroundColor = (Windows.UI.Color color) => { };
#endif
            if (selectedTheme != null)
            {
                ThemeHelper.RootTheme = App.GetEnum<ElementTheme>(selectedTheme);
                if (selectedTheme == "Dark")
                {
                    SetTitleBarButtonForegroundColor(Colors.White);
                }
                else if (selectedTheme == "Light")
                {
                    SetTitleBarButtonForegroundColor(Colors.Black);
                }
                else
                {
                    if (Application.Current.RequestedTheme == ApplicationTheme.Dark)
                    {
                        SetTitleBarButtonForegroundColor(Colors.White);
                    }
                    else
                    {
                        SetTitleBarButtonForegroundColor(Colors.Black);
                    }
                }
            }
        }

        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:easeofaccess-display"));
        }
    }
}
