﻿using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using be.trojkasoftware.portableCPVanity.ViewModels;
using System.Collections.ObjectModel;
using wpCPVanity;
using System.Collections.Generic;
using be.trojkasoftware.wpCPVanity;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public partial class CodeProjectMemberArticlesViewModel : CodeprojectBaseViewModel
    {

        public void Load()
        {
            Name = "Articles";
        }

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetField(ref isLoading, value, "IsLoading"); }
        }

        private List<CodeProjectMemberArticleViewModel> memberArticlesList;
        public List<CodeProjectMemberArticleViewModel> MemberArticlesList
        {
            get { return memberArticlesList; }
            set { SetField(ref memberArticlesList, value, "MemberArticlesList"); }
        }

        public DataTemplate ItemDataTemplate
        {
            get
            {
                return App.Current.Resources["MemberArticleListTemplate"] as DataTemplate;
            }
        }

        public void OnMemberArticlesLoaded()
        {
            IsLoading = false;

            MemberArticlesList = MemberArticles;
        }

        public override void OnLoad()
        {
            LoadMemberArticles(TaskScheduler.FromCurrentSynchronizationContext());
            IsLoading = true;
        }
    }
}
