using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Support.V4.App;

namespace PorAka
{
	public class ListOfertaAdapter:BaseAdapter<Ofertas>
	{

		List<Ofertas> items;
		FragmentActivity context;

		public ListOfertaAdapter (FragmentActivity context, List<Ofertas> items) : base ()
		{
			this.context = context;
			this.items = items;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Ofertas this [int position] {
			get { return items [position]; }
		}

		public override int Count {
			get { return items.Count; }
		}



		public  override View GetView (int position, View convertView, ViewGroup parent)
		{
			var item = items [position];
			View view = convertView;
			if (view == null) {
				view = context.LayoutInflater.Inflate (Resource.Layout.ItemListaPrecios, null);

				var ImgLogo = view.FindViewById<ImageView> (Resource.Id.ImgLogo);
				var TxtComercial = view.FindViewById<TextView> (Resource.Id.TxtComercial);
				var TxtCiudad = view.FindViewById<TextView> (Resource.Id.TxtCiudad);
				var TxtPrecio = view.FindViewById<TextView> (Resource.Id.TxtPrecio);
				var TxtIsSend = view.FindViewById<TextView> (Resource.Id.TxtIsSend);
				var TxtIsGarantia = view.FindViewById<TextView> (Resource.Id.TxtIsGarantia);
				//var BtnComprar = view.FindViewById<Button> (Resource.Id.BtnComprar);


				var LLayout = view.FindViewById<LinearLayout> (Resource.Id.LLayout);


				TxtPrecio.Text = string.Format ("${0:N}", item.Price);
				TxtComercial.Text = item.Trade;
				TxtCiudad.Text = item.City;
				TxtIsSend.Text = (item.IsSend ? "Si" : "No");
				TxtIsGarantia.Text = (item.IsWarranty ? "Si" : "No");

				LLayout.Enabled = false;

				using (var Download = new DownloadAsync ()) {

					Download.DownloadHistory (ImgLogo, item.UrlImg);
					Download.Dispose ();
				}		


//				BtnComprar.Click += (sender, e) => {
//			
//					context.SupportFragmentManager.BeginTransaction ()
//					.Replace (Resource.Id.content_frame, 
//						InfCMFragment.NewInstance ("http://www.wbuscas.com/wp-content/uploads/2014/10/smartphone-motorola-moto-g-4.jpg"), "InfCMFragment")
//					.AddToBackStack ("SubastaFragment")
//
//					.Commit ();
//				};
			}

			return view;
		}



	}







}