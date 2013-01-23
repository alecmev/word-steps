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
    public partial class NewWordPage : PhoneApplicationPage
    {
        public NewWordPage()
        {
            InitializeComponent();
        }

        private void CreateClick(object sender, EventArgs e)
        {
            if (NewWordOriginal.Text.Trim().Length > 0 && NewWordTranslation.Text.Trim().Length > 0)
            {
                App.DataBaseViewModel.AddWord(new Word() { Dictionary = App.DataBaseViewModel.SelectedDictionaries[0], Original = NewWordOriginal.Text.Trim(), Translation = NewWordTranslation.Text.Trim(), IsSelected = true });
                NavigationService.GoBack();
            }
            else
                MessageBox.Show("All required fields aren't filled", "Error", MessageBoxButton.OK);
        }

        private void CancelClick(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}