using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Collections.Generic;
using System.Windows;

namespace WordSteps
{
    public class DataBaseViewModel
        : INotifyPropertyChanged
    {
        private DataBaseContext dataBase { get; set; }

        private ObservableCollection<Language> _Languages;
        public ObservableCollection<Language> Languages
        {
            get { return _Languages; }
            set
            {
                if (_Languages != value)
                {
                    _Languages = value;
                    NotifyPropertyChanged("Languages");
                }
            }
        }

        private ObservableCollection<Dictionary> _Dictionaries;
        public ObservableCollection<Dictionary> Dictionaries
        {
            get { return _Dictionaries; }
            set
            {
                if (_Dictionaries != value)
                {
                    _Dictionaries = value;
                    NotifyPropertyChanged("Dictionaries");
                }
            }
        }

        public ObservableCollection<Dictionary> SelectedDictionaries
        {
            get { return new ObservableCollection<Dictionary>(from x in App.DataBaseViewModel.Dictionaries where (x.IsSelected == true) select x); }
        }
        public int SelectedDictionaryCount
        {
            get { return SelectedDictionaries.Count; }
        }

        public ObservableCollection<Word> Words
        {
            get
            {
                ObservableCollection<Dictionary> dictionaries = SelectedDictionaries;
                ObservableCollection<Word> words = new ObservableCollection<Word>();
                foreach (Dictionary dictionary in dictionaries)
                    foreach (Word word in dictionary.Words)
                        words.Add(word);
                return words;
            }
        }
        public int WordCount
        {
            get { return Words.Count; }
        }

        public ObservableCollection<Word> SelectedWords
        {
            get { return new ObservableCollection<Word>(from x in Words where (x.IsSelected == true) select x); }
        }
        public int SelectedWordCount
        {
            get { return SelectedWords.Count; }
        }

        public DataBaseViewModel(string connectionString)
        {
            dataBase = new DataBaseContext(connectionString);

            DataLoadOptions loadOptions = new DataLoadOptions();
            loadOptions.AssociateWith<Dictionary>(x => x.Words.OrderBy(y => y.Original));
            dataBase.LoadOptions = loadOptions;

            LoadLanguages();
            if (Languages.Count == 0)
            {
                AddLanguages(new Language[] {
                        new Language() { Code = "en", Name = "English" },
                        new Language() { Code = "ru", Name = "Русский" },
                        new Language() { Code = "lv", Name = "Latviešu" }
                    });
            }
        }

        public void SubmitChanges()
        {
            dataBase.SubmitChanges();
        }

        public void LoadLanguages()
        {
            Languages = new ObservableCollection<Language>(from x in dataBase.Languages orderby x.Code select x);
        }

        public void AddLanguage(Language language, bool submit = true)
        {
            dataBase.Languages.InsertOnSubmit(language);
            if (submit)
            {
                SubmitChanges();
                LoadLanguages();
            }
        }

        public void AddLanguages(Language[] languages, bool submit = true)
        {
            dataBase.Languages.InsertAllOnSubmit(languages);
            if (submit)
            {
                SubmitChanges();
                LoadLanguages();
            }
        }

        public void LoadData()
        {
            Dictionaries = new ObservableCollection<Dictionary>(from x in dataBase.Dictionaries where (x.TranslationLanguage == App.SettingsViewModel.TranslationLanguage) && (x.LearningLanguage == App.SettingsViewModel.LearningLanguage) orderby x.Name select x);

            RoutedEventHandler handler = new RoutedEventHandler(SelectionChanged);
            foreach (Dictionary dictionary in Dictionaries)
                if (!dictionary.IsSelectionChangedEventRegistered)
                    dictionary.SelectionChanged += handler;

            if (Dictionaries.Count > 1)
            {
                Dictionaries[Dictionaries.Count - 2].Position = ElementPosition.None;
                Dictionaries[0].Position = ElementPosition.First;
                Dictionaries[1].Position = ElementPosition.None;
                Dictionaries[Dictionaries.Count - 1].Position = ElementPosition.Last;
            }
            else if (Dictionaries.Count == 1)
                Dictionaries[0].Position = ElementPosition.Both;

            handler = new RoutedEventHandler(SelectionChanged);
            ObservableCollection<Word> words = Words;
            foreach (Word word in words)
                if (!word.IsSelectionChangedEventRegistered)
                    word.SelectionChanged += handler;

            if (Words.Count > 1)
            {
                Words[Words.Count - 2].Position = ElementPosition.None;
                Words[0].Position = ElementPosition.First;
                Words[1].Position = ElementPosition.None;
                Words[Words.Count - 1].Position = ElementPosition.Last;
            }
            else if (Words.Count == 1)
                Words[0].Position = ElementPosition.Both;

            NotifyAllChanges();
        }

        private void SelectionChanged(object sender, RoutedEventArgs e)
        {
            NotifyAllChanges();
        }

        private void NotifyAllChanges()
        {
            NotifyPropertyChanged("Dictionaries");
            NotifyPropertyChanged("DictionaryCount");
            NotifyPropertyChanged("SelectedDictionaries");
            NotifyPropertyChanged("SelectedDictionaryCount");
            NotifyPropertyChanged("Words");
            NotifyPropertyChanged("WordCount");
            NotifyPropertyChanged("SelectedWords");
            NotifyPropertyChanged("SelectedWordCount");
        }

        public void AddDictionary(Dictionary dictionary)
        {
            dataBase.Dictionaries.InsertOnSubmit(dictionary);
            SubmitChanges();
            LoadData();
        }

        public void DeleteDictionary(Dictionary dictionary)
        {
            dataBase.Dictionaries.DeleteOnSubmit(dictionary);
            SubmitChanges();
            LoadData();
        }

        public void AddWord(Word word)
        {
            dataBase.Words.InsertOnSubmit(word);
            SubmitChanges();
            LoadData();
        }

        public void DeleteWord(Word word)
        {
            dataBase.Words.DeleteOnSubmit(word);
            _Dictionaries = null;
            SubmitChanges();
            LoadData();
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

    [Database]
    public class DataBaseContext
        : DataContext
    {
        public Table<Language> Languages;
        public Table<Word> Words;
        public Table<Dictionary> Dictionaries;

        public DataBaseContext(string connectionString)
            : base(connectionString)
        {
            if (!DatabaseExists())
                CreateDatabase();
        }
    }

    public enum ElementPosition
    {
        First,
        Last,
        Both,
        None
    }
}