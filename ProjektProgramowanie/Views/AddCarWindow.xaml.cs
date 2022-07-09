using ProjektProgramowanie.Model;

using System;
using System.Windows;
namespace ProjektProgramowanie
{
    /// <summary>
    /// Logika działania okna dodawania samochodu
    /// </summary>
    public partial class AddCarWindow : Window
    {
        private readonly CarListDbContext _dbContext;
        private readonly User _currentUser;
        private readonly bool isEditMode;
        public AddCarWindow(CarListDbContext context, User user, CarVo carToEdit)
        {
            InitializeComponent();
            if (carToEdit != null)
            {
                isEditMode = true;
                FillTextFildsWithData(carToEdit);
            }
            this.
            _currentUser = user;
            _dbContext = context;
        }
        private void FillTextFildsWithData(CarVo carToEdit)
        {
            Price.Text = carToEdit.Price.ToString();
            vin.Text = carToEdit.Vin;
            brand.Text = carToEdit.Brand;
            model.Text = carToEdit.Model;
            registrationNumber.Text = carToEdit.RegistrationNumber;
            OwnerName.Text = carToEdit.OwnerName;
            RegistrationDate.Text = carToEdit.RegistrationDate.ToString();
        }
        private void AddCar(object sender, RoutedEventArgs e)
        {
            Car newCar = GetCarUserInput();
            Registration registration = string.IsNullOrEmpty(registrationNumber.Text) ? null : GetRegistrationUserInput(newCar.Vin);
            string message = CarValidator.CheckLongOfInsertedData(newCar, registration);
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Error", MessageBoxButton.OK);
                return;
            }
            try
            {
                if (isEditMode)
                {
                    _dbContext.Car.Update(newCar);
                    if (registration != null)
                    {
                        _dbContext.Registration.Update(registration);
                    }
                    return;
                }
                if (registration != null)
                {
                    _dbContext.Registration.Add(registration);
                }
                _dbContext.Car.Add(newCar);
                
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot save changes to database.", "Error", MessageBoxButton.OK);
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
            }
            if (string.IsNullOrEmpty(message)) BackToMainWindow(null, null);

        }
        private void BackToMainWindow(object sender, RoutedEventArgs e)
        {
            string message = "Car added succesfuly";
            MessageBox.Show(message, "Info", MessageBoxButton.OK);
            MainWindow main = new MainWindow(_dbContext, _currentUser);
            main.Show();
            Close();
        }
        private Car GetCarUserInput() => new Car()
        {
            Price = decimal.TryParse(Price.Text,out decimal result) ? result : 0,
            Vin = vin.Text,
            Brand = brand.Text,
            Model = model.Text
        };
        private Registration GetRegistrationUserInput(string vin) => new Registration()
        {
            RegistrationNumber = registrationNumber.Text,
            Vin = vin,
            OwnerName = OwnerName.Text,
            RegistrationDate = DateTime.Parse(RegistrationDate.Text)
        };
    }
}
