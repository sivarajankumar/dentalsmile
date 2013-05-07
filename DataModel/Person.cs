using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smileUp.DataModel
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Gender { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }

        public Person()
        {
        }
    }
}
