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
using System.ComponentModel;
using System.Collections.ObjectModel;
using be.trojkasoftware.portableCPVanity.ViewModels;

namespace wpCPVanity.XamlViewModels
{
    public class CodeprojectForumFeedPageViewModel : INotifyPropertyChanged
    {
        public CodeprojectForumFeedPageViewModel(/*Action<string> gotoPageAction*/)
        {
            Items = new ObservableCollection<CodeprojectBaseViewModel>();

            rssFeedListPage = new CodeProjectForumFeedViewModel(/*gotoPageAction*/);
            rssFeedListPage.FeedLoaded = rssFeedListPage.OnFeedLoaded;

            Items.Add(rssFeedListPage);
        }

        public ObservableCollection<CodeprojectBaseViewModel> Items
        {
            get;
            private set;
        }

        public string CommunityName 
        {
            get 
            {
                return rssFeedListPage.CommunityName; 
            }
            set 
            {
                rssFeedListPage.CommunityName = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private CodeProjectForumFeedViewModel rssFeedListPage;


        public string FeedName { get; set; }
    }
}
