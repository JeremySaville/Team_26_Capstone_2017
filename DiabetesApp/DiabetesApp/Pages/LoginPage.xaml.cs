using Firebase.Xamarin.Database;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database.Query;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DiabetesApp.DataTypes;
using DiabetesApp.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Pages;
using System.Threading.Tasks;

namespace DiabetesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage {

        private const string FirebaseURL = "https://diabetesarp.firebaseio.com/";
        GameStats gStats;

        //Constructor
        public LoginPage() {
            InitializeComponent();
        }

        //Handles a login request
        async void onClick_loginButton(object sender, EventArgs e) {
            loginButton.IsEnabled = false;

            string usernameEntered = usernameEntry.Text;
            string birthdayEntered = birthdayEntry.Date.ToString("yyyy-MM-dd");

            if (usernameEntered == null || usernameEntered == "") {
                await DisplayAlert("Login Failed", "Must enter your username", "OK");
                return;
            }

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDMHZSuyi3F5RqkhDBJnTOSNxwDaxzRSVQ"));

            //Attempt to log in
            FirebaseAuthLink auth = null;
            try {
                auth = await authProvider.SignInWithEmailAndPasswordAsync(usernameEntered + "@diabetesarp.com", birthdayEntered);
            } catch {
                //login failed
                await DisplayAlert("Login Failed", "Unable to login at this time", "OK");
                loginButton.IsEnabled = true;
                return;
            }
            //Check whether logged in user is gamified
            var firebase = new FirebaseClient(FirebaseURL);
            bool gamified = false;

            try {
                var item = await firebase
                    .Child("users")
                    .Child(auth.User.LocalId)
                    .Child("gamified")
                    .WithAuth(auth.FirebaseToken)
                    .OnceSingleAsync<int>();

                gamified = item == 2;

            } catch { 
                //user is not gamified
            }

            //if gamified, check for num logins, increment if necessary
            if (gamified) {
                gStats = await GamificationTools.getGStats(auth);
                while (gStats == null) ;
                bool levelUp = false;
                DateTime loginTime = DateTime.Now;
                DateTime lastLogin = DateTime.Parse(gStats.lastLogin);
                DateTime lastLoginNextDay = lastLogin.AddDays(1);
                if (loginTime.Year == lastLogin.Year && loginTime.Month == lastLogin.Month && loginTime.Day == lastLogin.Day) {
                    //Nothing happens already logged in today
                } else if (loginTime.Year == lastLoginNextDay.Year && loginTime.Month == lastLoginNextDay.Month && loginTime.Day == lastLoginNextDay.Day) {
                    gStats.numLogins += 1;
                    gStats.lastLogin = loginTime.ToString("yyyy-MM-dd HH:mm:ss");
                    if(gStats.numLogins % 7 == 0) {
                        //Seven consecutive logins, bonus XP
                        levelUp = GamificationTools.levelUp(ref gStats, GamificationTools.loginBonusXp);
                        await DisplayAlert("Login Bonus", "Received " + GamificationTools.loginBonusXp + "xp for seven consecutive logins", "OK");
                    } else {
                        //Normal XP
                        levelUp = GamificationTools.levelUp(ref gStats, GamificationTools.loginXP);
                        await DisplayAlert("Login Reward", "Received " + GamificationTools.loginXP + "xp for logging in", "OK");
                    }
                } else {
                    gStats.numLogins = 1;
                    gStats.lastLogin = loginTime.ToString("yyyy-MM-dd HH:mm:ss");
                    levelUp = GamificationTools.levelUp(ref gStats, GamificationTools.loginXP);
                    await DisplayAlert("Login Reward", "Received " + GamificationTools.loginXP + "xp for logging in", "OK");
                }

                //Check whether a badge should be gained for logging in
                string loginBadge = GamificationTools.getLoginBadge(gStats);
                if (!loginBadge.Equals("")) {
                    gStats.badges += " " + loginBadge;
                    await Navigation.PushModalAsync(new Pages.NewBadgePage(loginBadge));
                    string loginCoinBadge = GamificationTools.addCoinsFromBadge(ref gStats, loginBadge);
                    if (!loginCoinBadge.Equals(""))
                        await Navigation.PushModalAsync(new Pages.NewBadgePage(loginCoinBadge));
                }

                //Level up and gain any associated badges
                if (levelUp) {
                    Random r = new Random();
                    int levelCoins = r.Next(100, 250);
                    string levelCoinBadge = GamificationTools.AddCoins(ref gStats, levelCoins);
                    if (!levelCoinBadge.Equals(""))
                        await Navigation.PushModalAsync(new Pages.NewBadgePage(levelCoinBadge));
                    await DisplayAlert("Levelled Up!",
                        "Advanced to Level " + gStats.level.ToString() + "\n" + GamificationTools.getExpToNextLevel(gStats.level, gStats.xp).ToString() + " Experience to the next level\n" +
                        levelCoins + " coins received",
                        "OK");
                    string levelBadge = BadgeList.gotLevelBadge(gStats.level);

                    if (!levelBadge.Equals("")) {
                        gStats.badges += " " + levelBadge;
                        await Navigation.PushModalAsync(new Pages.NewBadgePage(levelBadge));
                        string coinBadge = GamificationTools.addCoinsFromBadge(ref gStats, levelBadge);
                        if (!coinBadge.Equals(""))
                            await Navigation.PushModalAsync(new Pages.NewBadgePage(coinBadge));
                    }
                }
                await GamificationTools.updateGStatsDB(auth, gStats);
            }
            while (Navigation.ModalStack.Count > 0)
                await Task.Delay(100);
            Application.Current.MainPage = new TabbedContent(auth, gamified);
        }
    }
}