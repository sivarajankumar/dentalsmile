using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using HelixToolkit.Wpf;

namespace smileUp
{
    public class GumVisual3D : SmileVisual3D
    {
        public TeethVisual3D selectedTeeth;
        public Dictionary<int, String> teethDictionaries;
        public Dictionary<String, BraceVisual3D> braceDictionaries;
        public List<BraceVisual3D> braces;

        public GumContainer gc;
        public ToothContainer tc;
        public BraceContainer bc;
        public WireContainer wc;


        String id;
        public String Id
        {
            set { id = value; }
            get { return id; }
        }

        private JawVisual3D parent;
        public Point3D selectedPoint;
        public JawVisual3D Parent
        {
            set { parent = value; }
            get { return parent; }
        }
  

        public GumVisual3D(JawVisual3D parent, string id)
            : this(parent, Colors.BurlyWood, id)
        {
           
        }

        public GumVisual3D(JawVisual3D p, Color color, string id)
        {
            this.parent = p;
            gc = this.parent.gc;
            tc = this.parent.tc;
            bc = this.parent.bc;
            wc = this.parent.wc;
            if (id == null)
                Id = "gum" + p.gums.Count.ToString("00") + "." + p.patient.Id;
            else
                Id = id;

            sample(color);

            teethDictionaries = new Dictionary<int, string>();
            braceDictionaries = new Dictionary<string, BraceVisual3D>();
            braces = new List<BraceVisual3D>();

        }

        void sample(Color color)
        {
            /*
               GeometryModel3D smallCubeModel = GeometryGenerator.CreateCubeModel();
               smallCubeModel.Material = new DiffuseMaterial(new SolidColorBrush(color));

               Transform3DGroup transformGroup = new Transform3DGroup();
               transformGroup.Children.Add(new ScaleTransform3D(5, .3, 5));
               transformGroup.Children.Add(new TranslateTransform3D(0, -2.5, 0));
               smallCubeModel.Transform = transformGroup;

               this.Content = smallCubeModel;
            
               //TeethVisual3D t8 = new TeethVisual3D(Colors.Pink, this);
               //this.Children.Add(t8);
            
               //*/

            //TeethVisual3D t9 = new TeethVisual3D(Colors.Blue, this);
            //this.Children.Add(t9);
        }

        /// <summary>
        /// @Deprecated
        /// Not used anymore since changes in CONTAINER design.
        /// <see cref="addNewTeeth"/>
        /// </summary>
        public TeethVisual3D addTeeth()
        {
            Material material = MaterialHelper.CreateMaterial(Colors.PowderBlue);
            return addTeeth(material);        
        }

        /// <summary>
        /// @Deprecated
        /// Not used anymore since changes in CONTAINER design.
        /// <see cref="addNewTeeth"/>
        /// </summary>
        private TeethVisual3D addTeeth(Material material)
        {
            TeethVisual3D t = new TeethVisual3D(this);
            this.Children.Add(t);

            if (selectedPoint != null) {
                Transform3DGroup transformGroup = new Transform3DGroup();
                transformGroup.Children.Add(new TranslateTransform3D(selectedPoint.ToVector3D()));
                t.Transform = transformGroup;
            }

            int intID = getTeethIDint(t.Id);
            teethDictionaries.Add(intID, t.Id);
            
            if (material == null)
            {
                material = MaterialHelper.CreateMaterial(TeethVisual3D.getTeethColor(intID));
            }            
            ((GeometryModel3D)t.Content).Material = material;
            
            selectedTeeth = t;
            return t;
        }

        internal int getTeethIDint(String id){
                    int intID = 0;
            var gs = id.Split('_');
            if (gs.Length > 1)
            {
                if (gs[0].StartsWith("teeth"))
                {
                    int.TryParse(gs[0].Substring("teeth".Length, 2), out intID);
                }
                else if (gs[1].StartsWith("teeth"))
                {
                    int.TryParse(gs[1].Substring("teeth".Length, 2), out intID);
                }
            }
            return intID;
        }

        /// <summary>
        /// @Deprecated
        /// Not used anymore since changes in CONTAINER design.
        /// <see cref="deleteTeeth"/>
        /// </summary>
        public void removeTeeth()
        {
            if (selectedTeeth != null)
            {
                selectedTeeth.clearManipulator();
                this.Children.Remove(selectedTeeth);
                
                int intID = getTeethIDint(selectedTeeth.Id);
                teethDictionaries.Remove(intID);

                selectedTeeth = null;
            }
        }

        /// <summary>
        /// @Deprecated
        /// Not used anymore since changes in CONTAINER design.
        /// <see cref="deleteBrace"/>
        /// </summary>
        internal BraceVisual3D addBrace()
        {
            if (selectedTeeth != null)
            {
                return selectedTeeth.addBrace();
            }
            return null;
        }

