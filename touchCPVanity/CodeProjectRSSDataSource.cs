using System;
using MonoTouch.UIKit;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.portableCPVanity.RssFeeds;
using be.trojkasoftware.Ripit.Core;
using MonoTouch.Foundation;
using System.Drawing;

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
			//(cell.ViewWithTag (dateTag) as UILabel).Text = rssItem.Date;
			(cell.ViewWithTag (descriptionTag) as UILabel).Text = StripHTML(rssItem.Description);

			return cell;
		}

		public override float GetHeightForRow (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			RSSItem rssItem = RSSItemList[indexPath.Row];

			float textHeight = HeightOfText(StripHTML(rssItem.Description), 267);

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

		// http://www.codeproject.com/Articles/11902/Convert-HTML-to-Plain-Text
		private string StripHTML(string source)
		{
			try
			{
				string result;

				// Remove HTML Development formatting
				// Replace line breaks with space
				// because browsers inserts space
				result = source.Replace("\r", " ");
				// Replace line breaks with space
				// because browsers inserts space
				result = result.Replace("\n", " ");
				// Remove step-formatting
				result = result.Replace("\t", string.Empty);
				// Remove repeating spaces because browsers ignore them
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"( )+", " ");

				// Remove the header (prepare first by clearing attributes)
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"<( )*head([^>])*>","<head>",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"(<( )*(/)( )*head( )*>)","</head>",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					"(<head>).*(</head>)",string.Empty,
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);

				// remove all scripts (prepare first by clearing attributes)
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"<( )*script([^>])*>","<script>",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"(<( )*(/)( )*script( )*>)","</script>",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				//result = System.Text.RegularExpressions.Regex.Replace(result,
				//         @"(<script>)([^(<script>\.</script>)])*(</script>)",
				//         string.Empty,
				//         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"(<script>).*(</script>)",string.Empty,
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);

				// remove all styles (prepare first by clearing attributes)
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"<( )*style([^>])*>","<style>",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"(<( )*(/)( )*style( )*>)","</style>",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					"(<style>).*(</style>)",string.Empty,
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);

				// insert tabs in spaces of <td> tags
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"<( )*td([^>])*>","\t",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);

				// insert line breaks in places of <BR> and <LI> tags
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"<( )*br( )*>","\r",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"<( )*li( )*>","\r",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);

				// insert line paragraphs (double line breaks) in place
				// if <P>, <DIV> and <TR> tags
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"<( )*div([^>])*>","\r\r",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"<( )*tr([^>])*>","\r\r",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"<( )*p([^>])*>","\r\r",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);

				// Remove remaining tags like <a>, links, images,
				// comments etc - anything that's enclosed inside < >
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"<[^>]*>",string.Empty,
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);

				// replace special characters:
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@" "," ",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);

				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"&bull;"," * ",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"&lsaquo;","<",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"&rsaquo;",">",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"&trade;","(tm)",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"&frasl;","/",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"&lt;","<",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"&gt;",">",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"&copy;","(c)",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"&reg;","(r)",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				// Remove all others. More can be added, see
				// http://hotwired.lycos.com/webmonkey/reference/special_characters/
				result = System.Text.RegularExpressions.Regex.Replace(result,
					@"&(.{2,6});", string.Empty,
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);

				// for testing
				//System.Text.RegularExpressions.Regex.Replace(result,
				//       this.txtRegex.Text,string.Empty,
				//       System.Text.RegularExpressions.RegexOptions.IgnoreCase);

				// make line breaking consistent
				result = result.Replace("\n", "\r");

				// Remove extra line breaks and tabs:
				// replace over 2 breaks with 2 and over 4 tabs with 4.
				// Prepare first to remove any whitespaces in between
				// the escaped characters and remove redundant tabs in between line breaks
				result = System.Text.RegularExpressions.Regex.Replace(result,
					"(\r)( )+(\r)","\r\r",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					"(\t)( )+(\t)","\t\t",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					"(\t)( )+(\r)","\t\r",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				result = System.Text.RegularExpressions.Regex.Replace(result,
					"(\r)( )+(\t)","\r\t",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				// Remove redundant tabs
				result = System.Text.RegularExpressions.Regex.Replace(result,
					"(\r)(\t)+(\r)","\r\r",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				// Remove multiple tabs following a line break with just one tab
				result = System.Text.RegularExpressions.Regex.Replace(result,
					"(\r)(\t)+","\r\t",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				// Initial replacement target string for line breaks
				string breaks = "\r\r\r";
				// Initial replacement target string for tabs
				string tabs = "\t\t\t\t\t";
				for (int index=0; index<result.Length; index++)
				{
					result = result.Replace(breaks, "\r\r");
					result = result.Replace(tabs, "\t\t\t\t");
					breaks = breaks + "\r";
					tabs = tabs + "\t";
				}

				// That's it.
				return result;
			}
			catch
			{
				return source;
			}
		}

		private static int titleTag = 100;
		private static int authorTag = 101;
		//private static int dateTag = 102;
		private static int descriptionTag = 103;
	}
}

