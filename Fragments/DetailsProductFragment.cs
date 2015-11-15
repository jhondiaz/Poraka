
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
using Android.Views.Animations;
using Android.Webkit;

namespace PorAka
{
		
	public class DetailsProductFragment : BaseFragment
	{

		public static DetailsProductFragment NewInstance (Products product)
		{
			var fragment = new DetailsProductFragment ();
			var args = new Bundle ();
			args.PutString ("Id", product.Id);
			args.PutString ("Info", product.Info);
			args.PutInt ("LikeValue", product.LikeValue);
			args.PutString ("Name", product.Name);
			args.PutInt ("Sales", product.Sales);
			args.PutString ("URLIMG", product.UrlImg);
			fragment.Arguments = args;

			return fragment;
		}

		ImageView ImgVPro;
		TextView TxtProduct;
		TextView TxtSales;
		TextView TxtLike;
		ProgressBar ProBar;
		WebView WebDetalis;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			return inflater.Inflate (Resource.Layout.DetailsProductFragment, container, false);

			//return base.OnCreateView (inflater, container, savedInstanceState);
		}


		public override void OnViewCreated (View view, Bundle savedInstanceState)
		{
			var BtnPrecios = view.FindViewById<Button> (Resource.Id.BtnPrecios);
			TxtProduct = view.FindViewById<TextView> (Resource.Id.TxtProduct);
			TxtSales = view.FindViewById<TextView> (Resource.Id.TxtSales);
			TxtLike = view.FindViewById<TextView> (Resource.Id.TxtLike);
			ImgVPro = view.FindViewById<ImageView> (Resource.Id.ImgVPro);




			WebDetalis = view.FindViewById<WebView> (Resource.Id.WebDetalis);
			ProBar = view.FindViewById<ProgressBar> (Resource.Id.ProBar);
			ProBar.Progress = 0;

			BtnPrecios.Click += BtnPrecios_Click;
			TxtLike.Click += (sender, e) => {

				TxtLike.Text = (int.Parse ((string.IsNullOrEmpty (TxtLike.Text) ? "0" : TxtLike.Text)) + 1).ToString ();
				TxtLike.Enabled = false;
				TxtLike.SetCompoundDrawablesWithIntrinsicBounds (Resource.Drawable.ic_action_action_thumb_up, 0, 0, 0);

				using (var _Bussines = new Bussines ()) {

					_Bussines.SetLikeProductById (this.Arguments.GetString ("Id"));

				}

			};



			LoadDatos ();

			base.OnViewCreated (view, savedInstanceState);
		}


		private void LoadDatos ()
		{
			
			var datos = (this.Arguments.GetString ("Info") ?? "No Aplica");

			var li = "<html><head>\t\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=0.7\" /></head><body> <ul>";


			foreach (var it in datos.Split(',')) {
			
				li = li + "<li>" + it + "</li>";
			
			}

			li = li + "</ul></body></html>";

			TxtSales.Text = this.Arguments.GetInt ("Sale", 1).ToString ();
			TxtLike.Text = this.Arguments.GetInt ("LikeValue", 1).ToString ();
			TxtProduct.Text = this.Arguments.GetString ("Name");

			var settings = WebDetalis.Settings; 
			settings.JavaScriptEnabled = true;
			settings.UseWideViewPort = true;  
			settings.LoadWithOverviewMode = true;
			settings.JavaScriptCanOpenWindowsAutomatically = true; 
			settings.DomStorageEnabled = true;
			settings.SetRenderPriority (WebSettings.RenderPriority.High); 
			settings.BuiltInZoomControls = false; 

			settings.JavaScriptCanOpenWindowsAutomatically = false;
			WebDetalis.SetWebViewClient (new WebVClient ());
			settings.AllowFileAccess = true;
			settings.SetPluginState (WebSettings.PluginState.OnDemand);  


			WebDetalis.LoadDataWithBaseURL (null, li, "text/html", "utf-8", null);
			WebDetalis.Settings.LoadsImagesAutomatically = true;
			WebDetalis.Settings.AllowContentAccess = true;

			if (!string.IsNullOrEmpty (this.Arguments.GetString ("URLIMG"))) {

				using (var img = new DownloadAsync ()) {
					img.DownloadHistory (ImgVPro, this.Arguments.GetString ("URLIMG"), ProBar);
				}
			}



		
		}

		private	void BtnPrecios_Click (object sender, EventArgs e)
		{
			//var inputView = this.Activity.LayoutInflater.Inflate (Resource.Layout.ProcessBarView, null);

			var Client = _DatosUser.GetDatosClients ();

			_DatosUser.SetDatosProduct (new Products { 
				Id = this.Arguments.GetString ("Id"),
				Sales = this.Arguments.GetInt ("Sale", 1),
				LikeValue = this.Arguments.GetInt ("LikeValue", 1),
				Name = this.Arguments.GetString ("Name"),
				Info = this.Arguments.GetString ("Info"),
				UrlImg = this.Arguments.GetString ("URLIMG"),
			});


			if (Client != null && !string.IsNullOrEmpty (Client.Email)) {
				
				this.Activity.SupportFragmentManager.BeginTransaction ()
					.Replace (Resource.Id.content_frame, 
					SubastaFragment.NewInstance (), "SubastaFragment")
					.AddToBackStack ("DetailsProductFragment")

					.Commit ();
			} else {
			
				this.Activity.SupportFragmentManager.BeginTransaction ()
					.Replace (Resource.Id.content_frame, 
					LoginUserFragment.NewInstance (GoFragments.SubastaFragment), "LoginUserFragment")
					.AddToBackStack ("DetailsProductFragment")

					.Commit ();
			
			}	


		}


		public override Animation OnCreateAnimation (int transit, bool enter, int nextAnim)
		{
			return AnimationUtils.LoadAnimation (Activity,
				enter ? Android.Resource.Animation.FadeIn : Android.Resource.Animation.SlideOutRight);
		}

	}
}

