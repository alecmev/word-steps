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
    public partial class WordsPage : PhoneApplicationPage, INotifyPropertyChanged
    {
        public static readonly DependencyProperty SelectedWordCountProperty = DependencyProperty.Register("SelectedWordCount", typeof(int), typeof(WordsPage), new PropertyMetadata(new PropertyChangedCallback(SelectedWordCountPropertyChanged)));
        public int SelectedWordCount
        {
            get { return (int)GetValue(SelectedWordCountProperty); }
            set { SetValue(SelectedWordCountProperty, value); }
        }
        private static void SelectedWordCountPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as WordsPage).SelectedWordCountPropertyChanged(e);
        }
        private void SelectedWordCountPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            (ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = ((int)e.NewValue >= 1);
            (ApplicationBar.Buttons[2] as ApplicationBarIconButton).IsEnabled = ((int)e.NewValue == 1);
            (ApplicationBar.Buttons[3] as ApplicationBarIconButton).IsEnabled = ((int)e.NewValue >= 1);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public WordsPage()
        {
            InitializeComponent();

            this.DataContext = App.DataBaseViewModel;

            Binding bind = new Binding("SelectedWordCount");
            bind.Source = App.DataBaseViewModel;
            bind.Mode = BindingMode.OneWay;
            this.SetBinding(WordsPage.SelectedWordCountProperty, bind);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            (ApplicationBar.Buttons[1] as ApplicationBarIconButton).IsEnabled = (App.DataBaseViewModel.SelectedDictionaryCount == 1);
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

        private void WordTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            CheckBox targetCheckBox = (CheckBox)((Grid)sender).Children[0];
            if (!e.OriginalSource.Equals((object)targetCheckBox)) targetCheckBox.IsChecked = !targetCheckBox.IsChecked;
        }

        private void WordCheckBoxTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            e.Handled = true;
        }

        private void NewClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewWordPage.xaml", UriKind.Relative));
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            ObservableCollection<Word> selected = App.DataBaseViewModel.SelectedWords;
            if (selected.Count > 0 && MessageBox.Show("Are you sure you want to delete " + selected.Count.ToString() + " selected word" + (selected.Count > 1 ? "s" : "") + "?", "Alert", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                foreach (Word word in selected)
                    App.DataBaseViewModel.DeleteWord(word);
        }

        private void EditClick(object sender, EventArgs e)
        {

        }
    }
}