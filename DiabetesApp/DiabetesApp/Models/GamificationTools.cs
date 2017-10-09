using DiabetesApp.DataTypes;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
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
            if (newXP > getExpToNextLevel(gStats.level, gStats.xp)) {
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
            if (allBadges.b1_starting_out) badgeCodes.Add("b1_starting_out");
            if (allBadges.b2_ramping_up) badgeCodes.Add("b2_ramping_up");
            if (allBadges.b3_getting_there) badgeCodes.Add("b3_getting_there");
            if (allBadges.b4_skilled) badgeCodes.Add("b4_skilled");
            if (allBadges.b5_rugged) badgeCodes.Add("b5_rugged");
            if (allBadges.b6_hardened) badgeCodes.Add("b6_hardened");
            if (allBadges.b7_experienced) badgeCodes.Add("b7_experienced");

            return badgeCodes;
        }

        //Add a new list of badges for a user to the DB and return it
        public static async Task<Badges> newBadgeList(FirebaseAuthLink auth) {
            Badges newBadges = new Badges();
            newBadges.b1_starting_out = false;
            newBadges.b2_ramping_up = false;
            newBadges.b3_getting_there = false;
            newBadges.b4_skilled = false;
            newBadges.b5_rugged = false;
            newBadges.b6_hardened = false;
            newBadges.b7_experienced = false;

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
    }
}
