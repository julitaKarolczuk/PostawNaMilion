using Newtonsoft.Json;
using PostawNaMilion.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
        public Random rnd = new Random();

        public int award = 1000000;
        public int correctAnswerIndex = 0;
        public int currentQuestionNumber = 1;

        public int[] amount = new int[4];

        public Answer[] answers;
        public IEnumerable<Category> Categories;

        public Category currentCategory;
        public Question currentQuestion;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            SubmitButton.Content = "Dalej";
            AmountLabel.Content = $"Kwota: 1000000";
        }

        #region Handlers

        private void Submit(object sender, RoutedEventArgs e)
        {
            if (StartWindow.IsVisible)
            {
                StartWindow.Visibility = StartWindow.IsVisible ? Visibility.Hidden : Visibility.Visible;
                CategoryContent.Visibility = CategoryContent.IsVisible ? Visibility.Hidden : Visibility.Visible;
                SubmitButton.Visibility = SubmitButton.IsVisible ? Visibility.Hidden : Visibility.Visible;

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

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);
        }

        private void SelectCategory(object sender, RoutedEventArgs e)
        {
            currentCategory = Categories.FirstOrDefault(c => c.Name.Equals((sender as Button).Content.ToString(), StringComparison.InvariantCultureIgnoreCase));
            CategoryName.Content = currentCategory.Name;
            currentQuestion = currentCategory.Questions
                .OrderBy(x => rnd.Next())
                .FirstOrDefault();
            QuestionContent.Text = currentQuestion.Content;

            answers = currentQuestion.Answers.ToArray();
            Answer1.Text = answers[0].Content;
            Answer2.Text = answers[1].Content;
            Answer3.Text = answers[2].Content;
            Answer4.Text = answers[3].Content;

            SubmitButton.Content = "Zatwierdź";
            SubmitButton.Visibility = SubmitButton.IsVisible ? Visibility.Hidden : Visibility.Visible;

            CategoryContent.Visibility = CategoryContent.IsVisible ? Visibility.Hidden : Visibility.Visible;
            AnswersContent.Visibility = AnswersContent.IsVisible ? Visibility.Hidden : Visibility.Visible;
        }

        private void GoToMainScreen(object sender, RoutedEventArgs e)
        {
            ResultContent.Visibility = ResultContent.IsVisible ? Visibility.Hidden : Visibility.Visible;
            DisplayLevels();
        }

        private void ReTry(object sender, RoutedEventArgs e)
        {
            InitializeVariables();
            ClearBets();
            DisplayLevels();
        }
        #endregion Handlers

        #region Helpers
        private void InitializeVariables()
        {
            award = 1000000;
            currentQuestionNumber = 1;
            SummaryContent.Visibility = SummaryContent.IsVisible ? Visibility.Hidden : Visibility.Visible;
            TheEndContent.Visibility = Visibility.Hidden;

            SubmitButton.Content = "Dalej";
            #region images
            P7.Visibility = P6.Visibility = P5.Visibility = P4.Visibility = P3.Visibility = P2.Visibility= P1.Visibility = Visibility.Visible;
            img7.Visibility = img6.Visibility = img5.Visibility = img4.Visibility = img3.Visibility = img2.Visibility = img1.Visibility = Visibility.Hidden;
            #endregion images
        }

        private static bool IsTextNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            return reg.IsMatch(str);
        }

        public void ClearBets()
        {
            betAnswer1.Text = betAnswer2.Text = betAnswer3.Text = betAnswer4.Text = "0";
        }

        public async Task LoadData()
        {
            var streamReader = new StreamReader("DataFile.json");
            string jsonData = streamReader.ReadToEnd();

            Categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(jsonData);
            await Task.Delay(4000);

            SplashScreen.Visibility = Visibility.Hidden;
            GameContent.Visibility = Visibility.Visible;
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

        private void ShowResult()
        {
            SubmitButton.Visibility = SubmitButton.IsVisible ? Visibility.Hidden : Visibility.Visible;
            for (var i = 0; i < answers.Length; i++)
            {
                if (currentQuestion.Answers.FirstOrDefault(a => a.IsCorrect).Content == answers[i].Content)
                {
                    correctAnswerIndex = i;
                    award = amount[i];
                    ClearBets();
                    SubmitButton.Content = "Dalej";
                }
            }

            if (award == 0)
            {
                TheEndContent.Visibility = Visibility.Visible;
                ResultContent.Visibility = ResultContent.IsVisible ? Visibility.Hidden : Visibility.Visible;
                SummaryContent.Visibility = SummaryContent.IsVisible ? Visibility.Hidden : Visibility.Visible;
                Summary.Content = "KONIEC GRY";
            }

        }

        private void DisplayLevels()
        {
            AmountLabel.Content = $"Kwota: {award}";
            SubmitButton.Visibility = SubmitButton.IsVisible ? Visibility.Hidden : Visibility.Visible;
            StartWindow.Visibility = StartWindow.IsVisible ? Visibility.Hidden : Visibility.Visible;
            switch (currentQuestionNumber)
            {
                case 8:
                    P7.Visibility = Visibility.Hidden;
                    img7.Visibility = Visibility.Visible;

                    goto case 7;
                case 7:
                    P6.Visibility = Visibility.Hidden;
                    img6.Visibility = Visibility.Visible;

                    goto case 6;
                case 6:
                    P5.Visibility = Visibility.Hidden;
                    img5.Visibility = Visibility.Visible;

                    goto case 5;
                case 5:
                    P4.Visibility = Visibility.Hidden;
                    img4.Visibility = Visibility.Visible;

                    goto case 4;
                case 4:
                    P3.Visibility = Visibility.Hidden;
                    img3.Visibility = Visibility.Visible;

                    goto case 3;
                case 3:
                    P2.Visibility = Visibility.Hidden;
                    img2.Visibility = Visibility.Visible;

                    goto case 2;
                case 2:
                    P1.Visibility = Visibility.Hidden;
                    img1.Visibility = Visibility.Visible;

                    break;

            }
        }
        #endregion Helpers
    }
}
