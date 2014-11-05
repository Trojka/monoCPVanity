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

namespace wpCPVanity.XamlViewModels
{
    public class CodeprojectMemberPageViewModel : INotifyPropertyChanged
    {
        public CodeprojectMemberPageViewModel(Action<string> gotoPageAction)
        {
            Items = new ObservableCollection<CodeprojectBaseViewModel>();

            var availablePagesWM = new AvailablePagesViewModel();
            availablePagesWM.Name = "Goto";
            availablePagesWM.AvailablePages = new ObservableCollection<GotoPageViewModel>();
            availablePagesWM.AvailablePages.Add(new GotoPageViewModel(gotoPageAction) { Name = "Articles", TargetPage = "/CodeprojectArticlesPage.xaml" });
            availablePagesWM.AvailablePages.Add(new GotoPageViewModel(gotoPageAction) { Name = "Forums", TargetPage = "/CodeprojectForumsPage.xaml" });

            Items.Add(availablePagesWM);

            var memberListPage = new CodeprojectMemberListViewModel();
            memberListPage.Name = "Members";
            memberListPage.Members = new ObservableCollection<CodeprojectMemberViewModel>();
            memberListPage.Members.Add(new CodeprojectMemberViewModel(){ Name = "Serge Desmedt"});
            memberListPage.Members.Add(new CodeprojectMemberViewModel(){ Name = "Detje Maerten"});

            Items.Add(memberListPage);
        }

        public ObservableCollection<CodeprojectBaseViewModel> Items
        {
            get;
            private set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
