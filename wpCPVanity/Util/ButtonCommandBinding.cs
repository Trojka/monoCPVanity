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

namespace wpCPVanity.Util
{
    // http://www.mindfiresolutions.com/Binding-Button-Click-Command-with-ViewModal-2193.php
    public class ButtonCommandBinding<T> : ICommand where T : class
    {
        //delegate command to register method to be executed
        private readonly Action<T> handler;
        private bool isEnabled;

        /// <summary>
        /// Bind method to be executed to the handler
        /// So that it can direct on event execution
        /// </summary>
        /// <param name="handler"></param>
        public ButtonCommandBinding(Action<T> handler)
        {
            // Assign the method name to the handler
            this.handler = handler;
        }

        //Property that helps to
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (value != isEnabled)
                {
                    isEnabled = value;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// method to specify if the event will execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        // Helps to execute the respective method using the handler
        public void Execute(object parameter)
        {
            //calls the respective method that has been registered with the handler
            handler(parameter as T);
        }
    }
}
