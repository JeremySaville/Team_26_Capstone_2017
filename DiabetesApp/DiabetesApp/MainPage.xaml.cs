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

        private const string FirebaseURL = "https://testapp-18ee0.firebaseio.com/";

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

            status.Text = "Items Retrieved";

            foreach(var item in items){
                if (item.Object.name.Equals(uName)){
                    loggedIn = item.Object.password.Equals(pWord);
                }
            }

            if (loggedIn){
                status.Text = "Success";
                var nextPage = new HomePage(uName, bDate);
                await Navigation.PushAsync(nextPage);
            }
            else{
                status.Text = "Failure";
            }
        }
    }
}
