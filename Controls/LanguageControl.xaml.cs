using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.ComponentModel;
using System.Windows.Data;

namespace WordSteps
{
    public partial class LanguageControl : UserControl
    {
        private PhoneApplicationPage parentPage;

        public LanguageControl()
        {
            InitializeComponent();

            this.DataContext = App.SettingsViewModel;
            Loaded += new RoutedEventHandler(LanguageControlLoaded);
        }

        private void LanguageControlLoaded(object sender, RoutedEventArgs e)
        {
            DependencyObject parent = Parent;
            while (!(parent is PhoneApplicationPage))
                parent = ((FrameworkElement)parent).Parent;
            parentPage = ((PhoneApplicationPage)parent);
        }

        private void LanguageControlClick(object sender, RoutedEventArgs e)
        {
            parentPage.NavigationService.Navigate(new Uri("/SettingsPage.xaml?section=language", UriKind.Relative));
        }
    }
}
