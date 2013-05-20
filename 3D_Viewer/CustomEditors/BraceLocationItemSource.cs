using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace smileUp.CustomEditors
{
    class BraceLocationItemSource : IItemsSource
    {
        public ItemCollection GetValues()
        {
            ItemCollection sizes = new ItemCollection();
            sizes.Add(Smile.INNERBRACE,"Inner");
            sizes.Add(Smile.OUTERBRACE, "Outer");
            return sizes;
        }
    }
}
