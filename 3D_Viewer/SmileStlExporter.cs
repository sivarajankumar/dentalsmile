using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smileUp.Stl;
using System.Windows.Media.Media3D;
using smileUp.DataModel;
using System.IO;
using HelixToolkit.Wpf;
using System.Windows.Media;
using System.Globalization;

namespace smileUp
{
    class SmileStlExporter
    {
        private readonly string directory;
        private int vertexIndex = 1;
        private int normalIndex = 1;
        private int groupNo= 0;
        
        private readonly StreamWriter writer;
        //private readonly BinaryWriter binaryWriter;
        public bool ExportNormals { get; set; }
        private bool finished = false;
        public bool Finish { get { return finished; } set { this.finished = value; } }

        private STL stlExporter;
        private Patient patient;

        private List<Facet> faces;

        public SmileStlExporter(string outputFileName) : this(outputFileName, null) {}
        
        public SmileStlExporter(string outputFileName, string comment)
        {
            var fullPath = Path.GetFullPath(outputFileName);
            this.directory = Path.GetDirectoryName(fullPath);
            this.writer = new StreamWriter(outputFileName);
            //this.binaryWriter = new BinaryWriter(writer);

            this.stlExporter = new STL();
            faces = new List<Facet>();
        }
        public void Export(Visual3D visual, Patient p)
        {
            this.patient = p;
            Traverse<GeometryModel3D>(visual, this.ExportModel);
            finished = true;
        }
        public void WriteExport()
        {
            stlExporter.Facets = faces;
            stlExporter.Write(this.writer);
        }
        public void Close()
        {
            this.writer.Close();
        }
        protected void ExportModel(GeometryModel3D model, Transform3D transform)
        {
            //this.writer.WriteLine(string.Format("o object{0}", this.objectNo++));
            //this.writer.WriteLine(string.Format("g group{0}", this.groupNo++));


            var mesh = model.Geometry as MeshGeometry3D;
            this.ExportMesh(mesh, Transform3DHelper.CombineTransform(transform, model.Transform));
        }

        public void ExportMesh(MeshGeometry3D m, Transform3D t)
        {
            if (m == null)
            {
                throw new ArgumentNullException("m");
            }

            if (t == null)
            {
                throw new ArgumentNullException("t");
            }

            // mapping from local indices (0-based) to the obj file indices (1-based)
            var vertexIndexMap = new Dictionary<int, int>();
            var normalIndexMap = new Dictionary<int, int>();

/*
            int index = 0;

            if (m.Positions != null)
            {
                foreach (var v in m.Positions)
                {
                    vertexIndexMap.Add(index++, this.vertexIndex++);
                    var p = t.Transform(v);
                    this.writer.WriteLine(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "v {0} {1} {2}", p.X, this.SwitchYZ ? p.Z : p.Y, this.SwitchYZ ? -p.Y : p.Z));
                }

                this.writer.WriteLine(string.Format("# {0} vertices", index));
            }
*/
/*
            if (m.Normals != null && ExportNormals)
            {
                index = 0;
                foreach (var vn in m.Normals)
                {
                    normalIndexMap.Add(index++, this.normalIndex++);
                    this.writer.WriteLine(
                        string.Format(CultureInfo.InvariantCulture, "vn {0} {1} {2}", vn.X, vn.Y, vn.Z));
                }

                this.writer.WriteLine(string.Format("# {0} normals", index));
            }

            Func<int, string> formatIndices = i0 =>
            {
                bool hasNormalIndex = normalIndexMap.ContainsKey(i0);

                if (hasNormalIndex)
                {
                    return string.Format("{0}//{1}", vertexIndexMap[i0], normalIndexMap[i0]);
                }

                return vertexIndexMap[i0].ToString();
            };
*/
            if (m.TriangleIndices != null)
            {
                //int ni = 0;
                for (int i = 0; i < m.TriangleIndices.Count; i += 3)
                {
                    int i0 = m.TriangleIndices[i];
                    int i1 = m.TriangleIndices[i + 1];
                    int i2 = m.TriangleIndices[i + 2];

                    var p0 = m.Positions[i0];
                    var p1 = m.Positions[i1];
                    var p2 = m.Positions[i2];

//                  this.writer.WriteLine("f {0} {1} {2}", formatIndices(i0), formatIndices(i1), formatIndices(i2));

                    Vector3D n = CalculateNormal(ref p0, ref p1, ref p2);
                    //Vector3D n = m.Normals[ni++];
                    //TODO: Add Facet (normal, vertices)
                    Facet f = new Facet(new Normal((float)n.X, (float)n.Y, (float)n.Z),
                        new List<Vertex>()
                        {
                            new Vertex( (float)p0.X, (float)p0.Y, (float)p0.Z),
                            new Vertex( (float)p1.X, (float)p1.Y, (float)p1.Z),
                            new Vertex( (float)p2.X, (float)p2.Y, (float)p2.Z)
                        }
                        ,0);
                    faces.Add(f);
                }

                //this.writer.WriteLine(string.Format("# {0} faces", m.TriangleIndices.Count / 3));
            }
            //this.writer.WriteLine();
        }

