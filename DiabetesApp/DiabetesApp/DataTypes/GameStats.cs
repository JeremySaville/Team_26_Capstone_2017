using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesApp.DataTypes
{
    public class GameStats
    {
        public int xp { get; set; }
        public int level { get; set; }
        public int coins { get; set; }
        public int numLogins { get; set; }
        public string lastLogin { get; set; }
        public int entriesToday { get; set; }
        public string lastEntry { get; set; }
        public int numFullDayEntries { get; set; }
        public string lastFullDayEntry { get; set; }
        public bool dailyBonusReceived { get; set; }
        public int logEntriesMade { get; set; }
        public string currentProfilePic { get; set; }
        public string profilePictures { get; set; }
        public string badges { get; set; }
    }
}
