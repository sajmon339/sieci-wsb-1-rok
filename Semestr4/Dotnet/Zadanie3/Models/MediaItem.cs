using System;
using System.ComponentModel;
using System.Linq;

namespace Zadanie3.Models
{
    public class MediaItem : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _title;
        private string _directorOrAuthor;
        private string _studioOrPublisher;
        private string _mediaType;
        private TimeSpan _duration;
        private DateTime _releaseDate;

        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public string DirectorOrAuthor
        {
            get => _directorOrAuthor;
            set
            {
                if (_directorOrAuthor != value)
                {
                    _directorOrAuthor = value;
                    OnPropertyChanged(nameof(DirectorOrAuthor));
                }
            }
        }

        public string StudioOrPublisher
        {
            get => _studioOrPublisher;
            set
            {
                if (_studioOrPublisher != value)
                {
                    _studioOrPublisher = value;
                    OnPropertyChanged(nameof(StudioOrPublisher));
                }
            }
        }

        public string MediaType
        {
            get => _mediaType;
            set
            {
                if (_mediaType != value)
                {
                    _mediaType = value;
                    OnPropertyChanged(nameof(MediaType));
                }
            }
        }

        public TimeSpan Duration
        {
            get => _duration;
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }

        public DateTime ReleaseDate
        {
            get => _releaseDate;
            set
            {
                if (_releaseDate != value)
                {
                    _releaseDate = value;
                    OnPropertyChanged(nameof(ReleaseDate));
                }
            }
        }

        // Implementacja IDataErrorInfo
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Title):
                        if (string.IsNullOrWhiteSpace(Title))
                            return "Tytuł nie może być pusty.";
                        break;
                    case nameof(DirectorOrAuthor):
                        if (string.IsNullOrWhiteSpace(DirectorOrAuthor))
                            return "Reżyser/Autor nie może być pusty.";
                        break;
                    case nameof(StudioOrPublisher):
                        if (string.IsNullOrWhiteSpace(StudioOrPublisher))
                            return "Studio/Wydawca nie może być puste.";
                        break;
                    case nameof(MediaType):
                        if (string.IsNullOrWhiteSpace(MediaType))
                            return "Nośnik nie może być pusty.";
                        break;
                    case nameof(Duration):
                        if (Duration == TimeSpan.Zero)
                            return "Długość musi być większa od zera.";
                        break;
                    case nameof(ReleaseDate):
                        if (ReleaseDate == default)
                            return "Data Wydania nie może być pusta.";
                        break;
                }
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
