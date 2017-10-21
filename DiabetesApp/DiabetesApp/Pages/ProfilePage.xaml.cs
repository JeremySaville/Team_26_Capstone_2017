using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DiabetesApp.DataTypes;
using DiabetesApp.Models;
using System.Collections;
using System.Threading.Tasks;

namespace DiabetesApp {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage {

        private const string FirebaseURL = "https://diabetesarp.firebaseio.com/";
        bool gamified;
        FirebaseAuthLink auth;
        GameStats gStats;
        bool editButtonTapped;
        
        //Constructor
		public ProfilePage (FirebaseAuthLink auth, bool gamified) {
			InitializeComponent ();
            this.gamified = gamified;
            this.auth = auth;

            if (!gamified) {
                profileTable.Remove(gameStats);
                profileTable.Remove(badges);
                profileGrid.Children.Clear();
            }
		}

        //When the page is navigated back to 
        protected async override void OnAppearing() {
            updateStats();
            if (gamified) {
                await updateGamifiedStats();
                updateBadges();
            }
            editButtonTapped = false;
        }

        //update the table with number of logs made, etc.
        private async void updateStats() {
            DateTime latest = DateTime.MinValue;
            int count = 0;

            //Get the required information from the database
            var firebase = new FirebaseClient(FirebaseURL);
            try {
                var items = await firebase
                    .Child("logbooks")
                    .Child(auth.User.LocalId)
                    .WithAuth(auth.FirebaseToken)
                    .OnceAsync<LogbookEntry>();

                foreach (var item in items) {
                    DateTime newVal = DateTime.Parse(item.Key);
                    if (latest < newVal) {
                        latest = newVal;
                    }
                }

                count = items.Count;
            } catch {

            }

            //Update count
            nLogs.Detail = count.ToString();

            //Update latest entry
            if (count == 0)
                lastLogTime.Detail = "No Logs Found";
            else
                lastLogTime.Detail = latest.ToString("dddd d MMMM yyyy, h:mm tt");
        }

        //update the bits relevant to gamified users
        private async Task updateGamifiedStats() {
            gStats = await GamificationTools.getGStats(auth);
            updateGameStatsTable();
            updateProfileSection();
        }

        //Update the view with the relevant information gained
        private void updateGameStatsTable() {
            xp.Detail = gStats.xp.ToString();
            logins.Detail = gStats.numLogins.ToString();
        }

        //Update the badges in the 
        private void updateBadges() {
            badges.Clear();
            string[] badgeList = gStats.badges.Split(' ');
            foreach(string badge in badgeList) {
                if (!badge.Equals("none")) {
                    badges.Add(new ImageCell() {
                        ImageSource = BadgeList.getBadgeLink(badge),
                        Text = BadgeList.getBadgeName(badge),
                        Detail = BadgeList.getBadgeDescription(badge)
                    });
                }
            }
        }

        //Update the profile section with the relevant image and stats
        private void updateProfileSection() {
            int xpToLevel = GamificationTools.getLevelExp(gStats.level);
            int xpGainedInLevel = xpToLevel - GamificationTools.getExpToNextLevel(gStats.level, gStats.xp);
            level.Text = "Level " + gStats.level;
            coins.Text = "Coins: " + gStats.coins;
            profileImage.Source = gStats.currentProfilePic;
            profileXP.Text = "XP: " + xpGainedInLevel + "/" + xpToLevel;
            progress.Progress = (double)xpGainedInLevel / xpToLevel;
        }
        
        //Handle a click of the edit button
        public void onClick_editButton(object sender, EventArgs e) {
            if (!editButtonTapped) {
                editButtonTapped = true;
                Navigation.PushModalAsync(new Pages.ChangeProfilePicturePage(auth, gStats));
            }
        }

    }
}