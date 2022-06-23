using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test1
{
    public class Menu
    {
        public static void InputMenu(Base B)
        {
            Console.WriteLine("Войти (1) Зарегистрироваться (2) Выход из программы (3)");
            Console.ResetColor();
            string a = Console.ReadLine();     //выбираем действие
            if (a == "1")        //вход в аккаунт
            {
                InputMenu2(B);
            }
            else if (a == "2")    //регистрация нового пользователя
            {
                InputMenu1(B);
            }
            else if (a == "3")   //выход из программы
            {
                Environment.Exit(0);
            }
            else               //если введен непредусмотренный символ
            {
                InputMenu(B);
            }
        }
        public static void InputMenu1(Base B)   // регистрация
        {
            Account account = new Account();
            Console.WriteLine("Введите логин");
            string s1 = Console.ReadLine();
            List<User> Y = B.L;
            while (FInL.Finding(Y, s1) == true)    //пользователь с таким логином уже существует
            {
                Console.WriteLine("Пользователь с таким логином уже существует введите другой");
                s1 = Console.ReadLine();
            }
            Console.WriteLine("Введите пароль");
            string s2 = Console.ReadLine();
            Console.WriteLine("Введите ФИО");
            account.Name = Console.ReadLine();
            User A = new User(s1, s2, account.Name, account.Order, account.Balance);    //создание нового аккаунта с введенными логином и паролем
            Console.WriteLine("Введите страну");
            account.Country = Console.ReadLine();
            Console.WriteLine("Введите возраст");
            account.Age = Convert.ToInt32(Console.ReadLine());
            try
            {
                if (Check.Finding1(account) == false)
                {
                    Console.WriteLine("Вам недостаточно лет :( Мы не сможем Вас обслужить. До свидания!");
                    Environment.Exit(0);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Неверно введён возраст");
            }
            Console.WriteLine("Введите номер телефона (+7...)");
            account.Phone = Console.ReadLine();
            var CharArray = account.Phone.ToCharArray();
            while (Check.Finding2(account) == false)
            {
                Console.WriteLine("Введите номер телефона повторно.");
                account.Phone = Console.ReadLine();
            }
            Console.WriteLine("Введите почту");
            account.Email = Console.ReadLine();
            Cars car = new Cars();
            account.Login = s1;
            account.Password = s2;
            Console.WriteLine("Пользователь зарегистрирован");
            Console.WriteLine("В нашем сервисе первая аренда авто бесплатна!! ");
            Console.WriteLine("Так как Вы только что зарегистрировались, вы можете арендовать любой свободный транспорт!");
            Console.WriteLine("Введите категорию транспорта, который бы вы хотели арендовать: A, B, D");
            car.Category = Console.ReadLine();
            if (Check.Finding3(car) == false)
            {
                Console.WriteLine("Транспорта данной категории нет. Хотите продолжить?");
                string answ = Console.ReadLine().ToUpper();
                if (answ == "ДА")
                {
                    InputMenu(B);
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            Console.WriteLine("Показываем доступный транспорт, соответствующий Вашей категории...");
            car.Operation("Вывести доступный транспорт и взять в аренду", car.Category, account, A);
            Y.Add(A);                    //новый аккаунт добавляется в список
            B.Current(A);                         //текущий пользователь обновляется
            InputMenu(B);
        }
        public static void InputMenu2(Base B)   // авторизация
        {
            Console.WriteLine("Введите логин");
            string s1 = Console.ReadLine();     //ввод логина
            //List<string> D = B.Spisok;
            List<User> Y = B.L;
            if (FInL.Finding(Y, s1) == false)  //если аккаунта с введенным логином нет в списке аккаунтов, то либо вводим заново либо регистрируемся
            {
                Console.WriteLine("Пользователя с таким логином не существует. Ввести заново (1) - Зарегистрироваться (2)");
                string c = Console.ReadLine();
                if (c == "2")    //регистрируем аккаунт
                {
                    InputMenu1(B);
                }
                else           //снова вводим логин
                {
                    InputMenu2(B);
                }
            }
            else       //если аккаунт есть в списке
            {
                User A = Y.Find(AA => AA.Login == s1);
                Console.WriteLine("Введите пароль");
                string s2 = Console.ReadLine();
                while (A.Password != s2) //если пароль не соответствует ни одному из логинов
                {
                    Console.WriteLine("Пароль не совпадает с логином");
                    s2 = Console.ReadLine();
                }
                Console.WriteLine("Вход совершен");    //пароль и логин совпадают
                B.Current(A);                        //новый текущий пользователь в базе
                Console.WriteLine($"Здравствуйте, {A.UserName}, Ваш баланс: {A.Balance} рублей. Что вы хотите сделать?");
                Console.WriteLine("Узнать баланс (0) Пополнить баланс (1) Узнать состояние по аренде (2) Взять авто в аренду (3) Вернуть авто в сервис (4) Выйти из аккаунта (5)");
                A.Notify += A_NotifyGreen;
                A.BalanceNotify += A_NotifyBalance;
                A.CarYesNotify += A_CarYesNotify;
                A.CarNoNotify += A_CarNoNotify;
                A.CarTakeSuccessNotify += A_CarTakeSuccessNotify;
                A.CarTakeDenialNotify += A_CarTakeDenialNotify;
                string a = Console.ReadLine();
                while (a != "5")
                {
                    if (a == "0")
                    {
                        A.CheckSum();
                        Console.WriteLine("Что вы хотите сделать?");
                        Console.WriteLine("Узнать баланс (0) Пополнить баланс (1) Узнать состояние по аренде (2) Взять авто в аренду (3) Вернуть авто в сервис (4) Выйти из аккаунта (5)");
                        a = Console.ReadLine();
                    }
                    else if (a == "1")
                    {
                        Console.WriteLine($"Ваш баланс: {A.Sum}. На какую сумму Вы хотите пополнить ваш аккаунт?");
                        int sum = Convert.ToInt32(Console.ReadLine());
                        A.Put(sum);
                        A.CheckSum();
                        Console.WriteLine("Узнать баланс (0) Пополнить баланс (1) Узнать состояние по аренде (2) Взять авто в аренду (3) Вернуть авто в сервис (4) Выйти из аккаунта (5)");
                        a = Console.ReadLine();
                    }
                    else if (a == "2")
                    {
                        A.CheckCar();
                        Console.WriteLine("Что вы хотите сделать?");
                        Console.WriteLine("Узнать баланс (0) Пополнить баланс (1) Узнать состояние по аренде (2) Взять авто в аренду (3) Вернуть авто в сервис (4) Выйти из аккаунта (5)");
                        a = Console.ReadLine();

                    }
                    else if (a == "3")
                    {
                        A.TakeCar();
                        Console.WriteLine("Что вы хотите сделать?");
                        Console.WriteLine("Узнать баланс (0) Пополнить баланс (1) Узнать состояние по аренде (2) Взять авто в аренду (3) Вернуть авто в сервис (4) Выйти из аккаунта (5)");
                        a = Console.ReadLine();
                    }
                    else if (a == "4")
                    {
                        Console.WriteLine("Вы уверены что хотите вернуть транспорт? Деньги, потраченные на аренду, не вернутся на ваш счет!");
                        Console.WriteLine("Введите ДА или НЕТ");
                        string b = Console.ReadLine();
                        if(b == "ДА")
                        {
                            A.PutCar();
                            Console.WriteLine("Что вы хотите сделать?");
                            Console.WriteLine("Узнать баланс (0) Пополнить баланс (1) Узнать состояние по аренде (2) Взять авто в аренду (3) Вернуть авто в сервис (4) Выйти из аккаунта (5)");
                            a = Console.ReadLine();
                        }
                        else if(b == "НЕТ")
                        {
                            Console.WriteLine("Что вы хотите сделать?");
                            Console.WriteLine("Узнать баланс (0) Пополнить баланс (1) Узнать состояние по аренде (2) Взять авто в аренду (3) Вернуть авто в сервис (4) Выйти из аккаунта (5)");
                            a = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Введен неверный символ");
                            Console.WriteLine("Что вы хотите сделать?");
                            Console.WriteLine("Узнать баланс (0) Пополнить баланс (1) Узнать состояние по аренде (2) Взять авто в аренду (3) Вернуть авто в сервис (4) Выйти из аккаунта (5)");
                            a = Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели неправильный символ.");
                        a = Console.ReadLine();
                    }
                }
                InputMenu(B);
            }
        }
        private static void A_CarTakeDenialNotify(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void A_CarTakeSuccessNotify(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void A_CarNoNotify(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void A_CarYesNotify(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void A_NotifyGreen(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void A_NotifyBalance(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
    static class FInL
    {
        public static bool Finding(List<User> L, string s)
        {
            bool F = false;
            foreach (var P in L)
            {
                if (P.Login == s)
                {
                    F = true;
                }
            }
            return F;
        }
    }
}

