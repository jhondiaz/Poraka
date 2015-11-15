using System;
using Android.Webkit;
using Android.Widget;
using Android.Views;

namespace PorAka
{
	public class WebVClient:WebViewClient
	{
		ProgressBar pbar;

		public	WebVClient(ProgressBar pbar =null){

			this.pbar=pbar;
		}

		public override bool ShouldOverrideUrlLoading (WebView view, string url)
		{
			view.LoadUrl (url);
			return true;
		}

		public override void OnPageStarted (WebView view, string url, Android.Graphics.Bitmap favicon)
		{
			base.OnPageStarted (view, url, favicon);

			if (this.pbar!=null) {
				this.pbar.Visibility = ViewStates.Visible;
			}

		}

		public override void OnPageFinished (WebView view, string url)
		{

			if (this.pbar != null) {
				this.pbar.Visibility = ViewStates.Gone;
			}
			base.OnPageFinished (view, url);


		}



	}

}

