using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.portableCPVanity.RssFeeds;

namespace touchCPVanity
{
	public partial class CodeProjectForumRSSViewController : CodeProjectRSSFeedViewController
	{
		public CodeProjectForumRSSViewController (IntPtr handle) : base (handle)
		{
			viewModel.ItemFeed = new CodeProjectLoungeFeed ();
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
		}

	}
}

