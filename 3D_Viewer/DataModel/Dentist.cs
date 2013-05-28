using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smileUp.DataModel
{
    public class Dentist : Person, IComparable<Dentist>
    {
        public string UserId { get; set; }
        public bool IsUser { get; set; }

        public int CompareTo(Dentist other)
        {
            if (UserId.CompareTo(other.UserId) > 0)
                return 1;
            return 0;
        }
    }
}
