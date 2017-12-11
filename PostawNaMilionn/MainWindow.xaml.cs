using Newtonsoft.Json;
using PostawNaMilion.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PostawNaMilionn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public int award = 1000000;
        public int correctAnswerIndex = 0;
        public int currentQuestionNumber = 1;
        public int[] amount = new int[4];
        public Answer[] answers;
        public IEnumerable<Category> Categories;
        public Random rnd = new Random();
        public Category currentCategory;
        public Question currentQuestion;

        private void NumericOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);

        }

        private static bool IsTextNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            return reg.IsMatch(str);

        }

        public void LoadData()
        {
            var streamReader = new StreamReader("DataFile.json");
            string jsonData = streamReader.ReadToEnd();

            Categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(jsonData);
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            DisplayLevels();
        }

        private void ShowCategories()
        {
            var categories = Categories.Where(cat => cat.NotUsed)
              .OrderBy(x => rnd.Next())
              .Take(2)
              .ToArray();

            Category1.Content = categories[0].Name;
            Category2.Content = categories[1].Name;
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            if (StartWindow.IsVisible)
            {
                StartWindow.Visibility = StartWindow.IsVisible ? Visibility.Hidden : Visibility.Visible;
                CategoryContent.Visibility = CategoryContent.IsVisible ? Visibility.Hidden : Visibility.Visible;
                button.Visibility = button.IsVisible ? Visibility.Hidden : Visibility.Visible;

                ShowCategories();

            }
            else if (AnswersContent.IsVisible)
            {
                if (int.TryParse(betAnswer1.Text, out int bet1) &&
                    int.TryParse(betAnswer2.Text, out int bet2) &&
                    int.TryParse(betAnswer3.Text, out int bet3) &&
                    int.TryParse(betAnswer4.Text, out int bet4) &&
                    bet1 + bet2 + bet3 + bet4 == award)
                {
                    amount[0] = bet1;
                    amount[1] = bet2;
                    amount[2] = bet3;
                    amount[3] = bet4;

                    if (amount.All(amt => amt != 0))
                    {
                        MessageBox.Show("Jedna odpowiedz ma byc za 0 zł (jedna zapadnia pusta), spróbuj ponownie", "Confirm", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        if (MessageBox.Show("Czy jesteś pewny?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            AnswersContent.Visibility = AnswersContent.IsVisible ? Visibility.Hidden : Visibility.Visible;
                            ResultContent.Visibility = ResultContent.IsVisible ? Visibility.Hidden : Visibility.Visible;
                            currentQuestionNumber++;
                            ShowResult();
                        }
                    }

                }
                else
                {
                    MessageBox.Show($"Podana kombinacja kwot nie jest mozliwa, masz {award}", "Confirm", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ShowResult()
        {
            button.Visibility = button.IsVisible ? Visibility.Hidden : Visibility.Visible;
            for (var i = 0; i < answers.Length; i++)
            {
                if (currentQuestion.Answers.FirstOrDefault(a => a.IsCorrect).Content == answers[i].Content)
                {
                    correctAnswerIndex = i;
                    award = amount[i];
                }
            }

            Result.Content = $"{currentQuestion.Content}   {currentQuestion.Answers.FirstOrDefault(a => a.IsCorrect).Content}\n\n WYGRAŁEŚ: {award}";
            if (award == 0)
            {
                ResultContent.Visibility = ResultContent.IsVisible ? Visibility.Hidden : Visibility.Visible;
                SummaryContent.Visibility = SummaryContent.IsVisible ? Visibility.Hidden : Visibility.Visible;
                Summary.Content = $"KONIEC GRY PRZEEBAŁEŚ HAJS";
            }

        }

        private void SelectCategory(object sender, RoutedEventArgs e)
        {
            currentCategory = Categories.FirstOrDefault(c => c.Name.Equals((sender as Button).Content.ToString(), StringComparison.InvariantCultureIgnoreCase));
            CategoryName.Content = currentCategory.Name;
            currentQuestion = currentCategory.Questions
                .OrderBy(x => rnd.Next())
                .FirstOrDefault();
            QuestionContent.Content = currentQuestion.Content;

            answers = currentQuestion.Answers.ToArray();
            Answer1.Content = answers[0].Content;
            Answer2.Content = answers[1].Content;
            Answer3.Content = answers[2].Content;
            Answer4.Content = answers[3].Content;

            CategoryContent.Visibility = CategoryContent.IsVisible ? Visibility.Hidden : Visibility.Visible;
            AnswersContent.Visibility = AnswersContent.IsVisible ? Visibility.Hidden : Visibility.Visible;
            button.Visibility = button.IsVisible ? Visibility.Hidden : Visibility.Visible;

        }

        private void GoToMainScreen(object sender, RoutedEventArgs e)
        {
            ResultContent.Visibility = ResultContent.IsVisible ? Visibility.Hidden : Visibility.Visible;
            DisplayLevels();
        }

        private void ReTry(object sender, RoutedEventArgs e)
        {
            SummaryContent.Visibility = SummaryContent.IsVisible ? Visibility.Hidden : Visibility.Visible;
            award = 1000000;
            currentQuestionNumber = 0;
            //img na hidden poustawiac, jak sie przegra, bo zostają okejki; CASE 1 może zrobic i tam poustawiac na wartosci startowe wszystko jak ma byc
            DisplayLevels();
        }


        private void DisplayLevels()
        {
            AmountLabel.Content = $"{award}";
            button.Visibility = button.IsVisible ? Visibility.Hidden : Visibility.Visible;
            StartWindow.Visibility = StartWindow.IsVisible ? Visibility.Hidden : Visibility.Visible;
            switch (currentQuestionNumber)
            {
                case 8:
                    if (currentQuestionNumber == 8)
                    {
                        P7.Visibility = P7.IsVisible ? Visibility.Hidden : Visibility.Visible;
                        img7.Visibility = img7.IsVisible ? Visibility.Hidden : Visibility.Visible;
                    }

                    goto case 7;
                case 7:
                    if (currentQuestionNumber == 7)
                    {
                        P6.Visibility = P6.IsVisible ? Visibility.Hidden : Visibility.Visible;
                        img6.Visibility = img6.IsVisible ? Visibility.Hidden : Visibility.Visible;
                    }

                    goto case 6;
                case 6:
                    if (currentQuestionNumber == 6)
                    {
                        P5.Visibility = P5.IsVisible ? Visibility.Hidden : Visibility.Visible;
                        img5.Visibility = img5.IsVisible ? Visibility.Hidden : Visibility.Visible;
                    }

                    goto case 5;
                case 5:
                    if (currentQuestionNumber == 5)
                    {
                        P4.Visibility = P4.IsVisible ? Visibility.Hidden : Visibility.Visible;
                        img4.Visibility = img4.IsVisible ? Visibility.Hidden : Visibility.Visible;
                    }

                    goto case 4;
                case 4:
                    if (currentQuestionNumber == 4)
                    {
                        P3.Visibility = P3.IsVisible ? Visibility.Hidden : Visibility.Visible;
                        img3.Visibility = img3.IsVisible ? Visibility.Hidden : Visibility.Visible;
                    }

                    goto case 3;
                case 3:
                    if (currentQuestionNumber == 3)
                    {
                        P2.Visibility = P2.IsVisible ? Visibility.Hidden : Visibility.Visible;
                        img2.Visibility = img2.IsVisible ? Visibility.Hidden : Visibility.Visible;
                    }

                    goto case 2;
                case 2:
                    if (currentQuestionNumber == 2)
                    {
                        P1.Visibility = P1.IsVisible ? Visibility.Hidden : Visibility.Visible;
                        img1.Visibility = img1.IsVisible ? Visibility.Hidden : Visibility.Visible;
                    }

                    break;

            }
        }
    }
}
