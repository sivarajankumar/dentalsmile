using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smileUp.DataModel
{
    public class Measurement
    {
        public int Id { get; set; }
        public string Patient { get; set; }
        public string Treatment { get; set; }
        public string Pfile { get; set; }
        public string Type { get; set; }

        public Measurement()
        {
        }
        

    }
}
