using AirCombatAuswertung.Helpers;
using AirCombatAuswertung.Services;
using AirCombatAuswertung.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirCombatAuswertung
{
    public sealed partial class MainWindow : Page, INavigation
    {
        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            SetCurrentNavigationViewItem(GetNavigationViewItems(typeof(_10_Home)).First());
            //WindowHelper.GetWindowForElement(this).Title = "AppDisplayName".GetLocalized();

            Window window = App.StartupWindow;
            window.ExtendsContentIntoTitleBar = true;  // enable custom titlebar
            window.SetTitleBar(AppTitleBar);      // set user ui element as titlebar
            window.Title = "AppDisplayName".GetLocalized();
            AppTitle.Text = "AppDisplayName".GetLocalized();
        }
        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            SetCurrentNavigationViewItem(args.SelectedItemContainer as NavigationViewItem);
        }
        public NavigationViewItem GetCurrentNavigationViewItem()
        {
            return NavigationView.SelectedItem as NavigationViewItem;
        }

        public List<NavigationViewItem> GetNavigationViewItems()
        {
            var result = new List<NavigationViewItem>();
            var getitems = NavigationView.MenuItems.Where(t => t is NavigationViewItem);
            var items = getitems.Select(i => (NavigationViewItem)i).ToList();
            var getfooteritems = NavigationView.FooterMenuItems.Where(t => t is NavigationViewItem);
            items.AddRange(getfooteritems.Select(i => (NavigationViewItem)i).ToList());
            result.AddRange(items);

            foreach(NavigationViewItem mainItem in items)
            {
                result.AddRange(mainItem.MenuItems.Select(i => (NavigationViewItem)i));
            }

            return result;
        }

        public List<NavigationViewItem> GetNavigationViewItems(Type type)
        {
            return GetNavigationViewItems().Where(i => i.Tag.ToString() == type.FullName).ToList();
        }

        public List<NavigationViewItem> GetNavigationViewItems(Type type, string title)
        {
            return GetNavigationViewItems(type).Where(ni => ni.Content.ToString() == title).ToList();
        }

        public void SetCurrentNavigationViewItem(NavigationViewItem item)
        {
            if (item == null)
            {
                return;
            }

            if (item.Tag == null)
            {
                return;
            }

            rootFrame.Navigate(Type.GetType(item.Tag.ToString()), item.Content);
            NavigationView.Header = item.Content;
            NavigationView.SelectedItem = item;
        }
    }
}
