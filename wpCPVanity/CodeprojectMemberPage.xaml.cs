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
using wpCPVanity.XamlViewModels;

namespace wpCPVanity
{
    public partial class CodeprojectMemberPage : PhoneApplicationPage
    {
        public CodeprojectMemberPage()
        {
            InitializeComponent();

            AttachDataSource();
        }

        public void AttachDataSource()
        {
            var viewModel = new CodeprojectMemberPageViewModel(this.GotoPage);
            //viewModel.GotoPageAction = this.GotoPage;
            this.DataContext = viewModel;
        }

        private void GotoPage(string page)
        {
            NavigationService.Navigate(new Uri(page, UriKind.Relative));
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
    }
}