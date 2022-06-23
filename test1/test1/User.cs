using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace test1
{
    public class User       //аккаунт пользователя
    {
        string LOgin;         //логин пользователя
        string PAssword;      //пароль для входа
        string USerName;
        public string Car { get; set; }
        public int Sum { get; set; }
        public delegate void AccountHandler(string message);
        public event AccountHandler? Notify;
        public event AccountHandler? BalanceNotify;
        public event AccountHandler? CarYesNotify;
        public event AccountHandler? CarNoNotify;
        public event AccountHandler? CarTakeDenialNotify;
        public event AccountHandler? CarTakeSuccessNotify;
        public User(string s1, string s2, string s3, string s4, int s5)    //инициализация происходит при регистрации
        {
            LOgin = s1;
            PAssword = s2;
            UserName = s3;
            Car = s4;
            Sum = s5;
        }

        public string Login     //изменение логина в настройках аккаунта и возможность просмотреть логин
        {
            get
            {
                return LOgin;
            }
            set
            {
                LOgin = value;
            }
        }
        public string Password    //изменение пароля в настройках аккаунта и возможность просмотреть пароль
        {
            get
            {
                return PAssword;
            }
            set
            {
                PAssword = value;
            }
        }
        public string UserName            //перечисление денег на счет
        {
            get
            {
                return USerName;
            }
            set
            {
                USerName = value;
            }
        }
        public int Balance
        {
            get
            {
                return Sum;
            }
            set
            {
                Sum = value;
            }
        }
        public void CheckSum()
        {
            BalanceNotify?.Invoke($"Ваш баланс: {Sum}");
        }
        public void Put(int sum)
        {
            Sum += sum;
            Notify?.Invoke($"На счет поступило: {sum}");
            var path = @"C:\Users\Сергей\source\repos\test1\Accounts.csv";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding(1251);
            string[] forChange = File.ReadAllLines(path, encoding);
            for (int s = 1; s < forChange.Length; s++)
            {
                var splite = forChange[s].Split(';');
                if (splite[8] == Login)
                {
                    int a = Convert.ToInt32(splite[10]);
                    a = a + sum;
                    splite[10] = a.ToString();
                    forChange[s] = splite[0] + ";" + splite[1] + ";" + splite[2] + ";" + splite[3] + ";" + splite[4] + ";" + splite[5] + ";" + splite[6] + ";" + splite[7] + ";" + splite[8] + ";" + splite[9] + ";" +splite[10];
                }
            }
            File.WriteAllLines(path, forChange);
        }
        public void CheckCar()
        {
            if (Car != "")
            {
                CarYesNotify?.Invoke($"В данный момент у вас есть арендованный транспорт: {Car}");
            }
            else
            {
                CarNoNotify?.Invoke("Сейчас вы не арендуете никакого транспорта.");
            }
        }
        public void TakeCar()
        {
            if (Car != "")
            {
                CarTakeDenialNotify?.Invoke($"В данный момент вы уже арендуете {Car}!");
            }
            else
            {
                var path1 = @"C:\Users\Сергей\source\repos\test1\Accounts.csv";
                var path2 = @"C:\Users\Сергей\source\repos\test1\Cars.csv";
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding = Encoding.GetEncoding(1251);
                string[] forChange = File.ReadAllLines(path1, encoding);

                string[] CarsForTake = File.ReadAllLines(path2, encoding);
                Console.WriteLine("Введите категорию транспорта, который бы вы хотели арендовать: A, B, D");
                string a = Console.ReadLine();
                
                if (a == "A")
                {
                    Console.WriteLine("Показываем доступный транспорт, соответствующий Вашей категории...");
                    for (int s = 1; s < CarsForTake.Length; s++)
                    {
                        var splite = CarsForTake[s].Split(';');
                        if (splite[2] == a)
                        {
                            Console.WriteLine($"Id: {splite[0]}");
                            Console.WriteLine($" Модель: {splite[1]}");
                            Console.WriteLine($" Категория: {splite[2]}");
                            Console.WriteLine($" Количество: {splite[3]}");
                            Console.WriteLine($" Цена: {splite[4]}");
                            Console.WriteLine();
                        }
                    }
                    Console.WriteLine("Выберите подходящий транспорт и введите его Id");
                    string id = Console.ReadLine();
                    for(int y = 1; y < CarsForTake.Length; y++)
                    {
                        var splitee = CarsForTake[y].Split(';');
                        if(splitee[0] == id)
                        {
                            if(Convert.ToInt32(splitee[3]) > 0)
                            {
                                Console.WriteLine("На какой срок Вы арендуете транспорт(сут.)?");
                                string date = Console.ReadLine();
                                for (int sss = 1; sss < CarsForTake.Length; sss++)
                                {
                                    var splite = CarsForTake[sss].Split(';');
                                    if (splite[0] == id)
                                    {
                                        int price = Convert.ToInt32(date) * Convert.ToInt32(splite[4]);
                                        if (Sum >= price)
                                        {
                                            Sum = Sum - price;
                                            string CAR = splite[1];
                                            for (int s = 1; s < forChange.Length; s++)
                                            {

                                                var splite1 = forChange[s].Split(';');
                                                if (splite1[8] == Login)
                                                {
                                                    splite1[6] = splite[1];
                                                    splite1[10] = Sum.ToString();
                                                    forChange[s] = splite1[0] + ";" + splite1[1] + ";" + splite1[2] + ";" + splite1[3] + ";" + splite1[4] + ";" + splite1[5] + ";" + splite1[6] + ";" + splite1[7] + ";" + splite1[8] + ";" + splite1[9] + ";" + splite1[10];
                                                }
                                            }
                                            File.WriteAllLines(path1, forChange);
                                            CarTakeSuccessNotify?.Invoke($"Вы успешно арендовали: {CAR}");
                                            Car = CAR;
                                            int bruh = Convert.ToInt32(splite[3]);
                                            bruh--;
                                            splite[3] = bruh.ToString();
                                            CarsForTake[sss] = splite[0] + ";" + splite[1] + ";" + splite[2] + ";" + splite[3] + ";" + splite[4];
                                            File.WriteAllLines(path2, CarsForTake);
                                        }
                                        else
                                        {
                                            CarTakeDenialNotify?.Invoke("Недостаточно денег на балансе!");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                CarTakeDenialNotify?.Invoke("Данного транспорта нет в наличии.");
                            }
                        }
                    }
                    
                    
                }else if(a == "B")
                {
                    Console.WriteLine("Показываем доступный транспорт, соответствующий Вашей категории...");
                    for (int s = 1; s < CarsForTake.Length; s++)
                    {
                        var splite = CarsForTake[s].Split(';');
                        if (splite[2] == a)
                        {
                            Console.WriteLine($"Id: {splite[0]}");
                            Console.WriteLine($" Модель: {splite[1]}");
                            Console.WriteLine($" Категория: {splite[2]}");
                            Console.WriteLine($" Количество: {splite[3]}");
                            Console.WriteLine($" Цена: {splite[4]}");
                            Console.WriteLine();
                        }
                    }
                    Console.WriteLine("Выберите подходящий транспорт и введите его Id");
                    string id = Console.ReadLine();
                    for (int y = 1; y < CarsForTake.Length; y++)
                    {
                        var splitee = CarsForTake[y].Split(';');
                        if (splitee[0] == id)
                        {
                            if (Convert.ToInt32(splitee[3]) > 0)
                            {
                                Console.WriteLine("На какой срок Вы арендуете транспорт(сут.)?");
                                string date = Console.ReadLine();
                                for (int sss = 1; sss < CarsForTake.Length; sss++)
                                {
                                    var splite = CarsForTake[sss].Split(';');
                                    if (splite[0] == id)
                                    {
                                        int price = Convert.ToInt32(date) * Convert.ToInt32(splite[4]);
                                        if (Sum >= price)
                                        {
                                            Sum = Sum - price;
                                            string CAR = splite[1];
                                            for (int s = 1; s < forChange.Length; s++)
                                            {

                                                var splite1 = forChange[s].Split(';');
                                                if (splite1[8] == Login)
                                                {
                                                    splite1[6] = splite[1];
                                                    splite1[10] = Sum.ToString();
                                                    forChange[s] = splite1[0] + ";" + splite1[1] + ";" + splite1[2] + ";" + splite1[3] + ";" + splite1[4] + ";" + splite1[5] + ";" + splite1[6] + ";" + splite1[7] + ";" + splite1[8] + ";" + splite1[9] + ";" + splite1[10];
                                                }
                                            }
                                            File.WriteAllLines(path1, forChange);
                                            CarTakeSuccessNotify?.Invoke($"Вы успешно арендовали: {CAR}");
                                            Car = CAR;
                                            int bruh = Convert.ToInt32(splite[3]);
                                            bruh--;
                                            splite[3] = bruh.ToString();
                                            CarsForTake[sss] = splite[0] + ";" + splite[1] + ";" + splite[2] + ";" + splite[3] + ";" + splite[4];
                                            File.WriteAllLines(path2, CarsForTake);
                                        }
                                        else
                                        {
                                            CarTakeDenialNotify?.Invoke("Недостаточно денег на балансе!");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                CarTakeDenialNotify?.Invoke("Данного транспорта нет в наличии.");
                            }
                        }
                    }
                }
                else if(a == "D")
                {
                    Console.WriteLine("Показываем доступный транспорт, соответствующий Вашей категории...");
                    for (int s = 1; s < CarsForTake.Length; s++)
                    {
                        var splite = CarsForTake[s].Split(';');
                        if (splite[2] == a)
                        {
                            Console.WriteLine($"Id: {splite[0]}");
                            Console.WriteLine($" Модель: {splite[1]}");
                            Console.WriteLine($" Категория: {splite[2]}");
                            Console.WriteLine($" Количество: {splite[3]}");
                            Console.WriteLine($" Цена: {splite[4]}");
                            Console.WriteLine();
                        }
                    }
                    Console.WriteLine("Выберите подходящий транспорт и введите его Id");
                    string id = Console.ReadLine();
                    for (int y = 1; y < CarsForTake.Length; y++)
                    {
                        var splitee = CarsForTake[y].Split(';');
                        if (splitee[0] == id)
                        {
                            if (Convert.ToInt32(splitee[3]) > 0)
                            {
                                Console.WriteLine("На какой срок Вы арендуете транспорт(сут.)?");
                                string date = Console.ReadLine();
                                for (int sss = 1; sss < CarsForTake.Length; sss++)
                                {
                                    var splite = CarsForTake[sss].Split(';');
                                    if (splite[0] == id)
                                    {
                                        int price = Convert.ToInt32(date) * Convert.ToInt32(splite[4]);
                                        if (Sum >= price)
                                        {
                                            Sum = Sum - price;
                                            string CAR = splite[1];
                                            for (int s = 1; s < forChange.Length; s++)
                                            {

                                                var splite1 = forChange[s].Split(';');
                                                if (splite1[8] == Login)
                                                {
                                                    splite1[6] = splite[1];
                                                    splite1[10] = Sum.ToString();
                                                    forChange[s] = splite1[0] + ";" + splite1[1] + ";" + splite1[2] + ";" + splite1[3] + ";" + splite1[4] + ";" + splite1[5] + ";" + splite1[6] + ";" + splite1[7] + ";" + splite1[8] + ";" + splite1[9] + ";" + splite1[10];
                                                }
                                            }
                                            File.WriteAllLines(path1, forChange);
                                            CarTakeSuccessNotify?.Invoke($"Вы успешно арендовали: {CAR}");
                                            Car = CAR;
                                            int bruh = Convert.ToInt32(splite[3]);
                                            bruh--;
                                            splite[3] = bruh.ToString();
                                            CarsForTake[sss] = splite[0] + ";" + splite[1] + ";" + splite[2] + ";" + splite[3] + ";" + splite[4];
                                            File.WriteAllLines(path2, CarsForTake);
                                        }
                                        else
                                        {
                                            CarTakeDenialNotify?.Invoke("Недостаточно денег на балансе!");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                CarTakeDenialNotify?.Invoke("Данного транспорта нет в наличии.");
                            }
                        }
                    }
                }
                else
                {
                    CarTakeDenialNotify?.Invoke("Вы ввели неверную категорю.");
                }
                

            }
        }
        public void PutCar()
        {
            if (Car != "")
            {
                CarYesNotify?.Invoke($"Вы успешно вернули транспорт: {Car}");
                var path1 = @"C:\Users\Сергей\source\repos\test1\Accounts.csv";
                var path2 = @"C:\Users\Сергей\source\repos\test1\Cars.csv";
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding = Encoding.GetEncoding(1251);
                string[] forChangeUser = File.ReadAllLines(path1, encoding);
                string[] forChangeCar = File.ReadAllLines(path2, encoding);
                for (int s = 1; s < forChangeUser.Length; s++)
                {
                    var splite = forChangeUser[s].Split(';');
                    if (splite[8] == Login)
                    {
                        splite[6] = "";
                        forChangeUser[s] = splite[0] + ";" + splite[1] + ";" + splite[2] + ";" + splite[3] + ";" + splite[4] + ";" + splite[5] + ";" + splite[6] + ";" + splite[7] + ";" + splite[8] + ";" + splite[9] + ";" + splite[10];
                    }
                }
                File.WriteAllLines(path1, forChangeUser);
                for (int i = 1; i < forChangeCar.Length; i++)
                {
                    var splite = forChangeCar[i].Split(';');
                    if (splite[1] == Car)
                    {
                        int a = Convert.ToInt32(splite[3]);
                        a++;
                        splite[3] = a.ToString();
                        forChangeCar[i] = splite[0] + ";" + splite[1] + ";" + splite[2] + ";" + splite[3] + ";" + splite[4];
                    }
                }
                File.WriteAllLines(path2, forChangeCar);
                Car = "";
            }
            else
            {
                CarNoNotify?.Invoke("Сейчас вы не арендуете никакого транспорта.");
            }
        }
    }
}

