
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Views.Animations;

namespace PorAka
{
	public class InfCMFragment : BaseFragment
	{
		public static InfCMFragment NewInstance ()
		{
			var fragment = new InfCMFragment ();
//			var args = new Bundle ();
//			args.PutString ("URLIMG", UlrImg);
//			fragment.Arguments = args;

			return fragment;
		}

		private Products Product;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			return inflater.Inflate (Resource.Layout.InfCMFragment, container, false);

			//return base.OnCreateView (inflater, container, savedInstanceState);
		}


		public override void OnViewCreated (View view, Bundle savedInstanceState)
		{
			Product = _DatosUser.GetDatosProduct ();

			var ImgPro = view.FindViewById<ImageView> (Resource.Id.ImgPro);
			var TxtProduct = view.FindViewById<TextView> (Resource.Id.TxtProduct);
			var TxtSale = view.FindViewById<TextView> (Resource.Id.TxtSale);
			var TxtLike = view.FindViewById<TextView> (Resource.Id.TxtLike);

			TxtProduct.Text = Product.Name;
			TxtSale.Text = Product.Sales.ToString();
			TxtLike.Text = Product.LikeValue.ToString();



			var TxtCelular = view.FindViewById<TextView> (Resource.Id.TxtCelular);

			var BtnCall = view.FindViewById<Button> (Resource.Id.BtnCall);

			var BtnNavegar = view.FindViewById<Button> (Resource.Id.BtnNavegar);

			TxtCelular.Click += (sender, e) => {
				Intent intent = new Intent (Intent.ActionCall, Android.Net.Uri.Parse ("tel:962849347"));
				this.Activity.StartActivity (intent);
			
			};

			BtnCall.Click += (sender, e) => {
				
				Intent intent = new Intent (Intent.ActionCall, Android.Net.Uri.Parse ("tel:962849347"));
				this.Activity.StartActivity (intent);
			
			};


			BtnNavegar.Click += (sender, e) => {
			
				var geo = string.Format ("google.navigation:q={0},{1}", "4.614102669338081", "-74.12763388903807");

				Intent intent = new Intent (Intent.ActionView, Android.Net.Uri.Parse (geo));
				this.Activity.StartActivity (intent);

//				Intent i = new Intent(Intent.ActionView,Android.Net.Uri.Parse("geo:37.827500,-122.481670"));
//				i.SetClassName("com.google.android.apps.maps","com.google.android.maps.MapsActivity");
//				this.Activity.StartActivity(i);

			
			};

			if (!string.IsNullOrEmpty (Product.UrlImg)) {

				var img = new DownloadAsync ();
				img.DownloadHistory (ImgPro,Product.UrlImg);

			}



			base.OnViewCreated (view, savedInstanceState);
		}

		public override Animation OnCreateAnimation (int transit, bool enter, int nextAnim)
		{
			return AnimationUtils.LoadAnimation (Activity,
				enter ? Android.Resource.Animation.FadeIn : Android.Resource.Animation.SlideOutRight);
		}


	}
}

