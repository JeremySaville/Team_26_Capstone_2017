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
	public partial class EducationPage : ContentPage
	{
		public EducationPage ()
		{
			InitializeComponent ();
		}

        void onClick_Lesson01(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson01());
        }

        void onClick_Lesson02(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson02());
        }

        void onClick_Lesson03(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson03());
        }

        void onClick_Lesson04(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson04());
        }

        void onClick_Lesson05(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson05());
        }

        void onClick_Lesson06(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson06());
        }

        void onClick_Lesson07(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson07());
        }

        void onClick_Lesson08(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson08());
        }

        void onClick_Lesson09(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson09());
        }

        void onClick_Lesson10(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson10());
        }

        void onClick_Lesson11(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson11());
        }

        void onClick_Lesson12(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson12());
        }

        void onClick_Lesson13(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson13());
        }

    }
}