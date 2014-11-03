using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.portableCPVanity.RssFeeds;
using be.trojkasoftware.Ripit.Core;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
	public delegate void FeedLoaded(IList<RSSItem> FeedLoaded);

	public class CodeProjectRssFeedViewModel
	{
		public FeedLoaded FeedLoaded;

		public CodeProjectRssFeed ItemFeed 
		{
			get;
			set;
		}

		public virtual Dictionary<string, string> GetBuilderParams() {
			Dictionary<string, string> paramList = new Dictionary<string, string> ();
			return paramList;
		}

		public void ReloadData()
		{
			LoadFeed (/*ItemFeed*/);
		}

		public void LoadFeed(/*CodeProjectRssFeed feed*/) {
			ObjectBuilder builder = new ObjectBuilder ();

			Task<IList<RSSItem>> loadFeedTask = builder.FillFeedAsync (ItemFeed, GetBuilderParams(), CancellationToken.None);

			var context = TaskScheduler.FromCurrentSynchronizationContext();

			loadFeedTask.Start ();
			loadFeedTask.ContinueWith (x => FeedLoaded(x.Result), context);
		}

//		void FeedLoaded(IList<RSSItem> feed) {
//
//			//progressView.StopAnimating ();
//
//			//RSSItemTable.Source = new CodeProjectRSSDataSource(ItemFeed);
//			//RSSItemTable.ReloadData ();
//		}
	}
}

