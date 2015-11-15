using System;
using Android.Widget;
using Android.App;
using System.Collections.Generic;
using Android.Views;
using Android.Support.V4.App;
using Android.Webkit;

namespace PorAka
{
	public class ListQuotationAdapter:BaseAdapter<Quotation>
	{

		List<Quotation> items;
		Activity context;

		public ListQuotationAdapter (FragmentActivity context, List<Quotation> items) : base ()
		{
			this.context = context;
			this.items = items;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Quotation this [int position] {
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
				
				view = context.LayoutInflater.Inflate (Resource.Layout.ItemListOfertas, null);

				var ImgProduct = view.FindViewById<ImageView> (Resource.Id.ImgProduct);
				var TxtProduct = view.FindViewById<TextView> (Resource.Id.TxtProduct);
				var TxtUser = view.FindViewById<TextView> (Resource.Id.TxtUser);
				var WebDetalis = view.FindViewById<WebView> (Resource.Id.WebDetalis);

				var BtnAplicarOferta = view.FindViewById<Button> (Resource.Id.BtnAplicarOferta);
				var BtnCount = view.FindViewById<Button> (Resource.Id.BtnCount);

				//var ChBoxIsEnvio = view.FindViewById<CheckBox> (Resource.Id.ChBoxIsEnvio);
				TxtProduct.Text = item.Name;
				TxtUser.Text = item.UserName;
				BtnCount.Text = item.Count.ToString ();
				//ChBoxIsEnvio.Checked = item.IsEnvio;

				var html = @"<html><body> <ul>
    <li>Pnatalle: 4.0</li>
   <li>Tecnologia: 3G</li>
 <li>Procesador: Dual Core 1.0</li>
 <li>Color: Azul</li>
 <li>Sistema Operativo: Android 4.0</li>
 <li>Libre : Si</li>
 <li>Modelo: S6 Edge Plus</li>
 <li>Camara: 5.0 Mps</li>
 <li>Memoria: 32 Mg</li>
  </ul></body>
</html>";


				WebDetalis.LoadData (html, "text/html", null);
				WebDetalis.Settings.LoadsImagesAutomatically = true;
				WebDetalis.Settings.AllowContentAccess = true;

				BtnAplicarOferta.Click += (sender, e) => {
			
					// no view to re-use, create new
					var AplicarOfertaView = context.LayoutInflater.Inflate (Resource.Layout.AplicarOfertaView, null);
			
					var ImgAProduct = AplicarOfertaView.FindViewById<ImageView> (Resource.Id.ImgAProduct);
					var TxtAProduct = AplicarOfertaView.FindViewById<TextView> (Resource.Id.TxtAProduct);
					var TxtAValorOferta = AplicarOfertaView.FindViewById<EditText> (Resource.Id.TxtAValorOferta);

					TxtAProduct.Text = item.Name;

					//	TxtAValorOferta.Text="$0,0";
					TxtAValorOferta.RequestFocus ();

					using (var Download = new DownloadAsync ()) {

						Download.DownloadHistory (ImgAProduct, item.UrlImg);
						Download.Dispose ();
					}		

					AlertDialog.Builder builder = new AlertDialog.Builder (this.context);
			
					builder.SetTitle ("Enviar Oferta");
					builder.SetIcon (Resource.Drawable.ic_stat_checka);
					builder.SetView (AplicarOfertaView);
					builder.SetCancelable (false);
					builder.SetPositiveButton ("Enviar", async delegate {

						if (string.IsNullOrEmpty (TxtAValorOferta.Text)) {
					
							TxtAValorOferta.SetError ("Digita el Valor de su Oferta", null);
							TxtAValorOferta.RequestFocus ();
							return;
						}

					view.Visibility= ViewStates.Invisible;


					});


					builder.SetNegativeButton ("Cancel", delegate {
					

					});

					builder.Show ();

			
				};

				using (var Download = new DownloadAsync ()) {

					Download.DownloadHistory (ImgProduct, item.UrlImg);
					Download.Dispose ();
				}		
			}

			return view;
		}



	}







}