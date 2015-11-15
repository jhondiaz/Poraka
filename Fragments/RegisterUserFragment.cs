
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
	public class RegisterUserFragment : BaseFragment
	{
		public static RegisterUserFragment NewInstance (GoFragments go)
		{
			var fragment = new RegisterUserFragment ();
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
			return inflater.Inflate (Resource.Layout.RegisterUserFragment, container, false);
			//Android.Resource.Animation.FadeIn

		}


		public override void OnViewCreated (View view, Bundle savedInstanceState)
		{
			var TxtNombres = view.FindViewById<EditText> (Resource.Id.TxtNombres);
			var TxtEmail = view.FindViewById<EditText> (Resource.Id.TxtEmail);
			var TxtCalular = view.FindViewById<EditText> (Resource.Id.TxtCelular);
			var TxtPwd = view.FindViewById<EditText> (Resource.Id.TxtPwd);
			var BtnRegistar = view.FindViewById<Button> (Resource.Id.BtnRegistar);


			BtnRegistar.Click += async (sender, e) => {
			
				if (string.IsNullOrEmpty (TxtNombres.Text)) {
					TxtNombres.SetError ("Digite su Nombre", null);
					TxtNombres.RequestFocus (); 
					return;
				}

				if (string.IsNullOrEmpty (TxtEmail.Text)) {
					TxtEmail.SetError ("Digite su Email", null);
					TxtEmail.RequestFocus (); 
					return;
				}
			
				if (string.IsNullOrEmpty (TxtCalular.Text)) {
					TxtCalular.SetError ("Digite su # Celular", null);
					TxtCalular.RequestFocus (); 
					return;
				}

				if (string.IsNullOrEmpty (TxtPwd.Text)) {
					TxtPwd.SetError ("Digite su Contrseña", null);
					TxtPwd.RequestFocus (); 
					return;
				}

				_ProgressDialog = ProgressDialog.Show (this.Activity, "", "Registrando los datos...", true);

				try {


					using (var _Bussines = new Bussines ()) {


						var Client = new Clients {
							Id = Guid.NewGuid ().ToString (),
							Name = TxtNombres.Text,
							Phone = TxtCalular.Text,
							Email = TxtEmail.Text,
							Pwd = TxtPwd.Text
						};

						int Result = 0;	


						Result =	await _Bussines.RegisterClients (Client);

						_ProgressDialog.Dismiss ();

						switch (Result) {

						case 0:
							Toast.MakeText (this.Activity, "Errer: Al registrar los datos", ToastLength.Long).Show ();

							break;
						case 1:

							Toast.MakeText (this.Activity, "Los datos se registraron Exitosamente", ToastLength.Long).Show ();

							_DatosUser.SetDatosClients (Client);


							int go = this.Arguments.GetInt ("Go", 0);

							switch (go) {

							case 0:

								this.Activity.SupportFragmentManager.BeginTransaction ()
									.Replace (Resource.Id.content_frame, InitFragment.NewInstance (Client.Email), "OfertasFragment")
									.AddToBackStack ("RegisterUserFragment")
									.Commit ();	

								break;


							case 1:

								this.Activity.SupportFragmentManager.BeginTransaction ()
									.Replace (Resource.Id.content_frame, SubastaFragment.NewInstance (), "SubastaFragment")
									.AddToBackStack ("RegisterUserFragment")
									.Commit ();	

								break;
							default:



								this.Activity.SupportFragmentManager.BeginTransaction ()
									.Replace (Resource.Id.content_frame, InitFragment.NewInstance (Client.Email), "OfertasFragment")
									.AddToBackStack ("RegisterUserFragment")
									.Commit ();	

								break;

							}

							
							break;

						case 2:
							Toast.MakeText (this.Activity, "E-mail: " + TxtEmail.Text + " ya esta Registrado...", ToastLength.Long).Show ();

							break;

						default:
							break;
						}




					}
				} catch (Exception ex) {
					_ProgressDialog.Dismiss ();
					Toast.MakeText (this.Activity, ex.Message, ToastLength.Long).Show ();
					return;
				}

			
			};


			try {
				Account[] accounts = AccountManager.Get (this.Activity).GetAccountsByType ("com.google");

				foreach (Account account in accounts) {

					if (!string.IsNullOrEmpty (account.Name)) {
						TxtEmail.Text = account.Name;

						break;
					}

					//accountsList.add(item);
				}
			} catch (Exception) {
				
			}

			base.OnViewCreated (view, savedInstanceState);
		}

		public override Animation OnCreateAnimation (int transit, bool enter, int nextAnim)
		{
			return AnimationUtils.LoadAnimation (Activity,
				enter ? Android.Resource.Animation.FadeIn : Android.Resource.Animation.SlideOutRight);
		}
	}
}

