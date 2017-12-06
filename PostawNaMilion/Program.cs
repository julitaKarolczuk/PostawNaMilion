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
        public Random rnd = new Random();
        public IEnumerable<Category> Categories;
        public int award = 1000000;
        public int currentQuestionNumber = 0;


        public void DisplayAwardsLvl()
        {
            Console.WriteLine("\n           -------POSTAW NA MILION-------\n          ");
            for (var i = 0; i < currentQuestionNumber; i++)
            {
                Console.Write(" x ");
            }
            for (var i = 0; i < 8 - currentQuestionNumber; i++)
            {
                Console.Write($" {currentQuestionNumber + i + 1} ");
            }

        }

        public Category ChooseCategory()
        {
            var categories = Categories.Where(cat => cat.NotUsed)
                .OrderBy(x => rnd.Next())
                .Take(2)
                .ToArray();

            Console.WriteLine($"\n          Wybierz kategorie:\n 1. {categories[0].Name} \n 2. {categories[1].Name} \n");
            string inputString = Console.ReadLine();
            int input = int.Parse(inputString);
            return categories[input - 1];
        }

        public Question ChooseQuestion(Category category)
        {
            var questions = category.Questions;
            category.NotUsed = false;

            var randomQuestion = questions.OrderBy(q => rnd.Next()).FirstOrDefault();

            Console.WriteLine($"\n      {category.Name}\n{randomQuestion.Content}");

            return randomQuestion;
        }

        public int[] BetAnswers(Question question)
        {
            var arrayBetAmount = new int[4];
            string amount;
            int amountInt;

            var answers = question.Answers;
            foreach (var answer in answers)
            {
                Console.WriteLine($"{answer.Content}");
            }

            for (int i = 0; i < arrayBetAmount.Length; i++)
            {
                Console.WriteLine($" Odpowiedz {i + 1}: ");

                do
                {
                    Console.WriteLine($"Podaj kwoty, ktore sa wielokrotnościami 10 000 zł");
                    amount = Console.ReadLine();
                    amountInt = int.Parse(amount);
                } while (amountInt % 10000 != 0);

                arrayBetAmount[i] = amountInt;
            }

            if (arrayBetAmount.All(amt => amt != 0))
            {
                Console.WriteLine("Jedna odpowiedz ma byc za 0 zł, spróbuj ponownie");
                return BetAnswers(question);
            }
            else
            {
                if (arrayBetAmount.Sum() == award)
                {
                    do
                    {
                        Console.WriteLine("Czy jesteś pewny? Y-tak N-nie");
                        var decision = Console.ReadLine();
                        if (decision.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                        {
                            return arrayBetAmount;
                        }
                        else if (decision.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                        {
                            return BetAnswers(question);
                        }
                    } while (true);

                }
                else
                {
                    Console.WriteLine($"Żle rozłozone pieniążki, spróbuj ponownie, masz {award}");
                    return BetAnswers(question);
                }
            }
        }

        public void LoadData()
        {
            var streamReader = new StreamReader("data.json");
            string jsonData = streamReader.ReadToEnd();

            Categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(jsonData);
        }

        public int CheckAnswers(int[] bets, Question question)
        {
            for( var i = 0; i < question.Answers.Count(); i++)
            {
                if (question.Answers.ElementAt(i).IsCorrect)
                {
                    return bets[i];
                }
            }
            return 0;
        }

        public Program()
        {
            LoadData();
            do
            {
                DisplayAwardsLvl();
                Console.WriteLine($"\nPozostało: {award}");
                var category = ChooseCategory();
                var question = ChooseQuestion(category);
                var bets = BetAnswers(question);
                award = CheckAnswers(bets, question);
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
