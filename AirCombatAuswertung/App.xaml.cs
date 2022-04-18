using AirCombatAuswertung.Helpers;
using AirCombatAuswertung.Interfaces;
using AirCombatAuswertung.Services;
using AirCombatAuswertung.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;

namespace AirCombatAuswertung
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
#if !UNIVERSAL
        private static Window startupWindow;
#endif
        // Get the initial window created for this app
        // On UWP, this is simply Window.Current
        // On Desktop, multiple Windows may be created, and the StartupWindow may have already
        // been closed.
        public static Window StartupWindow
        {
            get
            {
#if UNIVERSAL
                return Window.Current;
#else
                return startupWindow;
#endif
            }
        }
        public static UIElement appTitlebar = null;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
#if UNIVERSAL
           WindowHelper.TrackWindow(Window.Current);
#else
            startupWindow = WindowHelper.CreateWindow();
#endif

            Container = RegisterServices();
            var dataService = Container.GetService<IDataService>();
            var printService = Container.GetService<IPrintService>();
            await dataService.InitializeDataAsync();

            //draw into the title bar

#if UNIVERSAL
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
#endif

#if !UNIVERSAL
            // args.UWPLaunchActivatedEventArgs throws an InvalidCastException in desktop apps.
            EnsureWindow();
#else
            EnsureWindow(args.UWPLaunchActivatedEventArgs);
#endif
        }
        public static string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - V {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        /// <summary>
        /// This DI container manages the project's dependencies.
        /// </summary>
        public IServiceProvider Container { get; private set; }

        /// <summary>
        /// Initializes the DI container.
        /// </summary>
        /// <returns>An instance implementing IServiceProvider.</returns>
        private IServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<_10_HomeViewModel>();
            services.AddTransient<_40_EventoptionsViewModel>();
            services.AddTransient<_41_RegistrationViewModel>();
            services.AddTransient<_42_StartlistViewModel>();
            services.AddTransient<_50_ScoreboardsViewModel>();
            services.AddTransient<_60_ResultsViewModel>();

            services.AddSingleton<IDataService, SqliteDataService>();
            services.AddSingleton<IPrintService, PrintService>();

            return services.BuildServiceProvider();
        }
        public static TEnum GetEnum<TEnum>(string text) where TEnum : struct
        {
            if (!typeof(TEnum).GetTypeInfo().IsEnum)
            {
                throw new InvalidOperationException("Generic parameter 'TEnum' must be an enum.");
            }
            return (TEnum)Enum.Parse(typeof(TEnum), text);
        }
        private async void EnsureWindow(IActivatedEventArgs args = null)
        {
            Frame rootFrame = GetRootFrame();

            ThemeHelper.Initialize();

            // Ensure the current window is active
            StartupWindow.Activate();
        }

        private Frame GetRootFrame()
        {
            Frame rootFrame;
            MainWindow rootPage = StartupWindow.Content as MainWindow;
            if (rootPage == null)
            {
                rootPage = new MainWindow();
                rootFrame = (Frame)rootPage.FindName("rootFrame");
                if (rootFrame == null)
                {
                    throw new Exception("Root frame not found");
                }
                rootFrame.Language=Windows.Globalization.ApplicationLanguages.Languages[0];

                StartupWindow.Content = rootPage;
            }
            else
            {
                rootFrame = (Frame)rootPage.FindName("rootFrame");
            }

            return rootFrame;
        }
    }
}
