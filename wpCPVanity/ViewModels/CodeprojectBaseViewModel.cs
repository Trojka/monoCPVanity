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
using System.Collections.Generic;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public abstract class CodeprojectBaseViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public abstract void OnLoad();

        #region INotifyPropertyChanged Members

        //http://stackoverflow.com/questions/1315621/implementing-inotifypropertychanged-does-a-better-way-exist
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
            return;
        }

        #endregion
    }
}
