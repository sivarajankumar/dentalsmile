
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;
using HelixToolkit.Wpf;
using System.Collections.Generic;

namespace smileUp
{
    public class MeasurementContainer : SmileVisual3D
    {
        public MeasurementContainer()
        {

        }

        public void addBox(Point3D point)
        {
            BoxVisual3D box = new BoxVisual3D();
            box.Center = point;
            box.Width = 1;
            box.Height = 1;
            box.Length= 1;
            this.Children.Add(box);
        }

        public void removeBox(BoxVisual3D box)
        {
            this.Children.Remove(box);        
        }

        public Dictionary<Point3D, TubeVisual3D> lines = new Dictionary<Point3D, TubeVisual3D>();

        internal void drawLine(Point3D start, Point3D pt)
        {
            TubeVisual3D line;
            lines.TryGetValue(start, out line);

            if (line != null)
            {
                this.Children.Remove(line);
                lines.Remove(start);
            }
            Point3DCollection contours = new Point3DCollection();
            contours.Add(ToWorld(start));
            contours.Add(ToWorld(pt));
            line = new TubeVisual3D { Diameter = 0.5, Path = contours, Fill = Brushes.SandyBrown };
            lines.Add(start,line);
            this.Children.Add(line);

            addBox(start);
            addBox(pt);
        }
    }
}