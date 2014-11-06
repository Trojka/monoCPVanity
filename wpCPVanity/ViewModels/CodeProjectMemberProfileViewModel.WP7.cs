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
using be.trojkasoftware.portableCPVanity.ViewModels;
using wpCPVanity;
using System.Collections.Generic;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public partial class CodeProjectMemberProfileViewModel : CodeprojectBaseViewModel, INotifyPropertyChanged
    {
        private string memberName;
        public string MemberName
        {
            get { return memberName; }
            set { SetField(ref memberName, value, "MemberName"); }
        }

        public string MemberReputation { get; set; }
        public string ArticleCount { get; set; }
        public string AvgArticleRating { get; set; }
        public string BlogCount { get; set; }
        public string AvgBlogRating { get; set; }

        public DataTemplate ItemDataTemplate
        {
            get
            {
                return App.Current.Resources["MemberProfileTemplate"] as DataTemplate;
            }
        }

        public void OnMemberLoaded()
        {
            MemberName = Member.Name;
            MemberReputation = Member.Reputation;
            ArticleCount = "Articles: " + Member.ArticleCount;
            AvgArticleRating = "Average article rating: " + Member.AverageArticleRating;
            BlogCount = "Blogs: " + Member.BlogCount;
            AvgBlogRating = "Average blog rating: " + Member.AverageBlogRating;
        }

        public override void OnLoad()
        {
            //throw new NotImplementedException();
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
    }
}
