using System;
using System.Threading.Tasks;
using Android.Util;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace PorAka
{
	public class Bussines:IDisposable
	{

		static readonly Bussines _instance = new Bussines ();

		public static Bussines Instance {
			get {
				return _instance;
			}
		}

	
		public Bussines ()
		{
		}

		public void Dispose ()
		{
			GC.SuppressFinalize (this);
		}



		public async Task<ResultLogin<Vendors>> LoginVendors (string email, string pwd)
		{
		
			try {
				using (var Client = new  ApiClient ()) {


					var Result = await Client.GetAsync ("Vendors/LoginVendors?email=" + email + "&pwd=" + pwd);

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					var dato = JsonConvert.DeserializeObject<ResultLogin<Vendors>> (ResultJson);

					return dato;

				}
			} catch (Exception) {

				return null;//LoginVendors( email, pwd);
			}

		
		
		}


		public async Task<int> RegisterVendors (Vendors dato)
		{
			try {
				using (var Client = new  ApiClient ()) {
					
					var entity = JsonConvert.SerializeObject (dato);

					var Result = await Client.PostAsync ("Vendors/RegisterVendors", new StringContent (entity, UnicodeEncoding.UTF8, "application/json"));

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;
			
					return JsonConvert.DeserializeObject<int> (ResultJson);

				}
			} catch (Exception) {

				throw;
			
			}
		}

		public async Task<int> UpDateVendors (Vendors dato)
		{
			try {
				using (var Client = new  ApiClient ()) {

					var entity = JsonConvert.SerializeObject (dato);

					var Result = await Client.PostAsync ("Vendors/UpDateVendors", new StringContent (entity, UnicodeEncoding.UTF8, "application/json"));

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					return JsonConvert.DeserializeObject<int> (ResultJson);

				}
			} catch (Exception) {

				throw;

			}
		}


		public async Task<int> RegisterClients (Clients dato)
		{
			try {
				using (var Client = new  ApiClient ()) {

					var entity = JsonConvert.SerializeObject (dato);

					var Result = await Client.PostAsync ("Clients/RegisterClients", new StringContent (entity, UnicodeEncoding.UTF8, "application/json"));

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					return JsonConvert.DeserializeObject<int> (ResultJson);

				}
			} catch (Exception) {

				throw;

			}
		}

		public async Task<int> UpDateClients (Clients dato)
		{
			try {
				using (var Client = new  ApiClient ()) {

					var entity = JsonConvert.SerializeObject (dato);

					var Result = await Client.PostAsync ("Clients/UpDateClients", new StringContent (entity, UnicodeEncoding.UTF8, "application/json"));

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					return JsonConvert.DeserializeObject<int> (ResultJson);

				}
			} catch (Exception) {

				throw;

			}
		}

		public async Task<ResultLogin<Clients>> LoginClients (string email, string pwd)
		{

			try {
				using (var Client = new  ApiClient ()) {


					var Result = await Client.GetAsync ("Clients/LoginClients?email=" + email + "&pwd=" + pwd);

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					var dato = JsonConvert.DeserializeObject<ResultLogin<Clients>> (ResultJson);

					return dato;

				}
			} catch (Exception) {

				return null;//await LoginClients( email, pwd);
			}



		}


		public async Task <Vendors>GetUser (string email)
		{

			try {
				using (var Client = new  ApiClient ()) {


					var Result = await Client.GetAsync ("User/GetUser?email=" + email);

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					var dato = JsonConvert.DeserializeObject<Vendors> (ResultJson);

					return dato;

				}
			} catch (Exception) {

				return null;//await GetUser (email);
			}


		}

		public async Task<List<Categorys>> GetCategorys ()
		{

			try {
				
				using (var Client = new  ApiClient ()) {


					var Result = await Client.GetAsync ("App/GetAllCategorys");

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					var dato = JsonConvert.DeserializeObject<List<Categorys>> (ResultJson);

					return dato;

				}

			
			} catch (Exception) {
				
				return null;
			}
		}

		public async Task<List<Products>> GetProductByIdCategorys (string id)
		{

			try {

				using (var Client = new  ApiClient ()) {


					var Result = await Client.GetAsync ("App/GetProductByIdCategorys?id=" + id);

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					var dato = JsonConvert.DeserializeObject<List<Products>> (ResultJson);

					return dato;

				}


			} catch (Exception) {

				return null;
			}
		}

		public async void SetLikeProductById (string id)
		{

			try {

				using (var Client = new  ApiClient ()) {
					
					await Client.GetAsync ("App/SetLikeProductById?id=" + id);

				}


			} catch (Exception) {

				return;
			}
		}

		public async void SetConsultProduct (string id, string idp)
		{

			try {

				using (var Client = new  ApiClient ()) {

					await Client.GetAsync ("Clients/SetConsultProduct?id=" + id + "&idp=" + idp);

				}


			} catch (Exception) {

				return;
			}
		}



	}

	public  class ApiClient:HttpClient
	{
		public ApiClient ()
		{
			//			var encoder = Encoding.GetEncoding ("ISO-8859-1");
			//			var credentials = Convert.ToBase64String (encoder.GetBytes (string.Format ("{0}:{1}", "Vonline", "Vonline")));

			#if DEBUG
			this.BaseAddress = new Uri ("http://poraka.co/api/");
			this.BaseAddress = new Uri ("http://192.168.0.28/PorAka/api/");
			#else
			this.BaseAddress = new Uri ("http://poraka.co/api/");
			#endif

			//			this.DefaultRequestHeaders.Add ("User-Agent", "Vonline 1.0");
			//			this.DefaultRequestHeaders.Accept.Clear ();
			//			this.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
			//			this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Basic", credentials);
		}


		public Task AsyncPost (string metodo, object dato)
		{

			return PostAsync (metodo, new StringContent (JsonConvert.SerializeObject (dato), UnicodeEncoding.UTF8, "application/json"));
		
		}

		//		public Task AsyncGet(string metodo){
		//
		//
		//			var Result =  GetAsync (metodo).Result;
		//
		//			var ResultJson = Result.Content.ReadAsStringAsync ().Result;
		//
		//			var dato = JsonConvert.DeserializeObject<Vendors> (ResultJson);
		//
		//			return dato;
		//
		//
		//
		//
		//		}


	}

}

