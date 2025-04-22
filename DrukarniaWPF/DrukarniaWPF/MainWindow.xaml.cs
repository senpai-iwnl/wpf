using System.Windows;
using System.Windows.Controls;

namespace DrukarniaWPF;

public partial class MainWindow : Window
{
    private const int BasePriceA5Groszy = 20;
    private readonly decimal[] formatMultipliers = { 1.0m, 2.5m, 2.5m * 2.5m, 2.5m * 2.5m * 2.5m, 2.5m * 2.5m * 2.5m * 2.5m, 2.5m * 2.5m * 2.5m * 2.5m * 2.5m };
    private readonly string[] formatNames = { "A5", "A4", "A3", "A2", "A1", "A0" };

    private bool _isLoaded = false;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        _isLoaded = true;
        UpdateSummary();
    }


    private void UpdateSummary()
    {
        if (!_isLoaded)
        {
            return;
        }

        int naklad = 0;
        bool isNakladValid = int.TryParse(txtNaklad.Text, out naklad) && naklad > 0;

        if (!isNakladValid && !string.IsNullOrWhiteSpace(txtNaklad.Text))
        {
            tblFormatPrice.Text = "Nieprawidłowy nakład";
            txtPodsumowanie.Text = "Proszę wprowadzić prawidłową liczbę sztuk (> 0) dla wyceny.";
            return;
        }

        if (!isNakladValid && string.IsNullOrWhiteSpace(txtNaklad.Text))
        {
            naklad = 0;
        } else if (!isNakladValid && naklad <= 0 && !string.IsNullOrWhiteSpace(txtNaklad.Text))
        {
            tblFormatPrice.Text = "Nakład musi być > 0";
            txtPodsumowanie.Text = "Proszę wprowadzić nakład większy od zera.";
            return;
        }


        int formatIndex = (int)sldFormat.Value;
        string formatName = formatNames[formatIndex];
        decimal gramaturaMultiplier = 1.0m;
        string gramaturaText = "80 g/m²";

        if (rbGramatura120.IsChecked == true) { gramaturaMultiplier = 2.0m; gramaturaText = "120 g/m²"; }
        else if (rbGramatura200.IsChecked == true) { gramaturaMultiplier = 2.5m; gramaturaText = "200 g/m²"; }
        else if (rbGramatura240.IsChecked == true) { gramaturaMultiplier = 3.0m; gramaturaText = "240 g/m²"; }

        bool drukKolor = chkDrukKolor.IsChecked == true;
        bool drukDwustronny = chkDrukDwustronny.IsChecked == true;
        bool lakierUV = chkLakierUV.IsChecked == true;
        bool kolorowyPapier = chkKolorowyPapier.IsChecked == true;
        string kolorPapieru = cmbKolorPapieru.SelectedItem is ComboBoxItem selectedItem ? selectedItem.Content.ToString() : "nie wybrano koloru";

        bool terminEkspres = rbTerminEkspres.IsChecked == true;

        decimal basePricePerCopyGroszy = BasePriceA5Groszy * formatMultipliers[formatIndex];
        basePricePerCopyGroszy = Math.Round(basePricePerCopyGroszy);

        tblFormatPrice.Text = $"{formatName} - cena {basePricePerCopyGroszy}gr/szt.";

        if (naklad == 0)
        {
            string basicSummary = $"Przedmiot zamówienia: Podaj nakład, format {formatName}, {gramaturaText}";
            string opcjePart = "";
            if (drukKolor) opcjePart += "druk kolor, ";
            if (drukDwustronny) opcjePart += "druk dwustronny, ";
            if (lakierUV) opcjePart += "lakier UV, ";
            if (kolorowyPapier) opcjePart += $"papier {kolorPapieru}, ";
            if (!string.IsNullOrEmpty(opcjePart)) basicSummary += ", " + opcjePart.TrimEnd(',', ' ');

            txtPodsumowanie.Text = $"{basicSummary}\nCena przed rabatem: 0,00zł\nNaliczony rabat: 0% (0,00zł)\nCena po rabacie: 0,00zł";
            return;
        }


        decimal pricePerCopyWithGramatura = basePricePerCopyGroszy * gramaturaMultiplier;

        decimal additionalPercentageCostPerCopy = 0;
        if (drukKolor) additionalPercentageCostPerCopy += 0.30m;
        if (drukDwustronny) additionalPercentageCostPerCopy += 0.50m;
        if (lakierUV) additionalPercentageCostPerCopy += 0.20m;
        if (kolorowyPapier) additionalPercentageCostPerCopy += 0.50m;

        decimal totalPercentageIncreasePerCopy = basePricePerCopyGroszy * additionalPercentageCostPerCopy;

        decimal totalPricePerCopyGroszy = pricePerCopyWithGramatura + totalPercentageIncreasePerCopy;
        totalPricePerCopyGroszy = Math.Round(totalPricePerCopyGroszy);


        decimal totalPriceBeforeFixedCostsGroszy = totalPricePerCopyGroszy * naklad;


        decimal deliveryCostGroszy = terminEkspres ? 1500 : 0;


        decimal priceForDiscountCalculationGroszy = totalPriceBeforeFixedCostsGroszy;


        int discountPercentage = Math.Min(naklad / 100, 10);
        decimal discountAmountGroszy = priceForDiscountCalculationGroszy * (discountPercentage / 100.0m);
        discountAmountGroszy = Math.Round(discountAmountGroszy);


        decimal priceAfterDiscountBeforeFixedCostsGroszy = priceForDiscountCalculationGroszy - discountAmountGroszy;


        decimal finalPriceGroszy = priceAfterDiscountBeforeFixedCostsGroszy + deliveryCostGroszy;

        decimal cenaPrzedRabatemZl = (totalPriceBeforeFixedCostsGroszy + deliveryCostGroszy) / 100.0m;
        decimal naliczonyRabatZl = discountAmountGroszy / 100.0m;
        decimal cenaPoRabacieZl = finalPriceGroszy / 100.0m;


        string summaryText = "Przedmiot zamówienia: ";

        summaryText += $"{naklad} szt., ";


        summaryText += $"format {formatName}, {gramaturaText}";

        string opcjeWydrukuText = "";
        if (drukKolor) opcjeWydrukuText += "druk kolor, ";
        if (drukDwustronny) opcjeWydrukuText += "druk dwustronny, ";
        if (lakierUV) opcjeWydrukuText += "lakier UV, ";
        if (kolorowyPapier) opcjeWydrukuText += $"papier {kolorPapieru}, ";

        if (!string.IsNullOrEmpty(opcjeWydrukuText))
        {
            summaryText += $", {opcjeWydrukuText.TrimEnd(',', ' ')}";
        }

        summaryText += $"\nCena przed rabatem: {cenaPrzedRabatemZl:F2}zł";
        summaryText += $"\nNaliczony rabat: {discountPercentage}% ({naliczonyRabatZl:F2}zł)";
        summaryText += $"\nCena po rabacie: {cenaPoRabacieZl:F2}zł";


        txtPodsumowanie.Text = summaryText;
    }

    private void txtNaklad_TextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateSummary();
    }


    private void sldFormat_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        UpdateSummary();
    }

    private void chkKolorowyPapier_CheckedChanged(object sender, RoutedEventArgs e)
    {
        cmbKolorPapieru.IsEnabled = chkKolorowyPapier.IsChecked == true;
        if (chkKolorowyPapier.IsChecked == false)
        {
            cmbKolorPapieru.SelectedIndex = -1;
        }
        UpdateSummary();
    }

    private void cmbKolorPapieru_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateSummary();
    }

    private void rbGramatura_Checked(object sender, RoutedEventArgs e)
    {
        UpdateSummary();
    }

    private void chkOpcjeWydruku_CheckedChanged(object sender, RoutedEventArgs e)
    {
        UpdateSummary();
    }

    private void rbTerminRealizacji_Checked(object sender, RoutedEventArgs e)
    {
        UpdateSummary();
    }

    private void btnOK_Click(object sender, RoutedEventArgs e)
    {
        int naklad = 0;
        if (!int.TryParse(txtNaklad.Text, out naklad) || naklad <= 0)
        {
            MessageBox.Show("Proszę wprowadzić prawidłowy nakład (> 0) przed złożeniem zamówienia.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        MessageBox.Show("Zamówienie przyjęte!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
        ResetForm();
    }

    private void btnAnuluj_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void ResetForm()
    {
        _isLoaded = false;

        txtNaklad.Clear();
        sldFormat.Value = 0;
        chkKolorowyPapier.IsChecked = false;
        cmbKolorPapieru.SelectedIndex = -1;
        rbGramatura80.IsChecked = true;
        chkDrukKolor.IsChecked = false;
        chkDrukDwustronny.IsChecked = false;
        chkLakierUV.IsChecked = false;
        rbTerminStandard.IsChecked = true;

        _isLoaded = true;
        UpdateSummary();
    }
}