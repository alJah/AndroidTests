using Xamarin.Forms;

namespace AndroidTests
{
    public class MyLabel : Label
    {
        /// <summary>
        /// Был клик?
        /// </summary>
        public bool IsClicked { get; set ; }
        public static readonly BindableProperty IsClickedProperty = BindableProperty.Create(propertyName: "IsClicked",
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
        public static readonly BindableProperty IsAnswerProperty = BindableProperty.Create(propertyName: "IsAnswer",
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
        public MyLabel()
        {

        }
        public void ShowColor()
        {
            if (IsClicked && !IsAnswer)
            {
                BackgroundColor = Color.Red;
                return;
            }
            if (IsAnswer) BackgroundColor = Color.Green;
        }
    }
}
