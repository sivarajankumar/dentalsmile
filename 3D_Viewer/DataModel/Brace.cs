
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;
using System;

namespace smileUp.DataModel
{
    public class Brace : Observable
    {
        [XmlAttribute("Position")]
        public string XmlPosition
        {
            get { return Position.ToString(); }
            set { Position = Point3D.Parse(value.Replace(';', ',')); RaisePropertyChanged("Position"); }
        }

        [XmlAttribute("Colour")]
        public string XmlColour
        {
            get { return Colour.ToString(); }
            set
            {
                var obj = ColorConverter.ConvertFromString(value);
                if (obj != null) Colour = (Color)obj;
            }
        }

        [XmlIgnore]
        public Point3D Position { get; set; }

        [XmlIgnore]
        public Color Colour { get; set; }

        [XmlIgnore]
        public String Id { get; set; }

        [XmlIgnore]
        public Patient Patient { get; set; }

        [XmlIgnore]
        public Teeth Teeth { get; set; }

        [XmlIgnore]
        public int Location { get; set; }

        public Brace()
        {
        }

        public Brace(Point3D position, Color colour)
        {
            Position = position;
            Colour = colour;
        }
    }
}