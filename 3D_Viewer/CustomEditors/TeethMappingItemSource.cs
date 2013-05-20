using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace smileUp.CustomEditors
{
    class TeethMappingItemSource : IItemsSource
    {
        public ItemCollection GetValues()
        {
            ItemCollection sizes = new ItemCollection();
            for(var i = 1; i < 33; i++){
            sizes.Add(i);
            }
            return sizes;
        }
    }
}
