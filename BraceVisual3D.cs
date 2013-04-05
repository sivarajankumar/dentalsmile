using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Controls;
using HelixToolkit.Wpf;
using System.Windows.Data;

namespace smileUp
{
    public class BraceVisual3D : SmileVisual3D
    {
        
        public BraceVisual3D(TeethVisual3D parent)
            : this(Colors.Pink, parent)
        {
        }
        public BraceVisual3D(Color color, TeethVisual3D p)
        {
            if (p != null)
            {
                this.parent = p;
                Id = p.Id + "_brace" + p.Children.Count.ToString("00") + "." + p.Parent.Parent.patient.name; ;
                
                if (model == null) model = new Brace();
                model.Id = Id;

                sample(color);

                //BindingOperations.SetBinding(this, TransformProperty, new Binding("TargetTransform") { Source = this });

                //BindingOperations.SetBinding(this.Manipulator,CombinedManipulator.TargetTransformProperty,new Binding("TargetTransform") { Source = this });

            }
        }

        public virtual void Bind(WireVisual3D source)
        {
            BindingOperations.SetBinding(this, TransformProperty, new Binding("Transform") { Source = source });
        }

        internal void sample(Color color)
        {
            ///*
            GeometryModel3D bigCubeModel = GeometryGenerator.CreateCubeModel();
            bigCubeModel.Material = MaterialHelper.CreateMaterial(new LinearGradientBrush(Colors.Blue, Colors.Red, 45));

            this.Content = bigCubeModel;

            Rect3D r = this.parent.Content.Bounds;

            Transform3DGroup transformGroup = new Transform3DGroup();
            transformGroup.Children.Add(new ScaleTransform3D(2, 2, 2));
            Point3D p0 = new Point3D(0, 0, 0);
            Point3D p1 = this.parent.centroid();
            Console.WriteLine(Vector3D.DotProduct(p0.ToVector3D(),p1.ToVector3D()));
            transformGroup.Children.Add(new TranslateTransform3D(p1.X + (r.SizeX/2), p1.Y + (r.SizeY / 2), p1.Z + (r.SizeZ/2)));
            this.Transform = transformGroup;
            //*/
        }


        private TeethVisual3D parent;
        public TeethVisual3D Parent
        {
            set { parent = value; }
            get { return parent; }        
        }

        private CombinedManipulator _manipulator;
        public CombinedManipulator Manipulator { get; set; }
        Boolean showManipulator = false;
        String id;
        public String Id
        {
            set { id = value; }
            get { return id; }
        }

        Brace model;
        public Brace Model
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
                }
                this.parent.Children.Add(_manipulator);

                //showManipulator = false;
            //}
            //else
            //{
                //this.parent.Children.Remove(_manipulator);
                //showManipulator = true;
            //}

            //Console.WriteLine("ID: "+id);
        }

        public void clearManipulator()
        {
            List<Visual3D> childs = this.parent.Children.ToList();
            foreach (var m in childs)
            {
                if (m is CombinedManipulator)
                {
                    this.parent.Children.Remove(m);
                }
            }
            
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
