using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using be.trojkasoftware.portableCPVanity.ViewModels;
using be.trojkasoftware.portableCPVanity.RssFeeds;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public class CodeProjectArticleFeedViewModel : CodeProjectRssFeedViewModel
    {
        public CodeProjectArticleFeedViewModel(/*Action<string> gotoPageAction*/) //:
            //base(gotoPageAction)
        {
        }

        int feedId;
        public int FeedId
        {
            get { return feedId; }
            set
            {
                feedId = value;
                this.ItemFeed = CodeProjectArticleFeed.GetFeed(feedId);
                this.Name = this.ItemFeed.Name;
            }
        }
    }
}
