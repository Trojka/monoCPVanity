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
using wpCPVanity.Util;
using be.trojkasoftware.Ripit.Core;

namespace wpCPVanity.ViewModels
{
    public class CodeProjectRssItemViewModel
    {
        public CodeProjectRssItemViewModel(RSSItem item, Action<string> gotoPageAction)
        {
            this.GotoPageCommand = new ButtonCommandBinding<string>(gotoPageAction);
            this.item = item;
        }

        public string Title { get { return item.Title; } }

        public string TargetPage { get { return "" /* "/CodeprojectMemberProfilePage.xaml?id=" + member.Id*/ ; } }

        public ButtonCommandBinding<string> GotoPageCommand { get; private set; }

        private RSSItem item;

    }
}
