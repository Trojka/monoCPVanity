using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.portableCPVanity.RssFeeds;
using be.trojkasoftware.Ripit.Core;

namespace touchCPVanity
{
	public partial class CodeProjectArticleRSSViewController : CodeProjectRSSFeedViewController
	{
		public CodeProjectArticleRSSViewController (IntPtr handle) : base (handle)
		{
			viewModel.ItemFeed = CodeProjectArticleFeed.GetFeed(CodeProjectArticleFeed.DefaultArticleCategory);
		}

//		public override Dictionary<string, string> GetBuilderParams() {
//			Dictionary<string, string> paramList = new Dictionary<string, string> ();
//			paramList.Add ("Id", (ItemFeed as CodeProjectArticleFeed).Id.ToString());
//			return paramList;
//		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			if (segue.Identifier == "RSSArticleCategory") {
				var articleCategoryController = segue.DestinationViewController as ArticleCategoryViewController;

				if (articleCategoryController != null) {
					articleCategoryController.FeedReceiver = this;
				}
			}
//			if (segue.Identifier == "RSSArticle") {
//				var webPageController = segue.DestinationViewController as WebPageViewController;
//
//				if (webPageController != null) {
//					webPageController.PageURL = SelectedItem.Link;
//				}
//			}
		}
	}
}

