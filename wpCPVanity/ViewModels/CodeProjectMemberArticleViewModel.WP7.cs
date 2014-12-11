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
using Microsoft.Phone.Tasks;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public partial class CodeProjectMemberArticleViewModel
    {
        ButtonCommandBinding<string> gotoPageCommand = null;
        public ButtonCommandBinding<string> GotoPageCommand
        {
            get
            {
                if (gotoPageCommand == null)
                    gotoPageCommand = new ButtonCommandBinding<string>(NavigateToLink);

                return gotoPageCommand;
            }
        }

        public string TargetPage { get { return Link; } }

        public void NavigateToLink(string url)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask();
            webBrowserTask.Uri = new Uri(url);
            webBrowserTask.Show();
        }
    }
}
