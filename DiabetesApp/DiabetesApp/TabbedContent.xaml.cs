using Firebase.Xamarin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedContent : TabbedPage {
        public TabbedContent(FirebaseAuthLink auth, bool gamified) {
            InitializeComponent();

            Children.Add(new NavigationPage(new LogbookPage(auth, gamified)) { Icon = "ic_assignment_white.png" });
            Children.Add(new AnalyticsPage(auth));
            Children.Add(new ProfilePage(auth, gamified));
            Children.Add(new Pages.EducationPage(auth, gamified) { Icon = "ic_school_white.png" });
            Children.Add(new SettingsPage(auth));

            if (gamified)
                SelectedItem = Children[2];
            else
                SelectedItem = Children[1];
        }
    }
}