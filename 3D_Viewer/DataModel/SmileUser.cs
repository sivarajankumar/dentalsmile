using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smileUp.DataModel
{
    public class SmileUser
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        //public Person Person { get; set; }
        public Dentist Dentist { get; set; }
        public bool Admin { get; set; }
    }
}
