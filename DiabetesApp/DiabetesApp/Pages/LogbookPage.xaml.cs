using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiabetesApp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace DiabetesApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogbookPage : ContentPage
	{
        FirebaseAuthLink auth;
        private const string FirebaseURL = "https://diabetesarp.firebaseio.com/";
        private ObservableCollection<LogbookListItem> logs;

        //Constructor for the logbook entries page
        public LogbookPage (FirebaseAuthLink auth)
		{
			InitializeComponent ();
            this.auth = auth;
            NavigationPage.SetHasNavigationBar(this, false);
            logs = new ObservableCollection<LogbookListItem>();
            updateEntries();
		}

        //Handle click on create entry button
        void onClick_createEntry(object sender, EventArgs e) {
            Navigation.PushModalAsync(new Pages.EntryPage(auth));
        }
        
        //Handle click on list item
        void onClick_logEntryListItem(object sender, ItemTappedEventArgs e) {
            if(e.Item == null) {
                return;
            }
            var selection = ((ListView)sender).SelectedItem;
            ((ListView)sender).SelectedItem = null;        

            //OPEN VIEW ENTRY PAGE HERE
        }
        
        void onRefresh(object sender, EventArgs e) {
            updateEntries();
            logsList.IsRefreshing = false;
        }

        //Method to update the entries list
        async void updateEntries() {
            try {
                var firebase = new FirebaseClient(FirebaseURL);
                var items = await firebase
                    .Child("logbooks")
                    .Child(auth.User.LocalId)
                    .WithAuth(auth.FirebaseToken)
                    .OnceAsync<DataTypes.LogbookEntry>();

                logs.Clear();
                foreach (var item in items) {
                    LogbookListItem l = new LogbookListItem();
                    //Create strings for the listview
                    l.title = DateTime.Parse(item.Key).ToString("dddd MMMM yyyy, h:mm tt");
                    l.description = "Blood Glucose: " + item.Object.BG.ToString() + "  Carb Exchange: " + item.Object.carbEx.ToString();
                    //Add in the rest of the log entry information
                    l.BG = item.Object.BG;
                    l.BI = item.Object.BI;
                    l.carbCorrect = item.Object.carbCorrect;
                    l.carbEx = item.Object.carbEx;
                    l.comments = item.Object.comments;
                    l.entryTime = DateTime.Parse(item.Key);
                    l.mood = item.Object.mood;
                    l.QA = item.Object.QA;
                    l.qaCorrect = item.Object.qaCorrect;
                    l.updateTime = DateTime.Parse(item.Object.updateTime);
                    //add to list
                    logs.Add(l);
                }
                logsList.ItemsSource = null;
                logsList.ItemsSource = logs;
            } catch {
                await DisplayAlert("Error updating entries", "Unable to update log entries list at this time", "OK");
            }
        }

    }
}