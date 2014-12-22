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
using Microsoft.Phone.Tasks;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public class CodeProjectRssItemViewModel
    {
        public CodeProjectRssItemViewModel(RSSItem item /*, Action<string> gotoPageAction*/)
        {
            //this.GotoPageCommand = new ButtonCommandBinding<string>(NavigateToLink);
            this.item = item;
            Link = new Uri(item.Link, UriKind.Absolute);
        }

        public string Title { get { return item.Title; } }

        public string Author { get { return item.Author; } }

        public string Description { get { return item.Description; } }

        public Uri Link { private set; get; }

        ButtonCommandBinding<string> gotoPageCommand = null;
        public ButtonCommandBinding<string> GotoPageCommand 
        { 
            get 
            { 
                if(gotoPageCommand == null)
                    gotoPageCommand = new ButtonCommandBinding<string>(NavigateToLink);

                return gotoPageCommand; 
            } 
        }

        public string TargetPage { get { return item.Link; } }

        public void NavigateToLink(string url)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask();
            webBrowserTask.Uri = new Uri(url);
            webBrowserTask.Show(); 
        }

        private RSSItem item;

    }
}
