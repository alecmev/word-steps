using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WordSteps;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace WordSteps
{
    public partial class DictionariesPage : PhoneApplicationPage, INotifyPropertyChanged
    {
        public static readonly DependencyProperty SelectedDictionaryCountProperty = DependencyProperty.Register("SelectedDictionaryCount", typeof(int), typeof(DictionariesPage), new PropertyMetadata(new PropertyChangedCallback(SelectedDictionaryCountPropertyChanged)));
        public int SelectedDictionaryCount
        {
            get { return (int)GetValue(SelectedDictionaryCountProperty); }
            set { SetValue(SelectedDictionaryCountProperty, value); }
        }
        private static void SelectedDictionaryCountPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DictionariesPage).SelectedDictionaryCountPropertyChanged(e);
        }
        private void SelectedDictionaryCountPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            (ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = ((int)e.NewValue >= 1);
            (ApplicationBar.Buttons[1] as ApplicationBarIconButton).IsEnabled = ((int)e.NewValue >= 1);
            (ApplicationBar.MenuItems[0] as ApplicationBarMenuItem).IsEnabled = ((int)e.NewValue == 1);
            (ApplicationBar.MenuItems[1] as ApplicationBarMenuItem).IsEnabled = ((int)e.NewValue >= 1);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DictionariesPage()
        {
            InitializeComponent();

            this.DataContext = App.DataBaseViewModel;

            Binding bind = new Binding("SelectedDictionaryCount");
            bind.Source = App.DataBaseViewModel;
            bind.Mode = BindingMode.OneWay;
            this.SetBinding(DictionariesPage.SelectedDictionaryCountProperty, bind);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            App.DataBaseViewModel.LoadData();
            base.OnNavigatedTo(e);
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ApplicationBarStateChanged(object sender, ApplicationBarStateChangedEventArgs e)
        {
            this.ApplicationBar.Opacity = (this.ApplicationBar.Opacity == 1.0) ? 0.8 : 1.0;
        }

        private void DictionaryTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            CheckBox targetCheckBox = (CheckBox)((Grid)sender).Children[0];
            if (!e.OriginalSource.Equals((object)targetCheckBox)) targetCheckBox.IsChecked = !targetCheckBox.IsChecked;
        }

        private void DictionaryCheckBoxTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            e.Handled = true;
        }

        private void SettingsClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }

        private void NewClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewDictionaryPage.xaml", UriKind.Relative));
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            ObservableCollection<Dictionary> selected = App.DataBaseViewModel.SelectedDictionaries;
            if (selected.Count > 0 && MessageBox.Show("Are you sure you want to delete " + selected.Count.ToString() + " selected dictionar" + (selected.Count > 1 ? "ies" : "y") + "?", "Alert", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                foreach (Dictionary dictionary in selected)
                    App.DataBaseViewModel.DeleteDictionary(dictionary);
        }

        private void WordsClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/WordsPage.xaml", UriKind.Relative));
        }

        private void EditClick(object sender, EventArgs e)
        {

        }
    }
}