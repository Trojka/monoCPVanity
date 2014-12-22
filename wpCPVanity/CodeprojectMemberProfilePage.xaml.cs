using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using be.trojkasoftware.wpCPVanity.XamlViewModels;
using be.trojkasoftware.portableCPVanity.ViewModels;

namespace be.trojkasoftware.wpCPVanity
{
    public partial class CodeprojectMemberProfilePage : PhoneApplicationPage
    {
        public CodeprojectMemberProfilePage()
        {
            InitializeComponent();

            AttachDataSource();
        }

        public void AttachDataSource()
        {
            viewModel = new CodeprojectMemberProfilePageViewModel();
            this.DataContext = viewModel;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            String id = NavigationContext.QueryString["id"];
            viewModel.MemberId = int.Parse(id);
        }

        private void Pivoter_UnloadingPivotItem(object sender, PivotItemEventArgs e)
        {
        }

        private void Pivoter_LoadingPivotItem(object sender, PivotItemEventArgs e)
        {
            if (e.Item.DataContext is CodeprojectBaseViewModel)
            {
                (e.Item.DataContext as CodeprojectBaseViewModel).OnLoad();
            }
        }

        CodeprojectMemberProfilePageViewModel viewModel;
    }
}