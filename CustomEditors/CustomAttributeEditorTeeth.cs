using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;
using System.Text;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Text.RegularExpressions;
using smileUp.DataModel;

namespace smileUp.CustomEditors
{
    public class CustomAttributeEditorTeeth
    {
        [Category("Information")]
        [DisplayName("Type")]
        [Description("This property uses a TextBox as the default editor.")]
        //This custom editor is a Class that implements the ITypeEditor interface
        [Editor(typeof(TextBoxEditor), typeof(TextBoxEditor))]
        public string Type
        {
            get;
            set;
        }

        [Category("Information")]
        [DisplayName("Id")]
        [Description("This property uses a TextBox as the default editor.")]
        //This custom editor is a Class that implements the ITypeEditor interface
        [Editor(typeof(TextBoxEditor), typeof(TextBoxEditor))]
        public string Id
        {
            get;
            set;
        }

        [Category("Information")]
        [DisplayName("TeethNumber")]
        [Description("This property uses the ListBox as the default editor.")]
        //[Editor(typeof(DropDownListEditor), typeof(DropDownListEditor))]
        [ItemsSource(typeof(TeethMappingItemSource))]
        public int TeethNumber
        {
            get;
            set;
        }


        [Category("Information")]
        [DisplayName("Color")]
        [Description("This property uses the ListBox as the default editor.")]
        //[Editor(typeof(DropDownListEditor), typeof(DropDownListEditor))]
        [ItemsSource(typeof(TeethColorMappingItemSource))]
        public string Color
        {
            get;
            set;
        }
        [Category("Measurement")]
        [DisplayName("Length(mm)")]
        [Editor(typeof(TextBoxEditor), typeof(TextBoxEditor))]
        public double Length
        {
            get;
            set;
        }


        public static CustomAttributeEditorTeeth CreateCustomAttributEditorTeeth(Teeth model)
        {
            var teeth = new CustomAttributeEditorTeeth();
            teeth.Type = "Teeth";
            //TODO: assign the model data to the editor
            teeth.Id = model.Id;

            String tn = model.Id;
            Match mt = Regex.Match(tn, @"teeth\d\d");
            if (mt.Value == "") { mt = Regex.Match(tn, @"teeth\d"); tn = mt.Value.Substring("teeth".Length, 1); }
            else { tn = mt.Value.Substring("teeth".Length, 2); }
            //tn = tn.Substring("teeth".Length, 2);
            int itn = 0;
            int.TryParse(tn, out itn);
            teeth.TeethNumber = itn;

            teeth.Length = model.Length;

            return teeth;
        }

    }

}
