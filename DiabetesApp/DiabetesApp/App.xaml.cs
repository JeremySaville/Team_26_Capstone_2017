using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DiabetesApp {
	public partial class App : Application {
		public App () {
			InitializeComponent();
<<<<<<< HEAD
			MainPage = new DiabetesApp.Lesson06();
=======
			MainPage = new DiabetesApp.LoginPage();
>>>>>>> a9d2d912ccf63094de7cffcf1202394a7805ef9e
		}

		protected override void OnStart () {
			// Handle when your app starts
		}

		protected override void OnSleep () {
			// Handle when your app sleeps
		}

		protected override void OnResume () {
			// Handle when your app resumes
		}
	}
}
