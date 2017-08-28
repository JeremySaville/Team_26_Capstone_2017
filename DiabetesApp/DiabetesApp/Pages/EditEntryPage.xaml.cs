using DiabetesApp.DataTypes;
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
	public partial class EditEntryPage : ContentPage {
        private FirebaseAuthLink auth;
        private LogbookListItem log;
        private const string FirebaseURL = "https://diabetesarp.firebaseio.com/";

        public EditEntryPage (FirebaseAuthLink auth, LogbookListItem log) {
            this.log = log;
            this.auth = auth;
			InitializeComponent ();

            //// Set all fields to the values already in the entry
            //Date and time
            entryDate.Date = log.entryTime;
            TimeSpan eTime = new TimeSpan(log.entryTime.Hour, log.entryTime.Minute, log.entryTime.Second);
            entryTime.Time = eTime;

            //Details
            BGEntry.Text = log.BG.ToString();
            BIEntry.Text = log.BI.ToString();
            CarbCorrectEntry.Text = log.carbCorrect.ToString();
            CarbEntry.Text = log.carbEx.ToString();
            QACorrectEntry.Text = log.qaCorrect.ToString();
            QAEntry.Text = log.QA.ToString();

            //Optional comments
            if (!log.comments.Equals("n/a")) {
                commentsEntry.Text = log.comments;
            }
            if (!log.mood.Equals("n/a")) {
                moodEntry.Text = log.mood;
            }
		}

        async void onClick_saveEntry(object sender, EventArgs e) {
            disableButtons();
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
            if (moodEntry.Text == "" || moodEntry.Text == null) {
                newLog.mood = "n/a";
            }
            if (commentsEntry.Text == "" || commentsEntry.Text == null) {
                newLog.comments = "n/a";
            }
            newLog.updateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //----------Post new entry to the Database ----------
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
                        if (item.Key == entryDateTime && !entryDateTime.Equals(log.entryTime.ToString("yyyy-MM-dd HH:mm:ss"))) {
                            await DisplayAlert("Error", "Logbook entry for this time already posted", "OK");
                            return;
                        }
                    }
                } catch {
                    //Log does not exist, continue
                }

                //Delete old log entry
                await firebase
                    .Child("logbooks")
                    .Child(auth.User.LocalId)
                    .Child(log.entryTime.ToString("yyyy-MM-dd HH:mm:ss"))
                    .WithAuth(auth.FirebaseToken)
                    .DeleteAsync();

                //Post the entry to the database
                await firebase
                    .Child("logbooks")
                    .Child(auth.User.LocalId)
                    .Child(entryDateTime)
                    .WithAuth(auth.FirebaseToken)
                    .PutAsync(newLog);
                await DisplayAlert("Upload complete", "Finished updating log", "OK");
                await Navigation.PopModalAsync();

            } else {
                //Unable to post log, error message
                await DisplayAlert("Unable to create Logbook Entry", errors, "OK");
            }
            enableButtons();
        }

        void onClick_cancelEntry(object sender, EventArgs e) {
            disableButtons();
            Navigation.PopModalAsync();
            Navigation.PushModalAsync(new ViewEntryPage(auth, log));
        }

        void disableButtons() {
            cancelButton.IsEnabled = false;
            saveButton.IsEnabled = false;
        }

        void enableButtons() {
            cancelButton.IsEnabled = true;
            saveButton.IsEnabled = true;
        }
	}
}