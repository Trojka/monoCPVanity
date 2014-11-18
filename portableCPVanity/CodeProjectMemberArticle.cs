using System;
using be.trojkasoftware.Ripit.Attributes;

namespace be.trojkasoftware.portableCPVanity
{
	public class CodeProjectMemberArticle
	{
		[SourceRef(0)]
		[PropertyCapture(@"<a id=""ctl\d*_MC_.R_ctl\d*_CAR_Title[\s\S]*?href=""([\s\S]*?)\"">([\s\S]*?)</a>", 1, false)]
		public string Link {
			get;
			set;
		}

		[SourceRef(0)]
		[PropertyCapture(@"<a id=""ctl\d*_MC_.R_ctl\d*_CAR_Title[\s\S]*?href=""([\s\S]*?)\"">([\s\S]*?)</a>", 2, false)]
		public string Title {
			get;
			set;
		}

		[SourceRef(0)]
		[PropertyCapture(@"<span id=""ctl\d*_MC_.R_ctl\d*_CAR_Description"">([\s\S]*?)</span>", 1, false)]
		public string Description {
			get;
			set;
		}

		[SourceRef(0)]
		[PropertyCapture(@"pdated: <b>([123]?\d [A-z]* 20[012]\d)", 1, false)]
		public DateTime DateUpdated {
			get;
			set;
		}

		[SourceRef(0)]
		[PropertyCapture(@"ating.*?: ([0-9./]*)", 1, false)]
		public string Rating {
			get;
			set;
		}

		[SourceRef(0)]
		[PropertyCapture(@"ote.*?: ([0-9]*)", 1, false)]
		public string Votes {
			get;
			set;
		}

	}
}

