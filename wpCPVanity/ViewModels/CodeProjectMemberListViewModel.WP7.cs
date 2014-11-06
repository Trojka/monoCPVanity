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
using System.Collections.ObjectModel;
using wpCPVanity;
using be.trojkasoftware.monoCPVanity.Data;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public class CodeprojectMemberListViewModel : CodeprojectBaseViewModel
    {
        public CodeprojectMemberListViewModel(Action<string> gotoPageAction)
        {
            this.gotoPageAction = gotoPageAction;
        }

        public void Load()
        {
            Name = "Members";

            CodeProjectDatabase database = new CodeProjectDatabase();
            var memberList = database.GetMembers().Select(x => new CodeprojectMemberViewModel(x, gotoPageAction)).ToList();

            Members = new ObservableCollection<CodeprojectMemberViewModel>(memberList);
        }

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

        private Action<string> gotoPageAction;
    }
}
