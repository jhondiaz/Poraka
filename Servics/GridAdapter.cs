using System;
using Android.Widget;
using Android.App;
using System.Collections.Generic;
using Android.Views;
using Android.Graphics;

namespace PorAka
{
	public class GridAdapter:BaseAdapter<Categorys>
	{

		List<Categorys> items;
		Activity context;

		public GridAdapter (Activity context, List<Categorys> items) : base ()
		{
			this.context = context;
			this.items = items;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Categorys this [int position] {
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
				view = context.LayoutInflater.Inflate (Resource.Layout.ItemGrid, null);


			var LyItem=  view.FindViewById<LinearLayout> (Resource.Id.LyItem);
			var TxtName =     view.FindViewById<TextView> (Resource.Id.TxtName);
			var TxtDetail =   view.FindViewById<TextView> (Resource.Id.TxtDetail);
			var ImgView =     view.FindViewById<ImageView> (Resource.Id.ImgView);
			var PBar =        view.FindViewById<ProgressBar> (Resource.Id.PBar);


		//	LyItem.SetBackgroundColor (Color.ParseColor (Helpers.ColorHeader));

			PBar.Progress = 0;

			ImgView.SetScaleType (ImageView.ScaleType.FitXy);          


			if (!string.IsNullOrEmpty (item.UrlImg)) {

				var img = new DownloadAsync ();
				img.DownloadHistory ( ImgView,item.UrlImg,PBar);
			}
		
			TxtDetail.Text = item.Detail;
			TxtName.Text = item.Name;


			return view;
		}
	}
}