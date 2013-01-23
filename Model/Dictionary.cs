using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace WordSteps
{
    [Table(Name = "Dictionaries")]
    public class Dictionary
        : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private string _Name;
        private EntityRef<Language> _LearningLanguage;
        private EntityRef<Language> _TranslationLanguage;
        private EntitySet<Word> _Words;
        private bool _IsSelected = false;
        private ElementPosition _Position = ElementPosition.None;

        [Column(IsVersion = true)]
        private Binary Version
        {
            get;
            set;
        }

        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get;
            set;
        }

        [Column(Storage = "_Name")]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    NotifyPropertyChanging("Name");
                    _Name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        [Column]
        public int? LearningLanguageId
        {
            get;
            set;
        }
        [Association(Storage = "_LearningLanguage", ThisKey = "LearningLanguageId", OtherKey = "Id", IsForeignKey = true)]
        public Language LearningLanguage
        {
            get { return _LearningLanguage.Entity; }
            set
            {
                if ((_LearningLanguage.Entity != value) || (_LearningLanguage.HasLoadedOrAssignedValue == false))
                {
                    NotifyPropertyChanging("LearningLanguage");
                    if (_LearningLanguage.Entity != null)
                    {
                        _LearningLanguage.Entity.OriginalDictionaries.Remove(this);
                        _LearningLanguage.Entity = null;
                    }
                    _LearningLanguage.Entity = value;
                    if (value != null)
                    {
                        _LearningLanguage.Entity.OriginalDictionaries.Add(this);
                        LearningLanguageId = _LearningLanguage.Entity.Id;
                    }
                    else
                        LearningLanguageId = null;
                    NotifyPropertyChanged("LearningLanguage");
                }
            }
        }

        [Column]
        public int? TranslationLanguageId
        {
            get;
            set;
        }
        [Association(Storage = "_TranslationLanguage", ThisKey = "TranslationLanguageId", OtherKey = "Id", IsForeignKey = true)]
        public Language TranslationLanguage
        {
            get { return _TranslationLanguage.Entity; }
            set
            {
                if ((_TranslationLanguage.Entity != value) || (_TranslationLanguage.HasLoadedOrAssignedValue == false))
                {
                    NotifyPropertyChanging("TranslationLanguage");
                    if (_TranslationLanguage.Entity != null)
                    {
                        _TranslationLanguage.Entity.TranslationDictionaries.Remove(this);
                        _TranslationLanguage.Entity = null;
                    }
                    _TranslationLanguage.Entity = value;
                    if (value != null)
                    {
                        _TranslationLanguage.Entity.TranslationDictionaries.Add(this);
                        TranslationLanguageId = _TranslationLanguage.Entity.Id;
                    }
                    else
                        TranslationLanguageId = null;
                    NotifyPropertyChanged("TranslationLanguage");
                }
            }
        }

        [Association(Storage = "_Words", ThisKey = "Id", OtherKey = "DictionaryId")]
        public EntitySet<Word> Words
        {
            get { return _Words; }
        }

        public int WordCount
        {
            get { return Words.Count; }
        }
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (_IsSelected != value)
                {
                    NotifyPropertyChanging("IsSelected");
                    _IsSelected = value;
                    NotifyPropertyChanged("IsSelected");
                    NotifySelectionChanged();
                }
            }
        }
        public ElementPosition Position
        {
            get { return _Position; }
            set
            {
                if (value != _Position)
                {
                    NotifyPropertyChanging("Position");
                    _Position = value;
                    NotifyPropertyChanged("Position");
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        public event RoutedEventHandler SelectionChanged;
        public bool IsSelectionChangedEventRegistered
        {
            get { return SelectionChanged != null; }
        }

        public Dictionary()
        {
            _Words = new EntitySet<Word>(OnWordAdded, OnWordRemoved);
            _Words.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(OnWordsChanged);
        }

        private void OnWordAdded(Word word)
        {
            word.Dictionary = this;
        }
        private void OnWordRemoved(Word word)
        {
            word.Dictionary = null;
        }
        private void OnWordsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("Words");
        }

        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void NotifySelectionChanged()
        {
            if (IsSelectionChangedEventRegistered)
            {
                SelectionChanged(this, null);
            }
        }
    }

    public class DictionaryListBoxOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ElementPosition position = (ElementPosition)value;
            return new Thickness(0, position == ElementPosition.First || position == ElementPosition.Both ? (Int32)Application.Current.Resources["ListBoxFirstItemTopOffset"] : 0, 0, position == ElementPosition.Last || position == ElementPosition.Both ? (Int32)Application.Current.Resources["ListBoxLastItemBottomOffset"] : 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Thickness margin = (Thickness)value;
            if (margin.Top == (Int32)Application.Current.Resources["ListBoxFirstItemTopOffset"])
            {
                if (margin.Bottom == (Int32)Application.Current.Resources["ListBoxLastItemBottomOffset"]) return ElementPosition.Both;
                else return ElementPosition.First;
            }
            else if (margin.Bottom == (Int32)Application.Current.Resources["ListBoxLastItemBottomOffset"]) return ElementPosition.Last;
            else return ElementPosition.None;
        }
    }
}