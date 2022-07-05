using System.Linq;
using System.Windows;

namespace ProjektProgramowanie
{
    /// <summary>
    /// Logika działania okna głównego
    /// </summary>
    public partial class MainWindow : Window
    {

        readonly CarListDbContext context;
        //Car selectedCar = new Car();
        // AddCarWindow carWindow = new AddCarWindow(context);

        public MainWindow(CarListDbContext context)
        {
            this.context = context;
            InitializeComponent();
            GetCars();
            UpdateCarGrid.IsEnabled = false;
        }


        public void GetCars()
        {
            CarDG.ItemsSource = context.CarList.ToList();
        }

        private void UpdateCar(object s, RoutedEventArgs e)
        {
            context.Update((s as FrameworkElement).DataContext as Car);
            context.SaveChanges();
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
            context.CarList.Remove(productToDelete);
            context.SaveChanges();
            GetCars();
        }

        private void AddCarWindow(object sender, RoutedEventArgs e)
        {
            AddCarWindow addCarWindow = new AddCarWindow(context);
            addCarWindow.Show();
            Close();
        }

        private void RefreshGridButton(object sender, RoutedEventArgs e)
        {
            GetCars();
        }
    }
}
