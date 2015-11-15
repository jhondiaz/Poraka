
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
	public class InitFragment : BaseFragment
	{

		private List<Categorys> ListCategory;

		public static InitFragment NewInstance (string UlrImg)
		{
			var fragment = new InitFragment ();
			var args = new Bundle ();
			args.PutString ("URLIMG", UlrImg);
			fragment.Arguments = args;
			return fragment;
		}

		GridView Gridview;
		ProgressBar ProBar;
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			return	inflater.Inflate (Resource.Layout.InitFragment, container, false);


			//return base.OnCreateView (inflater, container, savedInstanceState);
		}





		public override void OnViewCreated (View view, Bundle savedInstanceState)
		{

			Gridview = view.FindViewById<GridView> (Resource.Id.GView);
			ProBar = view.FindViewById<ProgressBar> (Resource.Id.ProBar);


			Gridview.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args) {
				//	Toast.MakeText (this, args.Position.ToString (), ToastLength.Short).Show ();

				if (ListCategory != null && ListCategory.Count != 0) {

					this.Activity.SupportFragmentManager.BeginTransaction ()
						.Replace (Resource.Id.content_frame, ListProductFragment.NewInstance (ListCategory [args.Position]), "DetailsProductFragment")
						.AddToBackStack ("InitFragment")
					//.DisallowAddToBackStack()
						.Commit ();

				}

		


			};

			LoadGridView ();


			base.OnViewCreated (view, savedInstanceState);
		}

		private async void LoadGridView ()
		{
		
			//_ProgressDialog = ProgressDialog.Show (this.Activity, null, "Categorias...", true);

			using (var _Bussines = new Bussines ()) {

				ListCategory = (ListCategory == null ? await _Bussines.GetCategorys () : ListCategory);

				if (ListCategory != null && ListCategory.Count != 0) {
					
					Gridview.Adapter = new GridAdapter (this.Activity, ListCategory);
				}
			}
			ProBar.Visibility = ViewStates.Gone;
			//_ProgressDialog.Dismiss ();

		}






		public override Animation OnCreateAnimation (int transit, bool enter, int nextAnim)
		{
			return AnimationUtils.LoadAnimation (Activity,
				enter ? Android.Resource.Animation.SlideInLeft : Android.Resource.Animation.SlideOutRight);
		}


	}
}

