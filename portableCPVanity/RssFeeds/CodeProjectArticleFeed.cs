using System;
using System.Collections.Generic;
using System.Linq;
using be.trojkasoftware.Ripit.Attributes;
using be.trojkasoftware.Ripit.Core;

namespace be.trojkasoftware.portableCPVanity.RssFeeds
{
	[HttpSource(1, "http://www.codeproject.com/webservices/articlerss.aspx?cat=Id")]
	public class CodeProjectArticleFeed : CodeProjectRssFeed
	{
		public const int DefaultArticleCategory = 1;

		public static List<CodeProjectArticleFeedCategory> Categories {
			get 
			{
				if (mCategories == null) {
					mCategories = new List<CodeProjectArticleFeedCategory> ();
					mCategories.Add(new CodeProjectArticleFeedCategory(){ Id=1, Name="All Articles" });
					mCategories.Add(new CodeProjectArticleFeedCategory(){ Id=22, Name="Android" });
					mCategories.Add(new CodeProjectArticleFeedCategory(){ Id=8, Name="Architect" });
					mCategories.Add(new CodeProjectArticleFeedCategory(){ Id=4, Name="ASP.NET" });
					mCategories.Add(new CodeProjectArticleFeedCategory(){ Id=3, Name="C#" });
					mCategories.Add(new CodeProjectArticleFeedCategory(){ Id=10, Name="Java" });
					mCategories.Add(new CodeProjectArticleFeedCategory(){ Id=12, Name="LAMP" });
					mCategories.Add(new CodeProjectArticleFeedCategory(){ Id=2, Name="MFC/C++" });
					mCategories.Add(new CodeProjectArticleFeedCategory(){ Id=18, Name="Mobile" });
					mCategories.Add(new CodeProjectArticleFeedCategory(){ Id=9, Name="SQL" });
					mCategories.Add(new CodeProjectArticleFeedCategory(){ Id=19, Name="Tech Lead" });
					mCategories.Add(new CodeProjectArticleFeedCategory(){ Id=6, Name="VB.NET" });
				}

				return mCategories;
			}
		}

		public static CodeProjectArticleFeed GetFeed(int id)
		{
			CodeProjectArticleFeed feed = new CodeProjectArticleFeed () { Id = id };
			return feed;
		}

		public static string GetFeedname(int id)
		{
			return Categories.Where (x => x.Id == id).Single ().Name;
		}

		public override string Name
		{
			get {
				return GetFeedname(Id);
			}
		}

		public int Id {
			get;
			set;
		}

		private static List<CodeProjectArticleFeedCategory> mCategories;
	}
}

