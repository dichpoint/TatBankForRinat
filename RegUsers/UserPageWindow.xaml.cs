using System.Windows;

namespace RegUsers
{
    /// <summary>
    /// Логика взаимодействия для UserPageWindow.xaml
    /// </summary>

    // Кабинет пользователя
    public partial class UserPageWindow : Window
    {
        string login = null;
        //string pass = null;
        //string email = null;
        int amount = 0;

        private const int ZERO = 0;

        // конструктор
        public UserPageWindow(string login, int amount)
        {
            InitializeComponent();
            this.login = login;
            //this.pass = pass;
            //this.email = email;
            this.amount = amount;
        }

        // обработчик события ("Выйти из системы")
        private void Button_WinAuth_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }

        // обрабточик события ("Узнать баланс")
        private void Button_Balance_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Баланс счета {login} = {amount} рублей");
        }

        // обрабточик события ("Снять")
        private void Button_Withdraw_Money_Click(object sender, RoutedEventArgs e)
        {
            string badCash = textBoxCash.Text.Trim().ToLower();
            int cash = 0;
            bool r = int.TryParse(badCash, out cash);

            if (r == false)
            {
                MessageBox.Show("Введенное значение не является числом!");
            }
            else if (cash > amount)
            {
                MessageBox.Show("Недостаточно средств на счете для совершения данной операциии!");
            }
            else if (cash <= ZERO)
            {
                MessageBox.Show("Введенное число должно быть положительным!");
            }
            else
            {
                amount -= cash;
                using (AppContext db = new AppContext())
                {
                    db.Database.ExecuteSqlCommand($"update Users set amount = {amount} WHERE login = '{login}'");
                    db.SaveChanges();
                }
            }
        }

        // обрабточик события ("Внести")
        private void Button_Deposit_Money_Click(object sender, RoutedEventArgs e)
        {
            string badCash = textBoxCash.Text.Trim().ToLower();
            int cash = 0;
            bool r = int.TryParse(badCash, out cash);

            // проверка введенного значения
            if (!r)
            {
                MessageBox.Show("Введенное значение не является числом!");
            }
            else if (cash <= ZERO)
            {
                MessageBox.Show("Введенное число должно быть положительным!");
            }
            else
            {
                amount += cash;
                using (AppContext db = new AppContext())
                {
                    db.Database.ExecuteSqlCommand($"update Users set amount = {amount} WHERE login = '{login}'");
                    db.SaveChanges();
                }
            }
        }
    }
}