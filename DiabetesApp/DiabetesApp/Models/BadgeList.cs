using System;
using System.Globalization;

namespace DiabetesApp.Models {
    class BadgeList {

        private static string[] bDescriptions = { "Reached Level 2", "Reached Level 5", "Reached Level 10", "Reached Level 20", "Reached Level 30", "Reached Level 40", "Reached Level 50",
                                                "Made your first log entry!", "Made 10 log entries", "Made 20 log entries", "Made 50 log entries", "Made 100 log entries",
                                                "Made 200 log entries", "Made 500 log entries", "Made 1,000 log entries!", "Logged in 5 days in a row", "Logged in 10 days in a row",
                                                "Logged in 20 days in a row", "Logged in 30 days in a row", "Collected 500 Coins", "Collected 1,000 Coins", "Collected 2,000 Coins",
                                                "Collected 5,000 Coins", "Collected 10,000 Coins"};
        private static int[] bCoins = { 20, 50, 100, 200, 200, 200, 300, 10, 20, 50, 75, 100, 100, 150, 200, 10, 20, 30, 50, 10, 50, 100, 150, 200 };
        private static int[] coinsForBadge = { 500, 1000, 2000, 5000, 10000 };
        private static int[] entriesForBadge = { 1, 10, 20, 50, 100, 200, 500, 1000 };

        //Convert the string for the badge name to 
        public static String getBadgeName(String badgeKey) {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            if (badgeKey.Equals("b24_ceo")) {
                return "CEO";
            } else {
                return textInfo.ToTitleCase(badgeKey.Substring(4).Replace("_", " "));
            }
        }

        //Get the image link for the badge
        public static String getBadgeLink(String badgeKey) {
            return badgeKey + ".png";
        }

        //Get the description of the badge based on the badge key
        public static String getBadgeDescription(String badgeKey) {
            int badgeIndex = int.Parse(badgeKey.Substring(1, 2))-1;
            return bDescriptions[badgeIndex];
        }

        //Check whether a badge for a level was received
        public static String gotLevelBadge(int level) {
            if (level == 2) return "b01_starting_out";
            if (level == 5) return "b02_ramping_up";
            if (level == 10) return "b03_getting_there";
            if (level == 20) return "b04_skilled";
            if (level == 30) return "b05_rugged";
            if (level == 40) return "b06_hardened";
            if (level == 50) return "b07_experienced";
            return "";
        }

        //Return the coins that should be received for a badge
        public static int getCoins(String badgeKey) {
            int badgeIndex = int.Parse(badgeKey.Substring(1, 2)) - 1;
            return bCoins[badgeIndex];
        }

        //Return the number of coins to the next badge, or -1 if there is no next badge
        public static int coinsToNextBadge(int coins) {
            foreach(int c in coinsForBadge) {
                if (c > coins) return c - coins;
            }
            return -1;
        }

        //Return the key of the next coin badge that can be gained based on current coins
        public static string GetCoinBadge(int coins) {
            if (coins < coinsForBadge[0]) return "b20_entrepreneur";
            if (coins < coinsForBadge[1]) return "b21_money_man";
            if (coins < coinsForBadge[2]) return "b22_investor";
            if (coins < coinsForBadge[3]) return "b23_banker";
            if (coins < coinsForBadge[4]) return "b24_ceo";
            return "";
        }

        //Check whether a badge should be received for a certain amount of log entries
        public static string gotEntryBadge(int entries) {
            if (entries == entriesForBadge[0]) return "b08_pen_and_paper";
            if (entries == entriesForBadge[1]) return "b09_scribe";
            if (entries == entriesForBadge[2]) return "b10_research_assistant";
            if (entries == entriesForBadge[3]) return "b11_researcher_intern";
            if (entries == entriesForBadge[5]) return "b12_novice_researcher";
            if (entries == entriesForBadge[6]) return "b13_apprentice_researcher";
            if (entries == entriesForBadge[7]) return "b14_adept_researcher";
            if (entries == entriesForBadge[8]) return "b15_professional_researcher";
            return "";
        }
    }
}
