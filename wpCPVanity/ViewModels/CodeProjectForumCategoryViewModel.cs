﻿using System;
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
using wpCPVanity.Util;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public class CodeProjectForumCategoryViewModel : INotifyPropertyChanged
    {
        public CodeProjectForumCategoryViewModel(string forumFeed, Action<string> gotoPageAction)
        {
            this.GotoPageCommand = new ButtonCommandBinding<string>(gotoPageAction);
            this.Name = forumFeed;
        }

        public string Name { private set; get; }

        public string TargetPage { get { return "/CodeprojectForumFeedPage.xaml?name=" + Name; } }

        public ButtonCommandBinding<string> GotoPageCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
