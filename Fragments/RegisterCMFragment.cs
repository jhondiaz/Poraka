
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
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Accounts;

namespace PorAka
{
	public class RegisterCMFragment : BaseFragment
	{
		const string EMAIL = "EMAIL";
		const string ISUDAPE = "ISUPDATE";
		Vendors UserDatos;
		public static RegisterCMFragment NewInstance (String email, bool IsUpdate)
		{
			var fragment = new RegisterCMFragment ();
			var args = new Bundle ();
			args.PutString (EMAIL, email);
			args.PutBoolean (ISUDAPE, IsUpdate);
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
			return inflater.Inflate (Resource.Layout.RegisterCMFragment, container, false);

			//return base.OnCreateView (inflater, container, savedInstanceState);
		}

		public override void OnViewCreated (View view, Bundle savedInstanceState)
		{
			var TxtNit = view.FindViewById<EditText> (Resource.Id.TxtNit);
			var TxtRSocial = view.FindViewById<EditText> (Resource.Id.TxtRSocial);
			var TxtPCargo = view.FindViewById<EditText> (Resource.Id.TxtPCargo);
			var TxtCelular = view.FindViewById<EditText> (Resource.Id.TxtCelular);
			var TxtCComercial = view.FindViewById<EditText> (Resource.Id.TxtCComercial);
			var TxtCiudad = view.FindViewById<EditText> (Resource.Id.TxtCiudad);
			var TxtAddress = view.FindViewById<EditText> (Resource.Id.TxtAddress);
			var TxtEmail = view.FindViewById<EditText> (Resource.Id.TxtEmail);
			var TxtPwd = view.FindViewById<EditText> (Resource.Id.TxtPwd);
			var ChBoxTerminos = view.FindViewById<CheckBox> (Resource.Id.ChBoxTerminos);
			var BtnRegister = view.FindViewById<Button> (Resource.Id.BtnRegister);

			BtnRegister.Text = "Registrar";

			if (this.Arguments.GetBoolean (ISUDAPE, false)) {
			
				 UserDatos = _DatosUser.GetDatosVendors ();

				if (UserDatos != null) {

					BtnRegister.Text = "Actualizar";

					TxtNit.Text = UserDatos.Nit;
					TxtRSocial.Text = UserDatos.Name;
					TxtPCargo.Text = UserDatos.Contact;
					TxtCelular.Text = UserDatos.Phone;
					TxtCComercial.Text = UserDatos.ShoppingCenter;
					TxtCiudad.Text = UserDatos.City;
					TxtAddress.Text = UserDatos.Address;
					TxtEmail.Text = UserDatos.Email;
					TxtEmail.SetTextColor (Color.Red);
					TxtEmail.Enabled = false;
					TxtNit.Enabled = false;
					TxtPwd.Visibility = ViewStates.Gone;
					ChBoxTerminos.Visibility = ViewStates.Gone;
				}

			}


			TxtEmail.Text = this.Arguments.GetString ("EMAIL");

			BtnRegister.Click += async (sender, e) => {

				if (string.IsNullOrEmpty (TxtNit.Text)) {
					TxtNit.SetError ("Digite su Nit", null);
					TxtNit.RequestFocus (); 
					return;
				}

				if (string.IsNullOrEmpty (TxtRSocial.Text)) {
					TxtRSocial.SetError ("Digite la Razon Social", null);
					TxtRSocial.RequestFocus (); 
					return;
				}

				if (string.IsNullOrEmpty (TxtPCargo.Text)) {
					TxtPCargo.SetError ("Digite su Cargo", null);
					TxtPCargo.RequestFocus (); 
					return;
				}

				if (string.IsNullOrEmpty (TxtCelular.Text)) {
					TxtCelular.SetError ("Digite su Telefono", null);
					TxtCelular.RequestFocus (); 
					return;
				}

				if (string.IsNullOrEmpty (TxtCComercial.Text)) {
					TxtCComercial.SetError ("Digite el Centro Comercial", null);
					TxtCComercial.RequestFocus (); 
					return;
				}

				if (string.IsNullOrEmpty (TxtCiudad.Text)) {
					TxtCiudad.SetError ("Digite la Ciudad", null);
					TxtCiudad.RequestFocus (); 
					return;
				}

				if (string.IsNullOrEmpty (TxtAddress.Text)) {
					TxtAddress.SetError ("Digite la Direccion", null);
					TxtAddress.RequestFocus (); 
					return;
				}

				if (string.IsNullOrEmpty (TxtEmail.Text)) {
					TxtEmail.SetError ("Digite su Email", null);
					TxtEmail.RequestFocus (); 
					return;
				}
				if (!emailIsValid (TxtEmail.Text)) {
					TxtEmail.SetError ("Digite un Email  Valido", null);
					TxtEmail.RequestFocus (); 
					return;
				}



				if (string.IsNullOrEmpty (TxtPwd.Text)) {
					TxtPwd.SetError ("Digite su Contrseña para el ingreso", null);
					TxtPwd.RequestFocus (); 
					return;
				}

				if (!ChBoxTerminos.Checked) {
					Toast.MakeText (this.Activity, "Acepte los Terminos y Condiciones para Continuar", ToastLength.Long).Show ();
					ChBoxTerminos.SetTextColor (Color.Red); 
					return;
				}

				try {

				
					using (var _Bussines = new Bussines ()) {


						var Vendedor = new Vendors {
							Id = Guid.NewGuid ().ToString (),
							Nit = TxtNit.Text,
							Name = TxtRSocial.Text,
							Contact = TxtPCargo.Text,
							Phone = TxtCelular.Text,
							ShoppingCenter = TxtCComercial.Text,
							City = TxtCiudad.Text,
							Address = TxtAddress.Text,
							Email = TxtEmail.Text,
							Pwd = TxtPwd.Text
						};

						int Result =0;	
						_ProgressDialog = ProgressDialog.Show (this.Activity, "", "Registrando los datos...", true);

						if (!this.Arguments.GetBoolean (ISUDAPE, false)) {
						

							 Result =	await _Bussines.RegisterVendors (Vendedor);

						}else{

						    Vendedor.Id= UserDatos.Id;
    						 Result =	await _Bussines.UpDateVendors (Vendedor);
						
						}

						_ProgressDialog.Dismiss ();




						switch (Result) {

						case 0:
							Toast.MakeText (this.Activity, "Errer: Al registrar los datos", ToastLength.Long).Show ();

							break;
						case 1:
							
							Toast.MakeText (this.Activity, "Los datos se registraron Exitosamente", ToastLength.Long).Show ();

							_DatosUser.SetDatosVendors (Vendedor);

							if (!this.Arguments.GetBoolean (ISUDAPE, false)) {
								
							this.Activity.SupportFragmentManager.BeginTransaction ()
								.Replace (Resource.Id.content_frame, OfertasFragment.NewInstance (), "OfertasFragment")
								.AddToBackStack ("RegisterCMFragment")
								.Commit ();
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
					Toast.MakeText (this.Activity, ex.Message, ToastLength.Long).Show ();

					_ProgressDialog.Dismiss ();
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

