using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AndroidTests
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewMainPage : ContentPage
    {
        public NewMainPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Открываю настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Sets sets = new Sets();
            Navigation.PushAsync(sets);
        }
    }
}