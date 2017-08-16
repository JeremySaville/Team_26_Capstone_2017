using Firebase.Xamarin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiabetesApp.DataTypes;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EntryPage : ContentPage
	{
        FirebaseAuthLink auth;

		public EntryPage (FirebaseAuthLink auth)
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
            this.auth = auth;
		}

        void onClick_submitEntry(object sender, EventArgs e) {
            bool errorFound = false;
            LogbookEntry newLog = new LogbookEntry();
            Color errorColour = Color.FromHex(Application.Current.Resources["errorTextColour"].ToString());

            //----------Error Messages----------
            Label qaCorrectErrorMessage = new Label() { Text = "Please enter a number for Quick Acting Insulin Correction", TextColor = errorColour };
            Label carbCorrectErrorMessage = new Label() { Text = "Please enter a number for Carb Exchange Correction", TextColor = errorColour };
            Label BGErrorMessage = new Label() { Text = "Please enter a number for Blood Glucose", TextColor = errorColour };
            Label BIErrorMessage = new Label() { Text = "Please enter a number for Background Insulin", TextColor = errorColour };
            Label carbExErrorMessage = new Label() { Text = "Please enter a whole number for Carb Exchange", TextColor = errorColour };
            Label QAErrorMessage = new Label() { Text = "Please enter a number for Quick Acting Insulin", TextColor = errorColour };


            string entryDateTime = entryDate.Date.ToString("yyyy-MM-dd") + " " + entryTime.Time.ToString();

            //----------Corrections----------
            try {
                newLog.qaCorrect = float.Parse(QACorrectEntry.Text);
                qaCorrectError.Children.Remove(qaCorrectErrorMessage);
            } catch {
                qaCorrectError.Children.Add(qaCorrectErrorMessage);
                errorFound = true;
            }
            try {
                newLog.carbCorrect = float.Parse(CarbCorrectEntry.Text);
            } catch {
                //Error message goes here
                errorFound = true;
            }

            //----------Entry Details----------
            try {
                newLog.BG = float.Parse(BGEntry.Text);
            } catch {
                //Error message added in here.
                errorFound = true;
            }
            try {
                newLog.BI = float.Parse(BIEntry.Text);
            } catch {
                //Error message goes here
                errorFound = true;
            }
            try {
                newLog.carbEx = int.Parse(CarbEntry.Text);
            } catch {
                //Error message goes here
                errorFound = true;
            }
            try {
                newLog.QA = float.Parse(QAEntry.Text);
            } catch {
                //Error message goes here
                errorFound = true;
            }

            //----------Comments----------
            newLog.mood = moodEntry.Text;
            newLog.comments = commentsEntry.Text;

            //----------Post entry to the Database ----------
            if (!errorFound) {
                Navigation.PopModalAsync();
            }

        }

        void onClick_cancelEntry(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }
    }
}