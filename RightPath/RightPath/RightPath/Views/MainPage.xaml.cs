using System;
using System.Linq;
using RightPath.Enums;
using Xamarin.Forms;

namespace RightPath.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(string title)
        {
            InitializeComponent();
            
            NavigationPage.SetHasNavigationBar(this, false);
            Title = title;
            Questions = new Questions();

        }

        private Questions Questions { get; }

        private void Button_OnClicked(object sender, EventArgs e)
        {
			//object eulaAccepted;
			//var hasKey = Application.Current.Properties.TryGetValue("eulaAccepted", out eulaAccepted);
            //if (!hasKey || !(bool)eulaAccepted)
            //{
            //  Navigation.PushModalAsync(new EULAPage());
            //}

            foreach (var question in Questions.QList)
            {
                question.Reset();
            }
            GetNextQuestion(-1);
            checkPurchase();
        }

        private void GetNextQuestion(int currentIndex)
        {
            var newIndex = currentIndex + 1;

             //Check previous answer for Skip count
            if (newIndex > 0 && !Questions[newIndex - 1].IsTextAnswer &&
                Questions[newIndex - 1].Choices.All(
                    choice => choice.Type == AnswerChoiceType.SingleSelection))
            {
                newIndex += Questions[newIndex - 1].Choices.First(ans => ans.IsSelected).Skip;
            }

            //if (newIndex > 0)
            //{
            //    var currentQuestion = Questions[currentIndex];
            //    if (currentQuestion.Id == 34)
            //    {
            //        if ((currentQuestion.Choices[1].IsSelected) ||
            //        (currentQuestion.Choices[2].IsSelected) ||
            //          (currentQuestion.Choices[3].IsSelected))
            //        {
            //            newIndex++;
            //        }
            //    }
            //}


            if (newIndex < Questions.QList.Count)
            {
                Navigation.PushAsync(new QuestionPage(newIndex, Questions[newIndex],
                    GetNextQuestion, Questions.QList.Count));
            }
            else
            {
				Questions.Finish();
                Navigation.PushAsync(new ResultsPage(Questions));
            }
        }


        async void checkPurchase()
        {
            bool purchaseResponse = await App.userManager.MakePurchaseAsync();
            if (purchaseResponse)
            {
                var answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");

            }
            else
            {
                var answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
                if (answer)
                {

                }
                else
                {

                }
            }
        }

    }
}
