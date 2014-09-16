using System;
using System.Collections.Generic;
using be.trojkasoftware.Ripit.Core;

namespace be.trojkasoftware.portableCPVanity.RssFeeds
{
	public abstract class CodeProjectRssFeed : List<RSSItem>
	{
		public abstract string Name {
			get;
		}
	}
}

