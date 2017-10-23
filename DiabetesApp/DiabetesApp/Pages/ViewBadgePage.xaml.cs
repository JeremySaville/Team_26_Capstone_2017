<<<<<<< HEAD
<<<<<<< HEAD
﻿using DiabetesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp.Pages {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewBadgePage : ContentPage {
		public ViewBadgePage (string badgeKey) {
			InitializeComponent ();
            badgeName.Text = BadgeList.getBadgeName(badgeKey);
            badgeImage.Source = BadgeList.getBadgeLink(badgeKey);
            badgeDescription.Text = BadgeList.getBadgeDescription(badgeKey);
		}

        public void onClick_backButton(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }
	}
=======
﻿using DiabetesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp.Pages {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewBadgePage : ContentPage {
		public ViewBadgePage (string badgeKey) {
			InitializeComponent ();
            badgeName.Text = BadgeList.getBadgeName(badgeKey);
            badgeImage.Source = BadgeList.getBadgeLink(badgeKey);
            badgeDescription.Text = BadgeList.getBadgeDescription(badgeKey);
		}

        public void onClick_backButton(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }
	}
>>>>>>> 0d9259108b810199bfe41e5545b95dc2162cf056
=======
﻿using DiabetesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp.Pages {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewBadgePage : ContentPage {
		public ViewBadgePage (string badgeKey) {
			InitializeComponent ();
            badgeName.Text = BadgeList.getBadgeName(badgeKey);
            badgeImage.Source = BadgeList.getBadgeLink(badgeKey);
            badgeDescription.Text = BadgeList.getBadgeDescription(badgeKey);
		}

        public void onClick_backButton(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }
	}
>>>>>>> a96ed9bc9938ff2c92b2f6669ec9509c04c5b753
}