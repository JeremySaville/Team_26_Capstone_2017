using Firebase.Xamarin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogbookPage : ContentPage
	{
        FirebaseAuthLink auth;

		public LogbookPage (FirebaseAuthLink auth)
		{
			InitializeComponent ();
            this.auth = auth;
            NavigationPage.SetHasNavigationBar(this, false);
		}

        void onClick_createEntry(object sender, EventArgs e) {
            Navigation.PushModalAsync(new Pages.EntryPage(auth));
        }

    }
}