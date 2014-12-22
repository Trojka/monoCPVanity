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
using System.Collections.ObjectModel;
using System.ComponentModel;
using be.trojkasoftware.portableCPVanity.ViewModels;


namespace be.trojkasoftware.wpCPVanity.XamlViewModels
{
    public class CodeprojectForumsPageViewModel : INotifyPropertyChanged
    {
        public CodeprojectForumsPageViewModel(Action<string> gotoPageAction)
        {
            Items = new ObservableCollection<CodeprojectBaseViewModel>();

            var availablePagesWM = new AvailablePagesViewModel();
            availablePagesWM.Name = "Goto";
            availablePagesWM.AvailablePages = new ObservableCollection<GotoPageViewModel>();
            availablePagesWM.AvailablePages.Add(new GotoPageViewModel(gotoPageAction) { Name = "Members", TargetPage = "/CodeprojectMemberPage.xaml" });
            availablePagesWM.AvailablePages.Add(new GotoPageViewModel(gotoPageAction) { Name = "Articles", TargetPage = "/CodeprojectArticlesPage.xaml" });

            Items.Add(availablePagesWM);

            var rssFeedListPage = new CodeProjectForumFeedListViewModel(gotoPageAction);
            rssFeedListPage.Load("Categories");
            //rssFeedPage.FeedLoaded = rssFeedPage.OnFeedLoaded;

            Items.Add(rssFeedListPage);
        }

        public ObservableCollection<CodeprojectBaseViewModel> Items
        {
            get;
            private set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
