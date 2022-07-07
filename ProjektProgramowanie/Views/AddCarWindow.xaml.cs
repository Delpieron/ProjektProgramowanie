using System;
using System.Windows;
namespace ProjektProgramowanie
{
    /// <summary>
    /// Logika działania okna dodawania samochodu
    /// </summary>
    public partial class AddCarWindow : Window
    {
        private readonly CarListDbContext context;

        public AddCarWindow(CarListDbContext context)
        {
            this.context = context;
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
                context.Car.Add(newCar);
                context.SaveChanges();
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
            MainWindow main = new MainWindow(context);
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
