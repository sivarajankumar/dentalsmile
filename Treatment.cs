using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smileUp
{
    class Treatment
    {
        public string Id { get; set; }
        public Phase Phase { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public DateTime TreatmentDate { get; set; }
        public string TreatmentTime { get; set; }
        public string Room { get; set; }

    }
}
