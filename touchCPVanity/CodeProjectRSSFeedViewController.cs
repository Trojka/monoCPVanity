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

namespace touchCPVanity
{
	public interface IFeedReceiver
	{
		CodeProjectRssFeed ItemFeed {
			set;
		}
	}

	public abstract partial class CodeProjectRSSFeedViewController : UIViewController, IFeedReceiver
	{
		public CodeProjectRSSFeedViewController (IntPtr handle) : base (handle)
		{
		}

		public CodeProjectRssFeed ItemFeed 
		{
			get;
			set;
		}

		public virtual Dictionary<string, string> GetBuilderParams() {
			Dictionary<string, string> paramList = new Dictionary<string, string> ();
			return paramList;
		}

		public void LoadFeed(CodeProjectRssFeed feed) {
			ObjectBuilder builder = new ObjectBuilder ();

			Task<IList<RSSItem>> loadFeedTask = builder.FillFeedAsync (feed, GetBuilderParams(), CancellationToken.None);

			progressView.StartAnimating ();

			var context = TaskScheduler.FromCurrentSynchronizationContext();

			loadFeedTask.Start ();
			loadFeedTask.ContinueWith (x => FeedLoaded(x.Result), context);
		}

		void FeedLoaded(IList<RSSItem> feed) {

			progressView.StopAnimating ();

			RSSItemTable.Source = new CodeProjectRSSDataSource(ItemFeed);
			RSSItemTable.ReloadData ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			RSSItemTable.Source = new CodeProjectRSSDataSource(ItemFeed);
		}

		public override void ViewDidAppear (bool animated)
		{
			progressView = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			progressView.Center = new PointF (this.View.Frame.Width / 2, this.View.Frame.Height / 2);
			this.View.AddSubview (progressView);

			CategoryText = ItemFeed.Name;

			ReloadData ();
		}

		public void ReloadData()
		{
			LoadFeed (ItemFeed);
		}

		public RSSItem SelectedItem {
			get {
				return ItemFeed [RSSItemTable.IndexPathForSelectedRow.Row];
			}
		}

		protected string CategoryText {
			get { return CategoryLabel.Text; }
			set { CategoryLabel.Text = value; }
		}

		UIActivityIndicatorView progressView;

	}
}

