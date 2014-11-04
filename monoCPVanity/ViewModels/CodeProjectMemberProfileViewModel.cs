using System;
using System.Collections.Generic;
using be.trojkasoftware.Ripit.Core;
using System.Threading.Tasks;
using be.trojkasoftware.monoCPVanity.Data;
using be.trojkasoftware.monoCPVanity.Util;
using System.Threading;
using System.IO;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
	//public delegate void GravatarLoaded(byte[] gravatar);

	public delegate void MemberLoaded(/*CodeProjectMember member*/);

	public class CodeProjectMemberProfileViewModel
	{
		//public GravatarLoaded GravatarLoaded;

		public MemberLoaded MemberLoaded;

		public int MemberId {
			get;
			set;
		}

		public CodeProjectMember Member {
			get;
			set;
		}

		public void OnLoaded() {
		}

		public void SaveMember() {
			CodeProjectDatabase db = new CodeProjectDatabase ();
			db.AddMember (Member, false);
		}

		public void LoadMember(TaskScheduler uiContext) {
			Dictionary<String, String> param = new Dictionary<string, string> ();
			param.Add ("Id", MemberId.ToString());

			Member = new CodeProjectMember ();
			Member.Id = MemberId;

			ObjectBuilder objectBuilder = new ObjectBuilder ();
			Task<object> fillMemberTask = objectBuilder.FillAsync (Member, param, CancellationToken.None);

			//progressView.StartAnimating ();

			//var context = TaskScheduler.FromCurrentSynchronizationContext();

			fillMemberTask.Start ();
			fillMemberTask
				.ContinueWith (x => LoadGravatar (/*x.Result as CodeProjectMember*/))
				.ContinueWith (x => MemberLoaded (/*x.Result*/), uiContext);
		}

		CodeProjectMember /*UIImage*/ LoadGravatar(/*CodeProjectMember member*/) {

			//			FileStorageService storage = new FileStorageService ();
			//			if (storage.FileExists (Member.Id.ToString())) {
			//				byte[] imageData = storage.ReadBytes (Member.Id.ToString());
			var db = new CodeProjectDatabase ();
			byte[] gravatar = db.GetGravatar(Member.Id);
			if (gravatar != null) {

//				GravatarLoaded (gravatar);
//				Gravatar = UIImage.LoadFromData (NSData.FromArray (gravatar));

			} else {
				WebImageRetriever imageDownloader = new WebImageRetriever ();
				Task imageDownload = imageDownloader.GetImageStreamAsync (new Uri (Member.ImageUrl)).ContinueWith (t => {

					gravatar = t.Result; //ReadFully(t.Result);
//					GravatarLoaded (ReadFully(t.Result));

//					NSData imageData = t.Result.AsPNG();
//					gravatar = new byte[imageData.Length];
//					System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, gravatar, 0, Convert.ToInt32(imageData.Length));
//
//					Member.Gravatar = gravatar;
//					Gravatar = t.Result;

				});

				imageDownload.Wait ();
			}

			Member.Gravatar = gravatar;
			return Member;
		}

//		public static byte[] ReadFully (Stream stream)
//		{
//			byte[] buffer = new byte[32768];
//			using (MemoryStream ms = new MemoryStream())
//			{
//				while (true)
//				{
//					int read = stream.Read (buffer, 0, buffer.Length);
//					if (read <= 0)
//						return ms.ToArray();
//					ms.Write (buffer, 0, read);
//				}
//			}
//		}
	}
}

