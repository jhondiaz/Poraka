using System;
using Android.Content;
using Android.Preferences;
using Android.App;
using Newtonsoft.Json;

namespace PorAka
{
	public class DatosUser
	{

		string PackageName;

		private static ISharedPreferences SharedPreferences { get; set; }

		private static ISharedPreferencesEditor SharedPreferencesEditor { get; set; }

		public DatosUser ()
		{	
			SharedPreferences = PreferenceManager.GetDefaultSharedPreferences (Application.Context);
			SharedPreferencesEditor = SharedPreferences.Edit ();
			PackageName = Application.Context.ApplicationInfo.PackageName;
		}


		public	void  SetDatosVendors (Vendors datos)
		{
			SharedPreferencesEditor.PutString (PackageName + "DatosVendors", JsonConvert.SerializeObject (datos));
			SharedPreferencesEditor.Commit ();
		}

		public	Vendors  GetDatosVendors ()
		{
			return JsonConvert.DeserializeObject<Vendors> (SharedPreferences.GetString (PackageName + "DatosVendors", ""));
		}

		public	void  SetDatosClients (Clients datos)
		{
			SharedPreferencesEditor.PutString (PackageName + "DatosClients", JsonConvert.SerializeObject (datos));
			SharedPreferencesEditor.Commit ();
		}

		public	Clients  GetDatosClients ()
		{
			return JsonConvert.DeserializeObject<Clients> (SharedPreferences.GetString (PackageName + "DatosClients", ""));
		}

		public	void  SetDatosProduct (Products datos)
		{
			SharedPreferencesEditor.PutString (PackageName + "Products", JsonConvert.SerializeObject (datos));
			SharedPreferencesEditor.Commit ();
		}

		public	Products  GetDatosProduct ()
		{
			return JsonConvert.DeserializeObject<Products> (SharedPreferences.GetString (PackageName + "Products", ""));
		}





	}
}

