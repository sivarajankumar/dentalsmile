using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace smileUp.Calendar
{
    /// <summary>
    /// Interaction logic for MonthView.xaml
    /// </summary>
    public partial class MonthView : UserControl
    {
        static DateTime displayStartDate = DateTime.Now;
        int month = displayStartDate.Month;
        int year = displayStartDate.Year;
        List<Appointment> appointments;

        public MonthView()
        {
            InitializeComponent();
            appointments = new List<Appointment>();
            Loaded += new RoutedEventHandler(MonthView_Loaded);
            DayBoxDoubleClicked += new RoutedEventHandler(MonthView_DayBoxDoubleClicked);
            DisplayMonthChanged += new RoutedEventHandler(MonthView_DisplayMonthChanged);
        }


        void MonthView_Loaded(object sender, RoutedEventArgs e)
        {
            if (appointments == null) BuildCalendarUI();
        }

        public DateTime DisplayStartDate
        {
            get { return displayStartDate; }
            set
            {
                displayStartDate = value;
                month = displayStartDate.Month;
                year = displayStartDate.Year;
            }
        }

        public List<Appointment> Appointments
        {
            get { return appointments; }
            set { this.appointments = value; }
        }

        private void BuildCalendarUI()
        {
            int iDaysInMonth = DateTime.DaysInMonth(DisplayStartDate.Year, DisplayStartDate.Month);
            //int iOffsetDays = System.Enum.ToObject(System.DayOfWeek, DisplayStartDate.DayOfWeek);
            int iOffsetDays = 1;
            int iWeekCount = 0;
            WeekOfDaysControl weekRowCtrl = new WeekOfDaysControl();
            MonthViewGrid.Children.Clear();
            AddRowsToMonthGrid(iDaysInMonth, iOffsetDays);
            MonthYearLabel.Content = month +" "+year;

            for (int i = 1; i < iDaysInMonth; i++)
            {
                if ( (i != 1) && System.Math.IEEERemainder((i + iOffsetDays - 1), 7) == 0)
                {
                    Grid.SetRow(weekRowCtrl, iWeekCount);
                    MonthViewGrid.Children.Add(weekRowCtrl);
                    weekRowCtrl = new WeekOfDaysControl();
                    iWeekCount++;
                }

                DayBoxControl dayBox = new DayBoxControl();
                dayBox.Name = "DayBox" + i;
                dayBox.DayNumberLabel.Content = i.ToString();
                dayBox.Tag = i;
                dayBox.MouseDoubleClick += new MouseButtonEventHandler(dayBox_MouseDoubleClick);
                dayBox.PreviewDragEnter += new DragEventHandler(dayBox_PreviewDragEnter);
                dayBox.PreviewDragLeave += new DragEventHandler(dayBox_PreviewDragLeave);
                
                //rest the namescope of the daybox in case user drags appointment from this day to another day, then back again
                System.Windows.NameScope.SetNameScope(dayBox, new System.Windows.NameScope());
                RegisterName("DayBox" + i.ToString(), dayBox);

                //-- resets the list of control-names registered with this monthview (to avoid duplicates later)
                System.Windows.NameScope.SetNameScope(dayBox, new System.Windows.NameScope());
                RegisterName("DayBox" + i.ToString(), dayBox);

                if (DateTime.Now.Day == i)
                {
                    dayBox.DayLabelRowBorder.Background = Brushes.Aqua;
                    dayBox.DayAppointmentsStack.Background = Brushes.Wheat;
                }
                Grid.SetColumn(dayBox, (i - (iWeekCount * 7)) + iOffsetDays);
                weekRowCtrl.WeekRowGrid.Children.Add(dayBox);
            }
            Grid.SetRow(weekRowCtrl, iWeekCount);
            MonthViewGrid.Children.Add(weekRowCtrl);
        }

        void dayBox_PreviewDragLeave(object sender, DragEventArgs e)
        {
        }

        void dayBox_PreviewDragEnter(object sender, DragEventArgs e)
        {
        }

        void dayBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is DayBoxControl && e.OriginalSource is DayBoxAppointmentControl) 
            {
                NewAppointmentEventArgs ev = new NewAppointmentEventArgs();
                DayBoxControl d = e.Source as DayBoxControl;
                MonthView_DayBoxDoubleClicked(sender, ev);            
                //RaiseEvent(DayBoxDoubleClicked(ev));
                e.Handled = true;
            }
        }

        void MonthView_DayBoxDoubleClicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("MonthView_DayBoxDoubleClicked");
        }

        void MonthView_DisplayMonthChanged(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("MonthView_DisplayMonthChanged");
        }


        private void MonthGoNext_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateMonth(-1);
        }

        private void MonthGoPrev_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateMonth(1);
        }

        private void UpdateMonth(int p)
        {
            MonthChangedEventArgs args = new MonthChangedEventArgs();
            args.OldDisplayStartDate = DisplayStartDate;
            DisplayStartDate = DisplayStartDate.AddMonths(p);
            args.NewDisplayStartDate = DisplayStartDate;
            MonthView_DisplayMonthChanged(null, args);
            //RaiseEvent( args);
        }
        public void AddRowsToMonthGrid(int DaysInMonth, int OffSetDays)
        {
            MonthViewGrid.RowDefinitions.Clear();
            
            GridLength rowHeight = new GridLength(60, GridUnitType.Star);
            int EndOffSetDays = 7 - (DateTime.Now.Day);
            
            for(int i=1;i< (DaysInMonth + OffSetDays + EndOffSetDays);i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = rowHeight;
                MonthViewGrid.RowDefinitions.Add(rd);
            }
        }

        public static readonly RoutedEvent DisplayMonthChangedEvent = EventManager.RegisterRoutedEvent(
"DisplayMonthChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MonthView));

        public event RoutedEventHandler DisplayMonthChanged
        {
            add { AddHandler(DisplayMonthChangedEvent, value); }
            remove { RemoveHandler(DisplayMonthChangedEvent, value); }
        }

        public static readonly RoutedEvent DayBoxDoubleClickedEvent = EventManager.RegisterRoutedEvent(
"DayBoxDoubleClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MonthView));

        public event RoutedEventHandler DayBoxDoubleClicked
        {
            add { AddHandler(DayBoxDoubleClickedEvent, value); }
            remove { RemoveHandler(DayBoxDoubleClickedEvent, value); }
        }

        public static readonly RoutedEvent AppointmentDblClickedEvent = EventManager.RegisterRoutedEvent(
"AppointmentDblClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MonthView));

        public event RoutedEventHandler AppointmentDblClicked
        {
            add { AddHandler(AppointmentDblClickedEvent, value); }
            remove { RemoveHandler(AppointmentDblClickedEvent, value); }
        }

        public static readonly RoutedEvent AppointmentMovedEvent = EventManager.RegisterRoutedEvent(
"AppointmentMoved", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MonthView));
        
        public event RoutedEventHandler AppointmentMoved
        {
            add { AddHandler(AppointmentMovedEvent, value); }
            remove { RemoveHandler(AppointmentMovedEvent, value); }
        }
    }

    public class MonthChangedEventArgs : RoutedEventArgs
    {
        public DateTime OldDisplayStartDate;
        public DateTime NewDisplayStartDate;
    }
    public class NewAppointmentEventArgs : RoutedEventArgs
    {
        public DateTime StartDate;
        public DateTime EndDate;
        public int CandidateId;
        public int RequirementId;
    }
}
