using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ScreenplayConverter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnTypeClicked(object sender, RoutedEventArgs e)
        {
            var menu = Resources["typeMenu"] as ContextMenu;
            var button = (Button)sender;
            button.ContextMenu = menu;
            menu.IsOpen = true;
        }
    }
}
