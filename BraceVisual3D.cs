using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Controls;
using HelixToolkit.Wpf;


namespace smileUp
{
    public class BraceVisual3D : SmileVisual3D
    {
        public GumContainer gc;
        public ToothContainer tc;
        public BraceContainer bc;
        public WireContainer wc;


        public BraceVisual3D(TeethVisual3D parent, bool generatedSample)
            : this(Colors.Pink, parent, generatedSample)
        {
        }
        public BraceVisual3D(Color color, TeethVisual3D p, bool generatedSample)
        {
            if (p != null)
            {
                this.parent = p;
                gc = this.parent.gc;
                tc = this.parent.tc;
                bc = this.parent.bc;
                wc = this.parent.wc;

                Id = p.Id + "_brace" + bc.Children.Count.ToString("00") + "." + p.Parent.Parent.patient.Name; ;
                
                if (model == null) model = new Brace();
                model.Id = Id;
                
                if(generatedSample) sample(color);

                //BindingOperations.SetBinding(this, TransformProperty, new Binding("TargetTransform") { Source = this });

                //BindingOperations.SetBinding(this.Manipulator,CombinedManipulator.TargetTransformProperty,new Binding("TargetTransform") { Source = this });

            }
        }
        public BraceVisual3D(Material material, TeethVisual3D p)
        {
            if (p != null)
            {
                this.parent = p;
                Id = p.Id + "_brace" + p.Children.Count.ToString("00") + "." + p.Parent.Parent.patient.Name; ;

                if (model == null) model = new Brace();
                model.Id = Id;

                //sample(material);

                //BindingOperations.SetBinding(this, TransformProperty, new Binding("TargetTransform") { Source = this });

                //BindingOperations.SetBinding(this.Manipulator,CombinedManipulator.TargetTransformProperty,new Binding("TargetTransform") { Source = this });

                gc = this.parent.gc;
                tc = this.parent.tc;
                bc = this.parent.bc;
                wc = this.parent.wc;
            }
        }

        internal void sample(Material material)
        {
            ///*
            GeometryModel3D bigCubeModel = GeometryGenerator.CreateCubeModel();
            bigCubeModel.Material = material;
            
            this.Content = bigCubeModel;

            Rect3D r = this.parent.Content.Bounds;

            Transform3DGroup transformGroup = new Transform3DGroup();
            transformGroup.Children.Add(new ScaleTransform3D(2, 2, 2));
            Point3D p0 = new Point3D(0, 0, 0);
            Point3D p1 = this.parent.centroid();
            
            //TODO: locate brace in or out the teeth

            //Console.WriteLine(Vector3D.DotProduct(p0.ToVector3D(), p1.ToVector3D()));
            transformGroup.Children.Add(new TranslateTransform3D(p1.X + (r.SizeX / 2), p1.Y + (r.SizeY / 2), p1.Z + (r.SizeZ / 2)));
            this.Transform = transformGroup;
            //*/
        }

        internal void sample(Color color)
        {
            sample(MaterialHelper.CreateMaterial(new SolidColorBrush(Colors.Blue)));
        }


        private TeethVisual3D parent;
        public TeethVisual3D Parent
        {
            set { parent = value; }
            get { return parent; }        
        }

        private CombinedManipulator _manipulator;
        public CombinedManipulator Manipulator { get; set; }
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

        /// <summary>
        /// @Deprecated
        /// Will be removed soon.
        /// <see cref="displayManipulator"/>
        /// </summary>
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

        }

        /// <summary>
        /// @Deprecated
        /// Will be removed soon.
        /// <see cref="cleanManipulator"/>
        /// </summary>
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

        //==============using Container ==========

        public void cleanManipulator()
        {
            List<Visual3D> childs = bc.Children.ToList();
            foreach (var m in childs)
            {
                if (m is CombinedManipulator)
                {
                    bc.Children.Remove(m);
                }
            }
            _manipulator = null;
        }

        public void displayManipulator()
        {
            this.parent.cleanManipulator();
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
            bc.Children.Add(_manipulator);

        }


    }
}
