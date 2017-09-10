using DiabetesApp.Models;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnalyticsPage : ContentPage {

        FirebaseAuthLink auth;
        private const string FirebaseURL = "https://diabetesarp.firebaseio.com/";
        private ObservableCollection<LogbookListItem> logs;

        //Constructor
        public AnalyticsPage (FirebaseAuthLink auth) {
            InitializeComponent();
            logs = new ObservableCollection<LogbookListItem>();
            this.auth = auth;
            weekButton.IsEnabled = false;
        }

        private void refreshGraph() {
            //Get the week
            int days = DateTime.Now.DayOfWeek - DayOfWeek.Monday;
            DateTime startOfWeek = DateTime.Now.AddDays(-days);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            //Get the month
            DateTime startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            //Get the year
            DateTime startOfYear = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime endOfYear = startOfYear.AddYears(1).AddDays(-1);

            //Get the date range
            DateTime startOfRange = DateTime.Parse(startDate.Date.ToString("yyyy-MM-dd") + " 00:00:00");
            DateTime endOfRange = DateTime.Parse(endDate.Date.ToString("yyyy-MM-dd") + " 23:59:59");

            BSLGraph b;

            if(weekButton.IsEnabled == false) {
                b = new BSLGraph(startOfWeek, endOfWeek, logs);
            } else if(monthButton.IsEnabled == false) {
                b = new BSLGraph(startOfMonth, endOfMonth, logs);
            } else if(yearButton.IsEnabled == false) {
                b = new BSLGraph(startOfYear, endOfYear, logs);
            } else {
                b = new BSLGraph(startOfRange, endOfRange, logs);
            }

            this.BindingContext = b;
        }

        //When the page is navigated back to 
        protected override async void OnAppearing() {
            await updateEntries();
            refreshGraph();
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
            } catch {
                await DisplayAlert("Error updating entries", "Unable to update log entries list at this time", "OK");
            }
        }

        private void onClick_weekButton(object sender, EventArgs e) {
            enableButtons();
            weekButton.IsEnabled = false;
            refreshGraph();
        }

        private void onClick_monthButton(object sender, EventArgs e) {
            enableButtons();
            monthButton.IsEnabled = false;
            refreshGraph();
        }

        private void onClick_yearButton(object sender, EventArgs e) {
            enableButtons();
            yearButton.IsEnabled = false;
            refreshGraph();
        }

        private async void onClick_dateRangeButton(object sender, EventArgs e) {
            DateTime start = DateTime.Parse(startDate.Date.ToString("yyyy-MM-dd") + " 00:00:00");
            DateTime end = DateTime.Parse(endDate.Date.ToString("yyyy-MM-dd") + " 23:59:59");

            if(start.CompareTo(end) > 0) {
                await DisplayAlert("Error updating graph", "Invalid date range: Start Date must be before End Date", "OK");
                return;
            }

            enableButtons();
            dateRangeButton.IsEnabled = false;
            refreshGraph();
        }

        public void disableButtons() {
            weekButton.IsEnabled = false;
            monthButton.IsEnabled = false;
            yearButton.IsEnabled = false;
            dateRangeButton.IsEnabled = false;
        }

        public void enableButtons() {
            weekButton.IsEnabled = true;
            monthButton.IsEnabled = true;
            yearButton.IsEnabled = true;
            dateRangeButton.IsEnabled = true;
        }
    }
}