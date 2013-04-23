
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;

namespace smileUp
{
    public class GumContainer : ModelVisual3D
    {
        public GumContainer()
        {

        }
        public void show(GumVisual3D t)
        {
            this.Children.Add(t);
        }

        public void hide(GumVisual3D t)
        {
            this.Children.Remove(t);
        }

    }
}