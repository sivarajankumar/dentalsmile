using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using HelixToolkit.Wpf;
using System.Windows;
using System.Windows.Data;

namespace smileUp
{
    public class SmileVisual3D : ModelVisual3D
    {
        ModelVisual3D target;
        public static readonly DependencyProperty TargetTransformProperty = DependencyProperty.Register("TargetTransform", typeof(Transform3D), typeof(SmileVisual3D), new PropertyMetadata(TargetTransformChanged));

        protected static void TargetTransformChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //detect
            if (d is TeethVisual3D)
            {
                TeethVisual3D dt = (TeethVisual3D)d;
                GumVisual3D gum = dt.Parent;
                foreach (var t in gum.Children)
                {
                    if (t is TeethVisual3D)
                    {
                        TeethVisual3D teeth = (TeethVisual3D)t;
                        if (!teeth.Equals(d))
                        {
                            if (teeth.IsInside(dt))
                            {
                                Console.WriteLine(dt.Id+"TargetTransformChanged. TODO: detect collision detections"+teeth.Id);

                            }
                        }
                    }else
                        if (t is BraceVisual3D)
                        {
                        }
                }
            }
        }
         

        public SmileVisual3D() 
        {

        }

        public SmileVisual3D(ModelVisual3D t)
        {
            this.target = t;

        }
        public virtual void Bind(ModelVisual3D source)
        {
            BindingOperations.SetBinding(this, TargetTransformProperty, new Binding("Transform") { Source = source });
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

        bool IsInside(ModelVisual3D other)
        {
            Rect3D r = this.Content.Bounds;
            Rect3D o = other.Content.Bounds;
            return r.IntersectsWith(o);        
            /*return r.X <= o.X &&
                   r.Y <= o.Y &&
                   r.Z <= o.Z &&
                   o.X >= r.X &&
                   o.Y >= r.Y &&
                   o.Z >= r.Z;
             */
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
            //MeshGeometry3D localMesh = geo;
            if (this.Content is Model3DGroup)
            {
                Model3DGroup g = (Model3DGroup)this.Content;
                GeometryModel3D gm = (GeometryModel3D)g.Children[0];

                Model3DGroup gr = new Model3DGroup();
                var m = new GeometryModel3D(localMesh, gm.Material);
                m.BackMaterial = MaterialHelper.CreateMaterial(Brushes.Black);

                gr.Children.Add(m);
                this.Content = gr;
            }
            else
            {
                GeometryModel3D gm = (GeometryModel3D)this.Content;
                var m = new GeometryModel3D(localMesh, gm.Material);
                m.BackMaterial = MaterialHelper.CreateMaterial(Brushes.Black);
                this.Content = m;
            }            
        }

        public void drawEdges(MeshGeometry3D mesh)
        {
            Int32Collection edges = MeshGeometryHelper.FindEdges(mesh);
            Point3DCollection pos = mesh.Positions;
            for (int i = 1; i < edges.Count; i += 2)
            {
                Point3DCollection paths = new Point3DCollection();
                paths.Add(pos[edges[i - 1]]);
                paths.Add(pos[edges[i]]);
                TubeVisual3D t = new TubeVisual3D { Diameter = 0.02, Path = paths, Fill = Brushes.Yellow };
                //t.Transform = new TranslateTransform3D(1, 1, 1);
                this.Children.Add(t);
            }
        }
        public void drawBorderEdges(MeshGeometry3D mesh)
        {
            Int32Collection edges = MeshGeometryHelper.FindBorderEdges(mesh);
            Point3DCollection pos = mesh.Positions;
            for (int i = 1; i < edges.Count; i += 2)
            {
                Point3DCollection paths = new Point3DCollection();
                paths.Add(pos[edges[i - 1]]);
                paths.Add(pos[edges[i]]);
                TubeVisual3D t = new TubeVisual3D { Diameter = 0.02, Path = paths, Fill = Brushes.Yellow };
                //t.Transform = new TranslateTransform3D(1, 1, 1);
                this.Children.Add(t);
            }
        }
        public static Dictionary<Int32, SmileEdge> findNeighbours(MeshGeometry3D mesh)
        {
            Int32Collection edges = MeshGeometryHelper.FindEdges(mesh);
            Int32Collection triangles = mesh.TriangleIndices;

            Dictionary<Int32, SmileEdge> l = new Dictionary<Int32, SmileEdge>();
            for (int j = 0; j < triangles.Count; j++)
            {
                SmileEdge v = new SmileEdge();
                v.me = triangles[j];
                for (int i = 1; i < edges.Count; i += 2)
                {
                    if (edges[i - 1] == triangles[j])
                    {
                        v.neighbours.Add(edges[i]);
                    }
                    else
                        if (edges[i] == triangles[j])
                        {
                            v.neighbours.Add(edges[i - 1]);
                        }
                }
                if (!l.ContainsKey(v.me))
                    l.Add(v.me, v);
            }
            return l;
        }

        public static Vector3D CalculateNormal(Point3D p0, Point3D p1, Point3D p2)
        {
            Vector3D u = p1 - p0;
            Vector3D v = p2 - p0;
            Vector3D w = Vector3D.CrossProduct(u, v);
            //w.Normalize();
            //return w;    
            //Vector3D v0 = new Vector3D(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            //Vector3D v1 = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            //Vector3D w = Vector3D.CrossProduct(v0, v1);
            //w.Normalize();
            return w;
        }
    }


    public class SmileEdge
    {
        public Int32 me;
        public Int32Collection neighbours = new Int32Collection();
    }

}
