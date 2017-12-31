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
    public partial class Lesson01 : ContentPage
    {
        FirebaseAuthLink auth;
        bool gamified;

        public Lesson01(FirebaseAuthLink auth, bool gamified)
        {
            InitializeComponent();
            this.auth = auth;
            this.gamified = gamified;
        }

        void onClick_Quiz01(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Quiz01(auth, gamified));
        }

        void onClick_Back(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
