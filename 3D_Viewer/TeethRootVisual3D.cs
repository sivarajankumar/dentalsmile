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
using smileUp.DataModel;

namespace smileUp
{
    public class TeethRootVisual3D : SmileVisual3D
    {
        public GumContainer gc;
        public ToothContainer tc;
        public BraceContainer bc;
        public WireContainer wc;

        public TeethRootVisual3D(TeethVisual3D parent)
            : this(Colors.Pink, parent)
        {
        }
        public TeethRootVisual3D(Color color, TeethVisual3D p)
        {
            if (p != null)
            {
                this.parent = p;
                gc = this.parent.gc;
                tc = this.parent.tc;
                bc = this.parent.bc;
                wc = this.parent.wc;

                Id = p.Id + "_rootth" + tc.Children.Count.ToString("00") + "." + p.Parent.Parent.patient.Id;
                
                if (model == null) model = new Teeth();
                model.Id = Id;
                model.Length = 0.0;

                sample(color);

                //Adding Teeth Label
                //TextVisual3D text = new TextVisual3D();
                //text.Text = Id.ToString();
                //this.Children.Add(text);
                
                //showHideBoundingBox();
            }

        }
        public static Color getTeethColor(int num) {

            if ((num > 0 && num <= 3) || (num >= 14 && num <= 16) || (num >= 17 && num <= 19) || (num >= 30 && num <= 32)) return Colors.Blue;
            else if ((num >= 4 && num <= 5) || (num >= 12 && num <= 13) || (num >= 20 && num <= 21) || (num >= 28 && num <= 29)) return Colors.Red;
            else if ((num >= 6 && num <= 6) || (num >= 11 && num <= 11) || (num >= 22 && num <= 22) || (num >= 27 && num <= 27)) return Colors.Green;
            else if ((num >= 7 && num <= 8) || (num >= 9 && num <= 10) || (num >= 23 && num <= 24) || (num >= 25 && num <= 26)) return Colors.BlueViolet;
            
            return Colors.Gold;
        }

        internal void sample(Color color)
        {
            ///*
            GeometryModel3D bigCubeModel = GeometryGenerator.CreateCubeModel();
            bigCubeModel.Material = MaterialHelper.CreateMaterial(new SolidColorBrush(color));

            this.Content = bigCubeModel;
            Rect3D r = this.Parent.Content.Bounds;
            double min = Math.Min(r.SizeX, Math.Min(r.SizeY, r.SizeZ));
            Transform3DGroup transformGroup = new Transform3DGroup();
            //transformGroup.Children.Add(new TranslateTransform3D(0.5 * parent.Children.Count, 0, 0));
            //transformGroup.Children.Add(new TranslateTransform3D(new Point3D(r.X, r.Y, r.Z).ToVector3D()));
            //transformGroup.Children.Add(new ScaleTransform3D(new Vector3D(r.SizeY, r.SizeY, r.SizeY), new Point3D(r.X,r.Y,r.Z)));
            transformGroup.Children.Add(new TranslateTransform3D(parent.rootPoint().ToVector3D()));
            transformGroup.Children.Add(new ScaleTransform3D(new Vector3D(min, min, min), parent.rootPoint()));
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
        String id;

        public static readonly DependencyProperty ColorProperty
           = DependencyProperty.Register("ColorTeethRoot", typeof(Brush), typeof(TeethRootVisual3D), new UIPropertyMetadata(ColorChanged));

        public static readonly DependencyProperty IdProperty
           = DependencyProperty.Register("IdTeethRoot", typeof(String), typeof(TeethRootVisual3D),
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

        public Brush Color
        {
            set
            {
                SetValue(ColorProperty, value);
                //if(Model != null) Model.Colour = value;
            }
            get
            {
                return (Brush)GetValue(ColorProperty);
            }
        }

        Teeth model;
        public Teeth Model
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
            this.parent.clearManipulator();
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
        }

        private BoundingBoxWireFrameVisual3D _boundingBox;
        private BoundingBoxWireFrameVisual3D BoundingBox { get; set; }
        private Boolean showBoundingBox = true;
        public Boolean ShowBoundingBox
        {
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

        public void showHideBoundingBox()
        {
            if (ShowBoundingBox)
            {
                if (_boundingBox == null)
                {
                    _boundingBox = new BoundingBoxWireFrameVisual3D();
                    _boundingBox.BoundingBox = this.Content.Bounds;
                    this.parent.Children.Add(_boundingBox);
                    showBoundingBox = false;
                }
            }
            else
            {
                this.parent.Children.Remove(_boundingBox);
                _boundingBox = null;
                showBoundingBox = true;
            }
        }

        /// <summary>
        /// @Deprecated
        /// Not used anymore since changes in CONTAINER design.
        /// <see cref="cleanManipulator"/>
        /// </summary>
        internal void clearManipulator()
        {
            if (this.parent.Children.Count > 0)
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
            }
            //this.parent.Children.Remove(_manipulator);
            _manipulator = null;
        }

        public Point3D centroid()
        {
            Rect3D bounds = this.Content.Bounds;
            
            var x = bounds.X - (bounds.SizeX / 2);
            var y = bounds.Y - (bounds.SizeY / 2);
            var z = bounds.Z - (bounds.SizeZ / 2);

            Point3D p = new Point3D(x,y,z);
            
            return p;
        }


        internal void cleanManipulator()
        {
            //Remove all Tooth manipulators
            List<Visual3D> childs = tc.Children.ToList();
            foreach (var m in childs)
            {
                if (m is CombinedManipulator)
                {
                    tc.Children.Remove(m);
                }
            }

            try
            {
                //Remove all Brace manipulators
                childs = bc.Children.ToList();
                foreach (var m in childs)
                {
                    if (m is BraceVisual3D)
                    {
                        ((BraceVisual3D)m).cleanManipulator();
                    }
                }

            }
            catch (Exception e) { }
            //this.parent.Children.Remove(_manipulator);
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
                Bind(_manipulator);
            }
            tc.Children.Add(_manipulator);

        }

        protected static void ColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
