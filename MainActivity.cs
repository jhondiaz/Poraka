using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PorAka
{
	[Activity (Theme = "@style/Theme.Splash",MainLauncher = true,
		NoHistory = true,Label = "PorAka", 
		Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);


			StartService (new Intent (this,typeof( NotificationServices)));

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			System.Threading.Thread.Sleep (500);




			StartActivity (new Intent (this, typeof(InitActivity)));

		
		}
	}
}


