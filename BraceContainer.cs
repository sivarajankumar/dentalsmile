
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;

namespace smileUp
{
    public class BraceContainer : ModelVisual3D
    {
        public BraceContainer()
        {

        }

        public void show(BraceVisual3D t)
        {
            this.Children.Add(t);
        }

        public void hide(BraceVisual3D t)
        {
            this.Children.Remove(t);
        }

    }
}