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
	public partial class Lesson13 : ContentPage
	{
		public Lesson13 ()
		{
			InitializeComponent ();
		}

        void onClick_Quiz13(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Quiz13());
        }

        void onClick_Back(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}