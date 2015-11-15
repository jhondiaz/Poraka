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
using Android.Accounts;

namespace PorAka
{
	public class LoginUserFragment : BaseFragment
	{
		private TextView TxtTypeUser;

		public static LoginUserFragment NewInstance (GoFragments go)
		{
			var fragment = new LoginUserFragment ();
			var args = new Bundle ();
			switch (go) {

			case GoFragments.InitFragment:
				
				args.PutInt ("Go", 0);

				break;

			case GoFragments.SubastaFragment:

				args.PutInt ("Go", 1);

				break;
			default:
				args.PutInt ("Go", 0);
				break;
			}

			fragment.Arguments = args;
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
			return inflater.Inflate (Resource.Layout.LoginCMFragment, container, false);

			//return base.OnCreateView (inflater, container, savedInstanceState);
		}

		public override void OnViewCreated (View view, Bundle savedInstanceState)
		{
			TxtTypeUser = view.FindViewById<TextView> (Resource.Id.TxtTypeUser);

			var TxtNit = view.FindViewById<EditText> (Resource.Id.TxtNit);
			var TxtPwd = view.FindViewById<EditText> (Resource.Id.TxtPwd);

			var BtnIniciar = view.FindViewById<TextView> (Resource.Id.BtnIniciar);
			var BtnRegister = view.FindViewById<TextView> (Resource.Id.BtnRegister);

			BtnRegister.Click += (sender, e) => {
			
				switch (this.Arguments.GetInt ("Go", 0)) {

				case 0:


					this.Activity.SupportFragmentManager.BeginTransaction ()
						.Replace (Resource.Id.content_frame, RegisterUserFragment.NewInstance (GoFragments.InitFragment), "RegisterUserFragment")
						.AddToBackStack ("LoginUserFragment")
						.Commit ();
					break;

				case 1:

			
					this.Activity.SupportFragmentManager.BeginTransaction ()
						.Replace (Resource.Id.content_frame, RegisterUserFragment.NewInstance (GoFragments.SubastaFragment), "RegisterUserFragment")
						.AddToBackStack ("LoginUserFragment")
						.Commit ();
					break;
				default:
					RegisterUserFragment.NewInstance (GoFragments.InitFragment);
					this.Activity.SupportFragmentManager.BeginTransaction ()
						.Replace (Resource.Id.content_frame, RegisterUserFragment.NewInstance (GoFragments.InitFragment), "RegisterUserFragment")
						.AddToBackStack ("LoginUserFragment")
						.Commit ();

					break;
				}



			};



			BtnIniciar.Click += async (sender, e) => {
			
				if (string.IsNullOrEmpty (TxtNit.Text)) {
					TxtNit.SetError ("Digite su Email", null);
					TxtNit.RequestFocus (); 
					return;
				}

				if (string.IsNullOrEmpty (TxtPwd.Text)) {
					TxtPwd.SetError ("Digite su Contrseña", null);
					TxtPwd.RequestFocus (); 
					return;
				}


				try {


					using (var _Bussines = new Bussines ()) {

						_ProgressDialog = ProgressDialog.Show (this.Activity, "", "Procesando...", true);

						var Result =	await _Bussines.LoginClients (TxtNit.Text, TxtPwd.Text);

						_ProgressDialog.Dismiss ();

						switch (Result.Msg) {

						case "Ok":

							_DatosUser.SetDatosClients (Result.dato);

							GoFragment (Result.dato);

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



			TxtTypeUser.Text = "Usuario";
			try {
				Account[] accounts = AccountManager.Get (this.Activity).GetAccountsByType ("com.google");

				foreach (Account account in accounts) {

					if (!string.IsNullOrEmpty (account.Name)) {
						TxtNit.Text = account.Name;

						break;
					}

					//accountsList.add(item);
				}
			} catch (Exception) {
				
			}

			base.OnViewCreated (view, savedInstanceState);
		}

		private void GoFragment (Clients dato)
		{

			int go = this.Arguments.GetInt ("Go", 0);

			switch (go) {

			case 0:

				this.Activity.SupportFragmentManager.BeginTransaction ()
					.Replace (Resource.Id.content_frame, InitFragment.NewInstance (dato.Email), "OfertasFragment")
					.AddToBackStack ("LoginCMFragment")
					.Commit ();	

				break;


			case 1:

				this.Activity.SupportFragmentManager.BeginTransaction ()
					.Replace (Resource.Id.content_frame, SubastaFragment.NewInstance (), "SubastaFragment")
					.AddToBackStack ("LoginCMFragment")
					.Commit ();	

				break;
			default:



				this.Activity.SupportFragmentManager.BeginTransaction ()
					.Replace (Resource.Id.content_frame, InitFragment.NewInstance (dato.Email), "OfertasFragment")
					.AddToBackStack ("LoginCMFragment")
					.Commit ();	

				break;

			}



		}

	}
}

