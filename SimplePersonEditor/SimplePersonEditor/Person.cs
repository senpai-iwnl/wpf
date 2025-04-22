using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimplePersonEditor;

public class Person : INotifyPropertyChanged
{
    private string _firstName;
    private string _lastName;
    private string _email;
    private bool _detailsVisible;
    private decimal _paymentAmount;
    private string _region;
    private int _accessLevel;

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (_firstName != value)
            {
                _firstName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayName)); 
            }
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (_lastName != value)
            {
                _lastName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayName)); 
            }
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            if (_email != value)
            {
                _email = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayName)); 
            }
        }
    }

    public bool DetailsVisible
    {
        get => _detailsVisible;
        set
        {
            if (_detailsVisible != value)
            {
                _detailsVisible = value;
                OnPropertyChanged();
            }
        }
    }

    public decimal PaymentAmount
    {
        get => _paymentAmount;
        set
        {
            if (_paymentAmount != value)
            {
                _paymentAmount = value;
                OnPropertyChanged();
            }
        }
    }

    public string Region
    {
        get => _region;
        set
        {
            if (_region != value)
            {
                _region = value;
                OnPropertyChanged();
            }
        }
    }

    public int AccessLevel
    {
        get => _accessLevel;
        set
        {
            if (_accessLevel != value)
            {
                _accessLevel = value;
                OnPropertyChanged();
            }
        }
    }
    
    public string DisplayName => $"{FirstName} {LastName} ({Email})";

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}