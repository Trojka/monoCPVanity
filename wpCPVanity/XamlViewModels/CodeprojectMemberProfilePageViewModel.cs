using System;
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
using System.ComponentModel;
using System.Collections.ObjectModel;
using be.trojkasoftware.portableCPVanity.ViewModels;
using be.trojkasoftware.portableCPVanity;

namespace wpCPVanity.XamlViewModels
{
    public class CodeprojectMemberProfilePageViewModel : INotifyPropertyChanged
    {
        public CodeprojectMemberProfilePageViewModel()
        {
            Items = new ObservableCollection<CodeprojectBaseViewModel>();

            memberProfilePage = new CodeProjectMemberProfileViewModel();
            memberProfilePage.Load();
            memberProfilePage.MemberLoaded = memberProfilePage.OnMemberLoaded;

            memberArticlesPage = new CodeProjectMemberArticlesViewModel();
            memberArticlesPage.Load();
            memberArticlesPage.ArticlesLoaded = memberArticlesPage.OnMemberArticlesLoaded;

            memberReputationPage = new CodeProjectMemberReputationViewModel();
            memberReputationPage.Load();
            memberReputationPage.ReputationGraphLoaded = memberReputationPage.OnReputationGraphLoaded;

            Items.Add(memberProfilePage);
            Items.Add(memberArticlesPage);
            Items.Add(memberReputationPage);
        }

        public ObservableCollection<CodeprojectBaseViewModel> Items
        {
            get;
            private set;
        }

        public int MemberId
        {
            get
            {
                return memberProfilePage.MemberId;
            }
            set
            {
                memberProfilePage.MemberId = value;
                memberArticlesPage.MemberId = value;
                memberReputationPage.MemberReputationGraphUrl = CodeProjectMember.GetReputationGraph(value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private CodeProjectMemberProfileViewModel memberProfilePage;
        private CodeProjectMemberArticlesViewModel memberArticlesPage;
        private CodeProjectMemberReputationViewModel memberReputationPage;

    }
}
