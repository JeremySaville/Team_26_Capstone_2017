using System;
using Firebase.Xamarin.Auth;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Lesson02 : ContentPage
	{
        FirebaseAuthLink auth;

        public Lesson02(FirebaseAuthLink auth)
		{
			InitializeComponent();
            this.auth = auth;
        }

        void onClick_Quiz02(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Quiz02(auth));
        }

        void onClick_Back(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}