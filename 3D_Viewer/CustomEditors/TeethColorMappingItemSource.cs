using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Drawing;
using System.Windows.Media;

namespace smileUp.CustomEditors
{
    class TeethColorMappingItemSource : IItemsSource
    {
        public ItemCollection GetValues()
        {
            ItemCollection sizes = new ItemCollection();
            for(var i = 1; i < 33; i++){
            sizes.Add(""+i);
            }
            return sizes;
        }
    }
}
