using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;
using System.Text;

namespace smileUp.CustomEditors
{
    public class Editors
    {
    }

    //Custom editors that are used as attributes MUST implement the ITypeEditor interface.
    public class TextBoxEditor : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        TextBox textBox = new TextBox();
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            //textBox.Background = new SolidColorBrush(Colors.Red);

            //create the binding from the bound property item to the editor
            var _binding = new Binding("Value"); //bind to the Value property of the PropertyItem
            _binding.Source = propertyItem;
            _binding.ValidatesOnExceptions = true;
            _binding.ValidatesOnDataErrors = true;
            _binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(textBox, TextBox.TextProperty, _binding);
            return textBox;
        }

    }

}
