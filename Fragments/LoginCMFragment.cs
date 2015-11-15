
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
using Android.Accounts;

namespace PorAka
{
	public class LoginCMFragment : BaseFragment
	{
		private TextView TxtTypeUser;
		public static LoginCMFragment NewInstance ()
		{
			var fragment = new LoginCMFragment ();
//			var args = new Bundle ();
//			args.GetBoolean ("TYPEUSER", typeUser);
//			fragment.Arguments = args;
			return fragment;
		}
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			return inflater.Inflate(Resource.Layout.LoginCMFragment, container, false);

			//return base.OnCreateView (inflater, container, savedInstanceState);
		}


		public override void OnViewCreated (View view, Bundle savedInstanceState)
		{
			TxtTypeUser = view.FindViewById<TextView> (Resource.Id.TxtTypeUser);
			TxtTypeUser.Text = "Vendedor";

			var TxtNit = view.FindViewById<EditText> (Resource.Id.TxtNit);
			var TxtPwd = view.FindViewById<EditText> (Resource.Id.TxtPwd);

			var BtnIniciar = view.FindViewById<TextView> (Resource.Id.BtnIniciar);
			var BtnRegister = view.FindViewById<TextView> (Resource.Id.BtnRegister);


			try {
				Account[] accounts = AccountManager.Get (this.Activity).GetAccountsByType ("com.google");

				var email=accounts.FirstOrDefault();

				if(email!=null)
					TxtNit.Text = email.Name;

			} catch (Exception) {
				
			}


			BtnRegister.Click+= (sender, e) => {

				this.Activity.SupportFragmentManager.BeginTransaction ()
					.Replace (Resource.Id.content_frame,RegisterCMFragment.NewInstance(TxtNit.Text,false),"RegisterCMFragment")
					.AddToBackStack("InitFragment")
					.Commit ();
			};



			BtnIniciar.Click+=async  (sender, e) => {
				

				if (string.IsNullOrEmpty(TxtNit.Text)) {
					TxtNit.SetError("Digite su Email",null);
					TxtNit.RequestFocus(); 
					return;
				}

				if (!emailIsValid (TxtNit.Text)) {
					TxtNit.SetError ("Digite un Email  Valido", null);
					TxtNit.RequestFocus (); 
					return;
				}


				if (string.IsNullOrEmpty(TxtPwd.Text)) {
					TxtPwd.SetError("Digite su Contrseña",null);
					TxtNit.RequestFocus(); 
					return;
				}

				try {


					using (var _Bussines = new Bussines ()) {

						_ProgressDialog = ProgressDialog.Show (this.Activity, "", "Procesando...", true);

						var Result =	await _Bussines.LoginVendors (TxtNit.Text,TxtPwd.Text);

						_ProgressDialog.Dismiss ();

						switch (Result.Msg) {

						case "Ok":

							_DatosUser.SetDatosVendors(Result.dato);

							this.Activity.SupportFragmentManager.BeginTransaction ()
								.Replace (Resource.Id.content_frame, OfertasFragment.NewInstance(),"OfertasFragment")
								.AddToBackStack("LoginCMFragment")
								.Commit ();	

							break;
						
						default:
							Toast.MakeText (this.Activity, Result.Msg, ToastLength.Long).Show ();


							break;
						}




					}
				} catch (Exception ex) {
					Toast.MakeText (this.Activity, ex.Message, ToastLength.Long).Show ();
					return;
				}







				
			};




			base.OnViewCreated (view, savedInstanceState);
		}

		public override Animation OnCreateAnimation (int transit, bool enter, int nextAnim)
		{
			return AnimationUtils.LoadAnimation (Activity,
				enter ? Android.Resource.Animation.SlideInLeft : Android.Resource.Animation.SlideOutRight);
		}

	}
}

