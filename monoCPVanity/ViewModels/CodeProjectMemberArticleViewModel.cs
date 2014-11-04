using System;
using System.Collections.Generic;
using be.trojkasoftware.Ripit.Core;
using System.Threading.Tasks;
using be.trojkasoftware.monoCPVanity.Data;
using be.trojkasoftware.monoCPVanity.Util;
using System.Threading;
using System.IO;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
	public class CodeProjectMemberArticleViewModel
	{
		public CodeProjectMemberArticleViewModel(CodeProjectMemberArticle article) {
			this.article = article;
		}

		public string Title {
			get{ return article.Title; }
		}

		public string DateUpdated {
			get{ return article.DateUpdated.ToString("d MMM yyyy"); }
		}

		public string Link {
			get{ return CodeProjectUrlScheme.BaseUrl + article.Link; }
		}

		CodeProjectMemberArticle article;
	}
}

