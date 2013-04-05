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
    public class CustomAttributeEditorBrace
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


        public static CustomAttributeEditorBrace CreateCustomAttributEditorBrace(Brace model)
        {
            var brace = new CustomAttributeEditorBrace();
            brace.Type = "Brace";
            //TODO: assign the model data to the editor
            brace.Id = model.Id;

            return brace;
        }

    }
}
