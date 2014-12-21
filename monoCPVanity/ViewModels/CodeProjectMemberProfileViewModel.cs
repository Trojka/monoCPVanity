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
	public delegate void MemberLoaded();

	public partial class CodeProjectMemberProfileViewModel
	{
		public MemberLoaded MemberLoaded;

		public int MemberId {
			get;
			set;
		}

		public CodeProjectMember Member {
			get;
			private set;
		}

//		public void OnLoaded() {
//		}

		public void SaveMember() {
			CodeProjectDatabase db = new CodeProjectDatabase ();
			db.AddMember (Member);
		}

		public void LoadMember(TaskScheduler uiContext) {
			Dictionary<String, String> param = new Dictionary<string, string> ();
			param.Add ("Id", MemberId.ToString());

			Member = new CodeProjectMember ();
			Member.Id = MemberId;

			ObjectBuilder objectBuilder = new ObjectBuilder ();
			Task<object> fillMemberTask = objectBuilder.FillAsync (Member, param, CancellationToken.None);

			fillMemberTask.Start ();
			fillMemberTask
				.ContinueWith (x => LoadGravatar ())
				.ContinueWith (x => MemberLoaded (), uiContext);
		}

		CodeProjectMember LoadGravatar() {

			var db = new CodeProjectDatabase ();
			byte[] gravatar = db.GetGravatar(Member.Id);
			if (gravatar != null) {

			} else {
				WebImageRetriever imageDownloader = new WebImageRetriever ();
				Task imageDownload = imageDownloader.GetImageStreamAsync (new Uri (Member.ImageUrl)).ContinueWith (t => {

					gravatar = t.Result;
				});

				imageDownload.Wait ();
			}

			Member.Gravatar = gravatar;
			return Member;
		}
	}
}

