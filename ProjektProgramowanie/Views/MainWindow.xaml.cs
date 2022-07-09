using ProjektProgramowanie.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
        }

        private void CheckAndManagePermissions()
        {
            if (_currentUser.PermissionsId == 1) return;

            CarDG.Columns.RemoveAt(CarDG.Columns.Count - 1);
            CarDG.Columns.RemoveAt(CarDG.Columns.Count - 1);
        }
        public void GetCars()
        { 
            var cars = dbContext.Car.ToList();
            List<CarVo> carVos = new List<CarVo>();
            foreach (Car car in cars)
            {
                var reg = dbContext.Registration.Where((item) => item.Vin == car.Vin).ToList();
                carVos.Add(new CarVo { 
                Id = car.Id,
                Vin = car.Vin,
                Brand = car.Brand,
                Model = car.Model,
                OwnerName = reg.Count > 1 ? reg.First().OwnerName : "",
                Price = car.Price,
                RegistrationDate = reg.Count > 1 ? reg.First().RegistrationDate : DateTime.Now,
                RegistrationNumber = reg.Count > 1 ? reg.First().RegistrationNumber: ""
                });
            }
            CarDG.ItemsSource = carVos;
        }
        private void SelectCarToEdit(object s, RoutedEventArgs e)
        {
            var carToEdit = (s as FrameworkElement).DataContext as CarVo;
            ShowCarWindow(carToEdit);
        }

        private void DeleteCar(object s, RoutedEventArgs e)
        {
            CarVo objectToDelete = (s as FrameworkElement).DataContext as CarVo;
            dbContext.Car.Remove(new Car {Vin = objectToDelete.Vin, Brand = objectToDelete.Brand,Model = objectToDelete.Model,Price = objectToDelete.Price,Id = objectToDelete.Id });
            dbContext.Remove(dbContext.Registration.Where((item)=> item.Vin == objectToDelete.Vin));
            dbContext.SaveChanges();
            GetCars();
        }

        private void AddCarWindow(object sender, RoutedEventArgs e)
        {
            ShowCarWindow();
        }

        private void RefreshGridButton(object sender, RoutedEventArgs e)
        {
            GetCars();
        }
        /// <summary>
        /// IF carToEdit is null then method will add new Car,
        /// if parametr is not null method will try to update if exists,
        /// if not car will be added
        /// </summary>
        /// <param name="carToEdit"></param>
        private void ShowCarWindow(CarVo carToEdit = null)
        {
            AddCarWindow addCarWindow = new AddCarWindow(dbContext, _currentUser, carToEdit);
            addCarWindow.Show();
            Close();
        }
    }
}
