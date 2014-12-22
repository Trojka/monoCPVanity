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
    public class CodeprojectArticlesPageViewModel : INotifyPropertyChanged
    {
        public CodeprojectArticlesPageViewModel(Action<string> gotoPageAction)
        {
            Items = new ObservableCollection<CodeprojectBaseViewModel>();

            var availablePagesWM = new AvailablePagesViewModel();
            availablePagesWM.Name = "Goto";
            availablePagesWM.AvailablePages = new ObservableCollection<GotoPageViewModel>();
            availablePagesWM.AvailablePages.Add(new GotoPageViewModel(gotoPageAction) { Name = "Members", TargetPage = "/CodeprojectMemberPage.xaml" });
            availablePagesWM.AvailablePages.Add(new GotoPageViewModel(gotoPageAction) { Name = "Forums", TargetPage = "/CodeprojectForumsPage.xaml" });

            Items.Add(availablePagesWM);

            var rssFeedListPage = new CodeProjectArticleFeedListViewModel(gotoPageAction);
            rssFeedListPage.Load("Categories");

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
