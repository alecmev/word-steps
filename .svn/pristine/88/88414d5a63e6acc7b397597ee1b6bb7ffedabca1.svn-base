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
using System.Windows.Data;

namespace WordSteps
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            this.DataContext = App.DataBaseViewModel;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (this.NavigationContext.QueryString.ContainsKey("section"))
            {
                switch (this.NavigationContext.QueryString["section"])
                {
                    case "language":
                        SettingsPivot.SelectedItem = LanguagePivotItem;
                        break;
                }
            }

            base.OnNavigatedTo(e);
        }

        private void SaveClick(object sender, EventArgs e)
        {
            App.SettingsViewModel.LearningLanguage = App.DataBaseViewModel.Languages[LearningLanguage.SelectedIndex];
            App.SettingsViewModel.TranslationLanguage = App.DataBaseViewModel.Languages[TranslationLanguage.SelectedIndex];
            App.SettingsViewModel.InterfaceLanguage = App.DataBaseViewModel.Languages[InterfaceLanguage.SelectedIndex];
            NavigationService.GoBack();
        }

        private void CancelClick(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ListPickerLoaded(object sender, RoutedEventArgs e)
        {
            ListPicker listPicker = ((ListPicker)sender);
            if (listPicker.Tag == null)
            {
                listPicker.Tag = true;
                Binding bind = new Binding((string)listPicker.Name);
                bind.Source = App.SettingsViewModel;
                bind.Converter = new LanguageListPickerConverter();
                bind.Mode = BindingMode.OneWay;
                listPicker.SetBinding(ListPicker.SelectedIndexProperty, bind);
            }
        }
    }
}