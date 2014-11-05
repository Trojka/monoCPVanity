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

namespace wpCPVanity.XamlViewModels
{
    public class CodeprojectMemberListViewModel : CodeprojectBaseViewModel
    {

        public ObservableCollection<CodeprojectMemberViewModel> Members { get; set; }

        public DataTemplate ItemDataTemplate
        {
            get
            {
                return App.Current.Resources["MemberListTemplate"] as DataTemplate;
            }
        }

        public override void OnLoad()
        {
            //throw new NotImplementedException();
        }
    }
}
