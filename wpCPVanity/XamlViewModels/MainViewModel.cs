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
using System.ComponentModel;

namespace wpCPVanity.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            Items = new ObservableCollection<CodeprojectBaseViewModel>();

            var availablePagesWM = new AvailablePagesViewModel();
            availablePagesWM.AvailablePages = new ObservableCollection<GotoPageViewModel>();
            availablePagesWM.AvailablePages.Add(new GotoPageViewModel() { Name = "Articles" });
            availablePagesWM.AvailablePages.Add(new GotoPageViewModel() { Name = "Forums" });

            Items.Add(availablePagesWM);
        }

        public ObservableCollection<CodeprojectBaseViewModel> Items
        {
            get;
            private set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
