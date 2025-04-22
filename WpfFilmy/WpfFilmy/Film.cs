namespace WpfFilmy;

public class Film
{
    public string Tytul { get; set; }
    public DateTime DataPremiery { get; set; }
    public string Opis { get; set; }

    public Film()
    {
        // Domyślna data premiery, np. dzisiejsza
        DataPremiery = DateTime.Today;
    }

    // Nadpisujemy ToString(), aby ListBox wyświetlał tylko tytuł
    public override string ToString()
    {
        return Tytul;
    }

    // Metoda do tworzenia kopii filmu (użyteczna przy edycji/przekazywaniu danych)
    public Film Clone()
    {
        return new Film
        {
            Tytul = this.Tytul,
            DataPremiery = this.DataPremiery,
            Opis = this.Opis
        };
    }
}