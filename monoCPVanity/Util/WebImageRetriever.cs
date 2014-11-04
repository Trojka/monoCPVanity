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
		public Task<byte[]> GetImageStreamAsync(Uri uri) {
			var req = WebRequest.Create (uri);

			var getTask = Task.Factory.FromAsync<WebResponse> (
				req.BeginGetResponse, req.EndGetResponse, null);

			return getTask.ContinueWith (task => {
				var res = task.Result;
				//return res.GetResponseStream ();
				return ReadFully(res.GetResponseStream ());
			});
		}

		public static byte[] ReadFully (Stream stream)
		{
			byte[] buffer = new byte[32768];
			using (MemoryStream ms = new MemoryStream())
			{
				while (true)
				{
					int read = stream.Read (buffer, 0, buffer.Length);
					if (read <= 0)
						return ms.ToArray();
					ms.Write (buffer, 0, read);
				}
			}
		}

	}
}

