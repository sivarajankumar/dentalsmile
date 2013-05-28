using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smileUp.DataModel
{
    public class Phase : IComparable<Phase>
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public int CompareTo(Phase other)
        {
            if (Id.CompareTo(other.Id) > 0)
                return 1;
            if (Name.CompareTo(other.Name) > 0)
                return 1;
            return 0;
        }
    }
}
