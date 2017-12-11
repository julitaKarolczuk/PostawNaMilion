﻿using Newtonsoft.Json;
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
                Console.WriteLine($"{answer.Content}\n");
            }

            for (int i = 0; i < arrayBetAmount.Length; i++)
            {
                Console.WriteLine($" Odpowiedz {i + 1}: ");

                do
                {
                    Console.WriteLine($"Podaj kwoty, ktore sa wielokrotnościami 100 000 zł");
                    amount = Console.ReadLine();
                    amountInt = int.Parse(amount);
                } while (amountInt % 100000 != 0);

                arrayBetAmount[i] = amountInt;
            }

            if (arrayBetAmount.All(amt => amt != 0))
            {
                Console.WriteLine("Jedna odpowiedz ma byc za 0 zł (jedna zapadnia pusta), spróbuj ponownie");
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
                    Console.WriteLine($"Podana kombinacja kwot nie jest mozliwa, masz {award}");
                    return BetAnswers(question);
                }
            }
        }

        public void drawAward(int award)
        {

            Console.WriteLine("WWWWWWWW                           WWWWWWWW     IIIIIIIIII     NNNNNNNN        NNNNNNNN  ");
            Console.WriteLine("W::::::W                           W::::::W     I::::::::I     N:::::::N       N::::::N  ");
            Console.WriteLine("W::::::W                           W::::::W     I::::::::I     N::::::::N      N::::::N  ");
            Console.WriteLine("W::::::W                           W::::::W     II::::::II     N:::::::::N     N::::::N  ");
            Console.WriteLine(" W:::::W           WWWWW           W:::::W        I::::I       N::::::::::N    N::::::N  ");
            Console.WriteLine("  W:::::W         W:::::W         W:::::W         I::::I       N:::::::::::N   N::::::N  ");
            Console.WriteLine("   W:::::W       W:::::::W       W:::::W          I::::I       N:::::::N::::N  N::::::N  ");
            Console.WriteLine("    W:::::W     W:::::::::W     W:::::W           I::::I       N::::::N N::::N N::::::N  ");
            Console.WriteLine("     W:::::W   W:::::W:::::W   W:::::W            I::::I       N::::::N  N::::N:::::::N  ");
            Console.WriteLine("      W:::::W W:::::W W:::::W W:::::W             I::::I       N::::::N   N:::::::::::N  ");
            Console.WriteLine("       W:::::W:::::W   W:::::W:::::W              I::::I       N::::::N    N::::::::::N  ");
            Console.WriteLine("        W:::::::::W     W:::::::::W               I::::I       N::::::N     N:::::::::N  ");
            Console.WriteLine("         W:::::::W       W:::::::W              II::::::II     N::::::N      N::::::::N  ");
            Console.WriteLine("          W:::::W         W:::::W               I::::::::I     N::::::N       N:::::::N  ");
            Console.WriteLine("           W:::W           W:::W                I::::::::I     N::::::N        N::::::N  ");
            Console.WriteLine("            WWW             WWW                 IIIIIIIIII     NNNNNNNN         NNNNNNN  \n\n\n");

            if (award == 0)
            {

                Console.WriteLine("     000000000");
                Console.WriteLine("   00:::::::::00");
                Console.WriteLine(" 00:::::::::::::00");
                Console.WriteLine("0:::::::000:::::::0");
                Console.WriteLine("0::::::0   0::::::0");
                Console.WriteLine("0:::::0     0:::::0");
                Console.WriteLine("0:::::0     0:::::0");
                Console.WriteLine("0:::::0 000 0:::::0");
                Console.WriteLine("0:::::0 000 0:::::0");
                Console.WriteLine("0:::::0     0:::::0");
                Console.WriteLine("0:::::0     0:::::0");
                Console.WriteLine("0::::::0   0::::::0");
                Console.WriteLine("0:::::::000:::::::0");
                Console.WriteLine(" 00:::::::::::::00");
                Console.WriteLine("   00:::::::::00");
                Console.WriteLine("     000000000");

            }
            if (award == 1000000)
            {
                Console.WriteLine($"  1111111         000000000             000000000             000000000             000000000             000000000             000000000");
                Console.WriteLine($" 1::::::1       00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00");
                Console.WriteLine($"1:::::::1     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00");
                Console.WriteLine($"111:::::1     0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0 ");
                Console.WriteLine($"   1::::1     0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0");
                Console.WriteLine($"   1::::1     0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0");
                Console.WriteLine($"   1::::1     0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0");
                Console.WriteLine($"   1::::1     0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0");
                Console.WriteLine($"   1::::1     0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0");
                Console.WriteLine($"   1::::1     0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0");
                Console.WriteLine($"   1::::1     0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0");
                Console.WriteLine($"   1::::1     0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0");
                Console.WriteLine($"111::::::111  0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0");
                Console.WriteLine($"1::::::::::1   00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00");
                Console.WriteLine($"1::::::::::1     00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00");
                Console.WriteLine($"111111111111       000000000             000000000             000000000             000000000             000000000             000000000");

            }
            if (award == 100000)
            {
                Console.WriteLine($"  1111111         000000000             000000000             000000000             000000000             000000000         ");
                Console.WriteLine($" 1::::::1       00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00       ");
                Console.WriteLine($"1:::::::1     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     ");
                Console.WriteLine($"111:::::1     0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   ");
                Console.WriteLine($"   1::::1     0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   ");
                Console.WriteLine($"   1::::1     0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine($"   1::::1     0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine($"   1::::1     0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   ");
                Console.WriteLine($"   1::::1     0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   ");
                Console.WriteLine($"   1::::1     0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine($"   1::::1     0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine($"   1::::1     0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   ");
                Console.WriteLine($"111::::::111  0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   ");
                Console.WriteLine($"1::::::::::1   00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00    ");
                Console.WriteLine($"1::::::::::1     00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00      ");
                Console.WriteLine($"111111111111       000000000             000000000             000000000             000000000             000000000        ");

            }
            if (award == 200000)
            {

                Console.WriteLine(" 222222222222222           000000000             000000000             000000000             000000000             000000000  ");
                Console.WriteLine("2:::::::::::::::22       00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00           ");
                Console.WriteLine("2::::::222222:::::2    00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00        ");
                Console.WriteLine("2222222     2:::::2   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0       ");
                Console.WriteLine("            2:::::2   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0       ");
                Console.WriteLine("            2:::::2   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0       ");
                Console.WriteLine("         2222::::2    0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0       ");
                Console.WriteLine("    22222::::::22     0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0      ");
                Console.WriteLine("  22::::::::222       0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0       ");
                Console.WriteLine(" 2:::::22222          0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0    ");
                Console.WriteLine("2:::::2               0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine("2:::::2               0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   ");
                Console.WriteLine("2:::::2       222222  0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0     ");
                Console.WriteLine("2::::::2222222:::::2   00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00  ");
                Console.WriteLine("2::::::::::::::::::2     00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00   ");
                Console.WriteLine("22222222222222222222       000000000             000000000             000000000             000000000             000000000  ");
            }
            if (award == 300000)
            {
                Console.WriteLine($"333333333333333           000000000             000000000             000000000             000000000             000000000        ");
                Console.WriteLine($"3:::::::::::::::33      00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00      ");
                Console.WriteLine($"3::::::33333::::::3    00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00    ");
                Console.WriteLine($"3333333     3:::::3   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   ");
                Console.WriteLine($"            3:::::3   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   ");
                Console.WriteLine($"            3:::::3   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine($"    33333333:::::3    0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine($"    3:::::::::::3     0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   ");
                Console.WriteLine($"    33333333:::::3    0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   ");
                Console.WriteLine($"            3:::::3   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine($"            3:::::3   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine($"            3:::::3   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   ");
                Console.WriteLine($"3333333     3:::::3   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   ");
                Console.WriteLine($"3::::::33333::::::3    00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00    ");
                Console.WriteLine($"3:::::::::::::::33       00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00      ");
                Console.WriteLine($" 333333333333333           000000000             000000000             000000000             000000000             000000000        ");

            }
            if (award == 400000)
            {

                Console.WriteLine("       444444444          000000000             000000000             000000000             000000000             000000000");
                Console.WriteLine("      4::::::::4        00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00");
                Console.WriteLine("     4:::::::::4      00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00 ");
                Console.WriteLine("    4::::44::::4     0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0  ");
                Console.WriteLine("   4::::4 4::::4     0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0 ");
                Console.WriteLine("  4::::4  4::::4     0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine(" 4::::4   4::::4     0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine("4::::444444::::444   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0 ");
                Console.WriteLine("4::::::::::::::::4   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0 ");
                Console.WriteLine("4444444444:::::444   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0 ");
                Console.WriteLine("          4::::4     0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0 ");
                Console.WriteLine("          4::::4     0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0");
                Console.WriteLine("          4::::4     0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0 ");
                Console.WriteLine("        44::::::44    00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00 ");
                Console.WriteLine("        4::::::::4      00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00");
                Console.WriteLine("        4444444444        000000000             000000000             000000000             000000000             000000000");

            }
            if (award == 500000)
            {
                Console.WriteLine($"555555555555555555         000000000             000000000             000000000             000000000             000000000        ");
                Console.WriteLine($"5::::::::::::::::5       00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00      ");
                Console.WriteLine($"5::::::::::::::::5     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00    ");
                Console.WriteLine($"5:::::555555555555    0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   ");
                Console.WriteLine($"5:::::5               0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   ");
                Console.WriteLine($"5:::::5               0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine($"5:::::5555555555      0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine($"5:::::::::::::::5     0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   ");
                Console.WriteLine($"555555555555:::::5    0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   ");
                Console.WriteLine($"            5:::::5   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine($"            5:::::5   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine($"5555555     5:::::5   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   ");
                Console.WriteLine($"5::::::55555::::::5   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   ");
                Console.WriteLine($"55:::::::::::::55      00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00    ");
                Console.WriteLine($"   55:::::::::55         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00      ");
                Console.WriteLine($"     555555555             000000000             000000000             000000000             000000000             000000000        ");

            }
            if (award == 600000)
            {
                Console.WriteLine("        66666666          000000000             000000000             000000000             000000000             000000000");
                Console.WriteLine("       6::::::6         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00");
                Console.WriteLine("      6::::::6        00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00  ");
                Console.WriteLine("     6::::::6        0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0 ");
                Console.WriteLine("    6::::::6         0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0 ");
                Console.WriteLine("   6::::::6          0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine("  6::::::6           0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0  ");
                Console.WriteLine(" 6::::::::66666      0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0 ");
                Console.WriteLine("6::::::::::::::66    0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0");
                Console.WriteLine("6::::::66666:::::6   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0 ");
                Console.WriteLine("6:::::6     6:::::6  0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0 ");
                Console.WriteLine("6:::::6     6:::::6  0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0 ");
                Console.WriteLine("6::::::66666::::::6  0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0 ");
                Console.WriteLine(" 66:::::::::::::66    00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00 ");
                Console.WriteLine("   66:::::::::66        00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00");
                Console.WriteLine("     666666666            000000000             000000000             000000000             000000000             000000000");


            }
            if (award == 700000)
            {
                Console.WriteLine($"77777777777777777777        000000000             000000000             000000000             000000000             000000000        ");
                Console.WriteLine($"7::::::::::::::::::7      00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00      ");
                Console.WriteLine($"7::::::::::::::::::7    00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00    ");
                Console.WriteLine($"777777777777:::::::7   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   ");
                Console.WriteLine($"           7::::::7    0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   ");
                Console.WriteLine($"          7::::::7     0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine($"         7::::::7      0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine($"        7::::::7       0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   ");
                Console.WriteLine($"       7::::::7        0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   ");
                Console.WriteLine($"      7::::::7         0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine($"     7::::::7          0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine($"    7::::::7           0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   ");
                Console.WriteLine($"   7::::::7            0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   ");
                Console.WriteLine($"  7::::::7              00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00    ");
                Console.WriteLine($" 7::::::7                 00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00      ");
                Console.WriteLine($"77777777                    000000000             000000000             000000000             000000000             000000000        ");

            }
            if (award == 800000)
            {

                Console.WriteLine("     888888888           000000000             000000000             000000000             000000000             000000000");
                Console.WriteLine("   88:::::::::88       00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00");
                Console.WriteLine(" 88:::::::::::::88   00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00  ");
                Console.WriteLine("8::::::88888::::::8  0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0 ");
                Console.WriteLine("8:::::8     8:::::8  0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0 ");
                Console.WriteLine("8:::::8     8:::::8  0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine(" 8:::::88888:::::8   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0  ");
                Console.WriteLine("  8:::::::::::::8    0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0 ");
                Console.WriteLine(" 8:::::88888:::::8   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0");
                Console.WriteLine("8:::::8     8:::::8  0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0 ");
                Console.WriteLine("8:::::8     8:::::8  0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0 ");
                Console.WriteLine("8:::::8     8:::::8  0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0 ");
                Console.WriteLine("8::::::88888::::::8  0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0 ");
                Console.WriteLine(" 88:::::::::::::88    00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00 ");
                Console.WriteLine("   88:::::::::88        00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00");
                Console.WriteLine("     888888888            000000000             000000000             000000000             000000000             000000000");


            }
            if (award == 900000)
            {
                Console.WriteLine("     999999999           000000000             000000000             000000000             000000000             000000000");
                Console.WriteLine("   99:::::::::99       00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00");
                Console.WriteLine(" 99:::::::::::::99   00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00  ");
                Console.WriteLine("9::::::99999::::::9  0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0 ");
                Console.WriteLine("9:::::9     9:::::9  0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0 ");
                Console.WriteLine("9:::::9     9:::::9  0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   ");
                Console.WriteLine(" 9:::::99999::::::9  0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0  ");
                Console.WriteLine("  99::::::::::::::9  0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0 ");
                Console.WriteLine("    99999::::::::9   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0");
                Console.WriteLine("         9::::::9    0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0 ");
                Console.WriteLine("        9::::::9     0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0   0:::::0     0:::::0 ");
                Console.WriteLine("      9::::::9       0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0   0::::::0 ");
                Console.WriteLine("     9::::::9        0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0   0:::::::000:::::::0 ");
                Console.WriteLine("    9::::::9          00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00     00:::::::::::::00 ");
                Console.WriteLine("   9::::::9             00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00         00:::::::::00");
                Console.WriteLine("  99999999                000000000             000000000             000000000             000000000             000000000");

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
            //Console.WriteLine($"WYGRANA {award} zł");
            drawAward(award);
        }

        static void Main(string[] args)
        {
            new Program();
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
    }
}