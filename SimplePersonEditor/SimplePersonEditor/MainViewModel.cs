using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input; 

namespace SimplePersonEditor
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);
        
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private Person _selectedPerson;

        public ObservableCollection<Person> People { get; set; }
        public ObservableCollection<string> Regions { get; set; }

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                if (_selectedPerson != value)
                {
                    _selectedPerson = value;
                    OnPropertyChanged();
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public ICommand AddPersonCommand { get; }
        public ICommand DeletePersonCommand { get; }

        public MainViewModel()
        {
            People = new ObservableCollection<Person>
            {
                new Person { FirstName = "Jan", LastName = "Kowalski", Email = "jan.kowalski@example.com", DetailsVisible = true, PaymentAmount = 150.50m, Region = "Mazowieckie", AccessLevel = 50 },
                new Person { FirstName = "Anna", LastName = "Nowak", Email = "anna.nowak@example.com", DetailsVisible = false, PaymentAmount = 0, Region = "Małopolskie", AccessLevel = 10 },
                new Person { FirstName = "Piotr", LastName = "Wiśniewski", Email = "piotr.wisniewski@pb.edu.pl", DetailsVisible = true, PaymentAmount = 249.95m, Region = "Podlaskie", AccessLevel = 90 }
            };


            Regions = new ObservableCollection<string>
            {
                "Dolnośląskie",
                "Kujawsko-Pomorskie",
                "Lubelskie",
                "Lubuskie",
                "Łódzkie",
                "Małopolskie",
                "Mazowieckie",
                "Opolskie",
                "Podkarpackie",
                "Podlaskie", 
                "Pomorskie",
                "Śląskie",
                "Świętokrzyskie",
                "Warmińsko-Mazurskie",
                "Wielkopolskie",
                "Zachodniopomorskie"
            };
            
            AddPersonCommand = new RelayCommand(AddPerson);
            DeletePersonCommand = new RelayCommand(DeletePerson, CanDeletePerson);
            
            if (People.Count > 0)
            {
                SelectedPerson = People[0];
            }
        }

        private void AddPerson(object parameter)
        {
            var newPerson = new Person
            {
                FirstName = "Nowa",
                LastName = "Osoba",
                Email = "nowa.osoba@example.com",
                DetailsVisible = false,
                PaymentAmount = 0,
                Region = Regions.FirstOrDefault(), 
                AccessLevel = 0
            };
            People.Add(newPerson);
            SelectedPerson = newPerson; 
        }

        private void DeletePerson(object parameter)
        {
            if (SelectedPerson != null)
            {
                People.Remove(SelectedPerson);
                SelectedPerson = null; 
            }
        }

    
        private bool CanDeletePerson(object parameter)
        {
            return SelectedPerson != null;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}