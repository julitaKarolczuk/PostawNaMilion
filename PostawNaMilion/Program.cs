using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostawNaMilion
{
    class Program
    {
        public int award = 1000000;
        public int currentQuestionNumber = 1;


        public void DisplayAwardsLvl()
        {
            Console.WriteLine("\n           -------POSTAW NA MILION-------");
            Console.WriteLine("\n          P1   P2  P3  P4  P5  P6  P7  P8");

        }

        public void ChooseCategory()
        {
            Console.WriteLine("\n          Wybierz kategorie:\n 1. SPORT\n 2. KRÓLOWIE \n");
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

        public int CheckAnswer(int[] array)
        {
            int correctAnswer = 0;//z bazy trzeba odpowiedzi numer
            award = array[correctAnswer];
            return award;
        }

        public Program()
        {
            do
            {
                DisplayAwardsLvl();
                ChooseCategory();
                Console.WriteLine(award);
            } while (award != 0);
        }

        static void Main(string[] args)
        {
            new Program();
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
    }
}
