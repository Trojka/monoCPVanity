using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Text;
using System.IO;


namespace be.trojkasoftware.monoCPVanity.Util
{
	public partial class WebImageRetriever
	{
		public Task<Stream> GetImageStreamAsync(Uri uri) {
			var req = WebRequest.Create (uri);

			var getTask = Task.Factory.FromAsync<WebResponse> (
				req.BeginGetResponse, req.EndGetResponse, null);

			return getTask.ContinueWith (task => {
				var res = task.Result;
				return res.GetResponseStream ();
			});
		}

	}
}