        private Vector3D CalculateNormal(ref Point3D p0, ref Point3D p1, ref Point3D p2)
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

        public void Traverse<T>(Visual3D visual, Action<T, Transform3D> action) where T : Model3D
        {
            Traverse(visual, Transform3D.Identity, action);
        }

        private void Traverse<T>(Visual3D visual, Transform3D transform, Action<T, Transform3D> action)
            where T : Model3D
        {
            var childTransform = Transform3DHelper.CombineTransform(visual.Transform, transform);
            var model = GetModel(visual);
            if (model != null)
            {
                if (visual is TeethVisual3D)
                {
                    TeethVisual3D t = (TeethVisual3D)visual;
                    //stlExporter.Name = string.Format("{0}", t.Id);
//                    this.writer.WriteLine(string.Format("g jaw_{0}", t.Id));
                }
                else if (visual is GumVisual3D)
                {
                    GumVisual3D t = (GumVisual3D)visual;
                    //stlExporter.Name = string.Format("{0}", t.Id);
//                    this.writer.WriteLine(string.Format("g jaw_{0}", t.Id));
                }
                else if (visual is BraceVisual3D)
                {
                    BraceVisual3D t = (BraceVisual3D)visual;
                    //stlExporter.Name = string.Format("{0}", t.Id);
//                    this.writer.WriteLine(string.Format("g jaw_{0}", t.Id));
                }
                else if (visual is WireVisual3D)
                {

                }
                else
                {
//                    this.writer.WriteLine(string.Format("g jaw_group{0}", this.groupNo++));
                }

                if (visual is Manipulator || visual is BoundingBoxWireFrameVisual3D)
                {
                }
                else
                {
                    TraverseModel(model, childTransform, action);
                }
            }

            foreach (var child in GetChildren(visual))
            {
                Traverse(child, childTransform, action);
            }
        }

        public static void TraverseModel<T>(Model3D model, Transform3D transform, Action<T, Transform3D> action)
            where T : Model3D
        {
            var mg = model as Model3DGroup;
            if (mg != null)
            {
                var childTransform = Transform3DHelper.CombineTransform(model.Transform, transform);
                foreach (var m in mg.Children)
                {
                    TraverseModel(m, childTransform, action);
                }
            }

            var gm = model as T;
            if (gm != null)
            {
                var childTransform = Transform3DHelper.CombineTransform(model.Transform, transform);
                action(gm, childTransform);
            }
        }

        private static Model3D GetModel(Visual3D visual)
        {
            Model3D model = null;
            var mv = visual as ModelVisual3D;
            if (mv != null)
            {
                model = mv.Content;
            }
            else
            {
                //model = Visual3DModelPropertyInfo.GetValue(visual, null) as Model3D;
            }

            return model;
        }
        private static IEnumerable<Visual3D> GetChildren(Visual3D visual)
        {
            int n = VisualTreeHelper.GetChildrenCount(visual);
            for (int i = 0; i < n; i++)
            {
                var child = VisualTreeHelper.GetChild(visual, i) as Visual3D;
                if (child == null)
                {
                    continue;
                }

                yield return child;
            }
        }

        public bool SwitchYZ { get; set; }

    }
}
