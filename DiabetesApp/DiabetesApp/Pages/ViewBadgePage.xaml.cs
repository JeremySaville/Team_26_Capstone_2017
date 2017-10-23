using DiabetesApp.Models;
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
}