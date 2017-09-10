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
        private ObservableCollection<LogbookListItem> displayedLogs;

        //Constructor for the logbook entries page
        public LogbookPage (FirebaseAuthLink auth)
		{
			InitializeComponent ();
            this.auth = auth;
            NavigationPage.SetHasNavigationBar(this, false);
            logs = new ObservableCollection<LogbookListItem>();
            displayedLogs = new ObservableCollection<LogbookListItem>();
		}

        //When the page is navigated back to 
        protected override async void OnAppearing() {
            enableButtons();
            logsList.IsRefreshing = true;
            await updateEntries();
            logsList.IsRefreshing = false;
        }

        //Handle click on create entry button
        void onClick_createEntry(object sender, EventArgs e) {
            disableButtons();
            Navigation.PushModalAsync(new Pages.EntryPage(auth));
        }
        
        //Handle click on list item
        void onClick_logEntryListItem(object sender, ItemTappedEventArgs e) {
            disableButtons();
            if(e.Item == null) {
                return;
            }
            LogbookListItem selection = (LogbookListItem)(((ListView)sender).SelectedItem);
            ((ListView)sender).SelectedItem = null;

            Navigation.PushModalAsync(new Pages.ViewEntryPage(auth, selection));
        }
        
        //List is pulled down to refresh
        async void onRefresh(object sender, EventArgs e) {
            await updateEntries();
            logsList.IsRefreshing = false;
        }

        //Method to update the entries list
        async Task updateEntries() {
            try {
                var firebase = new FirebaseClient(FirebaseURL);
                var items = await firebase
                    .Child("logbooks")
                    .Child(auth.User.LocalId)
                    .OrderByKey()
                    .WithAuth(auth.FirebaseToken)
                    .OnceAsync<DataTypes.LogbookEntry>();

                logs.Clear();
                foreach (var item in items) {
                    LogbookListItem l = new LogbookListItem();
                    //Create strings for the listview
                    l.title = DateTime.Parse(item.Key).ToString("dddd d MMMM yyyy, h:mm tt");
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
                reverseLogsList();
                logsList.ItemsSource = null;
                logsList.ItemsSource = logs;
            } catch {
                await DisplayAlert("Error updating entries", "Unable to update log entries list at this time", "OK");
            }
        }

        private void reverseLogsList() {
            ObservableCollection<LogbookListItem> rLogs = new ObservableCollection<LogbookListItem>();
            for(int i=logs.Count-1; i>=0; i--) {
                rLogs.Add(logs.ElementAt(i));
            }
            logs = rLogs;
        }

        private void onClick_searchEntries(object sender, EventArgs e) {
            disableButtons();
            logsList.IsRefreshing = true;
            DateTime start = DateTime.Parse(startDate.Date.ToString("yyyy-MM-dd") + " 00:00:00");
            DateTime end = DateTime.Parse(endDate.Date.ToString("yyyy-MM-dd") + " 23:59:59");
            displayedLogs.Clear();

            foreach(LogbookListItem l in logs){
                if (l.entryTime.CompareTo(start) >= 0 && l.entryTime.CompareTo(end) <= 0)
                    displayedLogs.Add(l);
            }

            logsList.ItemsSource = null;
            logsList.ItemsSource = displayedLogs;
            logsList.IsRefreshing = false;
            enableButtons();
        }

        private void enableButtons() {
            createButton.IsEnabled = true;
            searchButton.IsEnabled = true;
            
        }

        private void disableButtons() {
            createButton.IsEnabled = false;
            searchButton.IsEnabled = false;
        }
    }
}