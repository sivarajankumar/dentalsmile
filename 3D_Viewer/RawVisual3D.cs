using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using HelixToolkit.Wpf;
using System.Windows;

namespace smileUp
{
    public class RawVisual3D : SmileVisual3D
    {

        //public static readonly DependencyProperty ShowBoundingBoxProperty = DependencyProperty.Register(
   //         "ShowBoundingBox", typeof(int), typeof(RawVisual3D), new UIPropertyMetadata(false, GeometryChanged));

        private CombinedManipulator _manipulator;
        public CombinedManipulator Manipulator { get; set; }
        private RectangleVisual3D _plane;
        private RectangleVisual3D Plane { get; set; }
        private BoundingBoxWireFrameVisual3D _boundingBox;
        private BoundingBoxWireFrameVisual3D BoundingBox { get; set; }

        Boolean showManipulator = true;
        Boolean showPlane = true;

        private Boolean showBoundingBox = true;
        public Boolean ShowBoundingBox {
            get
            {
                return showBoundingBox;
        //        return (Boolean)this.GetValue(ShowBoundingBoxProperty);
            }

            set
            {
                this.showBoundingBox = value; 
                //       this.SetValue(ShowBoundingBoxProperty, value);
            }
        }

        ModelVisual3D parent;

        public RawVisual3D() 
        {
            
        }

        public RawVisual3D(ModelVisual3D p)
        {
            this.parent = p;
        }

        private static void GeometryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //((Visual3D)d).OnPositionChanged();
        }

        public void showHideBoundingBox()
        {
            if (ShowBoundingBox)
            {
                if (_boundingBox == null)
                {
                    _boundingBox = new BoundingBoxWireFrameVisual3D();
                    _boundingBox.BoundingBox = this.Content.Bounds;
                    this.Children.Add(_boundingBox);
                    showBoundingBox = false;
                }
            }
            else
            {
                this.Children.Remove(_boundingBox);
                _boundingBox = null;
                showBoundingBox = true;
            }
        }

        public void showHideManipulator()
        {
            if (showManipulator)
            {
                if (_manipulator == null)
                {
                    Rect3D r = this.Content.Bounds;
                    _manipulator = new CombinedManipulator();
                    //_manipulator.Position = new Point3D(r.X + (r.SizeX/2),r.Y + (r.SizeY / 2),r.Z + (r.SizeZ/2));
                    _manipulator.Offset = new Vector3D(r.X + (r.SizeX / 2), r.Y + (r.SizeY / 2), r.Z + (r.SizeZ / 2));
                    _manipulator.Pivot = new Point3D(r.X + (r.SizeX / 2), r.Y + (r.SizeY / 2), r.Z + (r.SizeZ / 2));
                    //_manipulator.Pivot = new Point3D(0, 0, 0);
                    _manipulator.Diameter = Math.Max(r.SizeX, Math.Max(r.SizeY, r.SizeZ)) ;
                    _manipulator.Length = _manipulator.Diameter * 0.75;
                    _manipulator.Bind(this);
                }

                this.parent.Children.Add(_manipulator);
                showManipulator = false;
            }
            else
            {
                this.parent.Children.Remove(_manipulator);
                _manipulator = null;
                showManipulator = true;
            }
        }
        internal void grabUpperPlane_x()
        {
            Model3DGroup _model = (Model3DGroup)this.Content;
            MeshGeometry3D _modelMesh = null;
            foreach (GeometryModel3D gm in _model.Children)
            {
                _modelMesh = (MeshGeometry3D)gm.Geometry;
            }

            Model3DGroup _planeModel = (Model3DGroup)_plane.Content;
            MeshGeometry3D _planeMesh = null; 
            foreach (GeometryModel3D gm in _planeModel.Children)
            {
                _planeMesh = (MeshGeometry3D)gm.Geometry;
            }
            Int32Collection indices = _modelMesh.TriangleIndices;
            SmileObjReader obj = new SmileObjReader();

            //TODO: cut the upper mesh
            for (int i = 0; i < indices.Count; i = i + 3)
            {
                int index1 = indices[i];
                int index2 = indices[i + 1];
                int index3 = indices[i + 2];

            }
        }

        internal GeometryModel3D cutByPlane()
        {
            var p = new Point3D(0, 0, 0);
            
            //MeshGeometry3D worldMesh = GetMesh();//ToWorldMesh();
            MeshGeometry3D worldMesh = ToWorldMesh();
            var n = _plane.Normal;
            var nn = _plane.Normal;
            nn.Negate();
            nn.Normalize();
            var geo = MeshGeometryHelper.Cut(worldMesh, p, nn);
            //var geo1 = ToLocalMesh(geo);
            var model = new GeometryModel3D(geo, MaterialHelper.CreateMaterial(Colors.Tomato));
            
            
            this.Cut(p, n);
            showManipulator = false;
            showHideManipulator();

            return model;
        }

