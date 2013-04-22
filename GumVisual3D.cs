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
  

        public GumVisual3D(JawVisual3D parent)
            : this(parent, Colors.BurlyWood)
        {
           
        }

        public GumVisual3D(JawVisual3D p, Color color)
        {
            this.parent = p;
            Id = "gum" +   p.gums.Count.ToString("00") +"."+p.patient.name;

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

        public TeethVisual3D addTeeth()
        {
            Material material = MaterialHelper.CreateMaterial(Colors.PowderBlue);
            return addTeeth(material);        
        }

        private TeethVisual3D addTeeth(Material material)
        {
            TeethVisual3D t = new TeethVisual3D(this);
            ((GeometryModel3D)t.Content).Material = material;
            this.Children.Add(t);

            if (selectedPoint != null) {
                Transform3DGroup transformGroup = new Transform3DGroup();
                transformGroup.Children.Add(new TranslateTransform3D(selectedPoint.ToVector3D()));
                t.Transform = transformGroup;
            }

            int intID = getTeethIDint(t.Id);
            teethDictionaries.Add(intID, t.Id);
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

        internal BraceVisual3D addBrace()
        {
            if (selectedTeeth != null)
            {
                return selectedTeeth.addBrace();
            }
            return null;
        }

        internal void removeBrace()
        {
            if (selectedTeeth != null)
            {
                selectedTeeth.removeBrace();
            }
        }

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

        internal void addTeeth(GeometryModel3D model)
        {
            selectedTeeth = addTeeth(model.Material);
            selectedTeeth.Content = model;
        }


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
    }
}
