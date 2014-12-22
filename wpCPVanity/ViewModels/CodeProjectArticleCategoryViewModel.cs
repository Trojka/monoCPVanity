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
using be.trojkasoftware.portableCPVanity.RssFeeds;
using wpCPVanity.Util;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public class CodeProjectArticleCategoryViewModel : INotifyPropertyChanged
    {
        public CodeProjectArticleCategoryViewModel(CodeProjectArticleFeedCategory articleFeed, Action<string> gotoPageAction)
        {
            this.GotoPageCommand = new ButtonCommandBinding<string>(gotoPageAction);
            this.articleFeed = articleFeed;
        }

        public int Id { get { return articleFeed.Id; } }

        public string Name { get { return articleFeed.Name; } }

        public string TargetPage { get { return "/CodeprojectArticleFeedPage.xaml?id=" + articleFeed.Id; } }

        public ButtonCommandBinding<string> GotoPageCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private CodeProjectArticleFeedCategory articleFeed;
    }
}
