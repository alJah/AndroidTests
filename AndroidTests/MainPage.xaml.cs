using System;
using System.ComponentModel;
using Xamarin.Forms;


namespace AndroidTests
{
    
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        #region пасхалка
        DateTime? LastTap = null;
        byte taps = 0;
        readonly private byte numTapsToAlert = 7;

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

        /// <summary>
        /// Сколько ждать миллисекунд до сброса счётчика тапов
        /// </summary>
        readonly private int waitTap = 400; 
        
        /// <summary>
        /// Контекст привязок
        /// </summary>
        public MyView answerView { get; set; }
        public MainPage(MyView myView)
        {
            InitializeComponent();
            answerView = myView;
            BindingContext = answerView;
        }

        /// <summary>
        /// Клик по кнопке "настройки"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Sets sets = new Sets
            {
                BindingContext = answerView
            };
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
            if (int.TryParse(s, out int n))
            {
                answerView.ChangeCase(n - 1);
            }
        }
    }
}
