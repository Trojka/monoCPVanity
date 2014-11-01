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


namespace be.trojkasoftware.droidCPVanity
{
	public class CodeProjectArticleFeedFragment : Fragment
	{
		View view;

//		public override void OnActivityCreated (Bundle savedInstanceState) 
//		{
//			base.OnActivityCreated (savedInstanceState);
//		}

		public CodeProjectRssFeed ItemFeed 
		{
			get;
			set;
		}

//		public virtual Dictionary<string, string> GetBuilderParams() {
//			Dictionary<string, string> paramList = new Dictionary<string, string> ();
//			return paramList;
//		}

		public /*override*/ Dictionary<string, string> GetBuilderParams() {
			Dictionary<string, string> paramList = new Dictionary<string, string> ();
			paramList.Add ("Id", (ItemFeed as CodeProjectArticleFeed).Id.ToString());
			return paramList;
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			view = inflater.Inflate (Resource.Layout.CodeProjectRssFeedLayout, null);

			textView = view.FindViewById<TextView>(Resource.Id.textViewFeedName);
			listView = view.FindViewById<ListView> (Resource.Id.listViewFeed);

			ItemFeed = CodeProjectArticleFeed.GetFeed(CodeProjectArticleFeed.DefaultArticleCategory);

			LoadFeed (ItemFeed);

			return view;
		}

		public void LoadFeed(CodeProjectRssFeed feed) {
			ObjectBuilder builder = new ObjectBuilder ();

			Task<IList<RSSItem>> loadFeedTask = builder.FillFeedAsync (feed, GetBuilderParams(), CancellationToken.None);

			var context = TaskScheduler.FromCurrentSynchronizationContext();

			loadFeedTask.Start ();
			loadFeedTask.ContinueWith (x => FeedLoaded(x.Result), context);
		}

		void FeedLoaded(IList<RSSItem> feed) {

			//progressView.StopAnimating ();

			textView.Text = ItemFeed.Name;
			listView.Adapter = new CodeProjectRssFeedAdapter (this.Activity, ItemFeed);
			//RSSItemTable.Source = new CodeProjectRSSDataSource(ItemFeed);
			//RSSItemTable.ReloadData ();
		}

		TextView textView;
		ListView listView;
	}
}

