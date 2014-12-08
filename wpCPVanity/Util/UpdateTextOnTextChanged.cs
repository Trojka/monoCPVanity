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
    public class UpdateTextOnTextChanged
    {
        public static bool GetUpdateSourceOnChange(DependencyObject d)
        {
            return (bool)d.GetValue(UpdateSourceOnChangeProperty);
        }

        public static void SetUpdateSourceOnChange(DependencyObject d, bool value)
        {
            d.SetValue(UpdateSourceOnChangeProperty, value);
        }

        public static readonly DependencyProperty
          UpdateSourceOnChangeProperty =
            DependencyProperty.RegisterAttached(
            "UpdateSourceOnChange",
            typeof(bool),
                      typeof(UpdateTextOnTextChanged),
            new PropertyMetadata(false, OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d,
          DependencyPropertyChangedEventArgs e)
        {
            var textBox = d as TextBox;
            if (textBox == null)
                return;
            if ((bool)e.NewValue)
            {
                textBox.TextChanged += OnTextChanged;
            }
            else
            {
                textBox.TextChanged -= OnTextChanged;
            }
        }
        static void OnTextChanged(object s, TextChangedEventArgs e)
        {
            var textBox = s as TextBox;
            if (textBox == null)
                return;

            var bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
            if (bindingExpression != null)
            {
                bindingExpression.UpdateSource();
            }
        }
    }
}
