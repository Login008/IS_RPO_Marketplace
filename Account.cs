using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace
{
    internal class Account //класс для аккаунтов
    {
        static public List<Account> list = new List<Account>(); //список зарегестрированных аккаунтов
        static public Account online; //авторизованный в данный момент аккаунт

        public string email; //поля одного акка
        public string login;
        public string pass;
        public string role;
        public Account(string email, string login, string pass, string role) //конструктор
        {
            this.email = email;
            this.login = login;
            this.pass = pass;
            this.role = role;
        }
        static public void WriteInFile() //процедура записи данных о всех аккаунтах в текстовый файл
        {
            StreamWriter sw = File.CreateText("Accounts.txt");
            foreach (Account acc in list)
            {
                sw.WriteLine($"{acc.email}~{acc.login}~{acc.pass}~{acc.role}");
            }
            sw.Close();
        }
        static public void ReadFromFile() //процедура считывания списка аккаунтов
        {
            if (File.Exists("Accounts.txt"))
            {
                list.Clear();
                StreamReader sr = File.OpenText("Accounts.txt");
                while (!sr.EndOfStream) 
                {
                    string[] data = sr.ReadLine().Split('~');
                    Account account = new Account(data[0], data[1], data[2], data[3]);
                    list.Add(account);
                }
                sr.Close();
            }
        }
        public bool CheckForExistingInList() //функция для проверки на существование аккаунта по логину или email
        {
            foreach (var acc in list)
            {
                if (acc.login == this.login || acc.email == this.email)
                    return false;
            }
            return true;
        }
    }
}
