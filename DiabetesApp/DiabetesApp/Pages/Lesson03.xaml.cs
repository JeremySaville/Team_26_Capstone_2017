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
    public partial class Lesson03 : ContentPage
    {
        FirebaseAuthLink auth;
        bool gamified;

        public Lesson03(FirebaseAuthLink auth, bool gamified)
        {
            InitializeComponent();
            this.auth = auth;
            this.gamified = gamified;
        }

        void onClick_Quiz03(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Quiz03(auth, gamified));
        }

        void onClick_Back(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}