using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smileUp.DataModel;

namespace smileUp.Calendar
{
    public class Appointment
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Dentist Dentist { get; set; }
        public string Room { get; set; }
        public string Aptime { get; set; }
        public DateTime ApDate { get; set; }
        public string Notes { get; set; }
        public string Subject { get; set; }

    }
}
