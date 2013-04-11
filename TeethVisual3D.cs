using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Controls;
using HelixToolkit.Wpf;
using System.Windows.Input;

namespace smileUp
{
    public class TeethVisual3D : SmileVisual3D
    {
        
        public TeethVisual3D(GumVisual3D parent)
            : this(Colors.Pink, parent)
        {
        }
        public TeethVisual3D(Color color, GumVisual3D p)
        {
            if (p != null)
            {
                this.parent = p;
                Id = p.Id + "_teeth" + p.Children.Count.ToString("00") + "." + p.Parent.patient.name;
                
                if (model == null) model = new Teeth();
                model.Id = Id;

                sample(color);

                
            }
        }
        public static Color getTeethColor(int num) {
            if (num % 5 == 0) return Colors.Orange;
            if (num % 4 == 0) return Colors.GreenYellow;
            if (num % 3 == 0) return Colors.Red;
            if (num % 2 == 0) return Colors.Blue;
            return Colors.Gold;
        }
        internal void sample(Color color)
        {
            ///*
            GeometryModel3D bigCubeModel = GeometryGenerator.CreateCubeModel();
            bigCubeModel.Material = new DiffuseMaterial(new SolidColorBrush(color));

            this.Content = bigCubeModel;

            Transform3DGroup transformGroup = new Transform3DGroup();
            transformGroup.Children.Add(new TranslateTransform3D(0.5 * parent.Children.Count, 0, 0));
            this.Transform = transformGroup;
            //*/
        }


        private GumVisual3D parent;
        public GumVisual3D Parent
        {
            set { parent = value; }
            get { return parent; }        
        }

        private CombinedManipulator _manipulator;
        public CombinedManipulator Manipulator { get; set; }
        Boolean showManipulator = false;
        String id;

        public static readonly DependencyProperty IdProperty
           = DependencyProperty.Register("Id", typeof(String), typeof(TeethVisual3D),
                                         new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsArrange));

        public String Id
        {
            set { 
                id = value;
                SetValue(IdProperty, value);
                if(Model != null) Model.Id = value;
            }
            get {
                id = (String) GetValue(IdProperty);
                return id; 
            }
        }

        Teeth model;
        public Teeth Model
        {
            set { model = value; }
            get { return model; }
        }

        public void showHideManipulator()
        {
            this.parent.clearManipulator();
            //if (showManipulator)
            //{
                if (_manipulator == null)
                {
                    Rect3D r = this.Content.Bounds;
                    _manipulator = new CombinedManipulator();
                    //_manipulator.Position = new Point3D(r.X + (r.SizeX/2),r.Y + (r.SizeY / 2),r.Z + (r.SizeZ/2));
                    _manipulator.Offset = new Vector3D(r.X + (r.SizeX / 2), r.Y + (r.SizeY / 2), r.Z + (r.SizeZ / 2));
                    _manipulator.Pivot = new Point3D(r.X + (r.SizeX / 2), r.Y + (r.SizeY / 2), r.Z + (r.SizeZ / 2));
                    //_manipulator.Pivot = new Point3D(0, 0, 0);
                    _manipulator.Diameter = Math.Max(r.SizeX, Math.Max(r.SizeY, r.SizeZ)) + 1;
                    _manipulator.Length = _manipulator.Diameter * 0.75;
                    _manipulator.Bind(this);
                    Bind(_manipulator);
                }
                this.parent.Children.Add(_manipulator);

                //showManipulator = false;
            //}
            //else
            //{
            //    this.parent.Children.Remove(_manipulator);
            //    showManipulator = true;
            //}

            //Console.WriteLine("ID: "+id);
        }

        internal void clearManipulator()
        {
            List<Visual3D> childs = this.parent.Children.ToList();
            foreach (var m in childs)
            {
                if (m is CombinedManipulator)
                {
                    this.parent.Children.Remove(m);
                }
            }
            try
            {
                childs = this.Children.ToList();
                foreach (var m in childs)
                {
                    if (m is BraceVisual3D)
                    {
                        ((BraceVisual3D)m).clearManipulator();
                    }
                }

            }
            catch (Exception e) { }
            //this.parent.Children.Remove(_manipulator);
            _manipulator = null;
        }

        public Point3D centroid()
        {
            Rect3D r = this.Content.Bounds;

            Point3D p = new Point3D(r.X + ((r.SizeX / 2)), r.Y + ((r.SizeY / 2)), r.Z + ((r.SizeZ / 2)));
            return p;
        }

    }
}
