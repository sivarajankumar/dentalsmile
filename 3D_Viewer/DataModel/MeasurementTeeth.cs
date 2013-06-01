using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smileUp.DataModel
{
    public class MeasurementTeeth
    {
        public int Id { get; set; }
        public string Identity { get; set; }
        public double Length { get; set; }
        public string SPoint { get; set; }
        public string EPoint { get; set; }
        public string Type { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public Boolean Loaded { get; set; }

        public MeasurementTeeth(string id, double length, string spoint, string epoint, string type, string modified_date, bool loaded)
        {
            this.Identity = id;
            this.Length = length;
            this.SPoint = spoint;
            this.EPoint = epoint;
            this.Type = type;
            this.ModifiedDate = modified_date;
            this.Loaded = loaded;
        }

        public MeasurementTeeth()
        {
        }

    }
}
