using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearnCs
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Queue<string> notValidLines = new Queue<string>();
                using (StreamReader readerNotValid = new StreamReader("not valid.txt"))
                {
                    while (readerNotValid.EndOfStream == false)
                    {
                        notValidLines.Enqueue(readerNotValid.ReadLine());
                    }
                }

                List<string> validLines = new List<string>();
                using (StreamReader readerNotValid = new StreamReader("valid.txt"))
                {
                    while (readerNotValid.EndOfStream == false)
                    {
                        validLines.Add(readerNotValid.ReadLine());
                    }
                }

                int countDeleteLines = 0;
                while (notValidLines.Count > 0)
                {
                    string lineForDelete = notValidLines.Dequeue();
                    for (int i = 0; i < validLines.Count; i++)
                    {
                        if (validLines[i].Contains(lineForDelete))
                        {
                            Console.WriteLine($"{validLines[i]} was deleted.");
                            validLines.RemoveAt(i);
                            countDeleteLines++;
                            break;
                        }
                    }
                }

                using (StreamWriter writer = new StreamWriter("valid.txt"))
                {
                    foreach (var line in validLines)
                    {
                        writer.WriteLine(line);
                    }
                }

                Console.WriteLine($"{countDeleteLines} line(s) was deleted.");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            Console.ReadKey();
        }
    }
}
