using System;
using System.Net;
using System.Threading.Tasks;
using Android.Util;
using System.IO;
using Android.Graphics;
using Android.Widget;

namespace PorAka
{
	public class DownloadAsync:IDisposable
	{
		
		string documentsPath;
		string localPath;
	
	
		public 	async void DownloadHistory ( ImageView imageView,string url,ProgressBar Pbar =null)
		{

			if (string.IsNullOrEmpty(url))
				return;


			try {	

				int index = url.LastIndexOf ("/");
				string localFilename = url.Substring (index + 1);
				var webClient = new WebClient ();
				var uri = new Uri (url);
				webClient.DownloadProgressChanged +=   (sender, e) => {if(Pbar!=null)  Pbar.Progress=e.ProgressPercentage; };


				documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);	

				localPath = System.IO.Path.Combine (documentsPath, localFilename);
				var localImage = new Java.IO.File (localPath);

				if (localImage.Exists ()) {

					var imgUri=Android.Net.Uri.Parse("file:"+localPath);
					imageView.SetImageURI(imgUri);

				} else {

					byte[] bytes = null;		
					bytes = await webClient.DownloadDataTaskAsync (uri);

					FileStream fs = new FileStream (localPath, FileMode.Create);
					await fs.WriteAsync (bytes, 0, bytes.Length);
					fs.Close ();	

					var imgUri=Android.Net.Uri.Parse("file:"+localPath);
					imageView.SetImageURI(imgUri);

//					teamBitmap = await BitmapFactory.DecodeByteArrayAsync (bytes, 0, bytes.Length);
//					imageView.SetImageBitmap (teamBitmap);

				}

			



			} catch (TaskCanceledException) {

				return;
			} catch (Exception) {
				return;
			}



		}

		public void Dispose()
		{
			// If this function is being called the user wants to release the
			// resources. lets call the Dispose which will do this for us.
			//Dispose(true);

			// Now since we have done the cleanup already there is nothing left
			// for the Finalizer to do. So lets tell the GC not to call it later.
			GC.SuppressFinalize(this);
		}

	}
}

