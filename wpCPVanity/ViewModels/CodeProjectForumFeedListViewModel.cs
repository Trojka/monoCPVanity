using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using be.trojkasoftware.portableCPVanity.ViewModels;
using System.Collections.ObjectModel;
using be.trojkasoftware.portableCPVanity.RssFeeds;
using be.trojkasoftware.wpCPVanity;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public class CodeProjectForumFeedListViewModel : CodeprojectBaseViewModel
    {
        public CodeProjectForumFeedListViewModel(Action<string> gotoPageAction)
        {
            this.gotoPageAction = gotoPageAction;
        }

        public void Load(string name)
        {
            Name = name;

            var allFeeds = categories.Categories.Select(x => new CodeProjectForumCategoryViewModel(x, gotoPageAction)).ToList();
            ForumCategories = new ObservableCollection<CodeProjectForumCategoryViewModel>(allFeeds);
        }

        private ObservableCollection<CodeProjectForumCategoryViewModel> forumCategories;
        public ObservableCollection<CodeProjectForumCategoryViewModel> ForumCategories 
        {
            get
            {
                return forumCategories;
            }
            set
            {
                SetField(ref forumCategories, value, "ForumCategories");
            }
        }

        public DataTemplate ItemDataTemplate
        {
            get
            {
                return App.Current.Resources["CodeProjectForumFeedListTemplate"] as DataTemplate;
            }
        }

        public override void OnLoad()
        {
            //throw new NotImplementedException();
        }

        private Action<string> gotoPageAction;

        private CommunityRssCategories categories = new CommunityRssCategories();

    }
}
