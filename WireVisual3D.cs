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
    public class WireVisual3D : SmileVisual3D
    {
        public static readonly DependencyProperty ThicknesProperty = DependencyProperty.Register("ThicknesTransform", typeof(double), typeof(WireVisual3D), new UIPropertyMetadata(1.2, ThicknesChanged));
        //public static readonly DependencyProperty ColourProperty = DependencyProperty.Register("ColourTransform", typeof(Brush), typeof(WireVisual3D), new UIPropertyMetadata(Brushes.Blue, ColourChanged));
        public static readonly DependencyProperty MaterialProperty = DependencyProperty.Register("MaterialTransform", typeof(String), typeof(WireVisual3D), new UIPropertyMetadata("ceramic", MaterialChanged));


        public WireVisual3D(Brush b, BraceVisual3D p1, BraceVisual3D p2)
        {
            if (p1 != null)
            {
                Id = p1.Id + "_wire" + p1.Children.Count.ToString("00") + "." + p1.Parent.Parent.Parent.patient.Name;

                brace1 = p1;
                brace2 = p2;

                contours(b);

                Bind(p1, p2);
            }
        }
        void contours(Brush b)
        {
                model = new Wire();
                model.Brace1 = brace1.Model;
                model.Brace2 = brace2.Model;

                Point3DCollection contours = new Point3DCollection();
                contours.Add(brace1.ToWorld(brace1.centroid()));
                contours.Add(brace2.ToWorld(brace2.centroid()));

                TubeVisual3D tube = new TubeVisual3D { Diameter = 1.02, Path = contours, Fill = b };
                this.Children.Add(tube);
        }

        internal void sample(Brush b)
        {            
            /*
            GeometryModel3D bigCubeModel = GeometryGenerator.CreateCubeModel();
            bigCubeModel.Material = new DiffuseMaterial(new SolidColorBrush(color));

            this.Content = bigCubeModel;

            Transform3DGroup transformGroup = new Transform3DGroup();
            transformGroup.Children.Add(new TranslateTransform3D(0.5 * parent.Children.Count, 0, 0));
            this.Transform = transformGroup;
            //*/
        }

        private Brush brush;
        public Brush Brush
        {
            set { brush = value; }
            get { return brush; }
        }

        private BraceVisual3D brace1;
        public BraceVisual3D Brace1
        {
            set { brace1 = value; }
            get { return brace1; }        
        }
        private BraceVisual3D brace2;
        public BraceVisual3D Brace2
        {
            set { brace2 = value; }
            get { return brace2; }
        }

        private CombinedManipulator _manipulator;
        public CombinedManipulator Manipulator { get; set; }

        String id;
        public String Id
        {
            set { id = value; }
            get { return id; }
        }

        Wire model;
        public Wire Model
        {
            set { model = value; }
            get { return model; }
        }

        /// <summary>
        /// @Deprecated
        /// Not used anymore since changes in CONTAINER design.
        /// <see cref="displayManipulator"/>
        /// </summary>
        public void showHideManipulator()
        {
            this.brace1.clearManipulator();
                if (_manipulator == null)
                {
                    Rect3D r = this.Content.Bounds;
                    _manipulator = new CombinedManipulator();
                    //_manipulator.Position = new Point3D(r.X + (r.SizeX/2),r.Y + (r.SizeY / 2),r.Z + (r.SizeZ/2));
                    _manipulator.Offset = new Vector3D(r.X + (r.SizeX / 2), r.Y + (r.SizeY / 2), r.Z + (r.SizeZ / 2));
                    _manipulator.Pivot = new Point3D(r.X + (r.SizeX / 2), r.Y + (r.SizeY / 2), r.Z + (r.SizeZ / 2));
                    //_manipulator.Pivot = new Point3D(0, 0, 0);
                    //_manipulator.Diameter = Math.Max(r.SizeX, Math.Max(r.SizeY, r.SizeZ)) + 1;
                    //_manipulator.Length = _manipulator.Diameter * 0.75;
                    _manipulator.Bind(this);
                }
                this.brace1.Children.Add(_manipulator);
        }

        /// <summary>
        /// @Deprecated
        /// Not used anymore since changes in CONTAINER design.
        /// <see cref="cleanManipulator"/>
        /// </summary>
        internal void clearManipulator()
        {
            List<Visual3D> childs = this.brace1.Children.ToList();
            foreach (var m in childs)
            {
                if (m is CombinedManipulator)
                {
                    this.brace1.Children.Remove(m);
                }
            }

            childs = this.Children.ToList();
            foreach (var m in childs)
            {
                if (m is BraceVisual3D)
                {
                    ((BraceVisual3D)m).clearManipulator();
                }
            }

            //this.parent.Children.Remove(_manipulator);
            _manipulator = null;
        }

        public static readonly DependencyProperty Target1TransformProperty = DependencyProperty.Register("Target1Transform", typeof(Transform3D), typeof(WireVisual3D), new PropertyMetadata(WireTransformChanged));
        public static readonly DependencyProperty Target2TransformProperty = DependencyProperty.Register("Target2Transform", typeof(Transform3D), typeof(WireVisual3D), new PropertyMetadata(WireTransformChanged));
        
        protected static void WireTransformChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WireVisual3D)
            {
                WireVisual3D dt = (WireVisual3D)d;
                
                Point3DCollection contours = new Point3DCollection();
                contours.Add(dt.brace1.ToWorld(dt.brace1.centroid()));
                contours.Add(dt.brace2.ToWorld(dt.brace2.centroid()));

                dt.Children.Clear();
                
                TubeVisual3D tube = new TubeVisual3D { Diameter = 1.02, Path = contours, Fill = dt.brush };
                dt.Children.Add(tube);
                
                //Console.WriteLine("wire:" + dt.Id);
            }
        }
        public virtual void Bind(BraceVisual3D source1, BraceVisual3D source2)
        {
            Bind1(source1);
            Bind2(source2);
        }
        public virtual void Bind1(BraceVisual3D source)
        {
            BindingOperations.SetBinding(this, Target1TransformProperty, new Binding("Transform") { Source = source });
        }
        public virtual void Bind2(BraceVisual3D source)
        {
            BindingOperations.SetBinding(this, Target2TransformProperty, new Binding("Transform") { Source = source });
        }

        
        protected static void ThicknesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        { 
        
        }

        protected static void MaterialChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
