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
	public class CodeProjectRSSFeedFragment : Fragment
	{
		View view;

//		public override void OnCreate (Bundle savedInstanceState)
//		{
//			base.OnCreate (savedInstanceState);
//
//			// Create your fragment here
//		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			view = inflater.Inflate (Resource.Layout.CodeProjectRssFeedLayout, null);

			textView = view.FindViewById<TextView>(Resource.Id.textViewFeedName);
			listView = view.FindViewById<ListView> (Resource.Id.listViewFeed);

			viewModel = new CodeProjectRssFeedViewModel ();
			viewModel.FeedLoaded += this.FeedLoaded;


			viewModel.ItemFeed = GetFeed (); //CodeProjectArticleFeed.GetFeed(CodeProjectArticleFeed.DefaultArticleCategory);

			LoadFeed ();

//			textView.Text = viewModel.ItemFeed.Name;
//
//			var context = TaskScheduler.FromCurrentSynchronizationContext();
//			viewModel.LoadFeed (context);

			//			ItemFeed = CodeProjectArticleFeed.GetFeed(CodeProjectArticleFeed.DefaultArticleCategory);

			//			LoadFeed (ItemFeed);

			return view;
		}

		public void LoadFeed() {
			textView.Text = viewModel.ItemFeed.Name;

			var context = TaskScheduler.FromCurrentSynchronizationContext();
			viewModel.LoadFeed (context);
		}

		public virtual CodeProjectRssFeed GetFeed() {
			return null;
		}

		public virtual void SelectCategory() {
		}

		void FeedLoaded(/*IList<RSSItem> feed*/) {

			//progressView.StopAnimating ();

			listView.Adapter = new CodeProjectRssFeedAdapter (this.Activity, viewModel.ItemFeed);
			//RSSItemTable.Source = new CodeProjectRSSDataSource(ItemFeed);
			//RSSItemTable.ReloadData ();
		}

		TextView textView;
		ListView listView;

		protected CodeProjectRssFeedViewModel viewModel;
	}
}

