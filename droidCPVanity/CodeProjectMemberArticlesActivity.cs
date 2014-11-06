﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.Ripit.Core;
using System.Threading;
using System.Threading.Tasks;
using be.trojkasoftware.portableCPVanity.ViewModels;

namespace be.trojkasoftware.droidCPVanity
{
	[Activity (Label = "CPVanity")]			
	public class CodeProjectMemberArticlesActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.CodeProjectMemberArticlesLayout);

			memberArticlesView = this.FindViewById<ListView>(Resource.Id.listViewArticles);

			// Not really happy with this but it'll have to do
			MemberId = Intent.Extras.GetInt (CodeProjectMemberProfileActivity.MemberIdKey);
			MemberReputationGraph = Intent.Extras.GetString (CodeProjectMemberProfileActivity.MemberReputationGraphKey);

			spinner = this.FindViewById<ProgressBar>(Resource.Id.progressBar1);
			spinner.Visibility = ViewStates.Gone;

			viewModel = new CodeProjectMemberArticlesViewModel ();
			viewModel.ArticlesLoaded += this.ArticlesLoaded;

			spinner.Visibility = ViewStates.Visible;

			var context = TaskScheduler.FromCurrentSynchronizationContext();

			viewModel.MemberId = MemberId;
			viewModel.LoadMemberArticles (context);

//			MemberArticles = new CodeProjectMemberArticles();
//
//			CodeProjectMemberArticles memberArticles = new CodeProjectMemberArticles ();
//			memberArticles.Id = MemberId;
//
//			Dictionary<String, String> param = new Dictionary<string, string> ();
//			param.Add ("Id", MemberId.ToString());
//
//			ObjectBuilder objectBuilder = new ObjectBuilder ();
//			Task<IList<CodeProjectMemberArticle>> loadArticleTask = objectBuilder.FillListAsync (MemberArticles, param, () => new CodeProjectMemberArticle(), CancellationToken.None);
//
//			var context = TaskScheduler.FromCurrentSynchronizationContext();
//
//			loadArticleTask.Start ();
//			loadArticleTask.ContinueWith (x => ArticlesLoaded (x.Result as CodeProjectMemberArticles), context);

		}

		void ArticlesLoaded(/*CodeProjectMemberArticles memberArticles*/) {

			spinner.Visibility = ViewStates.Gone;

			memberArticlesView.Adapter = new CodeProjectMemberArticleAdapter (this, viewModel.MemberArticles);
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater inflater = this.MenuInflater;
			inflater.Inflate(Resource.Menu.codeproject_member_articles_actions, menu);

			return base.OnCreateOptionsMenu (menu);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId) {
			case Resource.Id.action_member_reputation:
				GotoMemberReputation ();
				return true;
			case Android.Resource.Id.Home:
			default:
				return base.OnOptionsItemSelected(item);
			}
		}

		private void GotoMemberReputation()
		{
			var intent = new Intent (this, typeof(CodeProjectMemberReputationActivity));

			Bundle bundle = new Bundle ();
			bundle.PutInt (CodeProjectMemberProfileActivity.MemberIdKey, MemberId);
			bundle.PutString (CodeProjectMemberProfileActivity.MemberReputationGraphKey, MemberReputationGraph);

			intent.PutExtras(bundle);

			StartActivity (intent);
		}

		public int MemberId {
			get;
			set;
		}

		public string MemberReputationGraph {
			get;
			set;
		}

//		public CodeProjectMemberArticles MemberArticles {
//			get;
//			set;
//		}

		ListView memberArticlesView;
		ProgressBar spinner;

		CodeProjectMemberArticlesViewModel viewModel;
	}
}

