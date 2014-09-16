using System;
using System.Collections.Generic;
using be.trojkasoftware.Ripit.Core;
using be.trojkasoftware.Ripit.Attributes;

namespace be.trojkasoftware.portableCPVanity.RssFeeds
{
	[HttpSource(1, "http://www.codeproject.com/webservices/CodingHorrorsRSS.aspx")]
	public class CodeProjectCodingHorrorsFeed : CodeProjectRssFeed
	{
		public override string Name
		{
			get {
				return "Coding Horrors";
			}
		}
	}
}

