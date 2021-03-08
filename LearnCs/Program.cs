using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCs
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("1) Create folders.");
            Console.WriteLine("2) Search in yandex.");
            Console.WriteLine("Please enter number of task:");
            int number = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter car name: ");
            string nameCar = Console.ReadLine();
            DoTaskBy(number, nameCar);

            Console.ReadKey();
        }

        private static void DoTaskBy(int numberOfTask, string nameCar)
        {
            switch (numberOfTask)
            {
                case 1:
                    DoTask1(nameCar);
                    break;
                case 2:
                    DoTask2(nameCar);
                    break;
                default:
                    Console.WriteLine($"Task with {numberOfTask} is not exist!");
                    break;
            }
        }

        private static void DoTask1(string nameCar)
        {
            StringBuilder builder = new StringBuilder();
            string builderInString;
            using (StreamReader reader = new StreamReader("input.txt"))
            {
                while (reader.EndOfStream == false)
                {
                    builder.Append("md \"");
                    builder.Append(reader.ReadLine());
                    builder.Append(" ");
                    builder.Append(nameCar);
                    builder.Append('"');
                    builderInString = builder.ToString();
                    Console.WriteLine(builderInString);
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = $"cmd",
                        Arguments = $"/c {builderInString}",
                        WindowStyle = ProcessWindowStyle.Hidden
                    });
                    builder.Clear();
                }
            }
            Console.WriteLine("Folders created.");
        }

        private static void DoTask2(string nameCar)
        {
            string startSearch = "https://yandex.ua/images/search?text=";

            nameCar = GetNewFormat(nameCar, ' ', "%20");

            StringBuilder builder = new StringBuilder();
            string builderInString;
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader("input.txt"))
            {
                while (reader.EndOfStream == false)
                {
                    lines.Add(reader.ReadLine());
                }                
            }

            lines.Sort();
            foreach (var line in lines)
            {
                builder.Append(startSearch);
                builder.Append(GetNewFormat(line, ' ', "%20"));
                builder.Append("%20");
                builder.Append(nameCar);
                builderInString = builder.ToString();
                Console.WriteLine(builderInString);
                Process.Start(builderInString);
                builder.Clear();
            }
            Console.WriteLine("Search complited.");
        }

        private static string GetNewFormat(string line, char olsSymbol, string newSymbol)
        {
            StringBuilder builder = new StringBuilder();
            string[] words = line.Split(olsSymbol);
            for (int i = 0; i < words.Length - 1; i++)
            {
                builder.Append(words[i]);
                builder.Append(newSymbol);
            }
            builder.Append(words[words.Length - 1]);
            return builder.ToString();
        }
    }
}
