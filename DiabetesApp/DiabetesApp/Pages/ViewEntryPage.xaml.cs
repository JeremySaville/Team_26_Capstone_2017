using DiabetesApp.Models;
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

namespace DiabetesApp.Pages {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewEntryPage : ContentPage {
        private const string FirebaseURL = "https://diabetesarp.firebaseio.com/";
        LogbookListItem log;
        FirebaseAuthLink auth;

		public ViewEntryPage (FirebaseAuthLink auth, LogbookListItem log)
		{
			InitializeComponent ();
            this.auth = auth;
            this.log = log;
            this.BindingContext = this.log;
		}

        void onClick_editEntry(object sender, EventArgs e) {
            
        }

        async void onClick_deleteEntry(object sender, EventArgs e) {
            try {
                FirebaseClient firebase = new FirebaseClient(FirebaseURL);

                bool delete = await DisplayAlert("Delete Log?", "Are you sure you want to delete this log entry?", "Yes", "Cancel");
                if (!delete) return;

                await firebase
                    .Child("logbooks")
                    .Child(auth.User.LocalId)
                    .Child(log.entryTime.ToString("yyyy-MM-dd HH:mm:ss"))
                    .WithAuth(auth.FirebaseToken)
                    .DeleteAsync();

                await DisplayAlert("Done", "Log Entry Deleted", "OK");
                await Navigation.PopModalAsync();
            } catch {
                await DisplayAlert("Error", "Unable to delete log entry", "OK");
            }

        }

        void onClick_backButton(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }
    }
}