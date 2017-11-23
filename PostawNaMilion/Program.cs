using Newtonsoft.Json;
using PostawNaMilion.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostawNaMilion
{
    class Program
    {
        public IEnumerable<Category> Categories;
        public int award = 1000000;
        public int currentQuestionNumber = 1;


        public void DisplayAwardsLvl()
        {
            Console.WriteLine("\n          OLCIX-");
            Console.WriteLine("\n           -------POSTAW NA MILION-------");
            Console.WriteLine("\n          P1   P2  P3  P4  P5  P6  P7  P8");

        }

        public void ChooseCategory()
        {
            Console.WriteLine($"\n          Wybierz kategorie:\n 1. \n 2.  \n");
            string y = Console.ReadLine();
            int x = int.Parse(y);
            switch (x)
            {
                case 1:
                    BetAnswers();
                    break;
                case 2:
                    Console.WriteLine("\n   Królowie");
                    break;
            }
        }

        public void BetAnswers()
        {
            var sum = 0;
            var arrayBetAmount = new int[3];
            string amount;
            var counter = 0;
            Console.WriteLine("\n   Sport");
            Console.WriteLine("QUESTION");
            Console.WriteLine("Answer 1 " + "ANSWER 2 " + "ANSWER 3");
            Console.WriteLine("Obstaw odpowiedzi: ");
            for (int i = 0; i < arrayBetAmount.Length; i++)
            {
                amount = Console.ReadLine();
                arrayBetAmount[i] = int.Parse(amount);
                sum += arrayBetAmount[i];
            }
            for (int i = 0; i < arrayBetAmount.Length; i++)
            {
                if (arrayBetAmount[i] == 0)
                {
                    counter++;
                }
            }
            if (counter == 0)
            {
                Console.WriteLine("Jedna odpowiedz ma byc za 0 zł, spróbuj ponownie");
                BetAnswers();
            }
            else
            {
                if (sum == award)
                {
                   var x=  Categories.Where(cat => !cat.AlreadyUsed).Select(cat=> cat.Name);
                    Console.WriteLine("Czy jesteś pewny? Y-tak N-nie");
                    if (Console.ReadLine().Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine($"POZOSTAŁO:  {CheckAnswer(arrayBetAmount)} zł");
                    }
                    else if (Console.ReadLine().Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("Wprowadź kwoty ponownie");
                        BetAnswers();
                    }
                }
                else
                {
                    Console.WriteLine($"Żle rozłozone pieniążki, spróbuj ponownie, masz {award}");
                    BetAnswers();
                }
            }
        }

        public void LoadData()
        {
            var streamReader = new StreamReader("data.json");
            string jsonData = streamReader.ReadToEnd();

            Categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(jsonData);
        }

        public int CheckAnswer(int[] array)
        {
            int correctAnswer = 0;//z bazy trzeba odpowiedzi numer
            award = array[correctAnswer];
            return award;
        }

        public Program()
        {
            LoadData();
            do
            {
                DisplayAwardsLvl();
                ChooseCategory();
                currentQuestionNumber++;
                Console.WriteLine(award);
            } while (award != 0 || currentQuestionNumber < 8);
        }

        static void Main(string[] args)
        {
            new Program();
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
    }
}
