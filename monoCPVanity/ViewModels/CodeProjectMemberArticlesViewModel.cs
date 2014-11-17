using System;
using System.Collections.Generic;
using be.trojkasoftware.Ripit.Core;
using System.Threading.Tasks;
using be.trojkasoftware.monoCPVanity.Data;
using be.trojkasoftware.monoCPVanity.Util;
using System.Threading;
using System.IO;
using System.Linq;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
	public delegate void ArticlesLoaded();

	public class CodeProjectMemberArticlesViewModel
	{
		public ArticlesLoaded ArticlesLoaded;

		public int MemberId {
			get;
			set;
		}


		public string MemberReputationGraphUrl {
			get;
			set;
		}

//		public CodeProjectMember Member {
//			get;
//			set;
//		}

		public List<CodeProjectMemberArticleViewModel> MemberArticles {
			get {
				return memberArticles.Select (x => new CodeProjectMemberArticleViewModel (x)).ToList ();
			}
		}

		public void LoadMemberArticles(TaskScheduler uiContext) {
			memberArticles = new CodeProjectMemberArticles();

			Dictionary<String, String> param = new Dictionary<string, string> ();
			param.Add ("Id", MemberId.ToString());

			ObjectBuilder objectBuilder = new ObjectBuilder ();
			Task<IList<CodeProjectMemberArticle>> loadArticleTask = objectBuilder.FillListAsync (memberArticles, param, () => new CodeProjectMemberArticle(), CancellationToken.None);


			loadArticleTask.Start ();
			loadArticleTask.ContinueWith (x => ArticlesLoaded (), uiContext);
		}

		CodeProjectMemberArticles memberArticles;
	}
}

