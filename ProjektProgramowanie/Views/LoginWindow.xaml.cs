using System.Linq;
using System.Windows;
namespace ProjektProgramowanie
{
    /// <summary>
    /// Logika działania okna dodawania samochodu
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly CarListDbContext _dbContext;

        public LoginWindow(CarListDbContext context)
        {
            this._dbContext = context;
            InitializeComponent();
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (_dbContext.User.Where(x => x.Email == loginTextBox.Text).FirstOrDefault() == null)
            {
                _ = _dbContext.User.Add(
                new User
                {
                    Email = loginTextBox.Text,
                    Password = passwordBox.Password,
                    PermissionsId = 2

                }
            );
                _dbContext.SaveChanges(); ;
            }
            else
            {
                MessageBox.Show("User Already Exist. Try another Login.");
            }
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            User user = _dbContext.User.Where(x => x.Email == loginTextBox.Text).FirstOrDefault();
            if (user != null && user.Password == passwordBox.Password)
            {
                new MainWindow(_dbContext, user).Show();
                Close();
            }
        }
    }
}
