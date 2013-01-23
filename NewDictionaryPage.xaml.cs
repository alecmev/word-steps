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
using Microsoft.Phone.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;

namespace WordSteps
{
    public partial class NewDictionaryPage : PhoneApplicationPage
    {
        public NewDictionaryPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationService.GoBack();
                NavigationContext.QueryString.Add(new KeyValuePair<string, string>("foo", "bar"));
            }
            else
                base.OnNavigatedTo(e);
        }

        private void CreateClick(object sender, EventArgs e)
        {
            if (NewDictionaryName.Text.Trim().Length > 0)
            {
                if (App.DataBaseViewModel.SelectedDictionaryCount >= 1)
                    foreach (Dictionary dictionary in App.DataBaseViewModel.Dictionaries)
                        dictionary.IsSelected = false;
                App.DataBaseViewModel.AddDictionary(new Dictionary() { Name = NewDictionaryName.Text.Trim(), TranslationLanguage = App.SettingsViewModel.TranslationLanguage, LearningLanguage = App.SettingsViewModel.LearningLanguage, IsSelected = true });
                NavigationService.Navigate(new Uri("/WordsPage.xaml", UriKind.Relative));
            }
            else
                MessageBox.Show("The Name field is empty", "Error", MessageBoxButton.OK);
        }

        private void CancelClick(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}