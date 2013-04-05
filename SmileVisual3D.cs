using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using HelixToolkit.Wpf;

namespace smileUp
{
    public class SmileVisual3D : ModelVisual3D
    {
        ModelVisual3D target;

        public SmileVisual3D() 
        {
            
        }

        public SmileVisual3D(ModelVisual3D t)
        {
            this.target = t;

        }
        internal void createPlane()
        {
            if (target != null)
            {
                Rect3D r = target.Content.Bounds;
                GeometryModel3D cubeModel = GeometryGenerator.CreateCubeModel();
                cubeModel.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Red));

                Transform3DGroup transformGroup = new Transform3DGroup();
                transformGroup.Children.Add(new ScaleTransform3D(r.X, r.Y, 5));
                transformGroup.Children.Add(new TranslateTransform3D(0, 0, 0));
                cubeModel.Transform = transformGroup;

                this.Content = cubeModel;
            }
        }

        public Point3D ToLocal(Point3D worldPoint)
        {
            var mat = Visual3DHelper.GetTransform(this);
            mat.Invert();
            var t = new MatrixTransform3D(mat);
            return t.Transform(worldPoint);
        }

        public Point3D ToWorld(Point3D point)
        {
            var mat = Visual3DHelper.GetTransform(this);
            var t = new MatrixTransform3D(mat);
            return t.Transform(point);
        }

        public Vector3D ToWorld(Vector3D vector)
        {
            var mat = Visual3DHelper.GetTransform(this);
            var t = new MatrixTransform3D(mat);
            return t.Transform(vector);
        }

        public void SetMesh(MeshGeometry3D mesh)
        {
            if (this.Content is Model3DGroup)
            {
                Model3DGroup g = (Model3DGroup)this.Content;
                GeometryModel3D gm = (GeometryModel3D)g.Children[0];

                Model3DGroup gr = new Model3DGroup();
                var m = new GeometryModel3D(mesh, gm.Material);
                gr.Children.Add(m);
                this.Content = gr;
            }
            else
            {
                GeometryModel3D gm = (GeometryModel3D)this.Content;
                var m = new GeometryModel3D(mesh, gm.Material);
                this.Content = m;
            }            
        }
        public  MeshGeometry3D GetMesh()
        {
            MeshGeometry3D mesh = null;
            if (this.Content is Model3DGroup)
            {
                Model3DGroup g = (Model3DGroup)this.Content;
                foreach (GeometryModel3D gm in g.Children)
                {
                    mesh = (MeshGeometry3D)gm.Geometry;
                }
            }
            else
            {
                GeometryModel3D gm = (GeometryModel3D)this.Content;
                mesh = (MeshGeometry3D)gm.Geometry;
            }
            return mesh;

        }
        public MeshGeometry3D ToWorldMesh()
        {
            MeshBuilder mb = new MeshBuilder(false, false);

            MeshGeometry3D mesh = GetMesh();

            if (mesh != null)
            {
                Point3DCollection ind = mesh.Positions;
                for (int i = 0; i < ind.Count; i++)
                {
                    var p0 = ToWorld(ind[i]);
                    mb.Positions.Add(p0);
                }

                for (int i = 0; i < mesh.TriangleIndices.Count; i++)
                {
                    mb.TriangleIndices.Add(mesh.TriangleIndices[i]);
                }

            }
            return mb.ToMesh();
        }

        public MeshGeometry3D ToLocalMesh(MeshGeometry3D  mesh)
        {
            MeshBuilder mb = new MeshBuilder(false, false);

            if (mesh != null)
            {
                Point3DCollection ind = mesh.Positions;
                for (int i = 0; i < ind.Count; i++)
                {
                    var p0 = ToLocal(ind[i]);
                    mb.Positions.Add(p0);
                }

                for (int i = 0; i < mesh.TriangleIndices.Count; i++)
                {
                    mb.TriangleIndices.Add(mesh.TriangleIndices[i]);
                }

            }
            return mb.ToMesh();
        }

        public void Cut(Point3D position, Vector3D normal)
        {
            MeshGeometry3D worldMesh = ToWorldMesh();
            var geo = MeshGeometryHelper.Cut(worldMesh, position, normal);
            MeshGeometry3D localMesh = ToLocalMesh(geo);

            if (this.Content is Model3DGroup)
            {
                Model3DGroup g = (Model3DGroup)this.Content;
                GeometryModel3D gm = (GeometryModel3D)g.Children[0];

                Model3DGroup gr = new Model3DGroup();
                var m = new GeometryModel3D(localMesh, gm.Material);
                gr.Children.Add(m);
                this.Content = gr;
            }
            else
            {
                GeometryModel3D gm = (GeometryModel3D)this.Content;
                var m = new GeometryModel3D(localMesh, gm.Material);
                this.Content = m;
            }            
        }
    }
}