        /// <summary>
        /// @Deprecated
        /// Not used anymore since changes in CONTAINER design.
        /// <see cref="deleteBrace"/>
        /// </summary>
        internal void removeBrace()
        {
            if (selectedTeeth != null)
            {
                selectedTeeth.removeBrace();
            }
        }

        /// <summary>
        /// @Deprecated
        /// Not used anymore since changes in CONTAINER design.
        /// <see cref="cleanManipulator"/>
        /// </summary>
        internal void clearManipulator()
        {
            List<Visual3D> childs = this.Children.ToList();
            foreach (var m in childs)
            {
                if (m is TeethVisual3D)
                {
                    ((TeethVisual3D)m).clearManipulator();
                }
            }
        }

        /// <summary>
        /// @Deprecated
        /// Not used anymore since changes in CONTAINER design.
        /// <see cref="addNewTeeth"/>
        /// </summary>
        internal void addTeeth(GeometryModel3D model)
        {
            selectedTeeth = addTeeth(model.Material);
            selectedTeeth.Content = model;
        }

        /// <summary>
        /// @Deprecated
        /// Not used anymore since changes in CONTAINER design.
        /// <see cref="addBraceToAllTooth"/>
        /// </summary>
        internal void addAllBrace()
        {
            List<Visual3D> childs = this.Children.ToList();
            foreach (var m in childs)
            {
                if (m is TeethVisual3D)
                {
                    ((TeethVisual3D)m).addBrace();
                }
            }
        }

        //=============== using new Architecture (Container) ===========
        internal TeethVisual3D addNewTeeth()
        {
            TeethVisual3D t = new TeethVisual3D(this);
            tc.Children.Add(t);

            if (selectedPoint != null)
            {
                Transform3DGroup transformGroup = new Transform3DGroup();
                transformGroup.Children.Add(new TranslateTransform3D(selectedPoint.ToVector3D()));
                t.Transform = transformGroup;
            }

            int intID = getTeethIDint(t.Id);
            if (!teethDictionaries.ContainsKey(intID))
                teethDictionaries.Add(intID, t.Id);

            Material material = MaterialHelper.CreateMaterial(TeethVisual3D.getTeethColor(intID));
            ((GeometryModel3D)t.Content).Material = material;

            selectedTeeth = t;
            return t;
        }

        internal TeethVisual3D addNewTeeth(Material material)
        {
            TeethVisual3D t = new TeethVisual3D(this);
            tc.Children.Add(t);

            if (selectedPoint != null)
            {
                Transform3DGroup transformGroup = new Transform3DGroup();
                transformGroup.Children.Add(new TranslateTransform3D(selectedPoint.ToVector3D()));
                t.Transform = transformGroup;
            }

            int intID = getTeethIDint(t.Id);
            teethDictionaries.Add(intID, t.Id);

            if (material == null)
            {
                material = MaterialHelper.CreateMaterial(TeethVisual3D.getTeethColor(intID));
            }
            ((GeometryModel3D)t.Content).Material = material;
            ((GeometryModel3D)t.Content).BackMaterial = material;

            selectedTeeth = t;
            return t;
        }


        internal void addNewTeeth(GeometryModel3D model)
        {
            selectedTeeth = addNewTeeth(model.Material);
            selectedTeeth.Content = model;
        }

        public void deleteTeeth()
        {
            if (selectedTeeth != null)
            {
                selectedTeeth.clearManipulator();
                tc.Children.Remove(selectedTeeth);

                int intID = getTeethIDint(selectedTeeth.Id);
                teethDictionaries.Remove(intID);

                selectedTeeth = null;
            }
        }

        internal BraceVisual3D addNewBrace()
        {
            if (selectedTeeth != null)
            {
                return selectedTeeth.addNewBrace();
            }
            return null;
        }

        internal void deleteBrace()
        {
            if (selectedTeeth != null)
            {
                selectedTeeth.deleteBrace();
            }
        }

        internal void addBraceToAllTooth()
        {
            List<Visual3D> childs = tc.Children.ToList();
            foreach (var m in childs)
            {
                if (m is TeethVisual3D)
                {
                    ((TeethVisual3D)m).addNewBrace();
                }
            }
        }

        internal void cleanManipulator()
        {
            List<Visual3D> childs = tc.Children.ToList();
            foreach (var m in childs)
            {
                if (m is TeethVisual3D)
                {
                    ((TeethVisual3D)m).cleanManipulator();
                }
            }
        }

        public Point3DCollection Archs { get; set; }
        private ModelVisual3D arch3D;

        public void displayArchs(bool display){
            if (display)
            {
                if (Archs != null)
                {
                    arch3D = new TubeVisual3D { Diameter = .08, Path = Archs, Fill = Brushes.Pink };
                    this.Children.Add(arch3D);
                }
            }
            else
            {
                if(this.Children.Contains(arch3D))
                {
                    this.Children.Remove(arch3D);
                    arch3D = null;
                }
            }
        }
    }
}
