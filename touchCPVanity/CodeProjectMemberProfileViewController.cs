using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using be.trojkasoftware.Ripit.Core;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.monoCPVanity.Data;
using be.trojkasoftware.monoCPVanity.Util;
using System.Threading;
using System.Threading.Tasks;
using be.trojkasoftware.portableCPVanity.ViewModels;

namespace touchCPVanity
{
	public partial class CodeProjectMemberProfileViewController : UIViewController
	{
		public CodeProjectMemberProfileViewController (IntPtr handle) : base (handle)
		{
			viewModel = new CodeProjectMemberProfileViewModel ();

			//viewModel.GravatarLoaded += this.GravatarLoaded;
			viewModel.MemberLoaded += this.MemberLoaded;
		}

		public void SetMemberId(int memberId) {
			viewModel.MemberId = memberId;
		}

//		public int MemberId {
//			get;
//			set;
//		}

//		public CodeProjectMember Member {
//			get;
//			set;
//		}

//		public UIImage Gravatar {
//			get;
//			set;
//		}


		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			progressView = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			progressView.Center = new PointF (this.View.Frame.Width / 2, this.View.Frame.Height / 2);
			this.View.AddSubview (progressView);

			this.SaveBtn.TouchUpInside += HandleTouchUpInside;

			progressView.StartAnimating ();

			var context = TaskScheduler.FromCurrentSynchronizationContext();
			viewModel.LoadMember(context);

//			Dictionary<String, String> param = new Dictionary<string, string> ();
//			param.Add ("Id", MemberId.ToString());
//
//			Member = new CodeProjectMember ();
//			Member.Id = MemberId;
//
//			ObjectBuilder objectBuilder = new ObjectBuilder ();
//			Task<object> fillMemberTask = objectBuilder.FillAsync (Member, param, CancellationToken.None);
//
//			progressView.StartAnimating ();
//
//			var context = TaskScheduler.FromCurrentSynchronizationContext();
//
//			fillMemberTask.Start ();
//			fillMemberTask
//				.ContinueWith (x => LoadGravatar (x.Result as CodeProjectMember))
//				.ContinueWith (x => MemberLoaded (x.Result as CodeProjectMember), context);
		}

		void HandleTouchUpInside (object sender, EventArgs ea) {
			viewModel.SaveMember ();
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			var memberArticlesController = segue.DestinationViewController as CodeProjectMemberArticlesViewController;

			if (memberArticlesController != null) {
				memberArticlesController.SetMember(viewModel.Member);
			}
		}

//		CodeProjectMember /*UIImage*/ LoadGravatar(CodeProjectMember member) {
//
//			//			FileStorageService storage = new FileStorageService ();
//			//			if (storage.FileExists (Member.Id.ToString())) {
//			//				byte[] imageData = storage.ReadBytes (Member.Id.ToString());
//			CodeProjectDatabase db = new CodeProjectDatabase ();
//			byte[] gravatar = db.GetGravatar(Member.Id);
//			if (gravatar != null) {
//
//				Gravatar = UIImage.LoadFromData (NSData.FromArray (gravatar));
//
//			} else {
//				WebImageRetriever imageDownloader = new WebImageRetriever ();
//				Task imageDownload = imageDownloader.GetImageAsync (new Uri (Member.ImageUrl)).ContinueWith (t => {
//
//					NSData imageData = t.Result.AsPNG();
//					gravatar = new byte[imageData.Length];
//					System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, gravatar, 0, Convert.ToInt32(imageData.Length));
//
//					Member.Gravatar = gravatar;
//					Gravatar = t.Result;
//
//				});
//
//				imageDownload.Wait ();
//			}
//
//			return member;
//		}

		void GravatarLoaded(byte[] gravatar) {
//			NSData imageData = t.Result.AsPNG();
//			gravatar = new byte[imageData.Length];
//			System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, gravatar, 0, Convert.ToInt32(imageData.Length));
//
//			Member.Gravatar = gravatar;
//			Gravatar = t.Result;

//			NSData data = NSData.FromArray(gravatar);
//			this.MemberImage.Image = UIImage.LoadFromData (data, 1);

		}

		void MemberLoaded(CodeProjectMember member) {

			//Member = member;

			progressView.StopAnimating ();

			FillScreen (member);
		}

		void FillScreen(CodeProjectMember member) {

			this.MemberNameLbl.Text = member.Name;
			this.MemberReputationLbl.Text = member.Reputation;
			this.ArticleCountLbl.Text = "Articles: " + member.ArticleCount;
			this.AvgArticleRatingLbl.Text = "Average article rating: " + member.AverageArticleRating;
			this.BlogCountLbl.Text = "Blogs: " + member.BlogCount;
			this.AvgBlogRatingLbl.Text = "Average blog rating: " + member.AverageBlogRating;

			if (member.Gravatar != null) {
				NSData data = NSData.FromArray (member.Gravatar);
				this.MemberImage.Image = UIImage.LoadFromData (data, 1);
			}

//			if (Gravatar != null) {
//				this.MemberImage.Image = Gravatar;
//			}
		}

		UIActivityIndicatorView progressView;
		CodeProjectMemberProfileViewModel viewModel;
	}
}

