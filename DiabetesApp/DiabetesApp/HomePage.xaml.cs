using App1.DataTypes;
using Firebase.Xamarin.Database;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage{

        String username;
        public PlotModel BSLModel { get; set; }

        private const string FirebaseURL = "https://diabetesarp.firebaseio.com/";

        public HomePage (String username){
			InitializeComponent ();
            this.username = username;
            welcomeLabel.Text = $"Hello {username}";

            updateBirthday();

            NavigationPage.SetHasNavigationBar(this, true);

            var viewmodel = new GraphViewModel();
            this.BindingContext = viewmodel;
        }

        private async void updateBirthday() {
            var firebase = new FirebaseClient(FirebaseURL);

            var items = await firebase
                .Child("users")
                .OnceAsync<User>();

            foreach (var item in items) {
                if (item.Object.name.ToLower().Equals(username.ToLower())) {
                    bdaylabel.Text = $"Birthday: {item.Object.birthday}";
                }
            }
        }

    }
}