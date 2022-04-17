using AirCombatAuswertung.Helpers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Windows.ApplicationModel;

namespace AirCombatAuswertung.Views
{
    public sealed partial class _91_About : Page
    {
        private string versionDescription;

        public string VersionDescription
        {
            get { return versionDescription; }
            set { versionDescription = value; }
        }
        private string changelog;

        public string Changelog
        {
            get { return changelog; }
            set { changelog = value; }
        }
        private string nugetPackages;

        public string NugetPackages
        {
            get { return nugetPackages; }
            set { nugetPackages = value; }
        }


        public _91_About()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await InitializeAsync();
        }
        private async Task InitializeAsync()
        {
            VersionDescription = App.GetVersionDescription();
            Changelog = GetChangelog();
            NugetPackages =GetNugetPackages();
            await Task.CompletedTask;
        }        
        private string GetChangelog()
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Resources/Changelog.txt";
            var changelog = System.IO.File.ReadAllText(path);
            return $"{changelog}";
        }
        private string GetNugetPackages()
        {
            string output = "";
            var NugetNames = new[] {"CommunityToolkit.WinUI.UI",
                                    "CommunityToolkit.WinUI.UI.Control",
                                    "Dapper",
                                    "Dapper.Contrib",
                                    "Microsoft.Data.Sqlite",
                                    "Microsoft.Extensions.DependencyInjection",
                                    "Microsoft.Windows.SDK.BuildTools",
                                    "Microsoft.WindowsAppSDK",
                                    "Microsoft.Xaml.Behaviors.WinUI.Managed",
                                    "Newtonsoft.Json"};
            Assembly[] assam = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assam)
            {
                var searchstring = assembly.GetName().Name;
                if (NugetNames.Any(searchstring.Contains))
                {
                    string version = ((AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyFileVersionAttribute), false)).Version;
                    output += assembly.GetName().Name + "   Version: " + version + Environment.NewLine;                    
                }
            }
            return output;
        }
    }
}
