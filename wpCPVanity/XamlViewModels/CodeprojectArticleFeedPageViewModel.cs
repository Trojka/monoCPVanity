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


namespace be.trojkasoftware.wpCPVanity.XamlViewModels
{
    public class CodeprojectArticleFeedPageViewModel : INotifyPropertyChanged
    {
        public CodeprojectArticleFeedPageViewModel(/*Action<string> gotoPageAction*/)
        {
            Items = new ObservableCollection<CodeprojectBaseViewModel>();

            rssFeedListPage = new CodeProjectArticleFeedViewModel(/*gotoPageAction*/);
            rssFeedListPage.FeedLoaded = rssFeedListPage.OnFeedLoaded;

            Items.Add(rssFeedListPage);
        }

        public ObservableCollection<CodeprojectBaseViewModel> Items
        {
            get;
            private set;
        }

        public int FeedId 
        {
            get 
            { 
                return rssFeedListPage.FeedId; 
            }
            set 
            { 
                rssFeedListPage.FeedId = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private CodeProjectArticleFeedViewModel rssFeedListPage;

    }
}
