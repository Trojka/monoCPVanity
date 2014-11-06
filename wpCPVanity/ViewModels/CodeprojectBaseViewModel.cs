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

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public abstract class CodeprojectBaseViewModel
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public abstract void OnLoad();
    }
}
