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
        public Random random;
        public IEnumerable<Category> Categories;
        public int award = 1000000;
        public int currentQuestionNumber = 0;


        public void DisplayAwardsLvl()
        {
            Console.WriteLine("\n           -------POSTAW NA MILION-------\n          ");
            for(var i = 0; i < currentQuestionNumber; i++)
            {
                Console.Write(" x ");
            }
            for (var i = 0; i < 8 - currentQuestionNumber; i++)
            {
                Console.Write($" {currentQuestionNumber + i + 1} ");
            }


        }

        public void ChooseCategory()
        {
            string[] categories = new string[2];

            var types = Categories.Where(cat => cat.NotUsed).Select(cat => cat.Name).ToArray();
            int index1; 
            int index2;
            do
            {
                index1 = random.Next(types.Length);
                index2 = random.Next(types.Length);
            } while (index1 == index2);

            categories[0] = types[index1];
            categories[1] = types[index2];
            
            Console.WriteLine($"\n          Wybierz kategorie:\n 1. {categories[0]} \n 2. {categories[1]} \n");
            string inputString = Console.ReadLine();
            int input = int.Parse(inputString);
            switch (input)
            {
                case 1:
                    DisplayQuestion(categories[0]);
                    break;
                case 2:
                    DisplayQuestion(categories[1]);
                    break;
                default:
                    Console.WriteLine("Wybierz kategorię ponownie poprzez naciniecie 1 lub 2");
                    return;
            }
        }

        public void DisplayQuestion(string category)
        {
            string randomQuestion;
            var questions = Categories.Where(cat => cat.Name.Equals(category)).Select(c => c.Questions.Select(cate => cate.Content).ToArray()).ToArray();
            Categories.Where(cat => cat.Name.Equals(category)).First().NotUsed = false; 
            
            var questionsCount = questions.Select(c => c.Length).ToArray()[0];
            var r = random.Next(questionsCount);
            randomQuestion = questions[0][r];

            Console.WriteLine($"\n      {category}\n{randomQuestion}\nAnswer 1 ANSWER 2 ANSWER 3\nObstaw odpowiedzi: (Pamiętaj że najmniejsza kwota 10 000)");

            BetAnswers();
 
        }

        public void BetAnswers()
        {
            var sum = 0;
            var arrayBetAmount = new int[3];
            string amount;
            var counter = 0;

            // var answers = Categories.Where(c => c.Name.Equals(category)).Where(c => c.Questions.Equals(randomQuestion)).Select(cat => cat.Questions.Select(c => c.Answers.Select(cate => cate.Content))).ToArray(); ;

            for (int i = 0; i < arrayBetAmount.Length; i++)
            {
                Console.WriteLine($" Odpowiedz {i + 1}: ");
                amount = Console.ReadLine();
                var amountInt = int.Parse(amount);
                if ( amountInt % 10000 == 0)
                {
                    arrayBetAmount[i] = amountInt;
                    sum += arrayBetAmount[i];
                }
                else
                {
                    Console.WriteLine($"Podaj kwoty, ktore sa wielokrotnościami 10 000 zł");
                    BetAnswers();
                }
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
            random = new Random();
            do
            {
                DisplayAwardsLvl();
                ChooseCategory();
                currentQuestionNumber++;
            } while (award != 0 && currentQuestionNumber < 8);
            Console.WriteLine($"WYGRANA {award} zł");
        }

        static void Main(string[] args)
        {
            new Program();
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
    }
}
