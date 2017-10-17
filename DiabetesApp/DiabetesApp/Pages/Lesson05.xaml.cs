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
	public partial class Lesson05 : ContentPage
	{
		public Lesson05()
		{
			InitializeComponent ();
		}

        void onClick_Quiz05(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Quiz05());
        }

        void onClick_Back(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}