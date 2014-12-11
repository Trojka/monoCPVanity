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
using be.trojkasoftware.portableCPVanity.RssFeeds;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public class CodeProjectForumFeedViewModel : CodeProjectRssFeedViewModel
    {
        public CodeProjectForumFeedViewModel(/*Action<string> gotoPageAction*/) //:
//            base(/*gotoPageAction*/)
        {
            //this.ItemFeed = new CodeProjectLoungeFeed();
        }

        string communityName;
        public string CommunityName
        {
            get { return communityName; }
            set
            {
                communityName = value;
                this.ItemFeed = categories.GetFeedForCategory(communityName);
                this.Name = this.ItemFeed.Name;
            }
        }

        private CommunityRssCategories categories = new CommunityRssCategories();
    }
}
