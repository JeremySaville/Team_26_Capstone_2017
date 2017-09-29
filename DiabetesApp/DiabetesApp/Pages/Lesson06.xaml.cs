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
	public partial class Lesson06 : ContentPage
	{
		public Lesson06()
		{
			InitializeComponent ();
		}
        async void OnButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            await DisplayAlert("Clicked!",
                "The button labeled '" + button.Text + "' has been clicked",
                "OK");
        }
    }
}