using System;
using Xamarin.Forms;

namespace AndroidTests
{
    public class MyLabel : Label
    {
        /// <summary>
        /// Был клик?
        /// </summary>
        public bool IsClicked { get; set; }
        public static readonly BindableProperty IsClickedProperty = BindableProperty.Create(propertyName: nameof(IsClicked),
                                                         returnType: typeof(bool),
                                                         declaringType: typeof(MyLabel),
                                                         defaultValue: false,
                                                         defaultBindingMode: BindingMode.OneWay,
                                                         propertyChanged: IsClickedPropertyChanged);
        private static void IsClickedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MyLabel)bindable;
            control.IsClicked = (bool)newValue;
        }
        /// <summary>
        /// Здесь правильный ответ?
        /// </summary>
        public bool IsAnswer { get; set; }
        public static readonly BindableProperty IsAnswerProperty = BindableProperty.Create(propertyName: nameof(IsAnswer),
                                                         returnType: typeof(bool),
                                                         declaringType: typeof(MyLabel),
                                                         defaultValue: false,
                                                         defaultBindingMode: BindingMode.OneWay,
                                                         propertyChanged: IsAnswerPropertyChanged);
        private static void IsAnswerPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MyLabel)bindable;
            control.IsAnswer = (bool)newValue;
        }
        public bool ShowColor { get; set; }
        public static readonly BindableProperty ShowColorProperty = BindableProperty.Create(propertyName: nameof(ShowColor),
            returnType: typeof(bool),
            declaringType: typeof(MyLabel),
            defaultValue: false,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: ShowColorPropertyChanged);

        private static void ShowColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var lb = (MyLabel)bindable);
            if ((bool)newValue)
            {
                lb.ShowAnsweredColor();
            }
            else lb.ShowDefaultColor();
            lb.IsClicked = false;
        }

        public MyLabel()
        {

        }
        private void ShowAnsweredColor()
        {
            if (IsClicked && !IsAnswer)
            {
                BackgroundColor = Color.Red;
                return;
            }
            if (IsAnswer) BackgroundColor = Color.Green;
        }
        private void ShowDefaultColor()
        {
            BackgroundColor = Color.WhiteSmoke;
        }
    }
}
