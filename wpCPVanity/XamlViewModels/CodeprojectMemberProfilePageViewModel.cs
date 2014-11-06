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

namespace wpCPVanity.XamlViewModels
{
    public class CodeprojectMemberProfilePageViewModel : INotifyPropertyChanged
    {
        public CodeprojectMemberProfilePageViewModel()
        {
            Items = new ObservableCollection<CodeprojectBaseViewModel>();

            memberProfilePage = new CodeProjectMemberProfileViewModel();
            memberProfilePage.MemberLoaded = memberProfilePage.OnMemberLoaded;

            Items.Add(memberProfilePage);
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
                memberProfilePage.LoadMember(TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private CodeProjectMemberProfileViewModel memberProfilePage;

    }
}
