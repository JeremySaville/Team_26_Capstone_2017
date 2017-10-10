using DiabetesApp.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp.Popups {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BadgePopup : PopupPage {
		public BadgePopup (string badgeKey) {
			InitializeComponent ();
            badges.Add(new ImageCell() {
                ImageSource = BadgeList.getBadgeLink(badgeKey),
                Text = BadgeList.getBadgeName(badgeKey),
                Detail = BadgeList.getBadgeDescription(badgeKey)
            });
        }

        async void onClick_backButton(object sender, EventArgs e) {
            await PopupNavigation.PopAsync();
        }
	}
}