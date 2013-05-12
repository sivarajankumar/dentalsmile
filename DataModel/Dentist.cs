using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smileUp.DataModel
{
    public class Dentist : Person
    {
        public string UserId { get; set; }
        public bool IsUser { get; set; }
    }
}
