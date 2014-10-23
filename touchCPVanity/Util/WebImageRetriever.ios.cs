using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace touchCPVanity.Util
{
	partial class WebImageRetriever
	{

		public Task<UIImage> GetImageAsync(Uri uri)
		{
			return GetImageStreamAsync (uri)
				.ContinueWith (t => {
					NSData data = NSData.FromStream (t.Result);

					return UIImage.LoadFromData (data, 1);
				});

			//			var req = WebRequest.Create (uri);
			//
			//			var getTask = Task.Factory.FromAsync<WebResponse> (
			//				req.BeginGetResponse, req.EndGetResponse, null);
			//
			//			return getTask.ContinueWith (task => {
			//				var res = task.Result;
			//				Stream stream = res.GetResponseStream ();
			//				NSData data = NSData.FromStream (stream);
			//
			//				return UIImage.LoadFromData (data, 1);
			//			});
		}
	}
}

