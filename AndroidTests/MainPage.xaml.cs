using System;
using System.ComponentModel;
using Xamarin.Forms;


namespace AndroidTests
{
    
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        #region пасхалка
        DateTime? LastTap = null;
        byte taps = 0;
        byte numTapsToAlert = 7;

        /// <summary>
        /// Покажет пасхалку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ShowMessage(object sender, EventArgs e)
        {
            if (LastTap == null || (DateTime.Now - LastTap.Value).TotalMilliseconds < waitTap)
            {
                if (taps == (numTapsToAlert - 1))
                {

                    await App.Current.MainPage.DisplayAlert
     (/*это было произнесено в офисе конторы торгующей металлом*/
        "Учите металл!",
        "Круг - это то же самое, что и квадрат, только круг - круглый, а квадрат - квадратный.",
        "Ясно");

                    taps = 0;
                    LastTap = null;
                    return;
                }
                else
                {
                    taps++;
                    LastTap = DateTime.Now;
                }
            }
            else
            {
                taps = 1;
                LastTap = DateTime.Now;
            }
        }
        #endregion

        Color selected = Color.FromRgb(237,234,146);
        Color unselected = Color.FromRgb(253,253,253);
        /// <summary>
        /// Сколько ждать миллисекунд до сброса счётчика тапов
        /// </summary>
        int waitTap = 400; 
        
        /// <summary>
        /// Контекст привязок
        /// </summary>
        public MyView answerView;
        public MainPage(MyView myView)
        {
            InitializeComponent();
            answerView = myView;
            answerView.GotAnswer += AnswerView_GotAnswer;
            BindingContext = answerView;

        }
        /// <summary>
        /// Получен ответ
        /// </summary>
        private void AnswerView_GotAnswer()
        {
            if (!CheckAnswer()) answerView.QuestCase.Errors = 5;
            if (answerView.Errors > 0 & CheckAnswer()) answerView.QuestCase.Errors -= 1;
            lab0.ShowColor();
            lab1.ShowColor();
            lab2.ShowColor();
            lab3.ShowColor();
        }
        /// <summary>
        /// Клик по кнопке "Следующий вопрос"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Clicked(object sender, EventArgs e)
        {
            InitializeLabels();
            answerView.NextCase();
        }
        /// <summary>
        /// Сбросить свойства лейблов
        /// </summary>
        private void InitializeLabels()
        {
            lab0.BackgroundColor = unselected;
            lab1.BackgroundColor = unselected;
            lab2.BackgroundColor = unselected;
            lab3.BackgroundColor = unselected;

            lab0.IsClicked = false;
            lab1.IsClicked = false;
            lab2.IsClicked = false;
            lab3.IsClicked = false;
        }
        /// <summary>
        /// Тап по лейблу с ответом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (answerView.IsAnswered) return;
            MyLabel control = (MyLabel)sender;
            if(control.IsClicked)
            {
                answerView.Taps -= 1;
                control.IsClicked = false;
                if (control.BackgroundColor == selected) control.BackgroundColor = unselected;
            }
            else
            {
                control.IsClicked = true;
                answerView.Taps += 1;
                if (!answerView.IsAnswered) control.BackgroundColor = selected;
            }        
        }
        /// <summary>
        /// Ответ верный?
        /// </summary>
        /// <returns></returns>
        private bool CheckAnswer()
        {
            if (lab0.IsClicked && !lab0.IsAnswer) return false;
            if (lab1.IsClicked && !lab1.IsAnswer) return false;
            if (lab2.IsClicked && !lab2.IsAnswer) return false;
            if (lab3.IsClicked && !lab3.IsAnswer) return false;
            return true;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        /// <summary>
        /// Клик по кнопке "настройки"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Sets sets = new Sets();
            sets.BindingContext = answerView;
            Navigation.PushAsync(sets);
        }
        /// <summary>
        /// Переход к указанному вопросу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            string s = await DisplayPromptAsync(" ", "Перейти к вопросу №", initialValue: "1", maxLength: 3, keyboard: Keyboard.Numeric);
            int n = 0;
            if(int.TryParse(s,out n) )
            {
                InitializeLabels();
                answerView.NextCase(n-1);
            }
        }
    }
}
