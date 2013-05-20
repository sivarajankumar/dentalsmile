
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;
using System;

namespace smileUp.DataModel
{
    public class Wire
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
        public Brace Brace1{ get; set; }

        [XmlIgnore]
        public Brace Brace2 { get; set; }

        [XmlIgnore]
        public double Thicknes{ get; set; }

        [XmlIgnore]
        public String Material { get; set; }

        public Wire()
        {
        }

        public Wire(Point3D position, Color colour)
        {
            Position = position;
            Colour = colour;
        }
    }
}