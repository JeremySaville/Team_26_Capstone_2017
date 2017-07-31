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
        public TabbedContent (String username)
        {
            InitializeComponent();
            Children.Add(new LogbookPage(username));
            Children.Add(new AnalyticsPage(username));
            Children.Add(new HomePage(username));
            Children.Add(new ProfilePage(username));
            Children.Add(new SettingsPage(username));

            this.SelectedItem = this.Children[2];
        }
    }
}