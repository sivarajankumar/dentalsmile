using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smileUp.DataModel
{
    public class Treatment : IComparable<Treatment>
    {
        public string Id { get; set; }
        public Phase Phase { get; set; }
        public Dentist Dentist { get; set; }
        public Patient Patient { get; set; }
        public DateTime TreatmentDate { get; set; }
        public string TreatmentTime { get; set; }
        public string Room { get; set; }
        
        public List<SmileFile> Files { get; set; }
        public string RefId { get; set; }

        public int CompareTo(Treatment t)
        {        
            if (TreatmentDate.CompareTo(t.TreatmentDate) > 0)
                return 1;
            if (TreatmentTime.CompareTo(t.TreatmentTime) > 0)
                return 1;
            if (Id.CompareTo(t.Id) > 0)
                return 1;
            return 0;
        }
    }
}
