using System;
using System.Collections.Generic;
using System.Linq;

namespace be.trojkasoftware.portableCPVanity.RssFeeds
{
	public class CommunityRssCategories
	{
		public CommunityRssCategories ()
		{
			Feeds = new List<CodeProjectRssFeed>();

			Feeds.Add(new CodeProjectLoungeFeed());
			Feeds.Add(new CodeProjectMessagesFeed());
			Feeds.Add(new CodeProjectWickedCodeFeed());
			Feeds.Add(new CodeProjectCodingHorrorsFeed());
		}

		public List<string> Categories {
			get {
				return Feeds.Select (x => x.Name).ToList ();
			}
		}

		public List<CodeProjectRssFeed> Feeds {
			get;
			private set;
		}

		public CodeProjectRssFeed GetFeedForCategory(string categoryName) {
			return Feeds.Where (x => x.Name == categoryName).FirstOrDefault();
		}
	}
}

