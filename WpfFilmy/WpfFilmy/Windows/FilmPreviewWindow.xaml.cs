using System.Windows;

namespace WpfFilmy.Windows
{
    /// <summary>
    /// Interaction logic for FilmPreviewWindow.xaml
    /// </summary>
    public partial class FilmPreviewWindow : Window
    {
        public Film FilmToDisplay
        {
            get { return (Film)GetValue(FilmToDisplayProperty); }
            set { SetValue(FilmToDisplayProperty, value); }
        }

        public static readonly DependencyProperty FilmToDisplayProperty =
            DependencyProperty.Register("FilmToDisplay", typeof(Film), typeof(FilmPreviewWindow), new PropertyMetadata(null, OnFilmToDisplayChanged));

        
        private static void OnFilmToDisplayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FilmPreviewWindow window = (FilmPreviewWindow)d;
            window.UpdateDisplay(e.NewValue as Film);
        }

        public FilmPreviewWindow()
        {
            InitializeComponent();
        }
        
        private void UpdateDisplay(Film film)
        {
            if (film != null)
            {
                titleTextBlock.Text = film.Tytul;
                releaseDateTextBlock.Text = film.DataPremiery.ToShortDateString();
                descriptionTextBlock.Text = film.Opis;
            }
            else
            {
                ClearDisplay();
            }
        }
        
        public void ClearDisplay()
        {
            titleTextBlock.Text = "Brak wybranego filmu";
            releaseDateTextBlock.Text = string.Empty;
            descriptionTextBlock.Text = string.Empty;
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             UpdateDisplay(this.FilmToDisplay);
        }
        
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}