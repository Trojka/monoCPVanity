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
			viewModel.ItemFeed = feed;
		}

		void FeedLoaded() {

			progressView.StopAnimating ();

			RSSItemTable.Source = new CodeProjectRSSDataSource(viewModel.ItemFeed);
			RSSItemTable.ReloadData ();
		}

		public override void ViewDidAppear (bool animated)
		{
			progressView = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			progressView.Center = new PointF (this.View.Frame.Width / 2, this.View.Frame.Height / 2);
			this.View.AddSubview (progressView);

			CategoryLabel.Text = viewModel.ItemFeed.Name;

			progressView.StartAnimating ();

			var context = TaskScheduler.FromCurrentSynchronizationContext();

			viewModel.LoadFeed (context);
		}

		public RSSItem SelectedItem {
			get {
				return viewModel.ItemFeed [RSSItemTable.IndexPathForSelectedRow.Row];
			}
		}

		protected CodeProjectRssFeedViewModel viewModel;

		UIActivityIndicatorView progressView;

	}
}

