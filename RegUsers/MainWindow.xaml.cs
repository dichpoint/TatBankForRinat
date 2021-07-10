using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace RegUsers
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    // Регистрация
    public partial class MainWindow : Window
    {
        AppContext db; // создаем объект контекста

        // конструктор
        public MainWindow()
        {
            InitializeComponent();

            db = new AppContext(); // выделяем память
        }

        private const int START_CHECK_NUMBER = 0;
        private const int SUCCESSFUL_CHECK_NUMBER = 4;

        // обработчик события ("Зарегистрироваться")
        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim().ToLower();
            string pass = passBox.Password;
            string pass_2 = passBox_2.Password;
            string email = textBoxEmail.Text.Trim().ToLower();

            int check = START_CHECK_NUMBER;
            // CHECK ALL
            check = CheckLogin(login) ? ++check : check;
            check = CheckPass(pass) ? ++check : check;
            check = CheckPass_2(pass, pass_2) ? ++check : check;
            check = CheckEmail(email) ? ++check : check;
            if (check == SUCCESSFUL_CHECK_NUMBER)
            {
                User userTest = null; // объект для проверки на существование в БД
                userTest = db.Users.Where(b => (b.Login == login)).FirstOrDefault(); // проверка существования логина в БД

                if (userTest == null)
                {
                    User user = new User(login, pass, email);
                    MessageBox.Show("Регистрация прошла успешно!");
                    db.Users.Add(user); // если такого ОБЪЕКТА нет в БД, то мы его добавляем
                    db.SaveChanges(); // сохраняем изменения в БД
                    AuthWindow authWindow = new AuthWindow();
                    authWindow.Show();
                    Close();
                }
                else
                {
                    textBoxLogin.ToolTip = $"Логин {login} уже занят в системе!";
                    textBoxLogin.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                }
            }
            else
            {
                check = START_CHECK_NUMBER;
                //MessageBox.Show("Регистрация прошла НЕ успешно!");
            }
        }

        private const int EMPTY_LENGTH = 0;
        private const int MIN_LENGTH_LOGIN = 5;
        private const int MIN_LENGTH_PASS = 7;
        private const int MAX_LENGTH = 30;

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
            else if (login.Length < MIN_LENGTH_LOGIN)
            {
                textBoxLogin.ToolTip = "Логин должен содержать не меньше 5 символов!";
                textBoxLogin.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else if (login.Length > MAX_LENGTH)
            {
                textBoxLogin.ToolTip = "Логин должен содержать не больше 30 символов!";
                textBoxLogin.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else if (login.Contains(' '))
            {
                textBoxLogin.ToolTip = "Логин не должен содержать пробелов!";
                textBoxLogin.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else if (Regex.IsMatch(login, "[а-я]"))
            {
                textBoxLogin.ToolTip = "Логин должен состоять из букв только латинского алфавита!";
                textBoxLogin.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else if (!(Regex.IsMatch(login, "[A-Za-z]")))
            {
                textBoxLogin.ToolTip = "Логин должен содержать хотя бы одну букву!";
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
            else if (pass.Length > MAX_LENGTH)
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
        private bool CheckPass_2(string pass, string pass_2)
        {
            // checking PASS_2
            if (pass_2.Length == EMPTY_LENGTH)
            {
                passBox_2.ToolTip = "Это поле пустое!";
                passBox_2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else if (pass != pass_2)
            {
                passBox_2.ToolTip = "Пароли должны быть одинаковыми!";
                passBox_2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else
            {
                passBox_2.ToolTip = null;
                passBox_2.Background = Brushes.Transparent;
                return true;
            }
        }
        private bool CheckEmail(string email)
        {
            //checking EMAIL
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (email.Length == EMPTY_LENGTH)
            {
                textBoxEmail.ToolTip = "Это поле пустое!";
                textBoxEmail.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else if (!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            {
                textBoxEmail.ToolTip = "Некорректный Email!";
                textBoxEmail.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#ea9999");
                return false;
            }
            else
            {
                textBoxEmail.ToolTip = null;
                textBoxEmail.Background = Brushes.Transparent;
                return true;
            }
        }

        // обработчик события ("Войти")
        private void Button_WinAuth_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }
    }
}