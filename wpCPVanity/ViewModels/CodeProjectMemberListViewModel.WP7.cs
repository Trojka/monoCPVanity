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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using wpCPVanity;
using be.trojkasoftware.monoCPVanity.Data;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public class CodeprojectMemberListViewModel : CodeprojectBaseViewModel
    {
        public static string LOAD_MEMBER = "Load member ";

        public CodeprojectMemberListViewModel(Action<string> gotoPageAction)
        {
            this.gotoPageAction = gotoPageAction;
        }

        public void Load()
        {
            Name = "Members";
            //filter = "None";

            CodeProjectDatabase database = new CodeProjectDatabase();
            allMembers = database.GetMembers();

            var members = allMembers.Select(x => new CodeprojectMemberViewModel(x, gotoPageAction, this.DeleteMember)).ToList();
            Members = new ObservableCollection<CodeprojectMemberViewModel>(members);
        }

        public string Filter {
            get 
            { 
                return filter; 
            }
            set 
            {
                SetField(ref filter, value, "filter");

                Members = new ObservableCollection<CodeprojectMemberViewModel>();

                int searchId;
                if (int.TryParse(filter, out searchId))
                {
                    CodeProjectMember dummyMember = new CodeProjectMember();
                    dummyMember.Id = searchId;
                    dummyMember.Name = LOAD_MEMBER + filter;
                    Members.Add(new CodeprojectMemberViewModel(dummyMember, gotoPageAction, null));

                    var members = allMembers.Where(x => x.Id == searchId).Select(x => new CodeprojectMemberViewModel(x, gotoPageAction, this.DeleteMember)).ToList();
                    members.ForEach(x => Members.Add(x));
                }
                else
                {
                    var members = allMembers.Where(x => x.Name.Contains(filter)).Select(x => new CodeprojectMemberViewModel(x, gotoPageAction, this.DeleteMember)).ToList();
                    members.ForEach(x => Members.Add(x));
                }

            }
        }

        public ObservableCollection<CodeprojectMemberViewModel> Members 
        {
            get
            {
                return members;
            }
            set
            {
                SetField(ref members, value, "Members");
            }
        }

        public DataTemplate ItemDataTemplate
        {
            get
            {
                return App.Current.Resources["MemberListTemplate"] as DataTemplate;
            }
        }

        public override void OnLoad()
        {
            Load();
        }

        public void DeleteMember(string memberId)
        {
            CodeProjectDatabase db = new CodeProjectDatabase();
            db.DeleteMember(int.Parse(memberId));

            Load();
        }

        private Action<string> gotoPageAction;

        private List<CodeProjectMember> allMembers;

        private ObservableCollection<CodeprojectMemberViewModel> members;
        private string filter;
    }
}
