
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;
using System;

namespace smileUp
{
    public class Teeth
    {
        [XmlAttribute("Position")]
        public string XmlPosition
        {
            get { return Position.ToString(); }
            set { Position = Point3D.Parse(value.Replace(';',',')); }
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
        public double Length { get; set; }
        
        [XmlIgnore]
        public Patient Patient { get; set; }

        [XmlIgnore]
        public int TeethNumber { get; set; }

        [XmlIgnore]
        public double StartPosition { get; set; }

        [XmlIgnore]
        public double EndPosition { get; set; }

        public Teeth()
        {
        }

        public Teeth(Point3D position, Color colour)
        {
            Position = position;
            Colour = colour;
        }
    }
}