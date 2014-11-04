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
	public delegate void ArticlesLoaded(CodeProjectMemberArticles memberArticles);

	public class CodeProjectMemberArticlesViewModel
	{
		public ArticlesLoaded ArticlesLoaded;

		public CodeProjectMember Member {
			get;
			set;
		}

		public CodeProjectMemberArticles MemberArticles {
			get;
			set;
		}

		public void LoadMemberArticles(TaskScheduler uiContext) {
			MemberArticles = new CodeProjectMemberArticles();

			CodeProjectMemberArticles memberArticles = new CodeProjectMemberArticles ();
			memberArticles.Id = Member.Id;

			Dictionary<String, String> param = new Dictionary<string, string> ();
			param.Add ("Id", Member.Id.ToString());

			ObjectBuilder objectBuilder = new ObjectBuilder ();
			Task<IList<CodeProjectMemberArticle>> loadArticleTask = objectBuilder.FillListAsync (MemberArticles, param, () => new CodeProjectMemberArticle(), CancellationToken.None);


			loadArticleTask.Start ();
			loadArticleTask.ContinueWith (x => ArticlesLoaded (x.Result as CodeProjectMemberArticles), uiContext);
		}
	}
}

