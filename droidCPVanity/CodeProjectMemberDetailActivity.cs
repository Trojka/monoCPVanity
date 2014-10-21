using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;

using be.trojkasoftware.Ripit.Core;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.monoCPVanity.Data;
using Android.Graphics;
using Android.Util;
using be.trojkasoftware.droidCPVanity.Util;

namespace be.trojkasoftware.droidCPVanity
{
	[Activity (Label = "droidCPVanity", ParentActivity = typeof(MainActivity))]	
	[IntentFilter(new[]{Intent.ActionSearch})]
	[MetaData(("android.app.searchable"), Resource = "@xml/searchable")]
	public class CodeProjectMemberDetailActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			ActionBar.SetDisplayHomeAsUpEnabled (true);

			SetContentView (Resource.Layout.CodeProjectMemberLayout);

			pager = this.FindViewById<HorizontalPager>(Resource.Id.memberPager);

			LayoutInflater inflater = this.LayoutInflater;

			profile = inflater.Inflate (Resource.Layout.CodeProjectMemberProfileLayout, null);
			pager.AddView (profile);

			articles = inflater.Inflate (Resource.Layout.CodeProjectMemberArticlesLayout, null);
			pager.AddView (articles);

			reputation = inflater.Inflate (Resource.Layout.CodeProjectMemberReputationLayout, null);
			pager.AddView (reputation);

			HandleIntent(Intent);

		}

		protected override void OnNewIntent(Intent intent)
		{
			Intent = intent;
			HandleIntent(intent);
		}

		private void HandleIntent(Intent intent)
		{
			int memberId;
			if (Intent.ActionSearch == intent.Action) {
				String query = intent.GetStringExtra (SearchManager.Query);

				memberId = int.Parse (query);
			} else {
				memberId = intent.Extras.GetInt (MemberId);
			}

			ObjectBuilder objectBuilder = new ObjectBuilder ();

			Dictionary<String, String> param = new Dictionary<string, string> ();
			param.Add ("Id", memberId.ToString());

			member = new CodeProjectMember ();
			member.Id = memberId;

			objectBuilder.Fill (member, param);

			memberArticles = new CodeProjectMemberArticles ();
			memberArticles.Id = memberId;

			objectBuilder.FillList (memberArticles, param, () => new CodeProjectMemberArticle());

			FillScreen ();
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater inflater = this.MenuInflater;
			inflater.Inflate(Resource.Menu.codeproject_member_actions, menu);

			return base.OnCreateOptionsMenu (menu);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId) {
			case Resource.Id.action_member_add:
				CodeProjectDatabase db = new CodeProjectDatabase ();
				db.AddMember (member, false);
				return true;
			default:
				return base.OnOptionsItemSelected(item);
			}
		}

		private void FillScreen()
		{
			TextView memberName = profile.FindViewById<TextView>(Resource.Id.textViewMemberName);
			if(member != null)
				memberName.Text = member.Name;

			TextView memberReputation = profile.FindViewById<TextView>(Resource.Id.textViewMemberReputation);
			if(member != null)
				memberReputation.Text = member.Reputation;

			TextView memberArticleCnt = profile.FindViewById<TextView>(Resource.Id.textViewArticleCnt);
			if(member != null)
				memberArticleCnt.Text = "Articles: " + member.ArticleCount;

			TextView avgArticleRating = profile.FindViewById<TextView>(Resource.Id.textViewArticleRating);
			if(member != null)
				avgArticleRating.Text = "Average article rating: " + member.AverageArticleRating;

			TextView memberBlogCnt = profile.FindViewById<TextView>(Resource.Id.textViewBlogCnt);
			if(member != null)
				memberBlogCnt.Text = "Blogs: " + member.BlogCount;

			TextView avgBlogRating = profile.FindViewById<TextView>(Resource.Id.textViewBlogRating);
			if(member != null)
				avgBlogRating.Text = "Average blog rating: " + member.AverageBlogRating;

//			string memberIconFilenam = member.Id.ToString () + ".png";
//			ImageView memberIcon = profile.FindViewById<ImageView> (Resource.Id.imageViewMemberImage);
//			var dir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), memberIconFilenam);
//			if (!dir.Exists ()) {
//				new WebImageRetriever (memberIcon, memberIconFilenam, true)
//					.Execute (member.ImageUrl);
//			} else {
//				try {
//					var bitmap = BitmapFactory.DecodeFile (dir.AbsolutePath);
//					memberIcon.SetImageBitmap (bitmap);
//				} catch (Exception e) {
//					Log.Error ("Error", e.Message);
//				}
//			}
//
//			ListView articleList = articles.FindViewById<ListView>(Resource.Id.listViewArticles);
//			articleList.Adapter = new CodeProjectMemberArticleAdapter (this, memberArticles);
//
//			ImageView memberReputationGraph = reputation.FindViewById<ImageView> (Resource.Id.imageViewReputationGraph);
//			new WebImageRetriever (memberReputationGraph, null, false)
//				.Execute (member.ReputationGraph);

		}

		public static string MemberId = "CodeProjectMemberId";

		HorizontalPager pager = null;
		CodeProjectMember member = null;
		CodeProjectMemberArticles memberArticles = null;

		View profile;
		View articles;
		View reputation;
	}
}

