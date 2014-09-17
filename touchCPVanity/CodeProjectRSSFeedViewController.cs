using System;
using System.Drawing;
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

		public virtual void LoadFeed(CodeProjectRssFeed feed) {
			ObjectBuilder builder = new ObjectBuilder ();

			Dictionary<string, string> paramList = new Dictionary<string, string> ();

			builder.FillFeed (ItemFeed, paramList);
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
			CategoryText = ItemFeed.Name;
			ReloadData ();
		}

		public void ReloadData()
		{
			LoadFeed (ItemFeed);
			RSSItemTable.Source = new CodeProjectRSSDataSource(ItemFeed);
			RSSItemTable.ReloadData ();
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

	}
}

