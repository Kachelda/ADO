using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdoNet.Queries;

namespace AdoNet
{
    class Program
    {
        const string Keyword = "exit"; //ключевое слово для выхода

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите номер запроса или наберите 'exit' для выхода:");
                int n;
                string num = Console.ReadLine();

                if (Keyword == num)
                {
                    Environment.Exit(0);
                }
                else if (Int32.TryParse(num, out n))
                {
                    switch (n)
                    {
                        case 1:
                            new Query1();
                            break;
                        case 2:
                            new Query2();
                            break;
                        case 3:
                            new Query3();
                            break;
                        case 4:
                            new Query4();
                            break;
                        case 5:
                            new Query5();
                            break;
                        case 6:
                            new Query6();
                            break;
                        case 7:
                            new Query7();
                            break;
                        case 8:
                            new Query8();
                            break;
                        case 9:
                            new Query9();
                            break;
                        case 10:
                            new Query10();
                            break;
                        case 11:
                            new Query11();
                            break;
                        case 12:
                            new Query12();
                            break;
                        case 13:
                            new Query13();
                            break;
                        default:
                            Console.WriteLine("Запроса с таким номером не существует, введите число от 1 до 13");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод, сделайте правильный выбор!");
                }
            }
        }
    }
}
