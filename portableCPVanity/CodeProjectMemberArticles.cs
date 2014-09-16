using System;
using System.Collections.Generic;
using be.trojkasoftware.Ripit.Attributes;

namespace be.trojkasoftware.portableCPVanity
{
	[HttpSource(0, "http://www.codeproject.com/script/Articles/MemberArticles.aspx?amid=Id")]
	[CollectionCapture(@"<tr id=""ctl\d*_MC_.R_ctl\d*_CAR_MainArticleRow[\s\S]*?</table>[\s\S]*?</tr>")]
	public class CodeProjectMemberArticles : List<CodeProjectMemberArticle>
	{
		public int Id {
			get;
			set;
		}
	}
}

