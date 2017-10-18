using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using DiabetesApp.DataTypes;
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
	public partial class Quiz01 : ContentPage
	{
		public Quiz01 ()
		{
			InitializeComponent ();
		}

        public void onClick_Question01(object sender, EventArgs e)
        {
            var button = sender as Button;
            disableButtons(Q1A1, Q1A2, Q1A3, Q1A4);
            button.IsEnabled = false;
            if (button.Equals(Q1A4)) {
                button.BackgroundColor = Color.LawnGreen;
            } else
            {
                button.BackgroundColor = Color.IndianRed;
            }
        }

        public void onClick_Question02(object sender, EventArgs e)
        {
            var button = sender as Button;
            disableButtons(Q2A1, Q2A2, Q2A3, Q2A4);
            button.IsEnabled = false;
            if (button.Equals(Q2A1))
            {
                button.BackgroundColor = Color.LawnGreen;
            }
            else
            {
                button.BackgroundColor = Color.IndianRed;
            }
        }

        public void onClick_Question03(object sender, EventArgs e)
        {
            var button = sender as Button;
            disableButtons(Q3A1, Q3A2, Q3A3, Q3A4);
            button.IsEnabled = false;
            if (button.Equals(Q3A3))
            {
                button.BackgroundColor = Color.LawnGreen;
            }
            else
            {
                button.BackgroundColor = Color.IndianRed;
            }
        }

        public void disableButtons(Button btn1, Button btn2, Button btn3, Button btn4)
        {
            btn1.IsEnabled = false;
            btn2.IsEnabled = false;
            btn3.IsEnabled = false;
            btn4.IsEnabled = false;
        }

        public void onClick_Submit(object sender, EventArgs e)
        {

            Navigation.PopModalAsync();
        }

    }
 
}