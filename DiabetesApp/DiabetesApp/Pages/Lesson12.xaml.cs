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
    public partial class Lesson12 : ContentPage
    {
        FirebaseAuthLink auth;
        bool gamified;

        public Lesson12(FirebaseAuthLink auth, bool gamified)
        {
            InitializeComponent();
            this.auth = auth;
            this.gamified = gamified;
        }

        void onClick_Quiz12(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Quiz12(auth, gamified));
        }

        void onClick_Back(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
