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
    public class MoveFocusUpOnKeyEnter
    {
        public static bool GetMoveUpOnEnter(DependencyObject d)
        {
            return (bool)d.GetValue(MoveUpOnEnterProperty);
        }

        public static void SetMoveUpOnEnter(DependencyObject d, bool value)
        {
            d.SetValue(MoveUpOnEnterProperty, value);
        }

        // Using a DependencyProperty as the backing store for …
        public static readonly DependencyProperty
          MoveUpOnEnterProperty =
            DependencyProperty.RegisterAttached(
            "MoveUpOnEnter",
            typeof(bool),
                      typeof(MoveFocusUpOnKeyEnter),
            new PropertyMetadata(false, OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d,
          DependencyPropertyChangedEventArgs e)
        {
            var textBox = d as TextBox;
            if (textBox == null)
                return;
            if ((bool)e.NewValue)
            {
                textBox.KeyUp += OnKeyUp;
            }
            else
            {
                textBox.KeyUp -= OnKeyUp;
            }
        }
        static void OnKeyUp(object s, KeyEventArgs e)
        {
            var textBox = s as TextBox;
            if (textBox == null || textBox.Parent == null)
                return;
            if (e.Key == Key.Enter)
            {
                var parent = textBox.Parent as UIElement;
                if(parent != null)
                {
                    //parent.;

                }
            }
        }
    }
}
