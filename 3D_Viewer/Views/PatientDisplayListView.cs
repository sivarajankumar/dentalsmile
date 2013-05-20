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
    class PatientDisplayListView : PersonFilterSortListView
    {
        /// <summary>
        /// Called for each item in the list. Return true if the item should be in
        /// the current result set, otherwise return false to exclude the item.
        /// </summary>
        protected override bool FilterCallback(object item)
        {
            Patient person = item as Patient;
            if (person == null)
                return false;

            if (this.Filter.Matches(person.FirstName) ||
                this.Filter.Matches(person.LastName) || 
                this.Filter.MatchesYear(person.BirthDate) ||
                this.Filter.Matches(person.BirthDate) ||
                this.Filter.Matches(person.City) ||
                this.Filter.Matches(person.Address1) ||
                this.Filter.Matches(person.Address2) ||
                this.Filter.Matches(person.Gender))
                return true;

            return false;
        }
    }
}
