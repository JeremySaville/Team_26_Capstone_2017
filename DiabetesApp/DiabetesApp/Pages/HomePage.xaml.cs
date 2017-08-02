using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp{

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage {

        private const string FirebaseURL = "https://diabetesarp.firebaseio.com/";
        public HomePage (FirebaseAuthLink auth){

			InitializeComponent ();

        }

        private async void postItem(FirebaseAuthLink auth) {
            var firebase = new FirebaseClient(FirebaseURL);

            var u = new DataTypes.User();
            u.birthday = "date";
            u.email = "email";
            u.gamified = 0;
            u.phone = "0467355221";

            await firebase
                .Child("users")
                .Child(auth.User.LocalId)
                .WithAuth(auth.FirebaseToken)
                .PutAsync(u);
        }
    }
}