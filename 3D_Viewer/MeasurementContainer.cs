
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
        public Dictionary<Point3D, BoxVisual3D> boxes = new Dictionary<Point3D, BoxVisual3D>();

        public void addBox(Point3D point)
        {
            BoxVisual3D box;
            boxes.TryGetValue(point, out box);

            if (box != null)
            {
                this.Children.Remove(box);
                boxes.Remove(point);
            }
            box= new BoxVisual3D();
            box.Center = point;
            box.Width = 1;
            box.Height = 1;
            box.Length= 1;
            this.Children.Add(box);
            boxes.Add(point, box);
        }

        public void removeBox(Point3D point)
        {
            BoxVisual3D box;
            boxes.TryGetValue(point, out box);
            if (box != null)
            { this.Children.Remove(box); }
        }

        public Dictionary<Point3D, TubeVisual3D> lines = new Dictionary<Point3D, TubeVisual3D>();

/*        internal void drawLine(Point3D start, Point3D pt, bool hit)
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
            if(hit) addBox(pt);
        }
*/
        internal void drawLine(Point3D start, Point3D end)
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
            contours.Add(ToWorld(end));
            line = new TubeVisual3D { Diameter = 0.3, Path = contours, Fill = Brushes.DarkOliveGreen};
            lines.Add(start, line);
            this.Children.Add(line);
            
            string x = string.Format("{0:0.00}",end.X);
            string y = string.Format("{0:0.00}", end.Y);
            string z = string.Format("{0:0.00}", end.Z);

            string text = x + "," + y + "," + z;
            addBox(start);
            addBox(end);
            add3DText(end, text);
        }

        internal void undrawLine(Point3D start, Point3D end)
        {
            TubeVisual3D line;            
            lines.TryGetValue(start, out line);
            if (line != null)
            {
                this.Children.Remove(line);
                this.removeBox(start);
                this.removeBox(end);
                this.remove3DText(end);
            }


        }

        public Dictionary<Point3D, BillboardTextVisual3D> _text = new Dictionary<Point3D, BillboardTextVisual3D>();
        internal void add3DText(Point3D point, string text)
        {
          BillboardTextVisual3D txt = new BillboardTextVisual3D();
          point.Y = point.Y + 5;
          txt.Position = point;
          txt.FontSize = 12;
          txt.Text = text;
          txt.Height = 25;
          this.Children.Add(txt);
          _text.Add(point, txt);
        }

        internal void remove3DText(Point3D point)
        {
            BillboardTextVisual3D txt;
            _text.TryGetValue(point, out txt);
            if (txt != null)
            { this.Children.Remove(txt); }
        }

    }

}