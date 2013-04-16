using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using HelixToolkit.Wpf;
using System.Text.RegularExpressions;

namespace smileUp
{
    public class JawVisual3D : SmileVisual3D
    {
        public Dictionary<String, GumVisual3D> gums = new Dictionary<String, GumVisual3D>();
        public GumVisual3D selectedGum;
        public Patient patient;

        public JawVisual3D(Patient p)
        {
            //sample();
            this.patient = p;
        }

        void sample()
        {
            /*

            TeethVisual3D t = new TeethVisual3D(Colors.Green, this);
            this.Children.Add(t);
            TeethVisual3D t2 = new TeethVisual3D(Colors.Red, this);
            this.Children.Add(t2);
            TeethVisual3D t3 = new TeethVisual3D(Colors.Blue, this);
            this.Children.Add(t3);
            TeethVisual3D t4 = new TeethVisual3D(Colors.Yellow, this);
            this.Children.Add(t4);
            TeethVisual3D t5 = new TeethVisual3D(Colors.SkyBlue, this);
            this.Children.Add(t5);
            TeethVisual3D t6 = new TeethVisual3D(Colors.White, this);
            this.Children.Add(t6);
            TeethVisual3D t7 = new TeethVisual3D(Colors.Azure, this);
            this.Children.Add(t7);
            TeethVisual3D t8 = new TeethVisual3D(Colors.Pink, this);
            this.Children.Add(t8);
            TeethVisual3D t9 = new TeethVisual3D(Colors.Blue, this);
            this.Children.Add(t9);
            
            */

            //GumVisual3D gum1 = new GumVisual3D();
            //this.Children.Add(gum1);
            //gums.Add("gumDefault", gum1);
        }

        public TeethVisual3D addTeeth(Point3D center)
        {
            if (selectedGum != null)
            {
                selectedGum.selectedPoint = center;
                selectedGum.addTeeth();
                return selectedGum.selectedTeeth;
            }
            return null;
        }

        public void removeTeeth()
        {
            if (selectedGum != null)
            {
                selectedGum.removeTeeth();
            }
        }

        internal TeethVisual3D selectTeeth(int p)
        {
            TeethVisual3D teeth = null;
            String pStr = p.ToString("00");
            foreach (var g in gums)
            {
                GumVisual3D gum = g.Value;
                if (gum != null)
                {
                    foreach (var t in gum.Children)
                    {
                        if (t is TeethVisual3D)
                        {
                            teeth = (TeethVisual3D)t;
                            //Console.WriteLine(teeth.Id);
                            if(Regex.IsMatch(teeth.Id, @"teeth" + pStr))
                            //if (teeth.Id.StartsWith("teeth" + pStr))
                            {
                                //teeth.showHideManipulator();
                                selectedGum = gum;
                                gum.selectedTeeth = teeth;
                                break;
                            }
                        }
                    }
                }
            }
            return teeth;
        }

        internal BraceVisual3D addBrace(Point3D center)
        {
            if (selectedGum != null)
            {
                selectedGum.selectedPoint = center;
                return selectedGum.addBrace();
            }
            return null;
        }

        internal void removeBrace()
        {
            if (selectedGum != null)
            {
                selectedGum.removeBrace();
            }
        }

        internal void addTeeth(List<GeometryModel3D> models)
        {
            selectOrAddGum();
            foreach (var model in models)
            {
                selectedGum.addTeeth(model);
            }
        }

        private void selectOrAddGum()
        {
            if (selectedGum == null)
            {
                if (gums.Count == 0)
                {
                    GumVisual3D gum = new GumVisual3D(this);
                    gums.Add(gum.Id, gum);
                    selectedGum = gum;
                    this.Children.Add(gum);
                }
            }
        }

        internal void replaceGum(GeometryModel3D gumModel)
        {
            selectOrAddGum();
            selectedGum.Content = gumModel;
        }

        internal void drawWire(List<BraceVisual3D> bracesModel)
        {
            for (var i = 1; i < bracesModel.Count; i++)
            {
                BraceVisual3D brace1 = bracesModel[i - 1];
                BraceVisual3D brace2 = bracesModel[i];

                //brace1.SetMesh(brace1.ToWorldMesh());
                //brace2.SetMesh(brace2.ToWorldMesh());

                Wire wire = new Wire();
                wire.Brace1 = brace1.Model;
                wire.Brace2 = brace2.Model;

                Point3DCollection contours = new Point3DCollection();
                contours.Add(brace1.ToWorld(brace1.centroid()));
                contours.Add(brace2.ToWorld(brace2.centroid()));

                //brace1.SetMesh(brace1.ToLocalMesh(brace1.GetMesh()));
                //brace2.SetMesh(brace1.ToLocalMesh(brace2.GetMesh()));

                WireVisual3D wv = new WireVisual3D(brace1);

                TubeVisual3D tube = new TubeVisual3D { Diameter = 1.02, Path = contours, Fill = (i % 2 == 0 ? Brushes.Green : Brushes.Blue) };
                this.Children.Add(tube);
            }
        }

        internal void updateTeethMap(string oldid, string newid)
        {
            //find the existing new id
            foreach (var g in gums)
            {
                GumVisual3D gum = g.Value;
                if (gum != null)
                {
                    foreach (var t in gum.Children)
                    {
                        if (t is TeethVisual3D)
                        {
                            TeethVisual3D teeth = (TeethVisual3D)t;
                            if (teeth.Id.Equals(newid))
                            {
                                teeth.Id = oldid;
                                selectedGum.selectedTeeth.Id = newid;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
