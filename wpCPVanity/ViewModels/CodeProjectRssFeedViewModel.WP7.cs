using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using be.trojkasoftware.portableCPVanity.ViewModels;
using wpCPVanity;
using wpCPVanity.ViewModels;
using be.trojkasoftware.portableCPVanity.RssFeeds;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public partial class CodeProjectRssFeedViewModel : CodeprojectBaseViewModel
    {
        public CodeProjectRssFeedViewModel(/*Action<string> gotoPageAction*/)
        {
            //this.gotoPageAction = gotoPageAction;
        }

        //public void Load(string name)
        //{
        //    Name = name;
        //}

        private List<CodeProjectRssItemViewModel> rssFeedList;
        public List<CodeProjectRssItemViewModel> RssFeedList
        {
            get { return rssFeedList; }
            set { SetField(ref rssFeedList, value, "RssFeedList"); }
        }

        public DataTemplate ItemDataTemplate
        {
            get
            {
                return App.Current.Resources["RssFeedTemplate"] as DataTemplate;
            }
        }

        public void OnFeedLoaded()
        {
            RssFeedList = ItemFeed.Select(x => new CodeProjectRssItemViewModel(x /*, gotoPageAction*/)).ToList();
        }

        public override void OnLoad()
        {
            LoadFeed(TaskScheduler.FromCurrentSynchronizationContext());
        }

        //private Action<string> gotoPageAction;
    }
}
