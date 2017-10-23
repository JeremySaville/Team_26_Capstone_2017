﻿using System;
using Firebase.Xamarin.Auth;
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
        FirebaseAuthLink auth;

		public EducationPage (FirebaseAuthLink auth)
		{
            InitializeComponent ();
            this.auth = auth;
            
            // lesson 1 tapgesturerecogniser
            var lesson01Image = new TapGestureRecognizer();
            lesson01Image.Tapped += lesson01Image_Tapped;
            lesson01.GestureRecognizers.Add(lesson01Image);

            // lesson 2 tapgesturerecogniser
            var lesson02Image = new TapGestureRecognizer();
            lesson02Image.Tapped += lesson02Image_Tapped;
            lesson02.GestureRecognizers.Add(lesson02Image);

            // lesson 3 tapgesturerecogniser
            var lesson03Image = new TapGestureRecognizer();
            lesson03Image.Tapped += lesson03Image_Tapped;
            lesson03.GestureRecognizers.Add(lesson03Image);

            // lesson 4 tapgesturerecogniser
            var lesson04Image = new TapGestureRecognizer();
            lesson04Image.Tapped += lesson04Image_Tapped;
            lesson04.GestureRecognizers.Add(lesson04Image);

            // lesson 5 tapgesturerecogniser
            var lesson05Image = new TapGestureRecognizer();
            lesson05Image.Tapped += lesson05Image_Tapped;
            lesson05.GestureRecognizers.Add(lesson05Image);

            // lesson 6 tapgesturerecogniser
            var lesson06Image = new TapGestureRecognizer();
            lesson06Image.Tapped += lesson06Image_Tapped;
            lesson06.GestureRecognizers.Add(lesson06Image);

            // lesson 7 tapgesturerecogniser
            var lesson07Image = new TapGestureRecognizer();
            lesson07Image.Tapped += lesson07Image_Tapped;
            lesson07.GestureRecognizers.Add(lesson07Image);

            // lesson 8 tapgesturerecogniser
            var lesson08Image = new TapGestureRecognizer();
            lesson08Image.Tapped += lesson08Image_Tapped;
            lesson08.GestureRecognizers.Add(lesson08Image);

            // lesson 9 tapgesturerecogniser
            var lesson09Image = new TapGestureRecognizer();
            lesson09Image.Tapped += lesson09Image_Tapped;
            lesson09.GestureRecognizers.Add(lesson09Image);

            // lesson 10 tapgesturerecogniser
            var lesson10Image = new TapGestureRecognizer();
            lesson10Image.Tapped += lesson10Image_Tapped;
            lesson10.GestureRecognizers.Add(lesson10Image);

            // lesson 11 tapgesturerecogniser
            var lesson11Image = new TapGestureRecognizer();
            lesson11Image.Tapped += lesson11Image_Tapped;
            lesson11.GestureRecognizers.Add(lesson11Image);

            // lesson 12 tapgesturerecogniser
            var lesson12Image = new TapGestureRecognizer();
            lesson12Image.Tapped += lesson12Image_Tapped;
            lesson12.GestureRecognizers.Add(lesson12Image);

            // lesson 13 tapgesturerecogniser
            var lesson13Image = new TapGestureRecognizer();
            lesson13Image.Tapped += lesson13Image_Tapped;
            lesson13.GestureRecognizers.Add(lesson13Image);

        }

        void lesson01Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson01(auth));
        }

        void lesson02Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson02(auth));
        }

        void lesson03Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson03(auth));
        }

        void lesson04Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson04(auth));
        }

        void lesson05Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson05(auth));
        }

        void lesson06Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson06(auth));
        }

        void lesson07Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson07(auth));
        }

        void lesson08Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson08(auth));
        }

        void lesson09Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson09(auth));
        }

        void lesson10Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson10(auth));
        }

        void lesson11Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson11(auth));
        }

        void lesson12Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson12(auth));
        }

        void lesson13Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Lesson13(auth));
        }

        //void onClick_Lesson01(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new Lesson01(auth));
        //}

        //void onClick_Lesson02(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new Lesson02(auth));
        //}

        //void onClick_Lesson03(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new Lesson03(auth));
        //}

        //void onClick_Lesson04(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new Lesson04(auth));
        //}

        //void onClick_Lesson05(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new Lesson05(auth));
        //}

        //void onClick_Lesson06(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new Lesson06(auth));
        //}

        //void onClick_Lesson07(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new Lesson07(auth));
        //}

        //void onClick_Lesson08(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new Lesson08(auth));
        //}

        //void onClick_Lesson09(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new Lesson09(auth));
        //}

        //void onClick_Lesson10(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new Lesson10(auth));
        //}

        //void onClick_Lesson11(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new Lesson11(auth));
        //}

        //void onClick_Lesson12(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new Lesson12(auth));
        //}

        //void onClick_Lesson13(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new Lesson13(auth));
        //}

    }
}