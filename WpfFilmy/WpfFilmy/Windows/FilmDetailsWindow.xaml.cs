using System;
using System.Windows;

namespace WpfFilmy.Windows
{
    /// <summary>
    /// Interaction logic for FilmDetailsWindow.xaml
    /// </summary>
    public partial class FilmDetailsWindow : Window
    {
        public Film FilmData { get; set; }

        public FilmDetailsWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (FilmData != null)
            {
                titleTextBox.Text = FilmData.Tytul;
                releaseDatePicker.SelectedDate = FilmData.DataPremiery;
                descriptionTextBox.Text = FilmData.Opis;
            }
            else
            {

                releaseDatePicker.SelectedDate = DateTime.Today;
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titleTextBox.Text))
            {
                MessageBox.Show("Tytuł filmu nie może być pusty.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Nie zamykamy okna
            }
            
            if (FilmData == null)
            {
                FilmData = new Film();
            }
            
            FilmData.Tytul = titleTextBox.Text;
            FilmData.DataPremiery = releaseDatePicker.SelectedDate ?? DateTime.Today;
            FilmData.Opis = descriptionTextBox.Text;
            
            this.DialogResult = true;
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}