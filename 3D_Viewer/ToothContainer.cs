
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;

namespace smileUp
{
    public class ToothContainer : ModelVisual3D
    {
        public ToothContainer()
        {

        }

        public void show(TeethVisual3D t)
        {
            this.Children.Add(t);
        }

        public void hide(TeethVisual3D t)
        {
            this.Children.Remove(t);
        }


    }
}