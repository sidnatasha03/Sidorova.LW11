using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace test1
{
    public class Base   //внутренняя база данных
    {
        public List<User> L;   //список зарегистрированных пользователей (ключ - логин, значение - пользователь)
        public List<string> Spisok; // список логинов зарегистрированных пользователей
        public User U;         //текущий пользователь
        public Base()   //инициализация списка зарегистрированных пользователей
        {
            string Log = null, Pass = null, N = null, C, S;
            L = new List<User>();
            var path = @"C:\Users\Сергей\source\repos\test1\Accounts.csv";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding(1251);
            var lines = File.ReadAllLines(path, encoding);
            var acc = new Account[lines.Length - 1];
            using (StreamReader sr = new StreamReader(path))
            {
                for (int i = 1; i < lines.Length; i++)
                {
                    var splits = lines[i].Split(';');
                    Log = splits[8];
                    Pass = splits[9];
                    N = splits[1];
                    C = splits[6];
                    S = splits[10];
                    User B = new User(Log, Pass, N, C, Convert.ToInt32(S));
                    L.Add(B);
                }
            }
        }
        public void Current(User U1)   //установка текущего пользователя
        {
            U = U1;
        }
    }
}
