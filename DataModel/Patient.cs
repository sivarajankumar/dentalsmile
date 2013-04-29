
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;
using System.Collections.Generic;
using System;

namespace smileUp.DataModel
{
    public class Patient : Person
    {
        private string id = "001";
        private string name = "MrX";
        private string photo = "default.jpg";

        public string Id { get { return id; } set { this.id = value; } }
        public string Name { get { return name; } set { this.name = value; } }
        public string Photo { get { return photo; } set { this.photo = value; } }
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