
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
using System.Threading.Tasks;
using Android.Views.Animations;

namespace PorAka
{
				
	public class ListProductAdapter :BaseAdapter<Products>
	{

		List<Products> items;
		Activity context;

		public ListProductAdapter (Activity context, List<Products> items) : base ()
		{
			this.context = context;
			this.items = items;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Products this [int position] {
			get { return items [position]; }
		}

		public override int Count {
			get { return items.Count; }
		}



		public  override View GetView (int position, View convertView, ViewGroup parent)
		{
			var item = items [position];
			View view = convertView;
			if (view == null) // no view to re-use, create new
				view = context.LayoutInflater.Inflate (Resource.Layout.ItemListProduct, null);

			var ImgProduct = view.FindViewById<ImageView> (Resource.Id.ImgProduct);
			var TxtName = view.FindViewById<TextView> (Resource.Id.TxtName);
			var TxtRanking = view.FindViewById<TextView> (Resource.Id.TxtRanking);
			var TxtnVedidos = view.FindViewById<TextView> (Resource.Id.TxtnVedidos);
			var ProBar = view.FindViewById<ProgressBar> (Resource.Id.ProBar);
		
			TxtName.Text = item.Name;
			TxtnVedidos.Text = "Vendidos: "+item.Sales.ToString()+" +";
			TxtRanking.Text = "Valoraciones: "+item.LikeValue.ToString()+" +";


		
			using(var Download = new DownloadAsync ()){

				Download.DownloadHistory (ImgProduct, item.UrlImg,ProBar);
				Download.Dispose();
			}

			//ProBar.Visibility = ViewStates.Gone;

//			if (((newItems >> position) & 1) == 0) {
//				newItems |= 1L << position;
//				var density = context.Resources.DisplayMetrics.Density;
//				view.TranslationY = 60 * density;
//				view.RotationX = 12;
//				view.ScaleX = 1.1f;
//				view.PivotY = 180 * density;
//				view.PivotX = parent.Width / 2;
//				view.Animate ()
//					.TranslationY (0)
//					.RotationX (0)
//					.ScaleX (1)
//					.SetDuration (450)
//					.SetInterpolator (appearInterpolator)
//					.Start ();
//			}

			return view;
		}



	}







}