using System;
using System.Collections.Generic;
using be.trojkasoftware.Ripit.Core;
using be.trojkasoftware.Ripit.Attributes;

namespace be.trojkasoftware.portableCPVanity.RssFeeds
{
	[HttpSource(1, "http://www.codeproject.com/webservices/WickedCodeRSS.aspx")]
	public class CodeProjectWickedCodeFeed : CodeProjectRssFeed
	{
		public override string Name
		{
			get {
				return "Wicked Code";
			}
		}
	}
}

