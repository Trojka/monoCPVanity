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
using be.trojkasoftware.portableCPVanity.RssFeeds;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using be.trojkasoftware.wpCPVanity;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public class CodeProjectArticleFeedListViewModel : CodeprojectBaseViewModel
    {
        public CodeProjectArticleFeedListViewModel(Action<string> gotoPageAction)
        {
            this.gotoPageAction = gotoPageAction;
        }

        public void Load(string name)
        {
            Name = name;

            var allFeeds = CodeProjectArticleFeed.Categories.Select(x => new CodeProjectArticleCategoryViewModel(x, gotoPageAction)).ToList();
            ArticleCategories = new ObservableCollection<CodeProjectArticleCategoryViewModel>(allFeeds);
        }

        private ObservableCollection<CodeProjectArticleCategoryViewModel> articleCategories;
        public ObservableCollection<CodeProjectArticleCategoryViewModel> ArticleCategories 
        {
            get
            {
                return articleCategories;
            }
            set
            {
                SetField(ref articleCategories, value, "ArticleCategories");
            }
        }

        public DataTemplate ItemDataTemplate
        {
            get
            {
                return App.Current.Resources["CodeProjectArticleFeedListTemplate"] as DataTemplate;
            }
        }

        public override void OnLoad()
        {
            //throw new NotImplementedException();
        }

        private Action<string> gotoPageAction;

        //private List<CodeProjectArticleFeedCategory> allFeeds;
    }
}
