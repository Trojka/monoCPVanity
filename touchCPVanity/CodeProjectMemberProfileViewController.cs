using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.monoCPVanity.Data;
using be.trojkasoftware.monoCPVanity.Util;
using be.trojkasoftware.portableCPVanity.ViewModels;

namespace touchCPVanity
{
	public partial class CodeProjectMemberProfileViewController : UIViewController
	{
		public CodeProjectMemberProfileViewController (IntPtr handle) : base (handle)
		{
			viewModel = new CodeProjectMemberProfileViewModel ();

			viewModel.MemberLoaded += this.MemberLoaded;
		}

		public void SetMemberId(int memberId) {
			viewModel.MemberId = memberId;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.SaveBtn.TouchUpInside += HandleTouchUpInside;

			progressView = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			progressView.Center = new PointF (this.View.Frame.Width / 2, this.View.Frame.Height / 2);
			this.View.AddSubview (progressView);

			progressView.StartAnimating ();

			var context = TaskScheduler.FromCurrentSynchronizationContext();

			viewModel.LoadMember(context);
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

		void MemberLoaded() {

			progressView.StopAnimating ();

			FillScreen ();
		}

		void FillScreen() {

			this.MemberNameLbl.Text = viewModel.Member.Name;
			this.MemberReputationLbl.Text = viewModel.Member.Reputation;
			this.ArticleCountLbl.Text = "Articles: " + viewModel.Member.ArticleCount;
			this.AvgArticleRatingLbl.Text = "Average article rating: " + viewModel.Member.AverageArticleRating;
			this.BlogCountLbl.Text = "Blogs: " + viewModel.Member.BlogCount;
			this.AvgBlogRatingLbl.Text = "Average blog rating: " + viewModel.Member.AverageBlogRating;

			if (viewModel.Member.Avatar != null) {
				NSData data = NSData.FromArray (viewModel.Member.Avatar);
				this.MemberImage.Image = UIImage.LoadFromData (data, 1);
			}
		}

		UIActivityIndicatorView progressView;
		CodeProjectMemberProfileViewModel viewModel;
	}
}

