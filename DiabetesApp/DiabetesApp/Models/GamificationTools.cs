using DiabetesApp.DataTypes;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Models {
    class GamificationTools {
        private const string FirebaseURL = "https://diabetesarp.firebaseio.com/";

        public const int loginXP = 10;
        public const int loginBonusXp = 50;
        public const int logEntryXP = 50;
        public const int logEntryDailyBonusXP = 100;
        public const int logEntryWeeklyBonusXP = 1000;

        public static readonly int[] xpForLevel = new int[] { 200, 300, 500, 700, 900, 1100, 1300, 1400, 1500 };
        public const int xpForLaterLevels = 1500;

        public static readonly string[] profileImageList = new string[] { "p01_default_profile", "p02_knight", "p03_princess", "p04_ninja", "p05_wizard",
                                                            "p06_ranger", "p07_monk", "p08_heart", "p09_cat", "p10_dog", "p11_cookie" };

        //Add stats for a new log entry
        public static void addLogEntryStats(ref GameStats gStats, DateTime entryTime) {
            DateTime today = DateTime.Now;
            DateTime yesterday = today.AddDays(-1);
            DateTime prevEntry = DateTime.Parse(gStats.lastEntry);
            DateTime fullDayTime = DateTime.Parse(gStats.lastFullDayEntry);

            //Check that the entry is today, and the same day as the previous entry
            if (entryTime.Year == today.Year && entryTime.Month == today.Month && entryTime.Day == today.Day && 
                entryTime.Year == prevEntry.Year && entryTime.Month == prevEntry.Month && entryTime.Day == prevEntry.Day) {
                if(entryTime.Subtract(prevEntry).TotalMinutes >= 30)
                    gStats.entriesToday += 1;
            } else {
                gStats.entriesToday = 1;
            }
            gStats.lastEntry = entryTime.ToString("yyyy-MM-dd HH:mm:ss");

            //Check that last full day was yesterday and new full day is today
            if (gStats.entriesToday == 4 && fullDayTime.Year == yesterday.Year && fullDayTime.Month == yesterday.Month && fullDayTime.Day == yesterday.Day
                && entryTime.Year == today.Year && entryTime.Month == today.Month && entryTime.Day == today.Day) {
                gStats.numFullDayEntries += 1;
                gStats.lastFullDayEntry = entryTime.ToString("yyyy-MM-dd HH:mm:ss");
            } else if(fullDayTime.Year == today.Year && fullDayTime.Month == today.Month && fullDayTime.Day == today.Day) {
                //full day already done today
            } else {
                if (gStats.entriesToday == 4) {
                    gStats.numFullDayEntries = 1;
                    gStats.lastFullDayEntry = entryTime.ToString("yyyy-MM-dd HH:mm:ss");
                } else {
                    gStats.numFullDayEntries = 0;
                }
            }
            if (gStats.entriesToday == 1) {
                gStats.dailyBonusReceived = false;
            }
        }
        
        //return whether a bonus should be received by the user
        public static string getEntryBonus(GameStats gStats) {
            string bonus = "none";
            //Check for daily bonus
            Random r = new Random();
            if(gStats.entriesToday >= 6 && !gStats.dailyBonusReceived) {
                bonus = "daily";
            } else if(gStats.entriesToday >= 4 && !gStats.dailyBonusReceived && r.Next(0,100) < 60) {
                bonus = "daily";
            }
            //Check for weekly bonus
            if(gStats.numFullDayEntries >= 7) {
                if (bonus.Equals("daily")) {
                    bonus = "both";
                } else {
                    bonus = "weekly";
                }
            }else if(gStats.numFullDayEntries >= 4 && r.Next(0,100) < 25) {
                if (bonus.Equals("daily")) {
                    bonus = "both";
                } else {
                    bonus = "weekly";
                }
            }
            return bonus;
        }

        //Check whether the user should level up and if so add a level
        //Also adds on any new xp gained, regardless of level up
        public static bool levelUp(ref GameStats gStats, int newXP) {
            bool levelled = false;
            if (newXP >= getExpToNextLevel(gStats.level, gStats.xp)) {
                gStats.level += 1;
                levelled = true;
            }
            gStats.xp += newXP;
            return levelled;
        }

        //update the database with the new value of gStats
        public static async void updateGStatsDB(FirebaseAuthLink auth, GameStats gStats) {
            var firebase = new FirebaseClient(FirebaseURL);
            await firebase
                .Child("gameStats")
                .Child(auth.User.LocalId)
                .WithAuth(auth.FirebaseToken)
                .PutAsync(gStats);
        }

        //init stats for current user (new firebase database entry)
        public static GameStats initStats(FirebaseAuthLink auth) {
            var firebase = new FirebaseClient(FirebaseURL);
            GameStats newStats = new GameStats();
            string minDate = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
            newStats.xp = 0;
            newStats.level = 1;
            newStats.coins = 0;
            newStats.numLogins = 1;
            newStats.lastLogin = minDate;
            newStats.entriesToday = 0;
            newStats.lastEntry = minDate;
            newStats.numFullDayEntries = 0;
            newStats.lastFullDayEntry = minDate;
            newStats.dailyBonusReceived = false;
            newStats.logEntriesMade = 0;
            newStats.currentProfilePic = "p01_default_profile";
            newStats.profilePictures = "p01_default_profile";

            updateGStatsDB(auth, newStats);
            return newStats;
        }

        //Helper method to calculate the experience required for the next level
        public static int getExpToNextLevel(int currentLevel, int currentExp) {
            int totalRequired = 0;
            if (currentLevel > xpForLevel.Length) {
                totalRequired = SumArray(xpForLevel) + (currentLevel - xpForLevel.Length) * xpForLaterLevels;
            } else {
                for (int i = 0; i < currentLevel; i++) {
                    totalRequired += xpForLevel[i];
                }
            }
            return totalRequired - currentExp;
        }

        //Return the experience in the current level
        public static int getLevelExp(int currentLevel) {
            if (currentLevel > xpForLevel.Length) {
                return xpForLaterLevels;
            } else {
                return xpForLevel[currentLevel - 1];
            }
        }

        //Helper method to add all the elements of a numeric array
        private static int SumArray(int[] array) {
            int sum = 0;
            foreach (int i in array) {
                sum += i;
            }
            return sum;
        }

        //Return the game stats of the specified user
        public static async Task<GameStats> getGStats(FirebaseAuthLink auth) {
            GameStats gStats = null;
            var firebase = new FirebaseClient(FirebaseURL);
            try {
                var item = await firebase
                    .Child("gameStats")
                    .Child(auth.User.LocalId)
                    .WithAuth(auth.FirebaseToken)
                    .OnceSingleAsync<GameStats>();
                gStats = item;
            } catch {
                gStats = initStats(auth);
            }
            return gStats;
        }

        //Return a list of badges for the specified user
        public static async Task<ArrayList> getBadges(FirebaseAuthLink auth) {
            var firebase = new FirebaseClient(FirebaseURL);
            Badges allBadges;
            ArrayList badgeCodes = new ArrayList();
            try {
                allBadges = await firebase
                    .Child("badges")
                    .Child(auth.User.LocalId)
                    .WithAuth(auth.FirebaseToken)
                    .OnceSingleAsync<Badges>();
            } catch {
                allBadges = await newBadgeList(auth);
            }
            if (allBadges.b01_starting_out) badgeCodes.Add("b01_starting_out");
            if (allBadges.b02_ramping_up) badgeCodes.Add("b02_ramping_up");
            if (allBadges.b03_getting_there) badgeCodes.Add("b03_getting_there");
            if (allBadges.b04_skilled) badgeCodes.Add("b04_skilled");
            if (allBadges.b05_rugged) badgeCodes.Add("b05_rugged");
            if (allBadges.b06_hardened) badgeCodes.Add("b06_hardened");
            if (allBadges.b07_experienced) badgeCodes.Add("b07_experienced");
            if (allBadges.b08_pen_and_paper) badgeCodes.Add("b08_pen_and_paper");
            if (allBadges.b09_scribe) badgeCodes.Add("b09_scribe");
            if (allBadges.b10_research_assistant) badgeCodes.Add("b10_research_assistant");
            if (allBadges.b11_researcher_intern) badgeCodes.Add("b11_researcher_intern");
            if (allBadges.b12_novice_researcher) badgeCodes.Add("b12_novice_researcher");
            if (allBadges.b13_apprentice_researcher) badgeCodes.Add("b13_apprentice_researcher");
            if (allBadges.b14_adept_researcher) badgeCodes.Add("b14_adept_researcher");
            if (allBadges.b15_professional_researcher) badgeCodes.Add("b15_professional_researcher");
            if (allBadges.b16_habitual) badgeCodes.Add("b16_habitual");
            if (allBadges.b17_enthusiastic) badgeCodes.Add("b17_enthusiastic");
            if (allBadges.b18_committed) badgeCodes.Add("b18_committed");
            if (allBadges.b19_dedicated) badgeCodes.Add("b19_dedicated");
            if (allBadges.b20_entrepreneur) badgeCodes.Add("b20_entrepreneur");
            if (allBadges.b21_money_man) badgeCodes.Add("b21_money_man");
            if (allBadges.b22_investor) badgeCodes.Add("b22_investor");
            if (allBadges.b23_banker) badgeCodes.Add("b23_banker");
            if (allBadges.b24_ceo) badgeCodes.Add("b24_ceo");


            return badgeCodes;
        }

        //Add a new list of badges for a user to the DB and return it
        public static async Task<Badges> newBadgeList(FirebaseAuthLink auth) {
            Badges newBadges = new Badges();
            newBadges.b01_starting_out = false;
            newBadges.b02_ramping_up = false;
            newBadges.b03_getting_there = false;
            newBadges.b04_skilled = false;
            newBadges.b05_rugged = false;
            newBadges.b06_hardened = false;
            newBadges.b07_experienced = false;
            newBadges.b08_pen_and_paper = false;
            newBadges.b09_scribe = false;
            newBadges.b10_research_assistant = false;
            newBadges.b11_researcher_intern = false;
            newBadges.b12_novice_researcher = false;
            newBadges.b13_apprentice_researcher = false;
            newBadges.b14_adept_researcher = false;
            newBadges.b15_professional_researcher = false;
            newBadges.b16_habitual = false;
            newBadges.b17_enthusiastic = false;
            newBadges.b18_committed = false;
            newBadges.b19_dedicated = false;
            newBadges.b20_entrepreneur = false;
            newBadges.b21_money_man = false;
            newBadges.b22_investor = false;
            newBadges.b23_banker = false;
            newBadges.b24_ceo = false;

            try {
                var firebase = new FirebaseClient(FirebaseURL);
                await firebase
                    .Child("badges")
                    .Child(auth.User.LocalId)
                    .WithAuth(auth.FirebaseToken)
                    .PutAsync(newBadges);
            } catch {

            }
            return newBadges;
        }

        //Add the badge specified to the users database
        public async static Task addBadge(string badgeKey, FirebaseAuthLink auth) {
            var firebase = new FirebaseClient(FirebaseURL);
            Badges allBadges;
            try {
                allBadges = await firebase
                    .Child("badges")
                    .Child(auth.User.LocalId)
                    .WithAuth(auth.FirebaseToken)
                    .OnceSingleAsync<Badges>();
            } catch {
                allBadges = await newBadgeList(auth);
            }

            if (badgeKey.Equals("b01_starting_out")) allBadges.b01_starting_out = true;
            if (badgeKey.Equals("b02_ramping_up")) allBadges.b02_ramping_up = true;
            if (badgeKey.Equals("b03_getting_there")) allBadges.b03_getting_there = true;
            if (badgeKey.Equals("b04_skilled")) allBadges.b04_skilled = true;
            if (badgeKey.Equals("b05_rugged")) allBadges.b05_rugged = true;
            if (badgeKey.Equals("b06_hardened")) allBadges.b06_hardened = true;
            if (badgeKey.Equals("b07_experienced")) allBadges.b07_experienced = true;
            if (badgeKey.Equals("b08_pen_and_paper")) allBadges.b08_pen_and_paper = true;
            if (badgeKey.Equals("b09_scribe")) allBadges.b09_scribe = true;
            if (badgeKey.Equals("b10_research_assistant")) allBadges.b10_research_assistant = true;
            if (badgeKey.Equals("b11_researcher_intern")) allBadges.b11_researcher_intern = true;
            if (badgeKey.Equals("b12_novice_researcher")) allBadges.b12_novice_researcher = true;
            if (badgeKey.Equals("b13_apprentice_researcher")) allBadges.b13_apprentice_researcher = true;
            if (badgeKey.Equals("b14_adept_researcher")) allBadges.b14_adept_researcher = true;
            if (badgeKey.Equals("b15_professional_researcher")) allBadges.b15_professional_researcher = true;
            if (badgeKey.Equals("b16_habitual")) allBadges.b16_habitual = true;
            if (badgeKey.Equals("b17_enthusiastic")) allBadges.b17_enthusiastic = true;
            if (badgeKey.Equals("b18_committed")) allBadges.b18_committed = true;
            if (badgeKey.Equals("b19_dedicated")) allBadges.b19_dedicated = true;
            if (badgeKey.Equals("b20_entrepreneur")) allBadges.b20_entrepreneur = true;
            if (badgeKey.Equals("b21_money_man")) allBadges.b21_money_man = true;
            if (badgeKey.Equals("b22_investor")) allBadges.b22_investor = true;
            if (badgeKey.Equals("b23_banker")) allBadges.b23_banker = true;
            if (badgeKey.Equals("b24_ceo")) allBadges.b24_ceo = true;

            try {
                await firebase
                    .Child("badges")
                    .Child(auth.User.LocalId)
                    .WithAuth(auth.FirebaseToken)
                    .PutAsync(allBadges);
            } catch {

            }
        }

        //Add the coins that should be added from the badge received
        public async static Task<GameStats> addCoinsFromBadge(GameStats gStats, string badgeKey, FirebaseAuthLink auth) {
            int coinsToAdd = BadgeList.getCoins(badgeKey);
            if(coinsToAdd >= BadgeList.coinsToNextBadge(gStats.coins) && BadgeList.coinsToNextBadge(gStats.coins) != -1) {
                string coinBadge = BadgeList.GetCoinBadge(gStats.coins);
                gStats.coins += coinsToAdd + BadgeList.getCoins(coinBadge);
                await addBadge(coinBadge, auth);
                await PopupNavigation.PushAsync(new Popups.BadgePopup(coinBadge));
            }
            gStats.coins += coinsToAdd;
            updateGStatsDB(auth, gStats);
            return gStats;
        }

        //Return whether the login badge should be received
        public async static Task<string> getLoginBadge(FirebaseAuthLink auth, GameStats gStats) {
            if(gStats.numLogins != 5 && gStats.numLogins != 10 && gStats.numLogins != 20 && gStats.numLogins != 30) {
                return "";
            }
            var firebase = new FirebaseClient(FirebaseURL);
            Badges allBadges;
            try {
                allBadges = await firebase
                    .Child("badges")
                    .Child(auth.User.LocalId)
                    .WithAuth(auth.FirebaseToken)
                    .OnceSingleAsync<Badges>();
            } catch {
                allBadges = await newBadgeList(auth);
            }
            if (gStats.numLogins == 5 && !allBadges.b16_habitual) return "b16_habitual";
            if (gStats.numLogins == 10 && !allBadges.b17_enthusiastic) return "b17_enthusiastic";
            if (gStats.numLogins == 20 && !allBadges.b18_committed) return "b18_committed";
            if (gStats.numLogins == 30 && !allBadges.b19_dedicated) return "b19_dedicated";

            return "";
        }
    }
}
