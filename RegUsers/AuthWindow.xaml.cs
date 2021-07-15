using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace RegUsers
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>

    // Авторизация
    public partial class AuthWindow : Window
    {
        // конструктор
        public AuthWindow()
        {
            InitializeComponent();
        }

        private const int START_CHECK_NUMBER = 0;
        private const int SUCCESSFUL_CHECK_NUMBER = 3;

        // обработчик события ("Войти")
        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            // получение полей
            string login = textBoxLogin.Text.Trim().ToLower();
            string pass = passBox.Password;

            int check = START_CHECK_NUMBER;

            // CHECK ALL
            check = CheckLogin(login) ? ++check : check;
            check = CheckPass(pass) ? ++check : check;

            User authUser = null;

            check = CheckLoginInDB(authUser, login) ? ++check : check;

            if (check == SUCCESSFUL_CHECK_NUMBER)
            {
                if (CheckPassInDB(authUser, pass))
                {
                    MessageBox.Show("Авторизация прошла успешно!");

                    using (AppContext db = new AppContext())
                    {
                        authUser = db.Users.Where(b => (b.Login == login)).FirstOrDefault(); // проверка логина в БД
                    }

                    // регистрация прошла успешно, => переходим в авторизацию
                    UserPageWindow userPageWindow = new UserPageWindow(authUser.Login, authUser.Amount);
                    userPageWindow.Show();
                    Close();
                }
                else
                {
                    passBox.ToolTip = "Неверный пароль!";
                    passBox.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                    check = START_CHECK_NUMBER;
                }
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует!");
                textBoxLogin.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                passBox.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                check = START_CHECK_NUMBER;
            }

        }

        private const int EMPTY_LENGTH = 0;
        private const int LENGTH_LOGIN = 16;
        private const int MIN_LENGTH_PASS = 7;
        private const int MAX_LENGTH_PASS = 30;

        // ф-ции для проверки логина и пароля
        private bool CheckLogin(string login)
        {
            // checking LOGIN
            if (login.Length == EMPTY_LENGTH)
            {
                textBoxLogin.ToolTip = "Это поле пустое!";
                textBoxLogin.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else if (login.Length != LENGTH_LOGIN)
            {
                textBoxLogin.ToolTip = "Счет должен содержать должен состоять из 16 символов!";
                textBoxLogin.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else if (login.Contains(' '))
            {
                textBoxLogin.ToolTip = "Счет не должен содержать пробелов!";
                textBoxLogin.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else if ((Regex.IsMatch(login, "[A-Za-z]")))
            {
                textBoxLogin.ToolTip = "Счет должен состоять только из цифр!";
                textBoxLogin.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else
            {
                textBoxLogin.ToolTip = null;
                textBoxLogin.Background = Brushes.Transparent;
                return true;
            }
        }
        private bool CheckPass(string pass)
        {
            // checking PASS
            if (pass.Length == EMPTY_LENGTH)
            {
                passBox.ToolTip = "Это поле пустое!";
                passBox.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else if (pass.Length < MIN_LENGTH_PASS)
            {
                passBox.ToolTip = "Пароль должен содержать не меньше 7 символов!";
                passBox.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else if (pass.Length > MAX_LENGTH_PASS)
            {
                passBox.ToolTip = "Пароль должен содержать не больше 30 символов!";
                passBox.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else if (Regex.IsMatch(pass, "[а-я]"))
            {
                passBox.ToolTip = "Пароль должен состоять из букв только латинского алфавита!";
                passBox.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else if (!(Regex.IsMatch(pass, "[A-Za-z]")))
            {
                passBox.ToolTip = "Пароль должен содержать хотя бы одну букву!";
                passBox.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else if (!(Regex.IsMatch(pass, "[0-9]")))
            {
                passBox.ToolTip = "Пароль должен содержать хотя бы одну цифру!";
                passBox.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else
            {
                passBox.ToolTip = null;
                passBox.Background = Brushes.Transparent;
                return true;
            }
        }

        private bool CheckLoginInDB(User authUser, string login)
        {
            using (AppContext db = new AppContext())
            {
                authUser = db.Users.Where(b => (b.Login == login)).FirstOrDefault(); // проверка логина в БД
            }
            if (authUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckPassInDB(User authUser, string pass)
        {
            using (AppContext db = new AppContext())
            {
                authUser = db.Users.Where(b => (b.Pass == pass)).FirstOrDefault(); // проверка пароля в БД
            }
            if (authUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // обработчик события ("Регистрация")
        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}