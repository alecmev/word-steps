using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Windows;

namespace WordSteps
{
    public class SettingsViewModel
        : INotifyPropertyChanged
    {
        private IsolatedStorageSettings settings;

        public SettingsViewModel()
        {
            settings = IsolatedStorageSettings.ApplicationSettings;

            Initialize();
        }

        public void Initialize()
        {
            if (!settings.Contains("IsFirstRun") || (bool)settings["IsFirstRun"])
            {
                settings["IsFirstRun"] = false;

                int english = (from x in App.DataBaseViewModel.Languages where (x.Code == "en") select x.Id).ElementAt(0);
                settings["LearningLanguage"] = english;
                settings["TranslationLanguage"] = english;
                settings["InterfaceLanguage"] = english;

                Save();
            }
        }

        public void Save()
        {
            settings.Save();
        }

        public void Reset()
        {
            settings["IsFirstRun"] = true;
            Initialize();
            NotifyPropertyChanged("LearningLanguage");
            NotifyPropertyChanged("TranslationLanguage");
            NotifyPropertyChanged("InterfaceLanguage");
        }

        public Language LearningLanguage
        {
            get { return (from x in App.DataBaseViewModel.Languages where (x.Id == (int)settings["LearningLanguage"]) select x).ElementAt(0); }
            set
            {
                if (value.Id != (int)settings["LearningLanguage"])
                {
                    settings["LearningLanguage"] = value.Id;
                    Save();
                    NotifyPropertyChanged("LearningLanguage");
                    NotifyPropertyChanged("Languages");
                }
            }
        }

        public Language TranslationLanguage
        {
            get { return (from x in App.DataBaseViewModel.Languages where (x.Id == (int)settings["TranslationLanguage"]) select x).ElementAt(0); }
            set
            {
                if (value.Id != (int)settings["TranslationLanguage"])
                {
                    settings["TranslationLanguage"] = value.Id;
                    Save();
                    NotifyPropertyChanged("TranslationLanguage");
                    NotifyPropertyChanged("Languages");
                }
            }
        }

        public Language InterfaceLanguage
        {
            get { return (from x in App.DataBaseViewModel.Languages where (x.Id == (int)settings["InterfaceLanguage"]) select x).ElementAt(0); }
            set
            {
                if (value.Id != (int)settings["InterfaceLanguage"])
                {
                    settings["InterfaceLanguage"] = value.Id;
                    Save();
                    NotifyPropertyChanged("InterfaceLanguage");
                }
            }
        }

        public string Languages
        {
            get { return TranslationLanguage.Name.ToLower() + " > " + LearningLanguage.Name.ToLower(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}