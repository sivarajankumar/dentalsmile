
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;

namespace smileUp
{
    public class WireContainer : ModelVisual3D
    {
        public WireContainer()
        {

        }

        public void show(WireVisual3D wire)
        {
            this.Children.Add(wire);
        }

        public void hide(WireVisual3D wire)
        {
            this.Children.Remove(wire);        
        }
    }
}