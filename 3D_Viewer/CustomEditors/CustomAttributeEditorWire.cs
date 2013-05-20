using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;
using System.Text;
using smileUp.DataModel;

namespace smileUp.CustomEditors
{
    public class CustomAttributeEditorWire
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
        [DisplayName("Brace1")]
        [Description("This property uses a TextBox as the default editor.")]
        [Editor(typeof(TextBoxEditor), typeof(TextBoxEditor))]
        public String Brace1 { get; set; }

        [Category("Information")]
        [DisplayName("Brace2")]
        [Description("This property uses a TextBox as the default editor.")]
        [Editor(typeof(TextBoxEditor), typeof(TextBoxEditor))]
        public String Brace2 { get; set; }

        [Category("Information")]
        [DisplayName("Thicknes")]
        [Description("This property uses a TextBox as the default editor.")]
        [Editor(typeof(TextBoxEditor), typeof(TextBoxEditor))]
        public double Thicknes { get; set; }

        [Category("Information")]
        [DisplayName("Material")]
        [Description("This property uses a TextBox as the default editor.")]
        [Editor(typeof(TextBoxEditor), typeof(TextBoxEditor))]
        public String Material { get; set; }

        public static CustomAttributeEditorWire CreateCustomAttributEditorWire(Wire model)
        {
            var wire = new CustomAttributeEditorWire();
            wire.Type = "Wire";
            //TODO: assign the model data to the editor
            wire.Id = model.Id;
            wire.Brace1 = model.Brace1 != null? model.Brace1.Id: "";
            wire.Brace2 = model.Brace2 != null ? model.Brace2.Id : "";
            wire.Material = model.Material;
            wire.Thicknes = model.Thicknes;

            return wire;
        }

    }
}
