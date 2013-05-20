
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;

namespace smileUp
{
    public class BaseMarker
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

        public BaseMarker()
        {
        }

        public BaseMarker(Point3D position, Color colour)
        {
            Position = position;
            Colour = colour;
        }
    }
}