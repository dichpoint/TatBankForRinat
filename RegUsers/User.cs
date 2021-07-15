namespace RegUsers
{
    class User // класс-модель
    {
        public int id { get; set; }
        private string login, pass, email;
        private int amount;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public User() { }

        public User(string login, string pass, string email, int amount)
        {
            this.login = login;
            this.pass = pass;
            this.email = email;
            this.amount = amount;
        }
    }
}