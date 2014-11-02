using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.portableCPVanity.RssFeeds;
using be.trojkasoftware.Ripit.Core;
using be.trojkasoftware.portableCPVanity.ViewModels;

namespace touchCPVanity
{
	public interface IFeedReceiver
	{
		void SetItemFeed (CodeProjectRssFeed feed);
	}

	public abstract partial class CodeProjectRSSFeedViewController : UIViewController, IFeedReceiver
	{
		public CodeProjectRSSFeedViewController (IntPtr handle) : base (handle)
		{
			viewModel = new CodeProjectRssFeedViewModel ();
			viewModel.FeedLoaded += this.FeedLoaded;
		}

		public void SetItemFeed (CodeProjectRssFeed feed) {
		}

//		public CodeProjectRssFeed ItemFeed 
//		{
//			get;
//			set;
//		}

//		public virtual Dictionary<string, string> GetBuilderParams() {
//			Dictionary<string, string> paramList = new Dictionary<string, string> ();
//			return paramList;
//		}

//		public void LoadFeed(CodeProjectRssFeed feed) {
//			progressView.StartAnimating ();
//
//			ObjectBuilder builder = new ObjectBuilder ();
//
//			Task<IList<RSSItem>> loadFeedTask = builder.FillFeedAsync (feed, GetBuilderParams(), CancellationToken.None);
//
//			var context = TaskScheduler.FromCurrentSynchronizationContext();
//
//			loadFeedTask.Start ();
//			loadFeedTask.ContinueWith (x => FeedLoaded(x.Result), context);
//		}

		void FeedLoaded(IList<RSSItem> feed) {

			progressView.StopAnimating ();

			RSSItemTable.Source = new CodeProjectRSSDataSource(viewModel.ItemFeed);
			RSSItemTable.ReloadData ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

//			RSSItemTable.Source = new CodeProjectRSSDataSource(ItemFeed);
		}

		public override void ViewDidAppear (bool animated)
		{
			progressView = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			progressView.Center = new PointF (this.View.Frame.Width / 2, this.View.Frame.Height / 2);
			this.View.AddSubview (progressView);

			CategoryLabel.Text = viewModel.ItemFeed.Name;

			progressView.StartAnimating ();
			viewModel.ReloadData ();

//			ReloadData ();
		}

//		public void ReloadData()
//		{
//			LoadFeed (ItemFeed);
//		}

		public RSSItem SelectedItem {
			get {
				return viewModel.ItemFeed [RSSItemTable.IndexPathForSelectedRow.Row];
			}
		}

//		protected string CategoryText {
//			get { return CategoryLabel.Text; }
//			set { CategoryLabel.Text = value; }
//		}

		protected CodeProjectRssFeedViewModel viewModel;

		UIActivityIndicatorView progressView;

	}
}

