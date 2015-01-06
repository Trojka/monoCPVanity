using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using be.trojkasoftware.Ripit.Core;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.monoCPVanity.Data;
using be.trojkasoftware.monoCPVanity.Util;
using be.trojkasoftware.portableCPVanity.ViewModels;

namespace be.trojkasoftware.droidCPVanity
{
	[Activity (Label = "CPVanity", ParentActivity = typeof(MainActivity))]	
	[IntentFilter(new[]{Intent.ActionSearch})]
	[MetaData(("android.app.searchable"), Resource = "@xml/searchable")]
	public class CodeProjectMemberProfileActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			ActionBar.SetDisplayHomeAsUpEnabled (true);

			SetContentView (Resource.Layout.CodeProjectMemberProfileLayout);

			memberName = this.FindViewById<TextView>(Resource.Id.textViewMemberName);
			memberName.Text = "";

			memberReputation = this.FindViewById<TextView>(Resource.Id.textViewMemberReputation);
			memberReputation.Text = "";

			memberIcon = this.FindViewById<ImageView> (Resource.Id.imageViewMemberImage);
			memberIcon.SetImageBitmap (null);

			spinner = this.FindViewById<ProgressBar>(Resource.Id.progressBar1);
			spinner.Visibility = ViewStates.Gone;

			viewModel = new CodeProjectMemberProfileViewModel ();
			viewModel.MemberLoaded += this.MemberLoaded;

			HandleIntent(Intent);

		}

		protected override void OnNewIntent(Intent intent)
		{
			Intent = intent;
			HandleIntent(intent);
		}

		private void HandleIntent(Intent intent)
		{
			if (Intent.ActionSearch == intent.Action) {
				String query = intent.GetStringExtra (SearchManager.Query);

				viewModel.MemberId = int.Parse (query);
			} else {
				viewModel.MemberId = intent.Extras.GetInt (MemberIdKey);
			}

			spinner.Visibility = ViewStates.Visible;

			var context = TaskScheduler.FromCurrentSynchronizationContext();

			viewModel.LoadMember(context);
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater inflater = this.MenuInflater;
			inflater.Inflate(Resource.Menu.codeproject_member_profile_actions, menu);

			return base.OnCreateOptionsMenu (menu);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId) {
			case Resource.Id.action_member_add:
				SaveCurrentMember ();
				return true;
			case Resource.Id.action_member_articles:
				GotoMemberArticles ();
				return true;
			default:
				return base.OnOptionsItemSelected(item);
			}
		}

		private void SaveCurrentMember()
		{
			viewModel.SaveMember ();
		}

		private void GotoMemberArticles()
		{
			var intent = new Intent (this, typeof(CodeProjectMemberArticlesActivity));

			Bundle bundle = new Bundle ();
			bundle.PutInt (CodeProjectMemberProfileActivity.MemberIdKey, viewModel.Member.Id);
			bundle.PutString (CodeProjectMemberProfileActivity.MemberReputationGraphKey, viewModel.Member.ReputationGraph);

			intent.PutExtras(bundle);

			StartActivity (intent);
		}

		void MemberLoaded() {

			spinner.Visibility = ViewStates.Gone;

			FillScreen ();
		}

		private void FillScreen()
		{
			memberName.Text = viewModel.Member.Name;
			memberReputation.Text = viewModel.Member.Reputation;

			TextView memberArticleCnt = this.FindViewById<TextView>(Resource.Id.textViewArticleCnt);
			memberArticleCnt.Text = "Articles: " + viewModel.Member.ArticleCount;

			TextView avgArticleRating = this.FindViewById<TextView>(Resource.Id.textViewArticleRating);
			avgArticleRating.Text = "Average article rating: " + viewModel.Member.AverageArticleRating;

			TextView memberBlogCnt = this.FindViewById<TextView>(Resource.Id.textViewBlogCnt);
			memberBlogCnt.Text = "Blogs: " + viewModel.Member.BlogCount;

			TextView avgBlogRating = this.FindViewById<TextView>(Resource.Id.textViewBlogRating);
			avgBlogRating.Text = "Average blog rating: " + viewModel.Member.AverageBlogRating;

			if (viewModel.Member.Avatar != null) {
				Bitmap bitmap = BitmapFactory.DecodeByteArray (viewModel.Member.Avatar, 0, viewModel.Member.Avatar.Length);
				memberIcon.SetImageBitmap (bitmap);
			}
		}

		public static string MemberIdKey = "CodeProjectMemberId";
		public static string MemberReputationGraphKey = "CodeProjectMemberReputationGraph";

		TextView memberName;
		TextView memberReputation;
		ImageView memberIcon;
		ProgressBar spinner;

		CodeProjectMemberProfileViewModel viewModel;
	}
}

