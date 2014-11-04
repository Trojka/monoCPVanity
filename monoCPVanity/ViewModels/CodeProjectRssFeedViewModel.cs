﻿using System;
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
			if(ItemFeed is CodeProjectArticleFeed)
				paramList.Add ("Id", (ItemFeed as CodeProjectArticleFeed).Id.ToString());
			return paramList;
		}

//		public void ReloadData(TaskScheduler uiContext)
//		{
//			LoadFeed (uiContext);
//		}

		public void LoadFeed(TaskScheduler uiContext/*CodeProjectRssFeed feed*/) {
			ObjectBuilder builder = new ObjectBuilder ();

			Task<IList<RSSItem>> loadFeedTask = builder.FillFeedAsync (ItemFeed, GetBuilderParams(), CancellationToken.None);

			//var context = TaskScheduler.FromCurrentSynchronizationContext();

			loadFeedTask.Start ();
			loadFeedTask.ContinueWith (x => FeedLoaded(x.Result), uiContext);
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
