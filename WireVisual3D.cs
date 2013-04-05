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
    public class WireVisual3D : SmileVisual3D
    {
        

        public WireVisual3D(BraceVisual3D parent)
            : this(Colors.Pink, parent)
        {
        }
        public WireVisual3D(Color color, BraceVisual3D p)
        {
            if (p != null)
            {
                this.parent = p;
                Id = p.Id + "_wire" + p.Children.Count.ToString("00") + "." + p.Parent.Parent.Parent.patient.name;
                
                if (model == null) model = new Wire();
                model.Id = Id;

                sample(color);
            }
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


        private BraceVisual3D parent;
        public BraceVisual3D Parent
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

        Wire model;
        public Wire Model
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
                    //_manipulator.Diameter = Math.Max(r.SizeX, Math.Max(r.SizeY, r.SizeZ)) + 1;
                    //_manipulator.Length = _manipulator.Diameter * 0.75;
                    _manipulator.Bind(this);
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
    }
}
