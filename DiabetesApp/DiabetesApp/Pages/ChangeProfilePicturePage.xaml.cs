using DiabetesApp.DataTypes;
using DiabetesApp.Models;
using Firebase.Xamarin.Auth;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp.Pages {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChangeProfilePicturePage : ContentPage {
        GameStats gStats;
        FirebaseAuthLink auth;
        ObservableCollection<ProfileListItem> profilePics;

        //Constructor
        public ChangeProfilePicturePage(FirebaseAuthLink auth, GameStats gStats) {
            InitializeComponent();
            this.gStats = gStats;
            this.auth = auth;
            profilePics = new ObservableCollection<ProfileListItem>();
            updateListView();
        }

        //Profile Item Tapped
        async void onClick_profileItem(object sender, ItemTappedEventArgs e) {
            if (e.Item == null) {
                return;
            }
            ProfileListItem selection = (ProfileListItem)(((ListView)sender).SelectedItem);
            ((ListView)sender).SelectedItem = null;

            if (selection.details.Equals("Unlocked")) {
                gStats.currentProfilePic = selection.id;
                GamificationTools.updateGStatsDB(auth, gStats);
                await DisplayAlert("Success", selection.name + " set as profile picture", "OK");
                await Navigation.PopModalAsync();
            } else if (selection.details.Equals("Unlock for 500 coins")) {
                if (gStats.coins >= 500) {
                    bool buy = await DisplayAlert("Confirm unlock", "Spend 500 coins to unlock " + selection.name + "?", "Yes", "Cancel");
                    if (buy) {
                        gStats.coins -= 500;
                        gStats.profilePictures += " " + selection.id;
                        GamificationTools.updateGStatsDB(auth, gStats);
                        await DisplayAlert("Success", selection.name + " unlocked", "OK");
                        updateListView();
                    }
                } else {
                    await DisplayAlert("Insufficient Funds", "Not enough coins to unlock this item", "OK");
                }
            }
        }

        //Update to show all the profile pictures
        private void updateListView() {
            string[] allProfilePics = GamificationTools.profileImageList;
            string[] gainedProfilePics = gStats.profilePictures.Split(' ');
            profilePics.Clear();

            foreach (string p in allProfilePics) {
                ProfileListItem prof = new ProfileListItem();
                prof.id = p;
                prof.name = getProfilePicName(p);
                prof.imageLink = p + ".png";
                if (p.Equals(gStats.currentProfilePic)) {
                    prof.details = "Currently Using";
                } else if (gainedProfilePics.Contains(p)) {
                    prof.details = "Unlocked";
                } else {
                    prof.details = "Unlock for 500 coins";
                }
                profilePics.Add(prof);
            }
            profileList.ItemsSource = null;
            profileList.ItemsSource = profilePics;
        }

        //extract the name of the profile picture from the string
        private string getProfilePicName(string profilePicID) {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(profilePicID.Substring(4).Replace("_", " "));
        }

        //cancel the change of profile picture page
        public async void onClick_cancelChange(object sender, EventArgs e) {
            cancel.IsEnabled = false;
            await Navigation.PopModalAsync();
        }
    }
}