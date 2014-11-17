using System;
using MonoTouch.UIKit;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.portableCPVanity.RssFeeds;
using be.trojkasoftware.Ripit.Core;
using MonoTouch.Foundation;
using System.Drawing;
using be.trojkasoftware.portableCPVanity.ViewModels;

namespace touchCPVanity
{
	public class CodeProjectRSSDataSource : UITableViewSource
	{
		public CodeProjectRSSDataSource (CodeProjectRssFeed rssItemList)
		{
			RSSItemList = rssItemList;
		}

		public CodeProjectRssFeed RSSItemList {
			private set;
			get;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return RSSItemList.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("RSSItemCell");
			RSSItem rssItem = RSSItemList[indexPath.Row];

			(cell.ViewWithTag (titleTag) as UILabel).Text = rssItem.Title;
			(cell.ViewWithTag (authorTag) as UILabel).Text = rssItem.Author;
			(cell.ViewWithTag (descriptionTag) as UILabel).Text = CodeProjectRssFeedViewModel.StripHTML(rssItem.Description);

			return cell;
		}

		public override float GetHeightForRow (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			RSSItem rssItem = RSSItemList[indexPath.Row];

			float textHeight = HeightOfText(CodeProjectRssFeedViewModel.StripHTML(rssItem.Description), 267);

			float height = RoundValueToNearestMultiple(textHeight, 18.5f) + 50;

			return height;		
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			UIApplication.SharedApplication.OpenUrl (NSUrl.FromString (RSSItemList[indexPath.Row].Link));
			tableView.DeselectRow (indexPath, true);
		}

		private float HeightOfText(string text,int width)
		{
			UIFont font = UIFont.SystemFontOfSize(15.0f);

			NSMutableParagraphStyle paragraphStyle = new NSMutableParagraphStyle();
			paragraphStyle.LineBreakMode = UILineBreakMode.WordWrap;

			NSMutableDictionary attributes = new NSMutableDictionary();
			attributes[UIStringAttributeKey.Font] = font;
			attributes[UIStringAttributeKey.ParagraphStyle] = paragraphStyle;

			NSAttributedString attributedText = new NSAttributedString (text, attributes);
			RectangleF paragraphRect = attributedText.GetBoundingRect(
				new System.Drawing.SizeF(width, float.MaxValue),
				NSStringDrawingOptions.UsesLineFragmentOrigin,
				null);

			return paragraphRect.Height;
		}
					
		private float RoundValueToNearestMultiple(float value, float multiple)
		{
			int roundedMultiple = (int)Math.Round(value/multiple);
			return roundedMultiple * multiple;
		}

		private static int titleTag = 100;
		private static int authorTag = 101;
		private static int descriptionTag = 103;
	}
}

