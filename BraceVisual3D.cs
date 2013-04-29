using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Controls;
using HelixToolkit.Wpf;
using smileUp.DataModel;


namespace smileUp
{
    public class BraceVisual3D : SmileVisual3D
    {
        public GumContainer gc;
        public ToothContainer tc;
        public BraceContainer bc;
        public WireContainer wc;

        public static readonly DependencyProperty OuterBraceProperty = DependencyProperty.Register("OuterBrace", typeof(bool), typeof(BraceVisual3D), new UIPropertyMetadata(true, OuterBraceChanged));
        
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

                Id = p.Id + "_brace" + bc.Children.Count.ToString("00") + "." + p.Parent.Parent.patient.Id; ;
                
                if (model == null) model = new Brace();
                model.Id = Id;
                model.Location = (IsOuterBrace ? Smile.OUTERBRACE : Smile.INNERBRACE);
                
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
                gc = this.parent.gc;
                tc = this.parent.tc;
                bc = this.parent.bc;
                wc = this.parent.wc;

                Id = p.Id + "_brace" + p.Children.Count.ToString("00") + "." + p.Parent.Parent.patient.Id; ;

                if (model == null) model = new Brace();
                model.Id = Id;
                model.Location = (IsOuterBrace ? Smile.OUTERBRACE : Smile.INNERBRACE);

                //sample(material);
                //BindingOperations.SetBinding(this, TransformProperty, new Binding("TargetTransform") { Source = this });
                //BindingOperations.SetBinding(this.Manipulator,CombinedManipulator.TargetTransformProperty,new Binding("TargetTransform") { Source = this });

            }
        }

        internal void sample(Material material)
        {
            ///*
            GeometryModel3D bigCubeModel = GeometryGenerator.CreateBraceModel(1);
            bigCubeModel.Material = material;
            
            this.Content = bigCubeModel;

            //Transform3DGroup transformGroup = new Transform3DGroup();
            //transformGroup.Children.Add(scaleBrace());
            //transformGroup.Children.Add(locateBrace());            
            //this.Transform = transformGroup;

            scaleBrace();
            locateBrace();
        }

        private void scaleBrace()
        {
            int part = 5;
            Rect3D r = this.parent.Content.Bounds;
            double X = r.SizeX / part;
            double Y = r.SizeY / part;
            double Z = r.SizeZ / part;

            this.Transform = new ScaleTransform3D(2, 2, 2);
        }

        private void locateBrace()
        {
            Rect3D r = this.parent.Content.Bounds;
            Point3D p1 = this.parent.ToWorld(this.parent.centroid());

            //locate brace in or out the teeth
            double Xtrans = p1.X;
            double Ztrans = p1.Z;
            double Ytrans = p1.Y;
            //double Ytrans = p1.Y - (r.SizeY / 2);

            if (IsOuterBrace)
            {
                if (p1.X < 0)
                    Xtrans = p1.X - (r.SizeX / 2);

                if (p1.Z < -10)
                    Ztrans = p1.Z - (r.SizeZ / 2);
                else if (p1.Z > -10 && p1.Z < 5)
                    Ztrans = p1.Z + (r.SizeZ / 4);
                else
                    Ztrans = p1.Z + (r.SizeZ / 2);
            }
            else
            {
                if (p1.X < 0)
                    Xtrans = p1.X + (r.SizeX / 2);

                if (p1.Z > 0)
                    Ztrans = p1.Z - (r.SizeZ / 2);
                else
                    Ztrans = p1.Z + (r.SizeZ / 2);
            }

            this.Transform = new TranslateTransform3D(Xtrans, Ytrans, Ztrans);
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

        public bool IntToBoolOuterBrace(int str)
        {
            return str == Smile.OUTERBRACE;
        }

        public bool IsOuterBrace
        {
            get
            {
                return (bool)this.GetValue(OuterBraceProperty);
            }

            set
            {
                this.Model.Location = (value? Smile.OUTERBRACE:Smile.INNERBRACE);
                this.SetValue(OuterBraceProperty, value);
            }
        }

        protected static void OuterBraceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BraceVisual3D)d).locateBrace();
        }

    }
}
