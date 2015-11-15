using System;
using Android.App;
using Android.Widget;
using System.Collections.Generic;
using Android.Views;

namespace PorAka
{
	public class ItamDrawerAdapter:BaseAdapter<ItamDrawer>
	{

		List<ItamDrawer> items;
		Activity context;

		public ItamDrawerAdapter (Activity context, List<ItamDrawer> items) : base ()
		{
			this.context = context;
			this.items = items;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override ItamDrawer this [int position] {
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
				view = context.LayoutInflater.Inflate (Resource.Layout.DrawerListItem, null);

			var ImgMenu =  view.FindViewById<ImageView> (Resource.Id.ImgMenu);
			var TxtName =   view.FindViewById<TextView> (Resource.Id.TxtName);

			TxtName.Text = item.Name;

			using(var Download = new DownloadAsync ()){
				
				Download.DownloadHistory (ImgMenu, item.ImgMenu);
				Download.Dispose();
			}		


			return view;
		}



	}
}

