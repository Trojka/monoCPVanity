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
using System.Windows.Navigation;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{

    // http://stackoverflow.com/questions/14423847/windows-phone-mvvm-button-command-can-execute-and-command-paramtere
    public class GotoPageViewModel
    {
        public GotoPageViewModel(Action<string> gotoPageAction)
        {
            GotoPageCommand = new ButtonCommandBinding<string>(gotoPageAction);
        }

        public string Name { get; set; }

        public string TargetPage { get; set; }

        public ButtonCommandBinding<string> GotoPageCommand { get; private set; }

    }
}
