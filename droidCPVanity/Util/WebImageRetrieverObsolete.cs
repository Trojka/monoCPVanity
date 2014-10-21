//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using Android.App;
//using Android.Content;
//using Android.Graphics;
//using Android.OS;
//using Android.Runtime;
//using Android.Util;
//using Android.Views;
//using Android.Widget;
//using Java.IO;
//
//
//namespace be.trojkasoftware.droidCPVanity.Util
//{
//	//http://stackoverflow.com/questions/2471935/how-to-load-an-imageview-by-url-in-android
//	public class WebImageRetrieverObsolete : AsyncTask<string, int, Bitmap>
//	{
//		ImageView bmImage;
//		string fileName;
//		bool saveFile;
//
//		public WebImageRetrieverObsolete(ImageView bmImage, string fileName, bool saveFile = false) {
//			this.bmImage = bmImage;
//			this.fileName = fileName;
//			this.saveFile = saveFile;
//		}
//
//		protected override Bitmap RunInBackground(params string[] urls) {
//			String urlDisplay = urls[0];
//			Bitmap image = null;
//			try
//			{
//				byte[] data;
//				using(var webClient = new WebClient())
//				{
//					data = webClient.DownloadData(urlDisplay);
//				}
//				image = BitmapFactory.DecodeByteArray(data, 0, data.Length);
//				BitmapFactory.
//			} catch (Exception e) {
//				Log.Error("Error", e.Message);
//			}
//			return image;
//		}
//
//		protected override void OnPostExecute(Bitmap result) {
//			if (saveFile) {
//				var dir = new File (Android.OS.Environment.GetExternalStoragePublicDirectory (Android.OS.Environment.DirectoryPictures), fileName);
//				var parentDir = dir.ParentFile;
//				if (!parentDir.Exists ()) {
//					parentDir.Mkdirs ();
//				}
//
//				try {
//					var os = new System.IO.FileStream (dir.Path, System.IO.FileMode.OpenOrCreate);
//					result.Compress (Bitmap.CompressFormat.Png, 100, os);
//					os.Close ();
//				} catch (Exception e) {
//					Log.Error ("Error", e.Message);
//				}
//			}
//
//			bmImage.SetImageBitmap(result);
//		}	
//	}
//}

