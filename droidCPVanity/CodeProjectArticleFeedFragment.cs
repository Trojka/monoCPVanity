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
using Android.Util;
using Android.Views;
using Android.Widget;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.portableCPVanity.RssFeeds;
using be.trojkasoftware.Ripit.Core;
using be.trojkasoftware.portableCPVanity.ViewModels;

namespace be.trojkasoftware.droidCPVanity
{
	public class CodeProjectArticleFeedFragment : CodeProjectRSSFeedFragment
	{

		public override CodeProjectRssFeed GetFeed()
		{
			return CodeProjectArticleFeed.GetFeed(CodeProjectArticleFeed.DefaultArticleCategory);
		}

		public override void SelectCategory() {
			AlertDialog.Builder builderSingle = new AlertDialog.Builder(this.Activity);
			builderSingle.SetTitle ("Select Category");

			ArrayAdapter<String> arrayAdapter = new ArrayAdapter<String>(
				this.Activity,
				Android.Resource.Layout.SelectDialogSingleChoice);
			//arrayAdapter.AddAll (CodeProjectArticleFeed.Categories.Select(x => new CodeProjectArticleFeedCategoryViewModel(x)).ToList());
			arrayAdapter.AddAll (CodeProjectArticleFeed.Categories);

			builderSingle.SetAdapter (arrayAdapter, this.ItemClicked);
			builderSingle.Show ();
		}

		void ItemClicked(object source, DialogClickEventArgs eventArgs)
		{
			viewModel.ItemFeed = (CodeProjectArticleFeed.GetFeed(CodeProjectArticleFeed.Categories [eventArgs.Which].Id));

			LoadFeed ();
		}

//		public override void OnActivityCreated (Bundle savedInstanceState) 
//		{
//			base.OnActivityCreated (savedInstanceState);
//		}

//		public CodeProjectRssFeed ItemFeed 
//		{
//			get;
//			set;
//		}

//		public virtual Dictionary<string, string> GetBuilderParams() {
//			Dictionary<string, string> paramList = new Dictionary<string, string> ();
//			return paramList;
//		}

//		public /*override*/ Dictionary<string, string> GetBuilderParams() {
//			Dictionary<string, string> paramList = new Dictionary<string, string> ();
//			paramList.Add ("Id", (ItemFeed as CodeProjectArticleFeed).Id.ToString());
//			return paramList;
//		}

//		public void LoadFeed(CodeProjectRssFeed feed) {
//			ObjectBuilder builder = new ObjectBuilder ();
//
//			Task<IList<RSSItem>> loadFeedTask = builder.FillFeedAsync (feed, GetBuilderParams(), CancellationToken.None);
//
//			var context = TaskScheduler.FromCurrentSynchronizationContext();
//
//			loadFeedTask.Start ();
//			loadFeedTask.ContinueWith (x => FeedLoaded(x.Result), context);
//		}
	}
}

