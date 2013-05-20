using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace smileUp.Algorithm
{
    class SegmentationB
    {
        void main()
        {

        }
        //ref: Snake-Based Segmentation of Teeth from Virtual Dental Casts
        //pLane located manually, set of verteks D and P from manual cutting plane
        //the plan is: teeth and gum separated then teeth segmented using snake below
        public static void snake(MeshGeometry3D mesh)
        {
            Dictionary<Int32, SmileEdge> neighbours = SmileVisual3D.findNeighbours(mesh);
            Point3DCollection ind = mesh.Positions;
            Vector3DCollection t = new Vector3DCollection();
            Vector3DCollection b = new Vector3DCollection();
            Vector3DCollection n = new Vector3DCollection();
            for (int i = 0; i < ind.Count; i++)
            {
                Point3D pi = ind[i];
                int i0 = i;
                if (i - 1 >= 0)  i0 = i - 1;
                Point3D pi0 = ind[i0]; 
                int i1 = i;
                if (i + i < ind.Count)   i = i + 1;
                Point3D pi1 = ind[i1];

                t[i] = (pi1 - pi0) / Math.Sqrt((Point3D.Subtract(pi1, pi0).Length));
                n[i] = SmileVisual3D.CalculateNormal(pi0,pi,pi1);
                b[i] = Vector3D.CrossProduct(t[i], n[i]);
            }
        }

    }
}
