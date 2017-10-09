using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DiabetesApp.Models {
    class BadgeList {

        //Convert the string for the badge name to 
        public static String getBadgeName(String badgeKey) {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(badgeKey.Substring(3).Replace("_", " "));
        }

        //Get the image link for the badge
        public static String getBadgeLink(String badgeKey) {
            return badgeKey + ".png";
        }
    }
}
