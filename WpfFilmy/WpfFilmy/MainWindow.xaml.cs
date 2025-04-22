using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfFilmy.Windows; // Pamiętaj o tej przestrzeni nazw

namespace WpfFilmy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Używamy ObservableCollection, która automatycznie powiadamia UI o zmianach w kolekcji
        public ObservableCollection<Film> FilmsCollection { get; set; }

        private FilmPreviewWindow previewWindow = null; // Pole do przechowywania referencji do okna podglądu

        public MainWindow()
        {
            InitializeComponent();

            FilmsCollection = new ObservableCollection<Film>();

            // Ustawienie źródła danych dla ListBoxa
            filmListBox.ItemsSource = FilmsCollection;

            // Dodanie kilku przykładowych filmów
            FilmsCollection.Add(new Film { Tytul = "Matrix", DataPremiery = new System.DateTime(1999, 3, 31), Opis = "Hackers discover the true nature of their reality and their role in the war against its controllers." });
            FilmsCollection.Add(new Film { Tytul = "Incepcja", DataPremiery = new System.DateTime(2010, 7, 16), Opis = "A thief who steals corporate secrets through the use of dream-sharing technology." });
            FilmsCollection.Add(new Film { Tytul = "Parasite", DataPremiery = new System.DateTime(2019, 5, 30), Opis = "A poor family schemes to insinuate themselves into a wealthy family." });
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            // Tworzymy nowe okno do dodawania filmu
            FilmDetailsWindow detailsWindow = new FilmDetailsWindow();

            // Ustawiamy tryb na dodawanie (FilmData będzie na początku puste/domyślne)
            detailsWindow.FilmData = new Film();

            // Otwieramy okno modalnie
            bool? result = detailsWindow.ShowDialog();

            // Jeśli użytkownik kliknął OK (DialogResult == true)
            if (result == true)
            {
                // Dodajemy nowy film do kolekcji
                FilmsCollection.Add(detailsWindow.FilmData);
            }
            // W przeciwnym wypadku (Anuluj) nic nie robimy, obiekt FilmData jest po prostu tracony
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobieramy zaznaczony film
            Film selectedFilm = filmListBox.SelectedItem as Film;

            // Sprawdzamy czy coś jest zaznaczone (choć przycisk jest wyłączony gdy nic nie ma, to dobre zabezpieczenie)
            if (selectedFilm != null)
            {
                // Tworzymy nowe okno do edycji filmu
                FilmDetailsWindow detailsWindow = new FilmDetailsWindow();

                // Przekazujemy KOPIĘ zaznaczonego filmu do okna szczegółów
                // Dzięki temu oryginalny obiekt nie jest zmieniany, dopóki nie klikniemy OK
                detailsWindow.FilmData = selectedFilm.Clone();

                // Otwieramy okno modalnie
                bool? result = detailsWindow.ShowDialog();

                // Jeśli użytkownik kliknął OK (DialogResult == true)
                if (result == true)
                {
                    // Znajdujemy indeks oryginalnego filmu w kolekcji
                    int index = FilmsCollection.IndexOf(selectedFilm);

                    // Jeśli znaleziono film
                    if (index != -1)
                    {
                        // Zastępujemy stary obiekt nowym z edytowanymi danymi
                        FilmsCollection[index] = detailsWindow.FilmData;
                    }
                }
                // W przeciwnym wypadku (Anuluj) nic nie robimy, zmiany w FilmData okna szczegółów są tracone
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobieramy zaznaczony film
            Film selectedFilm = filmListBox.SelectedItem as Film;

            // Sprawdzamy czy coś jest zaznaczone
            if (selectedFilm != null)
            {
                // Pytamy o potwierdzenie usunięcia
                MessageBoxResult result = MessageBox.Show(
                    $"Czy na pewno chcesz usunąć film \"{selectedFilm.Tytul}\"?",
                    "Potwierdzenie usunięcia",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                // Jeśli użytkownik potwierdził
                if (result == MessageBoxResult.Yes)
                {
                    // Usuwamy film z kolekcji
                    FilmsCollection.Remove(selectedFilm);
                }
            }
        }

        private void previewButton_Click(object sender, RoutedEventArgs e)
        {
            Film selectedFilm = filmListBox.SelectedItem as Film;
            
            if (selectedFilm != null)
            {
                if (previewWindow == null)
                {
                    previewWindow = new FilmPreviewWindow();
                    
                    previewWindow.Closed += PreviewWindow_Closed;
                    
                    previewWindow.FilmToDisplay = selectedFilm;
                    
                    previewWindow.Show();
                }
                else
                {
                    previewWindow.Activate();
                    
                    previewWindow.FilmToDisplay = selectedFilm;
                }
            }
        }
        
        private void filmListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isItemSelected = filmListBox.SelectedItem != null;
            
            editButton.IsEnabled = isItemSelected;
            deleteButton.IsEnabled = isItemSelected;
            previewButton.IsEnabled = isItemSelected;
            
            if (previewWindow != null && isItemSelected)
            {
                previewWindow.FilmToDisplay = filmListBox.SelectedItem as Film;
            }
            else if (previewWindow != null && !isItemSelected)
            {
                previewWindow.ClearDisplay(); 
            }
        }
        
        private void PreviewWindow_Closed(object sender, System.EventArgs e)
        {
            previewWindow = null;
        }
    }
}