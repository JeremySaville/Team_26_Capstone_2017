using Firebase.Xamarin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiabetesApp.DataTypes;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;

namespace DiabetesApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EntryPage : ContentPage
	{
        FirebaseAuthLink auth;
        private const string FirebaseURL = "https://diabetesarp.firebaseio.com/";

        public EntryPage (FirebaseAuthLink auth)
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
            this.auth = auth;
		}

        async void onClick_submitEntry(object sender, EventArgs e) {
            bool errorFound = false;
            LogbookEntry newLog = new LogbookEntry();

            //----------Error Messages----------
            string qaCorrectErrorMessage = "Please enter a number for Quick Acting Insulin Correction";
            string carbCorrectErrorMessage = "Please enter a number for Carb Exchange Correction";
            string BGErrorMessage = "Please enter a number for Blood Glucose";
            string BIErrorMessage = "Please enter a number for Background Insulin";
            string carbExErrorMessage = "Please enter a whole number for Carb Exchange";
            string QAErrorMessage = "Please enter a number for Quick Acting Insulin";
            
            string entryDateTime = entryDate.Date.ToString("yyyy-MM-dd") + " " + entryTime.Time.ToString();
            string errors = "\n\n";

            //----------Corrections----------
            try {
                newLog.qaCorrect = float.Parse(QACorrectEntry.Text);
            } catch {
                errors += qaCorrectErrorMessage + "\n\n";
                errorFound = true;
            }
            try {
                newLog.carbCorrect = float.Parse(CarbCorrectEntry.Text);
            } catch {
                errors += carbCorrectErrorMessage + "\n\n";
                errorFound = true;
            }

            //----------Entry Details----------
            try {
                newLog.BG = float.Parse(BGEntry.Text);
            } catch {
                errors += BGErrorMessage + "\n\n";
                errorFound = true;
            }
            try {
                newLog.BI = float.Parse(BIEntry.Text);
            } catch {
                errors += BIErrorMessage + "\n\n";
                errorFound = true;
            }
            try {
                newLog.carbEx = int.Parse(CarbEntry.Text);
            } catch {
                errors += carbExErrorMessage + "\n\n";
                errorFound = true;
            }
            try {
                newLog.QA = float.Parse(QAEntry.Text);
            } catch {
                errors += QAErrorMessage + "\n\n";
                errorFound = true;
            }
            
            //----------Comments and update time----------
            newLog.mood = moodEntry.Text;
            newLog.comments = commentsEntry.Text;
            newLog.updateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //----------Post entry to the Database ----------
            if (!errorFound) {
                var firebase = new FirebaseClient(FirebaseURL);
                //Check that a duplicate entry does not already exist
                try {
                    var items = await firebase
                        .Child("logbooks")
                        .Child(auth.User.LocalId)
                        .WithAuth(auth.FirebaseToken)
                        .OnceAsync<LogbookEntry>();
                    foreach (var item in items) {
                        if (item.Key == entryDateTime) {
                            await DisplayAlert("Error", "Logbook entry for this time already posted", "OK");
                            return;
                        }
                    }
                } catch {
                    //Log does not exist, continue
                }

                //Post the entry to the database
                await firebase
                    .Child("logbooks")
                    .Child(auth.User.LocalId)
                    .Child(entryDateTime)
                    .WithAuth(auth.FirebaseToken)
                    .PutAsync(newLog);
                await DisplayAlert("Upload complete", "Finished posting log", "OK");
                await Navigation.PopModalAsync();

            } else {
                //Unable to post log, error message
                await DisplayAlert("Unable to create Logbook Entry", errors, "OK");
            }

        }

        void onClick_cancelEntry(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }
    }
}