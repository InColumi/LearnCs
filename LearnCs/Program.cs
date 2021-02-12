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
            string startSearch = "https://yandex.ua/images/search?text=";
            string nameCar = Console.ReadLine();
            StringBuilder builder = new StringBuilder();
            string[] nameCarSplit = nameCar.Split(' ');
            for (int i = 0; i < nameCarSplit.Length - 1; i++)
            {
                builder.Append(nameCarSplit[i]);
                builder.Append("%20");
            }
            builder.Append(nameCarSplit[nameCarSplit.Length - 1]);
            nameCar = builder.ToString();
            builder.Clear();
            using (StreamReader reader = new StreamReader("input.txt"))
            {
                while (reader.EndOfStream == false)
                {
                    builder.Append(startSearch);
                    builder.Append(reader.ReadLine());
                    builder.Append(nameCar);
                    Console.WriteLine(builder.ToString());
                    Process.Start(builder.ToString());
                    builder.Clear();
                }
            }
            Console.WriteLine("Search complited.");
            Console.ReadKey();
        }
    }
}
