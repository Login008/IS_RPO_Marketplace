using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Marketplace
{
    public partial class Auth : Form
    {
        public bool CheckEmail(string email) //функция проверки валидности email
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return false;
                var adres = new MailAddress(email);
                return adres.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public bool CheckLogin(string login) //функция проверки логина на формат
        {
            if (!string.IsNullOrEmpty(login))
            {
                if (login.Length > 3)
                {
                    bool valid = true;

                    foreach (var ch in login)
                    {
                        if (!char.IsDigit(ch) && !char.IsLetter(ch) && !char.IsPunctuation(ch))
                            valid = false;
                    }

                    if (valid)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public bool CheckPass(string pass) //функция проверки пароля
        {
            if (string.IsNullOrEmpty(pass))
                return false;
            if (pass.Length < 8)
                return false;

            bool upLet = false;
            bool downlet = false;
            bool digit = false;

            foreach (var ch in pass)
            {
                if (char.IsUpper(ch))
                    upLet = true;
                if (char.IsLower(ch))
                    downlet = true;
                if (char.IsDigit(ch))
                    digit = true;
                if (char.IsWhiteSpace(ch))
                    return false;
            }

            if (upLet && downlet && digit)
                return true;
            else
                return false;
        }
        public void Register(string role) //функция регистрации
        {
            Account acc = new Account(textBox1.Text, textBox2.Text, textBox3.Text, role);
            if (acc.CheckForExistingInList())
            {
                Account.list.Add(acc);
                Account.WriteInFile();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                MessageBox.Show("Вы успешно зарегистрированны");
            }
            else
                MessageBox.Show("Аккаунт с таким логином или почтой уже зарегистрирован в системе");
        }
        public Auth()
        {
            InitializeComponent();
            Account.ReadFromFile(); //считываем инфу об акках
        }

        private void button1_Click(object sender, EventArgs e) //кнопка регистрации
        {
            try //проверки полей
            {
                if (CheckEmail(textBox1.Text))
                {
                    if (CheckLogin(textBox2.Text))
                    {
                        if (CheckPass(textBox3.Text))
                        {
                            if (textBox3.Text == textBox4.Text) //проверка на подтверждение пароля
                            {
                                if (radioButton1.Checked) //выбор роли
                                {
                                    Register("Seller");
                                }
                                else if (radioButton2.Checked)
                                {
                                    Register("Buyer");
                                }
                                else
                                    MessageBox.Show("Выберите, кто вы: продавец или покупатель");
                            }
                            else
                            {
                                MessageBox.Show("Пароли не совпадают");
                            }
                        }
                        else
                            MessageBox.Show("Пароль должен быть длиной не менее 8 символов\n" +
                                "В пароле должны содержаться и заглавные, и маленькие буквы\n" +
                                "В пароле должны быть цифры\n" +
                                "В пароле не могут содержаться пробелы");
                    }
                    else
                        MessageBox.Show("Длина логина должна быть больше 3-х символов\n" +
                            "В логине могут использоваться только буквы, цифры и знаки пунктуации");
                }
                else
                    MessageBox.Show("Электронная почта не соотвуетстует формату email@email.com");
            }
            catch
            {
                MessageBox.Show("А-та-та, какая-то ошибка");
            }
        }

        private void button2_Click(object sender, EventArgs e) //кнопка входа
        {
            if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrEmpty(textBox6.Text)) //проверки на пустые поля
            {
                foreach (var item in Account.list)
                {
                    if ((item.login == textBox5.Text || item.email == textBox5.Text) && item.pass == textBox6.Text) //проверки на совпадения
                    {
                        Account.online = item;
                        if (item.role == "Seller") //заходим в аккаунт в зависимости от роли
                        {
                            lk_seller f1 = new lk_seller();
                            f1.Show();
                            return;
                        }
                        else
                        {
                            lk_Buyer f1 = new lk_Buyer();
                            f1.Show();
                            return;
                        }
                    }
                }

                MessageBox.Show("Неверный логин или пароль");
            }
            else
                MessageBox.Show("Заполните все пустые поля");
        }

        bool vid1 = false;
        bool vid2 = false;
        bool vid3 = false;
        private void pictureBox1_Click(object sender, EventArgs e) //показываем/скрываем пароль
        {
            if (vid1 == false)
            {
                textBox3.PasswordChar = '\0';
                vid1 = true;
            }
            else
            {
                textBox3.PasswordChar = '*';
                vid1 = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e) //тоже показываем/скрываем пароль
        {
            if (vid2 == false)
            {
                textBox4.PasswordChar = '\0';
                vid2 = true;
            }
            else
            {
                textBox4.PasswordChar = '*';
                vid2 = false;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e) //то же самое
        {
            if (vid3 == false)
            {
                textBox6.PasswordChar = '\0';
                vid3 = true;
            }
            else
            {
                textBox6.PasswordChar = '*';
                vid3 = false;
            }
        }

        private void Auth_Load(object sender, EventArgs e)
        {

        }
    }
}
