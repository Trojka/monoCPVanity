using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Text;
using System.IO;
using Android.Graphics;

namespace be.trojkasoftware.monoCPVanity.Util
{
//	public partial class WebImageRetriever
//	{
////		public Task<Bitmap> GetImageAsync(Uri uri)
////		{
////			return GetImageStreamAsync (uri)
////				.ContinueWith (t => {
////					return BitmapFactory.DecodeStream(t.Result);
////				});
////		}
//
////		public Task<Bitmap> GetImageAsync(Uri uri)
////		{
////			var req = WebRequest.Create (uri);
////
////			var getTask = Task.Factory.FromAsync<WebResponse> (
////				req.BeginGetResponse, req.EndGetResponse, null);
////
////			return getTask.ContinueWith (task => {
////				var res = task.Result;
////				Stream stream = res.GetResponseStream ();
////
////				return BitmapFactory.DecodeStream(stream);
////			});
////		}
//	}
}

