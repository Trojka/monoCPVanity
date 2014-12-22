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
using System.Collections.ObjectModel;
using be.trojkasoftware.wpCPVanity;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public class AvailablePagesViewModel : CodeprojectBaseViewModel
    {
        public ObservableCollection<GotoPageViewModel> AvailablePages { get; set; }

        public DataTemplate ItemDataTemplate
        {
            get
            {
                return App.Current.Resources["AvailablePagesTemplate"] as DataTemplate;
            }
        }

        public override void OnLoad()
        {
            //throw new NotImplementedException();
        }
    }
}
