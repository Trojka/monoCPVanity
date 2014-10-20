using System;
using System.Drawing;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.portableCPVanity.RssFeeds;
using be.trojkasoftware.Ripit.Core;

namespace touchCPVanity
{
	public partial class CodeProjectForumRSSViewController : CodeProjectRSSFeedViewController
	{
		public CodeProjectForumRSSViewController (IntPtr handle) : base (handle)
		{
			ItemFeed = new CodeProjectLoungeFeed ();
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			if (segue.Identifier == "RSSForumCategory") {
				var forumCategoryController = segue.DestinationViewController as ForumCategoryViewController;

				if (forumCategoryController != null) {
					forumCategoryController.FeedReceiver = this;
				}
			}
			if (segue.Identifier == "RSSForum") {
				var webPageController = segue.DestinationViewController as WebPageViewController;

				if (webPageController != null) {
					webPageController.PageURL = SelectedItem.Link;
				}
			}
		}

	}
}

