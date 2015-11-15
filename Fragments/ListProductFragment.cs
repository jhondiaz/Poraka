
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
	public class ListProductFragment : BaseFragment
	{

		private List<Products> ListProducts;

		public static ListProductFragment NewInstance (Categorys category)
		{
			var fragment = new ListProductFragment ();
			var args = new Bundle ();
			args.PutString ("Id", category.Id);
			args.PutString ("Name", category.Name);


			fragment.Arguments = args;
			return fragment;
		}

		private ListView ListVProduct;
		ProgressBar ProBar;
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			return inflater.Inflate(Resource.Layout.ListProductFragment, container, false);

			//return base.OnCreateView (inflater, container, savedInstanceState);
		}
		public override void OnViewCreated (View view, Bundle savedInstanceState)
		{

			ProBar = view.FindViewById<ProgressBar> (Resource.Id.ProBar);

			var TxtCategory =view.FindViewById<TextView> (Resource.Id.TxtCategory);

			TxtCategory.Text = this.Arguments.GetString ("Name");

			ListVProduct= view.FindViewById<ListView> (Resource.Id.ListVProduct);
			ListVProduct.ItemClick+= ListVProduct_ItemClick;
				
			LoadProduct ();

			base.OnViewCreated (view, savedInstanceState);
		}


		void ListVProduct_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			if (ListProducts!=null&& ListProducts.Count!=0) {
				
				this.Activity.SupportFragmentManager.BeginTransaction ()
					.Replace (Resource.Id.content_frame, DetailsProductFragment.NewInstance(ListProducts[e.Position]),"DetailsProductFragment")
					.AddToBackStack("ListProductFragment")

					.Commit ();
			}


		}




		private async void LoadProduct (){

			//ListProducts

	       //_ProgressDialog= ProgressDialog.Show (this.Activity, null, "Productos...", true);

			using(var _Bussines= new Bussines()){

				ListProducts = (ListProducts==null? await _Bussines.GetProductByIdCategorys (this.Arguments.GetString("Id")):ListProducts);

				if (ListProducts != null && ListProducts.Count != 0) {

					ListVProduct.Adapter = new ListProductAdapter (this.Activity, ListProducts);
				}
			}

			ProBar.Visibility = ViewStates.Gone;

			//_ProgressDialog.Dismiss ();


//
//
//			ListVProduct.Adapter = new ListProductAdapter (this.Activity, new List<Products> () {
//				new Products{
//					Id="",
//					Name="Celular Samsung Galaxy S6 Edge Plus - Pantalla 5.7 - 32GB - Negro",
//					UrlImg="http://www.wbuscas.com/wp-content/uploads/2014/10/smartphone-motorola-moto-g-4.jpg",
//					Sales=120,
//					LikeValue=450
//
//				},
//
//				new Products{
//					Id="",
//					Name="Celular Samsung Galaxy S6 Edge Plus - Pantalla 5.7\" - 32GB - Negro",
//					UrlImg="http://www.wbuscas.com/wp-content/uploads/2014/10/smartphone-motorola-moto-g-4.jpg",
//					Sales=120,
//					LikeValue=450
//
//				},
//				new Products{
//					Id="",
//					Name="Celular Samsung Galaxy S6 Edge Plus - Pantalla 5.7 - 32GB - Negro",
//					UrlImg="http://www.wbuscas.com/wp-content/uploads/2014/10/smartphone-motorola-moto-g-4.jpg",
//					Sales=120,
//					LikeValue=450
//
//				},
//
//				new Products{
//					Id="",
//					Name="Celular Samsung Galaxy S6 Edge Plus - Pantalla 5.7\" - 32GB - Negro",
//					UrlImg="http://www.wbuscas.com/wp-content/uploads/2014/10/smartphone-motorola-moto-g-4.jpg",
//					Sales=120,
//					LikeValue=450
//
//				},
//				new Products{
//					Id="",
//					Name="Celular Samsung Galaxy S6 Edge Plus - Pantalla 5.7 - 32GB - Negro",
//					UrlImg="http://www.wbuscas.com/wp-content/uploads/2014/10/smartphone-motorola-moto-g-4.jpg",
//					Sales=120,
//					LikeValue=450
//
//				},
//
//				new Products{
//					Id="",
//					Name="Celular Samsung Galaxy S6 Edge Plus - Pantalla 5.7\" - 32GB - Negro",
//					UrlImg="http://www.wbuscas.com/wp-content/uploads/2014/10/smartphone-motorola-moto-g-4.jpg",
//					Sales=120,
//					LikeValue=450
//
//				},
//				new Products{
//					Id="",
//					Name="Celular Samsung Galaxy S6 Edge Plus - Pantalla 5.7 - 32GB - Negro",
//					UrlImg="http://www.wbuscas.com/wp-content/uploads/2014/10/smartphone-motorola-moto-g-4.jpg",
//					Sales=120,
//					LikeValue=450
//
//				},
//
//				new Products{
//					Id="",
//					Name="Celular Samsung Galaxy S6 Edge Plus - Pantalla 5.7\" - 32GB - Negro",
//					UrlImg="http://www.wbuscas.com/wp-content/uploads/2014/10/smartphone-motorola-moto-g-4.jpg",
//					Sales=120,
//					LikeValue=450
//
//				}
//
//			}
//
//			);

		
		}

		public override Animation OnCreateAnimation (int transit, bool enter, int nextAnim)
		{
			return AnimationUtils.LoadAnimation (Activity,
				enter ? Android.Resource.Animation.SlideInLeft : Android.Resource.Animation.SlideOutRight);
		}

	}
}

