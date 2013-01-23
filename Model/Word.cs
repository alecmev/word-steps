using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace WordSteps
{
    [Table(Name = "Words")]
    public class Word
        : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private string _Original;
        private string _Translation;
        private string _Transcription;
        private string _Sound;
        private WordStatus _Status;
        private int _Progress;
        private string _Source;
        private double _EaseFactor;
        private double _PrevEaseFactor;
        private int _NextInterval;
        private int _PrevInterval;
        private int _LessonNumber;
        private int _SuccessfulYesCount;
        private int _YesCount;
        private int _NoCount;
        //private DateTime _NextLessonTime;
        //private DateTime _LastLessonTime;
        //private DateTime _FirstLessonTime;
        //private DateTime _LastChangeTime;
        private string _Examples;
        private EntityRef<Dictionary> _Dictionary;
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

        [Column]
        public int ServerId
        {
            get;
            set;
        }

        [Column(Storage = "_Original")]
        public string Original
        {
            get { return _Original; }
            set
            {
                if (_Original != value)
                {
                    NotifyPropertyChanging("Original");
                    _Original = value;
                    NotifyPropertyChanged("Original");
                }
            }
        }

        [Column(Storage = "_Translation")]
        public string Translation
        {
            get { return _Translation; }
            set
            {
                if (_Translation != value)
                {
                    NotifyPropertyChanging("Translation");
                    _Translation = value;
                    NotifyPropertyChanged("Translation");
                }
            }
        }

        [Column(Storage = "_Transcription")]
        public string Transcription
        {
            get { return _Transcription; }
            set
            {
                if (_Transcription != value)
                {
                    NotifyPropertyChanging("Transcription");
                    _Transcription = value;
                    NotifyPropertyChanged("Transcription");
                }
            }
        }

        [Column(Storage = "_Sound")]
        public string Sound
        {
            get { return _Sound; }
            set
            {
                if (_Sound != value)
                {
                    NotifyPropertyChanging("Sound");
                    _Sound = value;
                    NotifyPropertyChanged("Sound");
                }
            }
        }

        [Column(Storage = "_Status")]
        public WordStatus Status
        {
            get { return _Status; }
            set
            {
                if (_Status != value)
                {
                    NotifyPropertyChanging("Status");
                    _Status = value;
                    NotifyPropertyChanged("Status");
                }
            }
        }

        [Column(Storage = "_Progress")]
        public int Progress
        {
            get { return _Progress; }
            set
            {
                if (_Progress != value)
                {
                    NotifyPropertyChanging("Progress");
                    _Progress = value;
                    NotifyPropertyChanged("Progress");
                }
            }
        }

        [Column(Storage = "_Source")]
        public string Source
        {
            get { return _Source; }
            set
            {
                if (_Source != value)
                {
                    NotifyPropertyChanging("Source");
                    _Source = value;
                    NotifyPropertyChanged("Source");
                }
            }
        }

        [Column(Storage = "_EaseFactor")]
        public double EaseFactor
        {
            get { return _EaseFactor; }
            set
            {
                if (_EaseFactor != value)
                {
                    NotifyPropertyChanging("EaseFactor");
                    _EaseFactor = value;
                    NotifyPropertyChanged("EaseFactor");
                }
            }
        }

        [Column(Storage = "_PrevEaseFactor")]
        public double PrevEaseFactor
        {
            get { return _PrevEaseFactor; }
            set
            {
                if (_PrevEaseFactor != value)
                {
                    NotifyPropertyChanging("PrevEaseFactor");
                    _PrevEaseFactor = value;
                    NotifyPropertyChanged("PrevEaseFactor");
                }
            }
        }

        [Column(Storage = "_NextInterval")]
        public int NextInterval
        {
            get { return _NextInterval; }
            set
            {
                if (_NextInterval != value)
                {
                    NotifyPropertyChanging("NextInterval");
                    _NextInterval = value;
                    NotifyPropertyChanged("NextInterval");
                }
            }
        }

        [Column(Storage = "_PrevInterval")]
        public int PrevInterval
        {
            get { return _PrevInterval; }
            set
            {
                if (_PrevInterval != value)
                {
                    NotifyPropertyChanging("PrevInterval");
                    _PrevInterval = value;
                    NotifyPropertyChanged("PrevInterval");
                }
            }
        }

        [Column(Storage = "_LessonNumber")]
        public int LessonNumber
        {
            get { return _LessonNumber; }
            set
            {
                if (_LessonNumber != value)
                {
                    NotifyPropertyChanging("LessonNumber");
                    _LessonNumber = value;
                    NotifyPropertyChanged("LessonNumber");
                }
            }
        }

        [Column(Storage = "_SuccessfulYesCount")]
        public int SuccessfulYesCount
        {
            get { return _SuccessfulYesCount; }
            set
            {
                if (_SuccessfulYesCount != value)
                {
                    NotifyPropertyChanging("SuccessfulYesCount");
                    _SuccessfulYesCount = value;
                    NotifyPropertyChanged("SuccessfulYesCount");
                }
            }
        }

        [Column(Storage = "_YesCount")]
        public int YesCount
        {
            get { return _YesCount; }
            set
            {
                if (_YesCount != value)
                {
                    NotifyPropertyChanging("YesCount");
                    _YesCount = value;
                    NotifyPropertyChanged("YesCount");
                }
            }
        }

        [Column(Storage = "_NoCount")]
        public int NoCount
        {
            get { return _NoCount; }
            set
            {
                if (_NoCount != value)
                {
                    NotifyPropertyChanging("NoCount");
                    _NoCount = value;
                    NotifyPropertyChanged("NoCount");
                }
            }
        }

        /*[Column(Storage = "_NextLessonTime")]
        public DateTime NextLessonTime
        {
            get { return _NextLessonTime; }
            set
            {
                if (_NextLessonTime != value)
                {
                    NotifyPropertyChanging("NextLessonTime");
                    _NextLessonTime = value;
                    NotifyPropertyChanged("NextLessonTime");
                }
            }
        }

        [Column(Storage = "_LastLessonTime")]
        public DateTime LastLessonTime
        {
            get { return _LastLessonTime; }
            set
            {
                if (_LastLessonTime != value)
                {
                    NotifyPropertyChanging("LastLessonTime");
                    _LastLessonTime = value;
                    NotifyPropertyChanged("LastLessonTime");
                }
            }
        }

        [Column(Storage = "_FirstLessonTime")]
        public DateTime FirstLessonTime
        {
            get { return _FirstLessonTime; }
            set
            {
                if (_FirstLessonTime != value)
                {
                    NotifyPropertyChanging("FirstLessonTime");
                    _FirstLessonTime = value;
                    NotifyPropertyChanged("FirstLessonTime");
                }
            }
        }

        [Column(Storage = "_LastChangeTime")]
        public DateTime LastChangeTime
        {
            get { return _LastChangeTime; }
            set
            {
                if (_LastChangeTime != value)
                {
                    NotifyPropertyChanging("LastChangeTime");
                    _LastChangeTime = value;
                    NotifyPropertyChanged("LastChangeTime");
                }
            }
        }*/

        [Column(Storage = "_Examples")]
        public string Examples
        {
            get { return _Examples; }
            set
            {
                if (_Examples != value)
                {
                    NotifyPropertyChanging("Examples");
                    _Examples = value;
                    NotifyPropertyChanged("Examples");
                }
            }
        }

        [Column]
        public int? DictionaryId
        {
            get;
            set;
        }
        [Association(Storage = "_Dictionary", ThisKey = "DictionaryId", OtherKey = "Id", IsForeignKey = true)]
        public Dictionary Dictionary
        {
            get { return _Dictionary.Entity; }
            set
            {
                if ((_Dictionary.Entity != value) || (_Dictionary.HasLoadedOrAssignedValue == false))
                {
                    NotifyPropertyChanging("Dictionary");
                    if (_Dictionary.Entity != null)
                    {
                        _Dictionary.Entity.Words.Remove(this);
                        _Dictionary.Entity = null;
                    }
                    _Dictionary.Entity = value;
                    if (value != null)
                    {
                        _Dictionary.Entity.Words.Add(this);
                        DictionaryId = _Dictionary.Entity.Id;
                    }
                    else
                        DictionaryId = null;
                    NotifyPropertyChanged("Dictionary");
                }
            }
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

        public Word()
        {
            _Dictionary = new EntityRef<Dictionary>();
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

    public enum WordStatus
    {
        Learn,
        Learned,
        Postpone
    }

    public class WordListBoxOffsetConverter : IValueConverter
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