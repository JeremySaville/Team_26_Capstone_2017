using System;
using Firebase.Xamarin.Auth;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lesson10 : ContentPage
    {
        FirebaseAuthLink auth;

        public Lesson10(FirebaseAuthLink auth)
        {
            InitializeComponent();
            this.auth = auth;
        }

        void onClick_Quiz10(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Quiz10(auth));
        }

        void onClick_Back(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
