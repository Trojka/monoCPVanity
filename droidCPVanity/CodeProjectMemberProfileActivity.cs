using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
using be.trojkasoftware.monoCPVanity.Util;
using System.IO;

namespace be.trojkasoftware.droidCPVanity
{
	[Activity (Label = "CPVanity", ParentActivity = typeof(MainActivity))]	
	[IntentFilter(new[]{Intent.ActionSearch})]
	[MetaData(("android.app.searchable"), Resource = "@xml/searchable")]
	public class CodeProjectMemberProfileActivity : Activity
	{
		public int MemberId {
			get;
			set;
		}

		public CodeProjectMember Member {
			get;
			set;
		}

		public Bitmap Gravatar {
			get;
			set;
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			ActionBar.SetDisplayHomeAsUpEnabled (true);

			SetContentView (Resource.Layout.CodeProjectMemberProfileLayout);

			memberName = /*profile*/ this.FindViewById<TextView>(Resource.Id.textViewMemberName);
			memberName.Text = "";

			memberReputation = /*profile*/ this.FindViewById<TextView>(Resource.Id.textViewMemberReputation);
			memberReputation.Text = "";

			memberIcon = /*profile*/ this.FindViewById<ImageView> (Resource.Id.imageViewMemberImage);

//			SetContentView (Resource.Layout.CodeProjectMemberLayout);
//
//			pager = this.FindViewById<HorizontalPager>(Resource.Id.memberPager);
//
//			LayoutInflater inflater = this.LayoutInflater;
//
//			profile = inflater.Inflate (Resource.Layout.CodeProjectMemberProfileLayout, null);
//			pager.AddView (profile);
//
//			articles = inflater.Inflate (Resource.Layout.CodeProjectMemberArticlesLayout, null);
//			pager.AddView (articles);
//
//			reputation = inflater.Inflate (Resource.Layout.CodeProjectMemberReputationLayout, null);
//			pager.AddView (reputation);

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

				MemberId = int.Parse (query);
			} else {
				MemberId = intent.Extras.GetInt (MemberIdKey);
			}


			Dictionary<String, String> param = new Dictionary<string, string> ();
			param.Add ("Id", MemberId.ToString());

			Member = new CodeProjectMember ();
			Member.Id = MemberId;

			ObjectBuilder objectBuilder = new ObjectBuilder ();
			Task<object> fillMemberTask = objectBuilder.FillAsync (Member, param, CancellationToken.None);

			var context = TaskScheduler.FromCurrentSynchronizationContext();

			fillMemberTask.Start ();
			fillMemberTask
				.ContinueWith (x => LoadGravatar (x.Result as CodeProjectMember))
				.ContinueWith (x => MemberLoaded (x.Result as CodeProjectMember), context);

//			ObjectBuilder objectBuilder = new ObjectBuilder ();
//
//			objectBuilder.Fill (member, param);
//
//			memberArticles = new CodeProjectMemberArticles ();
//			memberArticles.Id = memberId;
//
//			objectBuilder.FillList (memberArticles, param, () => new CodeProjectMemberArticle());
//
//			FillScreen ();
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
			CodeProjectDatabase db = new CodeProjectDatabase ();
			db.AddMember (Member, false);
		}

		private void GotoMemberArticles()
		{
			var intent = new Intent (this, typeof(CodeProjectMemberArticlesActivity));

			Bundle bundle = new Bundle ();
			bundle.PutInt (CodeProjectMemberProfileActivity.MemberIdKey, Member.Id);
			bundle.PutString (CodeProjectMemberProfileActivity.MemberReputationGraphKey, Member.ReputationGraph);

			intent.PutExtras(bundle);

			StartActivity (intent);
		}

		private CodeProjectMember /*UIImage*/ LoadGravatar(CodeProjectMember member) {

			//			FileStorageService storage = new FileStorageService ();
			//			if (storage.FileExists (Member.Id.ToString())) {
			//				byte[] imageData = storage.ReadBytes (Member.Id.ToString());
			CodeProjectDatabase db = new CodeProjectDatabase ();
			byte[] gravatar = db.GetGravatar(Member.Id);
			if (gravatar != null) {

				//Gravatar = UIImage.LoadFromData (NSData.FromArray (gravatar));
				Gravatar = BitmapFactory.DecodeByteArray (gravatar, 0, gravatar.Length);

			} else {
				WebImageRetriever imageDownloader = new WebImageRetriever ();
				Task imageDownload = imageDownloader.GetImageAsync (new Uri (Member.ImageUrl)).ContinueWith (t => {

					//NSData imageData = t.Result.
					//gravatar = new byte[imageData.Length];
					//System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, gravatar, 0, Convert.ToInt32(imageData.Length));

					MemoryStream stream = new MemoryStream();
					t.Result.Compress(Bitmap.CompressFormat.Png, 0, stream);
					gravatar = stream.ToArray();

					Member.Gravatar = gravatar;
					Gravatar = t.Result;

				});

				imageDownload.Wait ();
			}

			//return image;
			return member;
		}

		void MemberLoaded(CodeProjectMember member) {

			Member = member;
			FillScreen ();
		}

		private void FillScreen()
		{
			memberName.Text = Member.Name;
			memberReputation.Text = Member.Reputation;

			TextView memberArticleCnt = /*profile*/ this.FindViewById<TextView>(Resource.Id.textViewArticleCnt);
			memberArticleCnt.Text = "Articles: " + Member.ArticleCount;

			TextView avgArticleRating = /*profile*/ this.FindViewById<TextView>(Resource.Id.textViewArticleRating);
			avgArticleRating.Text = "Average article rating: " + Member.AverageArticleRating;

			TextView memberBlogCnt = /*profile*/ this.FindViewById<TextView>(Resource.Id.textViewBlogCnt);
			memberBlogCnt.Text = "Blogs: " + Member.BlogCount;

			TextView avgBlogRating = /*profile*/ this.FindViewById<TextView>(Resource.Id.textViewBlogRating);
			avgBlogRating.Text = "Average blog rating: " + Member.AverageBlogRating;

			if (Gravatar != null) {
				memberIcon.SetImageBitmap (Gravatar);
				//this.MemberImage.Image = Gravatar;
			}

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

		public static string MemberIdKey = "CodeProjectMemberId";
		public static string MemberReputationGraphKey = "CodeProjectMemberReputationGraph";

		TextView memberName;
		TextView memberReputation;
		ImageView memberIcon;

		//HorizontalPager pager = null;
		//CodeProjectMember member = null;
		//CodeProjectMemberArticles memberArticles = null;

		//View profile;
		//View articles;
		//View reputation;
	}
}

