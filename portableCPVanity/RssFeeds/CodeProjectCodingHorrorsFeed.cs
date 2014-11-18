using System;
using System.Collections.Generic;
using be.trojkasoftware.Ripit.Attributes;
using be.trojkasoftware.Ripit.Core;

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

