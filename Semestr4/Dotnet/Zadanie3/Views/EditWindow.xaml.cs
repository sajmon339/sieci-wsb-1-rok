using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Zadanie3.Models;

namespace Zadanie3.Views
{
    public partial class EditWindow : Window
    {
        public EditWindow(MediaItem mediaItem)
        {
            InitializeComponent();
            DataContext = mediaItem;
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            if (HasValidationErrors())
            {
                MessageBox.Show("Formularz zawiera błędy. Proszę je poprawić przed zapisaniem.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                DialogResult = true;
            }
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private bool HasValidationErrors()
        {
            return Validation.GetHasError(TitleTextBox) ||
                   Validation.GetHasError(DirectorOrAuthorTextBox) ||
                   Validation.GetHasError(StudioOrPublisherTextBox) ||
                   Validation.GetHasError(MediaTypeComboBox) ||
                   Validation.GetHasError(DurationTextBox) ||
                   Validation.GetHasError(ReleaseDatePicker);
        }
    }
}
