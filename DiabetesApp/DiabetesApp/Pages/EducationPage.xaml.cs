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
	public partial class EducationPage : ContentPage
	{
		public EducationPage ()
		{
			InitializeComponent ();                        
        }

        void onClick_lesson01(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson01());
        }

        
    }
}