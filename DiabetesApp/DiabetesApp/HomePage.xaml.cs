using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage{

        String username;
        public PlotModel BSLModel { get; set; }

		public HomePage (String username, String birthdate){
			InitializeComponent ();
            this.username = username;
            welcomeLabel.Text = $"Hello {username}";
            bdaylabel.Text = $"Birthday: {birthdate}";
            NavigationPage.SetHasNavigationBar(this, true);

            var viewmodel = new GraphViewModel();
            this.BindingContext = viewmodel;
        }

    }
}