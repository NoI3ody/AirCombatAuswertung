using Microsoft.UI;
using Microsoft.UI.Xaml;
using Windows.Storage;

namespace AirCombatAuswertung.Helpers
{
    /// <summary>
    /// Class providing functionality around switching and restoring theme settings
    /// </summary>
    public static class ThemeHelper
    {
        private const string SelectedAppThemeKey = "SelectedAppTheme";
        private static Window CurrentApplicationWindow;
        /// <summary>
        /// Gets the current actual theme of the app based on the requested theme of the
        /// root element, or if that value is Default, the requested theme of the Application.
        /// </summary>
        public static ElementTheme ActualTheme
        {
            get
            {
                foreach (Window window in WindowHelper.ActiveWindows)
                {
                    if (window.Content is FrameworkElement rootElement)
                    {
                        if (rootElement.RequestedTheme != ElementTheme.Default)
                        {
                            return rootElement.RequestedTheme;
                        }
                    }
                }
                return AirCombatAuswertung.App.GetEnum<ElementTheme>(App.Current.RequestedTheme.ToString());
            }
        }
        /// <summary>
        /// Gets or sets (with LocalSettings persistence) the RequestedTheme of the root element.
        /// </summary>
        public static ElementTheme RootTheme
        {
            get
            {
                foreach (Window window in WindowHelper.ActiveWindows)
                {
                    if (window.Content is FrameworkElement rootElement)
                    {
                        return rootElement.RequestedTheme;
                    }
                }

                return ElementTheme.Default;
            }
            set
            {
                foreach (Window window in WindowHelper.ActiveWindows)
                {
                    if (window.Content is FrameworkElement rootElement)
                    {
                        rootElement.RequestedTheme = value;
                    }
                }

                ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey] = value.ToString();
                UpdateSystemCaptionButtonColors();
            }
        }

        public static void Initialize()
        {
#if !UNPACKAGED
            // Save reference as this might be null when the user is in another app
            CurrentApplicationWindow = App.StartupWindow;
            string savedTheme = ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey]?.ToString();

            if (savedTheme != null)
            {
                RootTheme = AirCombatAuswertung.App.GetEnum<ElementTheme>(savedTheme);
            }
#endif
#if UNIVERSAL
            // Registering to color changes, thus we notice when user changes theme system wide
            uiSettings = new UISettings();
            uiSettings.ColorValuesChanged += UiSettings_ColorValuesChanged;
#endif
        }

#if UNIVERSAL
        private static void UiSettings_ColorValuesChanged(UISettings sender, object args)
        {
            // Make sure we have a reference to our window so we dispatch a UI change
            if (CurrentApplicationWindow != null)
            {
                // Dispatch on UI thread so that we have a current appbar to access and change
                CurrentApplicationWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                        {
                            UpdateSystemCaptionButtonColors();
                        });
            }
        }
#endif

        public static bool IsDarkTheme()
        {
            if (RootTheme == ElementTheme.Default)
            {
                return Application.Current.RequestedTheme == ApplicationTheme.Dark;
            }
            return RootTheme == ElementTheme.Dark;
        }

        public static void UpdateSystemCaptionButtonColors()
        {
#if UNIVERSAL
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

            if (ThemeHelper.IsDarkTheme())
            {
                titleBar.ButtonForegroundColor = Colors.White;
            }
            else
            {
                titleBar.ButtonForegroundColor = Colors.Black;
            }
#endif
#if !UNIVERSAL
            // Works only in Win11
            foreach (Window window in WindowHelper.ActiveWindows)
            {
                // Retrieve the window handle (HWND) of the current (XAML) WinUI 3 window.
                var hWnd =
                    WinRT.Interop.WindowNative.GetWindowHandle(window);
                // Retrieve the WindowId that corresponds to hWnd.
                Microsoft.UI.WindowId windowId =
                    Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
                // Lastly, retrieve the AppWindow for the current (XAML) WinUI 3 window.
                Microsoft.UI.Windowing.AppWindow appWindow =
                    Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
                if (appWindow != null)
                {
                    if (appWindow.TitleBar != null)
                    {
                        if (ThemeHelper.IsDarkTheme())
                        {
                            appWindow.TitleBar.ButtonForegroundColor = Colors.White;
                        }
                        else
                        {
                            appWindow.TitleBar.ButtonForegroundColor = Colors.Black;
                        }
                    }
                }
            }
#endif
        }
    }
}

