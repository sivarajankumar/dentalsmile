using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace smileUp
{
    class GeometryGenerator
    {
        public static GeometryModel3D CreateCubeModel()
        {
            MeshGeometry3D mesh = new MeshGeometry3D();

            mesh.Positions.Add(new Point3D(-1, -1, 1));
            mesh.Positions.Add(new Point3D(1, -1, 1));
            mesh.Positions.Add(new Point3D(1, 1, 1));
            mesh.Positions.Add(new Point3D(-1, 1, 1));
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.Normals.Add(new Vector3D(0, 0, 1));
            mesh.Normals.Add(new Vector3D(0, 0, 1));
            mesh.Normals.Add(new Vector3D(0, 0, 1));
            mesh.Normals.Add(new Vector3D(0, 0, 1));

            mesh.Positions.Add(new Point3D(1, -1, 1));
            mesh.Positions.Add(new Point3D(1, -1, -1));
            mesh.Positions.Add(new Point3D(1, 1, -1));
            mesh.Positions.Add(new Point3D(1, 1, 1));
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(7);
            mesh.Normals.Add(new Vector3D(1, 0, 0));
            mesh.Normals.Add(new Vector3D(1, 0, 0));
            mesh.Normals.Add(new Vector3D(1, 0, 0));
            mesh.Normals.Add(new Vector3D(1, 0, 0));

            mesh.Positions.Add(new Point3D(1, -1, -1));
            mesh.Positions.Add(new Point3D(-1, -1, -1));
            mesh.Positions.Add(new Point3D(-1, 1, -1));
            mesh.Positions.Add(new Point3D(1, 1, -1));
            mesh.TriangleIndices.Add(8);
            mesh.TriangleIndices.Add(9);
            mesh.TriangleIndices.Add(10);
            mesh.TriangleIndices.Add(8);
            mesh.TriangleIndices.Add(10);
            mesh.TriangleIndices.Add(11);
            mesh.Normals.Add(new Vector3D(0, 0, -1));
            mesh.Normals.Add(new Vector3D(0, 0, -1));
            mesh.Normals.Add(new Vector3D(0, 0, -1));
            mesh.Normals.Add(new Vector3D(0, 0, -1));

            mesh.Positions.Add(new Point3D(-1, -1, -1));
            mesh.Positions.Add(new Point3D(-1, -1, 1));
            mesh.Positions.Add(new Point3D(-1, 1, 1));
            mesh.Positions.Add(new Point3D(-1, 1, -1));
            mesh.TriangleIndices.Add(12);
            mesh.TriangleIndices.Add(13);
            mesh.TriangleIndices.Add(14);
            mesh.TriangleIndices.Add(12);
            mesh.TriangleIndices.Add(14);
            mesh.TriangleIndices.Add(15);
            mesh.Normals.Add(new Vector3D(-1, 0, 0));
            mesh.Normals.Add(new Vector3D(-1, 0, 0));
            mesh.Normals.Add(new Vector3D(-1, 0, 0));
            mesh.Normals.Add(new Vector3D(-1, 0, 0));

            mesh.Positions.Add(new Point3D(-1, -1, 1));
            mesh.Positions.Add(new Point3D(-1, -1, -1));
            mesh.Positions.Add(new Point3D(1, -1, -1));
            mesh.Positions.Add(new Point3D(1, -1, 1));
            mesh.TriangleIndices.Add(16);
            mesh.TriangleIndices.Add(17);
            mesh.TriangleIndices.Add(18);
            mesh.TriangleIndices.Add(16);
            mesh.TriangleIndices.Add(18);
            mesh.TriangleIndices.Add(19);
            mesh.Normals.Add(new Vector3D(0, -1, 0));
            mesh.Normals.Add(new Vector3D(0, -1, 0));
            mesh.Normals.Add(new Vector3D(0, -1, 0));
            mesh.Normals.Add(new Vector3D(0, -1, 0));

            mesh.Positions.Add(new Point3D(-1, 1, 1));
            mesh.Positions.Add(new Point3D(1, 1, 1));
            mesh.Positions.Add(new Point3D(1, 1, -1));
            mesh.Positions.Add(new Point3D(-1, 1, -1));
            mesh.TriangleIndices.Add(20);
            mesh.TriangleIndices.Add(21);
            mesh.TriangleIndices.Add(22);
            mesh.TriangleIndices.Add(20);
            mesh.TriangleIndices.Add(22);
            mesh.TriangleIndices.Add(23);
            mesh.Normals.Add(new Vector3D(0, 1, 0));
            mesh.Normals.Add(new Vector3D(0, 1, 0));
            mesh.Normals.Add(new Vector3D(0, 1, 0));
            mesh.Normals.Add(new Vector3D(0, 1, 0));

            GeometryModel3D model = new GeometryModel3D();
            model.Geometry = mesh;
            return model;

        }
    }
}
