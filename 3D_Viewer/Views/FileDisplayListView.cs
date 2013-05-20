/*
 * Derived class that filters data in the diagram view.
*/

using System;
using System.Windows;
using System.Windows.Controls;
using smileUp.Views;
using smileUp.DataModel;

namespace smileUp.Views
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    class FileDisplayListView : FileFilterSortListView
    {
        /// <summary>
        /// Called for each item in the list. Return true if the item should be in
        /// the current result set, otherwise return false to exclude the item.
        /// </summary>
        protected override bool FilterCallback(object item)
        {
            SmileFile t = item as SmileFile;
            if (t == null)
                return false;

            if (this.Filter.Matches(t.FileName) ||
                //this.Filter.Matches(t.Patient.FirstName) ||
                this.Filter.Matches(t.Screenshot) ||
                this.Filter.Matches(t.Type) ||
                this.Filter.Matches(t.Description) ||
                this.Filter.Matches(t.RefId) ||
                this.Filter.Matches(t.Id))
                return true;

            return false;
        }
    }
}
