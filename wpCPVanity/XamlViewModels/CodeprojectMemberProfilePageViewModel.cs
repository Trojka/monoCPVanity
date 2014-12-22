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
using System.Collections.Generic;

namespace be.trojkasoftware.wpCPVanity.XamlViewModels
{
    public class CodeprojectMemberProfilePageViewModel : INotifyPropertyChanged
    {
        public CodeprojectMemberProfilePageViewModel()
        {
            Items = new ObservableCollection<CodeprojectBaseViewModel>();

            memberProfilePage = new CodeProjectMemberProfileViewModel();
            memberProfilePage.PropertyChanged += new PropertyChangedEventHandler(memberPage_PropertyChanged);
            memberProfilePage.Load();
            memberProfilePage.MemberLoaded = memberProfilePage.OnMemberLoaded;

            memberArticlesPage = new CodeProjectMemberArticlesViewModel();
            memberArticlesPage.PropertyChanged += new PropertyChangedEventHandler(memberPage_PropertyChanged);
            memberArticlesPage.Load();
            memberArticlesPage.ArticlesLoaded = memberArticlesPage.OnMemberArticlesLoaded;

            memberReputationPage = new CodeProjectMemberReputationViewModel();
            memberReputationPage.PropertyChanged += new PropertyChangedEventHandler(memberPage_PropertyChanged);
            memberReputationPage.Load();
            memberReputationPage.ReputationGraphLoaded = memberReputationPage.OnReputationGraphLoaded;

            Items.Add(memberProfilePage);
            Items.Add(memberArticlesPage);
            Items.Add(memberReputationPage);
        }

        void memberPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsLoading")
            {
                ProgressBarIsVisible = memberProfilePage.IsLoading;
            }
        }

        private bool progressBarIsVisible;
        public bool ProgressBarIsVisible
        {
            get { return progressBarIsVisible; }
            set { SetField(ref progressBarIsVisible, value, "ProgressBarIsVisible"); }
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

        #region INotifyPropertyChanged Members

        //http://stackoverflow.com/questions/1315621/implementing-inotifypropertychanged-does-a-better-way-exist
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
            return;
        }

        #endregion

        private CodeProjectMemberProfileViewModel memberProfilePage;
        private CodeProjectMemberArticlesViewModel memberArticlesPage;
        private CodeProjectMemberReputationViewModel memberReputationPage;

    }
}
