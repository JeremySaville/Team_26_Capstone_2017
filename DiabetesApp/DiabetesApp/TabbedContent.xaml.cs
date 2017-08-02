using Firebase.Xamarin.Auth;
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
    public partial class TabbedContent : TabbedPage
    {
        public TabbedContent (FirebaseAuthLink auth)
        {
            InitializeComponent();
            Children.Add(new LogbookPage(auth));
            Children.Add(new AnalyticsPage(auth));
            Children.Add(new HomePage(auth));
            Children.Add(new ProfilePage(auth));
            Children.Add(new SettingsPage(auth));

            this.SelectedItem = this.Children[2];
        }
    }
}