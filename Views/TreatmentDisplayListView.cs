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
    class TreatmentDisplayListView : TreatmentFilterSortListView
    {
        /// <summary>
        /// Called for each item in the list. Return true if the item should be in
        /// the current result set, otherwise return false to exclude the item.
        /// </summary>
        protected override bool FilterCallback(object item)
        {
            Treatment t = item as Treatment;
            if (t == null)
                return false;

            if (this.Filter.Matches(t.Room) ||
                this.Filter.Matches(t.Patient.FirstName) ||
                this.Filter.Matches(t.Patient.LastName) ||
                this.Filter.MatchesYear(t.TreatmentDate) ||
                this.Filter.Matches(t.Phase.Name) ||
                this.Filter.Matches(t.TreatmentDate) ||
                this.Filter.Matches(t.Dentist.FirstName) ||
                this.Filter.Matches(t.Dentist.LastName) ||
                this.Filter.Matches(t.TreatmentTime))
                return true;

            return false;
        }
    }
}
