using Firebase.Xamarin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EntryPage : ContentPage
	{
		public EntryPage (FirebaseAuthLink auth)
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
		}

        void onClick_submitEntry(object sender, EventArgs e) {
            //TODO SUBMIT ENTERED DETAILS AS A LOGBOOK ENTRY
            Navigation.PopModalAsync();
        }

        void onClick_cancelEntry(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }
    }
}