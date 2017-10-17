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
	public partial class Lesson03 : ContentPage
	{
		public Lesson03()
		{
			InitializeComponent ();
		}

        void onClick_Quiz03(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Quiz03());
        }

        void onClick_Back(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}