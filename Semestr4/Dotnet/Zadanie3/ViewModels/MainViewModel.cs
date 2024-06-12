using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using Zadanie3.Models;
using Zadanie3.Views;

namespace Zadanie3.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MediaItem> MediaItems { get; set; } = new ObservableCollection<MediaItem>();

        private MediaItem _selectedMediaItem;
        public MediaItem SelectedMediaItem
        {
            get => _selectedMediaItem;
            set
            {
                _selectedMediaItem = value;
                OnPropertyChanged(nameof(SelectedMediaItem));
                ((RelayCommand)EditCommand).RaiseCanExecuteChanged();
                ((RelayCommand)RemoveCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand ImportCommand { get; }
        public ICommand ExportCommand { get; }

        public MainViewModel()
        {
            AddCommand = new RelayCommand(OnAdd);
            EditCommand = new RelayCommand(OnEdit, () => SelectedMediaItem != null);
            RemoveCommand = new RelayCommand(OnRemove, () => SelectedMediaItem != null);
            ImportCommand = new RelayCommand(OnImport);
            ExportCommand = new RelayCommand(OnExport);
        }

        private void OnAdd()
        {
            var newItem = new MediaItem();
            var editWindow = new EditWindow(newItem);
            if (editWindow.ShowDialog() == true)
            {
                if (!IsDuplicate(newItem))
                {
                    MediaItems.Add(newItem);
                }
                else
                {
                    MessageBox.Show("Element o takich samych danych już istnieje.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void OnEdit()
        {
            if (SelectedMediaItem == null) return;

            var editWindow = new EditWindow(new MediaItem
            {
                Title = SelectedMediaItem.Title,
                DirectorOrAuthor = SelectedMediaItem.DirectorOrAuthor,
                StudioOrPublisher = SelectedMediaItem.StudioOrPublisher,
                MediaType = SelectedMediaItem.MediaType,
                Duration = SelectedMediaItem.Duration,
                ReleaseDate = SelectedMediaItem.ReleaseDate
            });

            if (editWindow.ShowDialog() == true)
            {
                if (!IsDuplicate(editWindow.DataContext as MediaItem))
                {
                    SelectedMediaItem.Title = ((MediaItem)editWindow.DataContext).Title;
                    SelectedMediaItem.DirectorOrAuthor = ((MediaItem)editWindow.DataContext).DirectorOrAuthor;
                    SelectedMediaItem.StudioOrPublisher = ((MediaItem)editWindow.DataContext).StudioOrPublisher;
                    SelectedMediaItem.MediaType = ((MediaItem)editWindow.DataContext).MediaType;
                    SelectedMediaItem.Duration = ((MediaItem)editWindow.DataContext).Duration;
                    SelectedMediaItem.ReleaseDate = ((MediaItem)editWindow.DataContext).ReleaseDate;
                }
                else
                {
                    MessageBox.Show("Element o takich samych danych już istnieje.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void OnRemove()
        {
            if (SelectedMediaItem != null)
            {
                MediaItems.Remove(SelectedMediaItem);
            }
        }

        private void OnImport()
        {
            try
            {
                var openFileDialog = new Microsoft.Win32.OpenFileDialog
                {
                    DefaultExt = ".json",
                    Filter = "JSON Files (*.json)|*.json"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    var json = File.ReadAllText(openFileDialog.FileName);
                    var items = JsonSerializer.Deserialize<ObservableCollection<MediaItem>>(json);
                    MediaItems.Clear();
                    foreach (var item in items)
                    {
                        MediaItems.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas importu: " + ex.Message);
            }
        }

        private void OnExport()
        {
            try
            {
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    DefaultExt = ".json",
                    Filter = "JSON Files (*.json)|*.json"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    var json = JsonSerializer.Serialize(MediaItems);
                    File.WriteAllText(saveFileDialog.FileName, json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas eksportu: " + ex.Message);
            }
        }

        private bool IsDuplicate(MediaItem newItem)
        {
            return MediaItems.Any(item =>
                item.Title == newItem.Title &&
                item.DirectorOrAuthor == newItem.DirectorOrAuthor &&
                item.StudioOrPublisher == newItem.StudioOrPublisher &&
                item.MediaType == newItem.MediaType &&
                item.Duration == newItem.Duration &&
                item.ReleaseDate == newItem.ReleaseDate);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
