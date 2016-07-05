#region Copyright Syncfusion Inc. 2001-2016.
// Copyright Syncfusion Inc. 2001-2016. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using Android.Net;

namespace Project4Bicycle.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true, Label = "Project 4: Bicycle", Icon = "@drawable/AppIcon")]
    public class SplashScreenActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            bool isOnline = (activeConnection != null) && activeConnection.IsConnected;

            if(isOnline)
            {
                this.StartActivity(typeof(MainActivity));
            }
            else
            {
                new AlertDialog.Builder(this)
                    .SetPositiveButton("I understand", (sender, args) =>
                    {
                        //Exit app after user input
                        System.Environment.Exit(0);
                    })
                    .SetMessage("This app requires an active internet connection. App is about to exit.")
                    .SetTitle("No internet")
                    .Show();
            }
            
        }
    }
}