using Firebase.Xamarin.Database;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database.Query;
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
    public partial class LoginPage : ContentPage {

        public LoginPage() {
            InitializeComponent();
        }

        async void onClick_loginButton(object sender, EventArgs e) {
            string emailEntered = emailEntry.Text;
            string birthdayEntered = birthdayEntry.Date.ToString("yyyy-MM-dd");

            if (emailEntered == null || emailEntered == "") {
                await DisplayAlert("Login Failed", "Must enter your email", "OK");
                return;
            }

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDMHZSuyi3F5RqkhDBJnTOSNxwDaxzRSVQ"));
            
            try {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(emailEntered, birthdayEntered);
                App.Current.MainPage = new TabbedContent(auth);
            } catch (Exception ex) {
                await DisplayAlert("Login Failed", "Unable to login at this time", "OK");
            }
        }
    }
}