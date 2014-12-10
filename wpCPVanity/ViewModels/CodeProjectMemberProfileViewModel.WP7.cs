using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using be.trojkasoftware.portableCPVanity.ViewModels;
using wpCPVanity;
using System.Collections.Generic;
using wpCPVanity.Util;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public partial class CodeProjectMemberProfileViewModel : CodeprojectBaseViewModel
    {

        public void Load()
        {
            Name = "Profile";
            this.SaveMemberCommand = new ButtonCommandBinding<CodeProjectMember>(this.SaveMember);
        }

        private string memberName;
        public string MemberName
        {
            get { return memberName; }
            set { SetField(ref memberName, value, "MemberName"); }
        }

        private string memberReputation;
        public string MemberReputation
        {
            get { return memberReputation; }
            set { SetField(ref memberReputation, value, "MemberReputation"); }
        }

        private string memberArticleCount;
        public string MemberArticleCount 
        {
            get { return memberArticleCount; }
            set { SetField(ref memberArticleCount, value, "MemberArticleCount"); }
        }

        private string memberAvgArticleRating;
        public string MemberAvgArticleRating 
        {
            get { return memberAvgArticleRating; }
            set { SetField(ref memberAvgArticleRating, value, "MemberAvgArticleRating"); }
        }

        private string memberBlogCount;
        public string MemberBlogCount 
        {
            get { return memberBlogCount; }
            set { SetField(ref memberBlogCount, value, "MemberBlogCount"); }
        }

        private string memberAvgBlogRating;
        public string MemberAvgBlogRating 
        {
            get { return memberAvgBlogRating; }
            set { SetField(ref memberAvgBlogRating, value, "MemberAvgBlogRating"); } 
        }

        private BitmapImage memberGravatarImage;
        public BitmapImage MemberGravatarImage
        {
            get { return memberGravatarImage; }
            set { SetField(ref memberGravatarImage, value, "MemberGravatarImage"); }
        }

        public ButtonCommandBinding<CodeProjectMember> SaveMemberCommand { get; private set; }

        public void SaveMember(CodeProjectMember member)
        {
            SaveMember();
        }

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
            MemberArticleCount = "Articles: " + Member.ArticleCount;
            MemberAvgArticleRating = "Average article rating: " + Member.AverageArticleRating;
            MemberBlogCount = "Blogs: " + Member.BlogCount;
            MemberAvgBlogRating = "Average blog rating: " + Member.AverageBlogRating;

            BitmapImage bitmapImage = new BitmapImage();
            MemoryStream ms = new MemoryStream(Member.Gravatar);
            bitmapImage.SetSource(ms);

            MemberGravatarImage = bitmapImage;

        }

        public override void OnLoad()
        {
            LoadMember(TaskScheduler.FromCurrentSynchronizationContext());
        }

    }
}
