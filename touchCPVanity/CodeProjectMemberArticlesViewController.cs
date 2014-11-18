using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.portableCPVanity.ViewModels;

namespace touchCPVanity
{
	public partial class CodeProjectMemberArticlesViewController : UIViewController
	{
		public CodeProjectMemberArticlesViewController (IntPtr handle) : base (handle)
		{
			viewModel = new CodeProjectMemberArticlesViewModel ();
			viewModel.ArticlesLoaded += this.ArticlesLoaded;
		}

		[Export("tableView:cellForRowAtIndexPath:")]
		public UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("ArticleCell");

			(cell.ViewWithTag (100) as UILabel).Text = viewModel.MemberArticles[indexPath.Row].Title;
			(cell.ViewWithTag (101) as UILabel).Text = viewModel.MemberArticles [indexPath.Row].DateUpdated; 

			return cell;
		}

		[Export("tableView:numberOfRowsInSection:")]
		public int RowsInSection (UITableView tableView, int section)
		{
			return viewModel.MemberArticles.Count;
		}

		[Export("tableView:didSelectRowAtIndexPath:")]
		public void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			string articleLink = viewModel.MemberArticles [indexPath.Row].Link;
			UIApplication.SharedApplication.OpenUrl (NSUrl.FromString (articleLink));
			tableView.DeselectRow (indexPath, true);
		}


		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			if (segue.Identifier == "MemberReputationSegue") {

				var memberReputationController = segue.DestinationViewController as CodeProjectMemberReputationViewController;

				if (memberReputationController != null) {
					memberReputationController.SetMemberReputationGraphUrl(viewModel.MemberReputationGraphUrl);
				}
			}
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			progressView = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			progressView.Center = new PointF (this.View.Frame.Width / 2, this.View.Frame.Height / 2);
			this.View.AddSubview (progressView);

			progressView.StartAnimating ();

			var context = TaskScheduler.FromCurrentSynchronizationContext();

			viewModel.LoadMemberArticles (context);
		}

		void ArticlesLoaded() {

			progressView.StopAnimating ();

			MemberArticlesTable.WeakDataSource = this;
			MemberArticlesTable.WeakDelegate = this;

			MemberArticlesTable.ReloadData ();
		}

		public void SetMember(CodeProjectMember member) {
			viewModel.MemberId = member.Id;
			viewModel.MemberReputationGraphUrl = member.ReputationGraph;
		}

		UIActivityIndicatorView progressView;
		CodeProjectMemberArticlesViewModel viewModel;
	}
}

