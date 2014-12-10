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
using System.Collections.ObjectModel;
using wpCPVanity.Util;
using be.trojkasoftware.monoCPVanity.Data;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public class CodeprojectMemberViewModel : INotifyPropertyChanged
    {
        public CodeprojectMemberViewModel(CodeProjectMember member, Action<string> gotoPageAction, Action<string> deleteMember)
        {
            this.GotoPageCommand = new ButtonCommandBinding<string>(gotoPageAction);
            this.DeleteMemberCommand = new ButtonCommandBinding<string>(deleteMember);
            this.member = member;
        }

        public int Id { get { return member.Id; } }

        public string Name { get { return member.Name; } }

        public string Reputation { get { return member.Reputation; } }

        public string Posts { get { return "Posts: " + (member.ArticleCount + member.BlogCount); } }

        public string TargetPage { get { return "/CodeprojectMemberProfilePage.xaml?id=" + member.Id; } }

        public ButtonCommandBinding<string> GotoPageCommand { get; private set; }

        public ButtonCommandBinding<string> DeleteMemberCommand { get; private set; }

        public string IdAsString { get { return member.Id.ToString(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private CodeProjectMember member;
    }
}
