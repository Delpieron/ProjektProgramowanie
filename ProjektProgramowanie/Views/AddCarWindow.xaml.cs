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

        public AddCarWindow(CarListDbContext context, User user)
        {
            _currentUser = user;
            _dbContext = context;
            InitializeComponent();
        }
        private void AddCar(object sender, RoutedEventArgs e)
        {
            Car newCar = GetUserInput();
            string message = CarValidator.CheckLongOfInsertedData(newCar);
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Error", MessageBoxButton.OK);
                return;
            }
            try
            {
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
            MessageBox.Show(message, "Error", MessageBoxButton.OK);
            MainWindow main = new MainWindow(_dbContext, _currentUser);
            main.Show();
            Close();
        }
        private Car GetUserInput() => new Car()
        {
            RegistrationNumber = registrationNumber.Text,
            Vin = vin.Text,
            Brand = brand.Text,
            Model = model.Text
        };
    }
}
