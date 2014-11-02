using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using be.trojkasoftware.portableCPVanity.RssFeeds;

namespace touchCPVanity
{
	public partial class ForumCategoryViewController : UITableViewController
	{
		public ForumCategoryViewController (IntPtr handle) : base (handle)
		{
		}

		public IFeedReceiver FeedReceiver {
			get;
			set;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return categories.Categories.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("RSSForumCategoryCell");

			(cell.ViewWithTag (nameTag) as UILabel).Text = categories.Categories[indexPath.Row];

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			TableView.DeselectRow (indexPath, false);
			this.FeedReceiver.SetItemFeed(categories.GetFeedForCategory(categories.Categories[indexPath.Row]));

			this.NavigationController.PopViewControllerAnimated (true);
		}

		private static int nameTag = 100;

		private CommunityRssCategories categories = new CommunityRssCategories();
	}
}

