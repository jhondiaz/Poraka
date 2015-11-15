
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
	public class SubastaFragment : BaseFragment
	{

		public static SubastaFragment NewInstance ()
		{
			var fragment = new SubastaFragment ();
		   //Var args = new Bundle ();
		   //fragment.Arguments = args;
			return fragment;
		}


		ImageView ImgPro;
		ListView ListVOfertas;
		TextView TxtProduct;
		private Products Product;
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			return inflater.Inflate (Resource.Layout.SubastaFragment, container, false);

		}



		public override void OnViewCreated (View view, Bundle savedInstanceState)
		{

			Product = _DatosUser.GetDatosProduct ();

			var BtnPrecios = view.FindViewById<Button> (Resource.Id.ImgVPro);

			ImgPro = view.FindViewById<ImageView> (Resource.Id.ImgPro);
			TxtProduct = view.FindViewById<TextView> (Resource.Id.TxtProduct);
			var TxtSale = view.FindViewById<TextView> (Resource.Id.TxtSale);
			var TxtLike = view.FindViewById<TextView> (Resource.Id.TxtLike);

			ListVOfertas = view.FindViewById<ListView> (Resource.Id.ListVOfertas);



			ListVOfertas.ItemClick+= (sender, e) => {
				ConfirCompra();
			
			};


			TxtProduct.Text = Product.Name;
			TxtSale.Text = Product.Sales.ToString();
			TxtLike.Text = Product.LikeValue.ToString();
			LoadDatos ();
			base.OnViewCreated (view, savedInstanceState);
		}


		private void ConfirCompra(){
		


			AlertDialog.Builder builder = new AlertDialog.Builder (this.Activity);

			builder.SetTitle ("Comprar");
			builder.SetIcon (Resource.Drawable.ic_stat_checka);
			builder.SetMessage("¿Esta seguro de hacer la compra?");
			builder.SetCancelable (false);
			builder.SetPositiveButton ("Si",  delegate {

				this.Activity.SupportFragmentManager.BeginTransaction ()
					.Replace (Resource.Id.content_frame, 
						InfCMFragment.NewInstance (), "InfCMFragment")
					.AddToBackStack ("InitFragment")
					.Commit ();

			});


			builder.SetNegativeButton ("Cancel", delegate {


			});

			builder.Show ();



		
		}









		private  void LoadDatos ()
		{

			var r = new Random ();

			var nproveedores = r.Next (100, 623).ToString ();

			var msg = "En TIEMPO REAL a mas de " + nproveedores + " Provedores que TE enviaran las Mejores OFERTAS de este producto, Solo espera Unos MINUTOS";

			_ProgressDialog = ProgressDialog.Show (this.Activity, "Estamos Contactando", msg, true);
			_ProgressDialog.Show ();

			var DatosClient = _DatosUser.GetDatosClients ();

			var DatosProduct = _DatosUser.GetDatosProduct ();

			if(DatosClient!=null){
				
				using (var _Bussines = new Bussines ()) {
					
					 _Bussines.SetConsultProduct (DatosClient.Id, DatosProduct.Id);

				}

			}




			List<Ofertas> List = new List<Ofertas> () { 

				new Ofertas {

					Id = "",
					Trade = "TOTAL CELL",
					UrlImg = "http://www.tiendasjumbo.co/arquivos/logo-jumbo.png",
					Contact = "JHON HAROLD DIAZ",
					City = "VALLEDUPAR",
					IsSend = true,
					IsWarranty = true,
					Price = 10500

				},
				new Ofertas {

					Id = "",
					Trade = "TOTAL CELL",
					UrlImg = "http://production-alkosto-data.s3-website-us-east-1.amazonaws.com/media/ALKOSTO/contenido/logo-octubre-header.png",
					Contact = "JHON HAROLD DIAZ",
					City = "BOGOTA",
					IsSend = true,
					IsWarranty = true,
					Price = 105600

				}
				,
				new Ofertas {

					Id = "",
					Trade = "TOTAL CELL",
					UrlImg = "http://www.tiendasjumbo.co/arquivos/logo-jumbo.png",
					Contact = "JHON HAROLD DIAZ",
					City = "BOGOTA",
					IsSend = true,
					IsWarranty = true,
					Price = 1056000

				}

				,
				new Ofertas {

					Id = "",
					Trade = "TOTAL CELL",
					UrlImg = "http://production-alkosto-data.s3-website-us-east-1.amazonaws.com/media/ALKOSTO/contenido/logo-octubre-header.png",
					Contact = "JHON HAROLD DIAZ",
					City = "BOGOTA",
					IsSend = true,
					IsWarranty = true,
					Price = 105

				}


			};
		
			var Orderlist = List.OrderBy (d => d.Price).ToList();

//			var FirtPreci = Orderlist.FirstOrDefault ();
//
//			if (FirtPreci != null) {
//				TxtTrade.Text = FirtPreci.Trade;
//				TxtPrecio.Text = string.Format ("${0:N}", FirtPreci.Price);
//			}


			ListVOfertas.Adapter = new ListOfertaAdapter (this.Activity, Orderlist);

			_ProgressDialog.Dismiss ();

			if (!string.IsNullOrEmpty (Product.UrlImg)) {

				var img = new DownloadAsync ();
				img.DownloadHistory (ImgPro,Product.UrlImg);

			}




		}



		public override Animation OnCreateAnimation (int transit, bool enter, int nextAnim)
		{
			return AnimationUtils.LoadAnimation (Activity,
				enter ? Android.Resource.Animation.FadeIn : Android.Resource.Animation.SlideOutRight);
		}


	}
}

