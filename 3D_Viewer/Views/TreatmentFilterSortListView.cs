/*
 * A base class that sorts the data in a ListView control.
*/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Threading;
using System.Globalization;

namespace smileUp.Views
{
    /// <summary>
    /// Class that parses the filter text.
    /// </summary>
    public class TreatmentFilter
    {
        // Parsed data from the filter string.
        private string filterText;
        private DateTime? filterDate;

        /// <summary>
        /// Indicates if the filter is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(this.filterText); }
        }

        /// <summary>
        /// Return true if the filter contains the specified text.
        /// </summary>
        public bool Matches(string text)
        {
            return (this.filterText != null && text != null &&
                text.ToLower(CultureInfo.CurrentCulture).Contains(this.filterText));
        }

        /// <summary>
        /// Return true if the filter contains the specified date.
        /// </summary>
        public bool Matches(DateTime? date)
        {
            return (date != null && date.Value.ToShortDateString().Contains(this.filterText));
        }

        /// <summary>
        /// Return true if the filter contains the year in the specified date.
        /// </summary>
        public bool MatchesYear(DateTime? date)
        {
            return (date != null && date.Value.Year.ToString(CultureInfo.CurrentCulture).Contains(this.filterText));
        }

        /// <summary>
        /// Return true if the filter contains the month in the specified date.
        /// </summary>
        public bool MatchesMonth(DateTime? date)
        {
            return (date != null && this.filterDate != null &&
                date.Value.Month == this.filterDate.Value.Month);
        }

        /// <summary>
        /// Return true if the filter contains the day in the specified date.
        /// </summary>
        public bool MatchesDay(DateTime? date)
        {
            return (date != null && this.filterDate != null &&
                date.Value.Day == this.filterDate.Value.Day);
        }


        /// <summary>
        /// Parse the specified filter text.
        /// </summary>
        public void Parse(string text)
        {
            // Initialize fields.
            this.filterText = "";
            this.filterDate = null;

            // Store the filter text.
            this.filterText = string.IsNullOrEmpty(text) ? "" : text.ToLower(CultureInfo.CurrentCulture).Trim();

            // Parse date and age.
            ParseDate();
        }

        /// <summary>
        /// Parse the filter date.
        /// </summary>
        private void ParseDate()
        {
            DateTime date;
            if (DateTime.TryParse(this.filterText, out date))
                this.filterDate = date;
        }

    }


    /// <summary>
    /// ??
    /// </summary>
    public class TreatmentFilterSortListView : SortListView
    {
        private delegate void FilterDelegate();
        private TreatmentFilter filter = new TreatmentFilter();

        /// <summary>
        /// Get the filter for this control.
        /// </summary>
        protected TreatmentFilter Filter
        {
            get { return this.filter; }
        }

        /// <summary>
        /// Filter the data using the specified filter text.
        /// </summary>
        public void FilterList(string text)
        {
            // Setup the filter object.
            filter.Parse(text);

            // Start an async operation that filters the list.
            this.Dispatcher.BeginInvoke(
                DispatcherPriority.ApplicationIdle,
                new FilterDelegate(FilterWorker));
        }

        /// <summary>
        /// Worker method that filters the list.
        /// </summary>
        private void FilterWorker()
        {
            // Get the data the ListView is bound to.
            ICollectionView view = CollectionViewSource.GetDefaultView(this.ItemsSource);

            if (view != null)
            {
                // Clear the list if the filter is empty, otherwise filter the list.
                view.Filter = filter.IsEmpty ? null :
                    new Predicate<object>(FilterCallback);
            }
        }

        /// <summary>
        /// This is called for each item in the list. The derived classes 
        /// override this method.
        /// </summary>
        virtual protected bool FilterCallback(object item)
        {
            return false;
        }
    }
}
