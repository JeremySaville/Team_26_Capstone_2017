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
    public partial class LoginPage : ContentPage {
        public LoginPage() {
            InitializeComponent();
        }

        async void onClick_loginButton(object sender, EventArgs e) {
            string username = "Placeholder"; //TODO
            App.Current.MainPage = new TabbedContent(username);
        }
    }
}