
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;
using System.Collections.Generic;
using System;

namespace smileUp
{
    public class Patient
    {
        public string Id { get; set ;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Gender { get; set; }
        public string Address1{ get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }


        private string name = "MrX";
        private string photo = "default.jpg";

        public string Photo { get { return photo; } set { this.photo = value; } }
        public string Name { get { return name; } set { this.name = value; } }
        public List<SmileFile> Files { get; set; }

        public Patient()
        {
            Files = new List<SmileFile>();
            //SmileFile f = new SmileFile();
            //f.Type = Smile.SCANNING;
            //Files.Add(f);
        }

        public string PhotoPath 
        { 
            get {
                return Smile.PHOTOS_PATH + "" + photo;
            } 
        }
        

    }
}