        internal void grabUpperPlane_old()
        {
            Model3DGroup model = (Model3DGroup)this.Content;
            SmileObjReader obj = new SmileObjReader();
            Model3DCollection childs = model.Children;
            int c = 0;
            int i = 1;
            foreach (GeometryModel3D gm in childs)
            {
                MeshGeometry3D mesh = (MeshGeometry3D)gm.Geometry;
                obj.AddGroup("mesh" + c);
                Console.WriteLine("Positions:" + mesh.Positions.Count);
                Console.WriteLine("Indices:" + mesh.TriangleIndices.Count);
                Point3DCollection positions = mesh.Positions;
                foreach (var pp in positions)
                {
                    //Point3D pp = ToLocal(p);
                    if (pp.Z >= 0)
                    {
                        obj.AddVertex(pp.X + " " + pp.Y + " " + pp.Z);

                        if (i % 4 == 0)
                        {
                            obj.AddFace((i - 3) + " " + (i - 2) + " " + (i - 1));
                            obj.AddFace((i - 2) + " " + (i - 1) + " " + (i));
                        }

                        i++;
                    }
                    else
                    {
                        //obj.AddVertex(p.X + " " + p.Y + " " + 0);
                    }
                }

                /*
                 for (int i = 0; i < mesh.TriangleIndices.Count; i = i + 3)
                {
                    int index1 = mesh.TriangleIndices[i];
                    int index2 = mesh.TriangleIndices[i + 1];
                    int index3 = mesh.TriangleIndices[i + 2];

                    Point3D point1 = mesh.Positions[index1];
                    Point3D point2 = mesh.Positions[index2];
                    Point3D point3 = mesh.Positions[index3];

                    if (point1.Z >= 0)
                    {
                        obj.AddVertex(point1.X + " " + point1.Y + " " + point1.Z);
                    }
                    else
                    {
                        obj.AddVertex(point1.X + " " + point1.Y + " " + 0);
                    }

                    if (point2.Z >= 0)
                    {
                        obj.AddVertex(point2.X + " " + point2.Y + " " + point2.Z);
                    }
                    else
                    {
                        obj.AddVertex(point2.X + " " + point2.Y + " " + 0);
                    }

                    if (point3.Z >= 0)
                    {
                        obj.AddVertex(point3.X + " " + point3.Y + " " + point3.Z);
                    }
                    else
                    {
                        obj.AddVertex(point3.X + " " + point3.Y + " " + 0);
                    }


                    obj.AddFace(i + " " + (i + 1) + " " + (i + 2));
                    //obj.AddFace(index1 + " " + index2 + " " + index3);
                
                }
                 */
                c++;
            }
            ModelVisual3D v = new ModelVisual3D();
            v.Content = obj.BuildModel();

            parent.Children.Add(v);
        }

        internal void showHidePlane()
        {
            if (this.Content != null && showPlane)
            {
                Rect3D bounds = this.Content.Bounds;
                //var length = bound.SizeX > bound.SizeY ? bound.SizeX : bound.SizeY > bound.SizeZ ? bound.SizeY : bound.SizeZ > bound.SizeX ? bound.SizeZ : bound.SizeX;
                var length = Math.Max(bounds.Size.X, Math.Max(bounds.Size.Y, bounds.Size.Z));
                length += 10;

                if(_plane == null) _plane = new RectangleVisual3D{Normal = new Vector3D(0, 1, 0), Fill = new SolidColorBrush(Color.FromArgb(80, 255, 0, 0))};
                //_plane.LengthDirection = new Vector3D(0, 0.2, 1);
                _plane.Width = length;
                _plane.Length = length;
                parent.Children.Add(_plane);
                showPlane = false;
                //PlaneVisual3D p = new PlaneVisual3D(this);
                //p.createPlane();
                //parent.Children.Add(p);

            }else if (!showPlane)
            {
                parent.Children.Remove(_plane);
                _plane = null;
                showPlane = true;
            }
        }


        internal List<GeometryModel3D> manualSegment(Point3DCollection points, Vector3DCollection vectors)
        {
            List<GeometryModel3D> models = new List<GeometryModel3D>();
            //MeshGeometry3D worldMesh = GetMesh();//ToWorldMesh();
            MeshGeometry3D worldMesh = ToWorldMesh();
            for (var i = 1; i < points.Count; i++)
            {
                Point3D p0 = points[i-1];
                Vector3D n0 = vectors[i-1];
                Point3D pi = points[i];
                Vector3D ni = vectors[i];

                Point3D pp = new Point3D(0, 0, 0);
                Vector3D nn = Point3D.Subtract(p0, pp);
                if (i == 1)
                {
                    nn = Point3D.Subtract(pi, pp);
                }
                var g1 = MeshGeometryHelper.Cut(worldMesh, pp, nn);
                this.Children.Add(new RectangleVisual3D { Origin = ToLocal(pp), Normal = nn, Fill = new SolidColorBrush(Colors.LightGoldenrodYellow), BackMaterial = MaterialHelper.CreateMaterial(new SolidColorBrush(Colors.Blue)) });

                if (i > 1) n0.Negate();
                //var geo = MeshGeometryHelper.Cut(worldMesh, p0, n0);
                var geo = MeshGeometryHelper.Cut(g1, p0, n0);
                this.Children.Add(new RectangleVisual3D { Origin = ToLocal(p0), Normal = n0, Fill = new SolidColorBrush(Colors.LightGoldenrodYellow), BackMaterial = MaterialHelper.CreateMaterial(new SolidColorBrush(Colors.Blue)) });
                //Console.WriteLine(i+" = "+p0.ToString());
                var geo1 = MeshGeometryHelper.Cut(geo, pi, ni);
                this.Children.Add(new RectangleVisual3D { Origin = ToLocal(pi), Normal = ni, Fill = new SolidColorBrush(Color.FromArgb(80, 255, 0, 0)), BackMaterial = MaterialHelper.CreateMaterial(new SolidColorBrush(Colors.Green)) });

                //var geo2 = ToLocalMesh(geo1);
                
                var model = new GeometryModel3D(geo1, MaterialHelper.CreateMaterial(TeethVisual3D.getTeethColor(i)));
                models.Add(model);
            }
            return models;
        }

        internal void AddBox(Point3D? pt)
        {
            BoxVisual3D b = new BoxVisual3D();
            b.Width = 1;
            b.Height = 1;
            b.Length = 1;
            b.Center = ToLocal((Point3D)pt);
            this.Children.Add(b);
        }
    }
}
