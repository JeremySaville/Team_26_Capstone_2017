using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesApp.Models {
    public class LogbookListItem {
        public string title { get; set; }
        public string description { get; set; }
        public int carbEx { get; set; }
        public float BG { get; set; }
        public float QA { get; set; }
        public float BI { get; set; }
        public float qaCorrect { get; set; }
        public float carbCorrect { get; set; }
        public string mood { get; set; }
        public string comments { get; set; }
        public DateTime updateTime { get; set; }
        public DateTime entryTime { get; set; }
    }
}
