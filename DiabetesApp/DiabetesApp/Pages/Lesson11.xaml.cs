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
    public partial class Lesson11 : ContentPage
    {
        FirebaseAuthLink auth;

        public Lesson11(FirebaseAuthLink auth)
        {
            InitializeComponent();
            this.auth = auth;
        }

        void onClick_Quiz11(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Quiz11(auth));
        }

        void onClick_Back(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
