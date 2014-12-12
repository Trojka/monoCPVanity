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
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.portableCPVanity.ViewModels;

namespace wpCPVanity.Util
{
    public class CodeprojectMemberTemplateSelector : DataTemplateSelector
    {
        public DataTemplate KnownMemberTemplate
        {
            get;
            set;
        }

        public DataTemplate NewMemberTemplate
        {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            CodeprojectMemberViewModel codeprojectMember = item as CodeprojectMemberViewModel;
            if (codeprojectMember != null)
            {
                if (codeprojectMember.Name.StartsWith(CodeprojectMemberListViewModel.LOAD_MEMBER))
                {
                    return NewMemberTemplate;
                }
                else
                {
                    return KnownMemberTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }

    }
}
