using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using be.trojkasoftware.Ripit.Core;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.monoCPVanity.Data;
using touchCPVanity.Util;
using System.Threading.Tasks;

namespace touchCPVanity
{
	public partial class CodeProjectMemberProfileViewController : UIViewController
	{
		public CodeProjectMemberProfileViewController (IntPtr handle) : base (handle)
		{
		}

		public int MemberId {
			get;
			set;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.SaveBtn.TouchUpInside += HandleTouchUpInside;


			Dictionary<String, String> param = new Dictionary<string, string> ();
			param.Add ("Id", MemberId.ToString());

			Member = new CodeProjectMember ();
			Member.Id = MemberId;

			ObjectBuilder objectBuilder = new ObjectBuilder ();
			objectBuilder.Fill (Member, param);

			FillScreen ();
		}

		void HandleTouchUpInside (object sender, EventArgs ea) {

			CodeProjectDatabase db = new CodeProjectDatabase ();
			db.AddMember (Member, false);

		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			var memberArticlesController = segue.DestinationViewController as CodeProjectMemberArticlesViewController;

			if (memberArticlesController != null) {
				memberArticlesController.Member = Member;
			}
		}

		void FillScreen() {

			this.MemberNameLbl.Text = Member.Name;
			this.MemberReputationLbl.Text = Member.Reputation;
			this.ArticleCountLbl.Text = "Articles: " + Member.ArticleCount;
			this.AvgArticleRatingLbl.Text = "Average article rating: " + Member.AverageArticleRating;
			this.BlogCountLbl.Text = "Blogs: " + Member.BlogCount;
			this.AvgBlogRatingLbl.Text = "Average blog rating: " + Member.AverageBlogRating;

			FileStorageService storage = new FileStorageService ();
			if (storage.FileExists (Member.Id.ToString())) {
				byte[] imageData = storage.ReadBytes (Member.Id.ToString());

				UIImage image = UIImage.LoadFromData (NSData.FromArray (imageData));
				this.MemberImage.Image = image;

			} else {
				WebImageRetriever imageDownloader = new WebImageRetriever ();
				imageDownloader.GetImageAsync (new Uri (Member.ImageUrl)).ContinueWith (t => {

					NSData imageData = t.Result.AsPNG();
					byte[] dataBytes = new byte[imageData.Length];
					System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, dataBytes, 0, Convert.ToInt32(imageData.Length));
					storage.WriteBytes(dataBytes, Member.Id.ToString());

					this.MemberImage.Image = t.Result;

				}, TaskScheduler.FromCurrentSynchronizationContext ());
			}
		}

		public CodeProjectMember Member {
			get;
			set;
		}
	}
}

