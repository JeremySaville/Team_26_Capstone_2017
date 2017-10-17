using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lesson01 : ContentPage
    {
        public Lesson01()
        {
            InitializeComponent ();
        }

        void btn_ClickedQuiz01(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Quiz01());
        }

    }
}
