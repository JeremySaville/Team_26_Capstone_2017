using App1.DataTypes;
using Firebase.Database;
using Firebase.Xamarin.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1 {
    public partial class MainPage : ContentPage {

        private const string FirebaseURL = "https://diabetesarp.firebaseio.com/";

        public MainPage() {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void onClick_loginButton(object sender, EventArgs e) {
            String uName = usernameEntry.Text;
            String bDate = birthDate.Date.ToLongDateString();
            String pWord = passwordEntry.Text;
            bool loggedIn = false;

            status.Text = "Processing";

            var firebase = new FirebaseClient(FirebaseURL);

            var items = await firebase
                .Child("users")
                .OnceAsync<User>();

            foreach(var item in items){
                status.Text = item.Object.name;
                if (item.Object.name.ToLower().Equals(uName.ToLower())){
                    loggedIn = item.Object.password.Equals(pWord);
                }
            }

            if (loggedIn){
                status.Text = "Success";
                var nextPage = new HomePage(uName);
                await Navigation.PushAsync(nextPage);
            }
            else{
                status.Text = "Failure";
            }
        }
    }
}
