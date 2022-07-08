using System.Linq;
using System.Windows;

namespace ProjektProgramowanie
{
    /// <summary>
    /// Logika działania okna głównego
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly CarListDbContext dbContext;
        private readonly User _currentUser;

        public MainWindow(CarListDbContext context, User user)
        {
            dbContext = context;
            _currentUser = user;
            InitializeComponent();
            CheckAndManagePermissions();
            GetCars();
            UpdateCarGrid.IsEnabled = false;
        }

        private void CheckAndManagePermissions()
        {
            if (_currentUser.PermissionsId == 1) return;

            updateButton.Visibility = Visibility.Collapsed;
            CarDG.Columns.RemoveAt(CarDG.Columns.Count-1);
            CarDG.Columns.RemoveAt(CarDG.Columns.Count-1);
        }
        public void GetCars()
        {
            CarDG.ItemsSource = dbContext.Car.ToList();
        }

        private void UpdateCar(object s, RoutedEventArgs e)
        {
            dbContext.Update((s as FrameworkElement).DataContext as Car);
            dbContext.SaveChanges();
            GetCars();
            ClearUpdateCarGrid();
        }
        private void ClearUpdateCarGrid()
        {
            UpdateCarGrid.DataContext = null;
            UpdateCarGrid.IsEnabled = false;
        }
        private void SelectCarToEdit(object s, RoutedEventArgs e)
        {
            UpdateCarGrid.DataContext = (s as FrameworkElement).DataContext as Car;
            UpdateCarGrid.IsEnabled = true;
        }

        private void DeleteCar(object s, RoutedEventArgs e)
        {
            var productToDelete = (s as FrameworkElement).DataContext as Car;
            dbContext.Car.Remove(productToDelete);
            dbContext.SaveChanges();
            GetCars();
        }

        private void AddCarWindow(object sender, RoutedEventArgs e)
        {
            AddCarWindow addCarWindow = new AddCarWindow(dbContext, _currentUser);
            addCarWindow.Show();
            Close();
        }

        private void RefreshGridButton(object sender, RoutedEventArgs e)
        {
            GetCars();
        }
    }
}
