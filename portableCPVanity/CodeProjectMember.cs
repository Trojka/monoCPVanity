using System;
using be.trojkasoftware.Ripit.Attributes;

namespace be.trojkasoftware.portableCPVanity
{
	[HttpSource(1, "http://www.codeproject.com/script/Articles/MemberArticles.aspx?amid=Id")]
	[HttpSource(2, "http://www.codeproject.com/script/Membership/view.aspx?mid=Id")]
	public class CodeProjectMember
	{
		public int Id {
			get;
			set;
		}

		public bool IsMe
		{
			get;
			set;
		}

		[SourceRef(1)]
		// Articles by Serge Desmedt (Articles: 6, Technical Blogs: 2)
		[PropertyCapture(@"rticles by ([^\(]*)\(", 1, false)]
		public string Name {
			get;
			set;
		}

		[SourceRef(1)]
		// Articles by Serge Desmedt (Articles: 6, Technical Blogs: 2)
		[PropertyCapture(@"rticles by [^\(]*\([Aa]rticles?: ?([0-9]*)", 1, true)]
		[DefaultValue("0")]
		public string ArticleCount {
			get;
			set;
		}

		[SourceRef(1)]
		// Articles by Serge Desmedt (Articles: 6, Technical Blogs: 2)
		[PropertyCapture(@"rticles by [^\(]*\(([Aa]rticles?: ?[0-9]*, ?)?[Tt]echnical [Bb]logs?: ?([0-9]*)\)", 2, true)]
		[DefaultValue("0")]
		public string BlogCount {
			get;
			set;
		}

		[SourceRef(1)]
		// Average article rating: 4.66
		[PropertyCapture(@"verage article rating: ([0-9.]*)", 1, true)]
		[DefaultValue("-")]
		public string AverageArticleRating {
			get;
			set;
		}

		[SourceRef(1)]
		// Average blogs rating: 5.00
		[PropertyCapture(@"verage blogs rating: ([0-9.]*)", 1, true)]
		[DefaultValue("-")]
		public string AverageBlogRating {
			get;
			set;
		}

		[SourceRef(2)]
		// <span id="ctl00_MC_Prof_TotalRep" class="large-text">5,072</span>
		[PropertyCapture(@"span id=""ctl\d*_MC_Prof_TotalRep[\s\S]*?>([0-9,]*)", 1, false)]
		public string Reputation {
			get;
			set;
		}

		public string ReputationGraph {
			get {
				return "http://www.codeproject.com/script/Reputation/ReputationGraph.aspx?mid=" + Id;
			}
		}

		[SourceRef(2)]
		// <img id="ctl00_MC_Prof_MemberImage" class="padded-top" src="/script/Membership/Images/member_unknown.gif"
		[PropertyCapture(@"<img id=""ctl\d*_MC_Prof_MemberImage[\s\S]*?src=""([\s\S]*?)""", 1, false)]
		public string ImageUrl {
			get;
			set;
		}
	}
}

