using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Resources;

using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using smileUp.CustomEditors;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;

namespace smileUp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool teethMarkerBool = false;
        Boolean cutLineBool = false;
        Boolean alignObjectBool = false;

        private MainViewModel vm;
        Point3D startPoint;
        Point mousePoint;

        SmileVisual3D g;
		
		//added by achie
        int hit = 0;
        private readonly List<Point3D> mpoints = new List<Point3D>();
        public IList<TeethTextItem> TextItems { get; set; }

        private static MeasurementForm measurementForm;
		  
        /*
        public static MainWindow GetMainWindow(DependencyObject obj)
        {
            return (MainWindow)obj.GetValue(MainWindowProperty);
        }

        public static void SetMainWindow(DependencyObject obj, MainWindow value)
        {
            obj.SetValue(MainWindowProperty, value);
        }

        // Using a DependencyProperty as the backing store for MainWindow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainWindowProperty = DependencyProperty.RegisterAttached("MainWindow", typeof(MainWindow), typeof(MainWindow), null);
        */
        DispatcherTimer _timer = new DispatcherTimer();
        int cont = 0;

        public MainWindow()
        {
            InitializeComponent();
            vm = new MainViewModel(new FileDialogService(), view1, this);
            DataContext = vm;

            loadTeethNumberToChart();

            Loaded += new RoutedEventHandler(OnLoaded);
            _propertyGrid.PropertyValueChanged += new Xceed.Wpf.Toolkit.PropertyGrid.PropertyValueChangedEventHandler(_propertyGrid_PropertyValueChanged);

            measurementForm = new MeasurementForm(this);
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            _timer.Interval = new System.TimeSpan(0, 0, 0, 0, 1000);
            _timer.Tick += new System.EventHandler(OnTimerEvent);

            _timer.Start();

        }

        void OnTimerEvent(object sender, System.EventArgs args)
        {
            cont = cont + 1;
            statusBarText.Text = " ... "+cont.ToString();
        }

        #region TESTING

        /*
            watch = new Stopwatch();
            watch.Start();

            world = new World(new CollisionSystemSAP());
            world.Gravity = new JVector(0, 0, 0);
            world.CollisionSystem.CollisionDetected += new CollisionDetectedHandler(CollisionSystem_CollisionDetected);
           //tester();

            integratorThread = new Thread(IntegrationWorker);
            integratorThread.Start();

            this.Closing += MainWindow_Closing;

        }
        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            watch.Stop();
            integratorThread.Abort();
        }
        private void IntegrationWorker()
        {
            while (true)
            {
                watch.Restart();
                float step = 1.0f / 65;
                if (step > 1.0f / 100.0f) step = 1.0f / 100.0f;
                world.Step(step, true);
            }
        }
        void CollisionSystem_CollisionDetected(RigidBody body1, RigidBody body2, JVector point1, JVector point2, JVector normal, float penetration)
        {
            Console.WriteLine("CollisionSystem_CollisionDetected");
        }
        */
        private void tester()
        {
            

            /* Wiring Brace */
            Point3D p0 = new Point3D(-0.849706889491251, -3.41818201148931, -3.45752298819413);
            Point3D p1 = new Point3D(7.65467623338951, -2.94937570295315, -1.60678487891435);
            Point3DCollection contours = new Point3DCollection();
            contours.Add(p0);
            contours.Add(p1);

            BoxVisual3D b = new BoxVisual3D();
            b.Center = p0;
            b.Length = 2;
            b.Height = 2;
            b.Width = 2;
            view1.Children.Add(b);

            BoxVisual3D b1 = new BoxVisual3D();
            b1.Center = p1;
            b1.Length = 2;
            b1.Height = 2;
            b1.Width = 2;
            view1.Children.Add(b1);

            TubeVisual3D tube = new TubeVisual3D { Diameter = 1.02, Path = contours, Fill = Brushes.Green };
            view1.Children.Add(tube);

            //Point3D pp = new Point3D(0, 0, 0);
            //Vector3D mv = Point3D.Subtract(p0, pp);

           // view1.Children.Add(new RectangleVisual3D { Origin = p0, Normal = mv, Fill = new SolidColorBrush(Color.FromArgb(190, 255, 0, 0)), BackMaterial = MaterialHelper.CreateMaterial(new SolidColorBrush(Colors.Green)) });

            //view1.Children.Add(new RectangleVisual3D { Origin = pp, Normal = mv, Fill = new SolidColorBrush(Color.FromArgb(190, 255, 0, 0)), BackMaterial = MaterialHelper.CreateMaterial(new SolidColorBrush(Colors.AliceBlue)) });

            /* Normal Vector Directions
            Point3D p0 = new Point3D(-0.849706889491251,-3.41818201148931,-3.45752298819413);
            Point3D p1 = new Point3D(7.65467623338951,-2.94937570295315,-1.60678487891435);
            Point3D p2 = new Point3D(3.58212516465058, 3.19543635742251, -4.13603328982721);

            BoxVisual3D b = new BoxVisual3D();
            b.Center = p0;
            b.Length = 2;
            b.Height = 2;
            b.Width = 2;
            view1.Children.Add(b);

            BoxVisual3D b1 = new BoxVisual3D();
            b1.Center = p1;
            b1.Length = 2;
            b1.Height = 2;
            b1.Width = 2;
            view1.Children.Add(b1);

            BoxVisual3D b2 = new BoxVisual3D();
            b2.Center = p2;
            b2.Length = 2;
            b2.Height = 2;
            b2.Width = 2;
            view1.Children.Add(b2);

            Vector3D n0 = Point3D.Subtract(p0, p1);
            n0.Negate();
            view1.Children.Add(new RectangleVisual3D { Origin = p0, Normal = n0, Fill = new SolidColorBrush(Color.FromArgb(80, 255, 0, 0)), BackMaterial = MaterialHelper.CreateMaterial(new SolidColorBrush(Colors.Green)) });

            Vector3D n1 = Point3D.Subtract(p1, p0);
            n1.Negate();
            view1.Children.Add(new RectangleVisual3D { Origin = p1, Normal = n1, Fill = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0)), BackMaterial = MaterialHelper.CreateMaterial(new SolidColorBrush(Colors.Green)) });

            Point3D m = new Point3D();
            m.X = (p1.X + p0.X)/2;
            m.Y = (p1.Y + p0.Y)/2;
            m.Z = (p1.Z + p0.Z)/2;

            BoxVisual3D b3 = new BoxVisual3D();
            b3.Center = m;
            b3.Length = 1;
            b3.Height = 1;
            b3.Width = 1;
            view1.Children.Add(b3);

            Point3D pp = new Point3D(0, 0, 0);
            Vector3D mv = Point3D.Subtract(p0, pp);

            view1.Children.Add(new RectangleVisual3D { Origin = pp, Normal = mv, Fill = new SolidColorBrush(Color.FromArgb(190, 255, 0, 0)), BackMaterial = MaterialHelper.CreateMaterial(new SolidColorBrush(Colors.Green)) });

            Vector3D n2 = Point3D.Subtract(p2, m);
            n2.Negate();
            view1.Children.Add(new RectangleVisual3D { Origin = p2, Normal = n2, Fill = new SolidColorBrush(Color.FromArgb(190, 255, 0, 0)), BackMaterial = MaterialHelper.CreateMaterial(new SolidColorBrush(Colors.Green)) });

            //Vector3D n = CalculateNormal(ref p0, ref p1, ref p2);
            //view1.Children.Add(new RectangleVisual3D { Origin = p1, Normal = n, Fill = new SolidColorBrush(Color.FromArgb(190, 255, 0, 0)), BackMaterial = MaterialHelper.CreateMaterial(new SolidColorBrush(Colors.Green)) });
            */

            /* Cutting Plane
            var mb = new MeshBuilder();
            mb.AddSphere(new Point3D(-2, 0, 0), 2);
            var mesh = mb.ToMesh();
            var n = new Vector3D(0, 0.2, 1);
            var p = new Point3D(0, 0, 0.5);
            var geo = MeshGeometryHelper.Cut(mesh, p, n);
            var m = new GeometryModel3D(geo, Materials.Blue);
            m.BackMaterial = Materials.Red;

            var mv = new ModelVisual3D();
            mv.Content = m;
            view1.Children.Add(mv);

            var mbx = new MeshBuilder();
            mbx.AddSphere(new Point3D(2, 0, 0), 1);
            var mvd = new ModelVisual3D();
            GeometryModel3D g = new GeometryModel3D(mbx.ToMesh(), Materials.Blue);
            mvd.Content = g;
            view1.Children.Add(mvd);
            var segments = MeshGeometryHelper.GetContourSegments(mesh, p, n).ToList();
            foreach (IList<Point3D> contour in MeshGeometryHelper.CombineSegments(segments, 1e-6).ToList())
            {
                if (contour.Count == 0)
                    continue;
                Point3DCollection contours = new Point3DCollection(contour);
                view1.Children.Add(new TubeVisual3D { Diameter = 0.02, Path = contours, Fill = Brushes.Green });
            }
            view1.Children.Add(new RectangleVisual3D { Origin = p, Normal = n, Fill = new SolidColorBrush(Color.FromArgb(80, 255, 0, 0)) });
            */
            
            /*
            g = new SmileVisual3D();
            GeometryModel3D geo1 = GeometryGenerator.CreateCubeModel();
            geo1.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Red));

            Transform3DGroup transformGroup = new Transform3DGroup();
            transformGroup.Children.Add(new ScaleTransform3D(2, 2, 2));
            g.Transform = transformGroup;

            g.Content =geo1;

            view1.Children.Add(g);

            CombinedManipulator c = new CombinedManipulator();
            //c.Diameter = 5;
            c.Bind(g);
            view1.Children.Add(c);
            */

            /*

              List<Point3d> pointList = // list of candidate points

                
              Point3d bp = // point used to select nearest point in pointList
 
              // Because the same source element can be passed
              // to the function given to Aggregate() many times,
              // we cache each point along with the result of calling
              // DistanceTo() in an anonymous type, allowing us to
              // avoid redundant calls to DistanceTo():
 
              var items = pointList.Select( p => new{ point = p, dist = p.DistanceTo( bp ) } );
 
              // Find the nearest point:
 
              Point3d nearest = items.Aggregate( ( a, b ) => a.dist < b.dist ? a : b ).point;
            */
        }


        private void cut()
        {
            var n = new Vector3D(0, 0, 1);
            var p = new Point3D(0, 0, 0);
            MeshGeometry3D mesh = g.ToWorldMesh();

            var geo = MeshGeometryHelper.Cut(mesh, p, n);
            var m = new GeometryModel3D(geo, Materials.Blue);
            m.BackMaterial = Materials.Red;

            var mv = new ModelVisual3D();
            mv.Content = m;

            Transform3DGroup transformGroup = new Transform3DGroup();
            transformGroup.Children.Add(new TranslateTransform3D(5, 0, 0));
            mv.Transform = transformGroup;

            view1.Children.Add(mv);

        }
        #endregion



        private void ModelLoaded(object sender, RoutedEventArgs e)
        {
            view1.ZoomExtents();
        }

        private void ModelUIElement3D_MouseDown(object sender, MouseButtonEventArgs e)
        {

            /*if (sender == trololo)
            {
                RaiseModelClickEvent("auditorium");
            }
            else if (sender == LogoMouseDown)
            {
                RaiseModelClickEvent("logo");
            }*/

        }
        private void backup_addPlane1_Click(object sender, RoutedEventArgs e)
        {
            Model3D d = vm.RawVisual.Content;
            Rect3D bounds = d.Bounds;
            //var length = bound.SizeX > bound.SizeY ? bound.SizeX : bound.SizeY > bound.SizeZ ? bound.SizeY : bound.SizeZ > bound.SizeX ? bound.SizeZ : bound.SizeX;
            var length  = Math.Max(bounds.Size.X, Math.Max(bounds.Size.Y, bounds.Size.Z));
            length += 10;
            
            RectangleVisual3D r = new RectangleVisual3D();
            r.LengthDirection = new Vector3D(0,1,0);
            if (d != null)
            {
                r.Width = length;
                r.Length = length;
            }
            view1.Children.Add(r);

            Model3DGroup mg = (Model3DGroup)d;

            Model3DGroup nm = new Model3DGroup();

            foreach (GeometryModel3D gm in mg.Children)
            {
                MeshGeometry3D mesh = (MeshGeometry3D)gm.Geometry;
                Point3DCollection positions = mesh.Positions;
                Vector3DCollection normals = mesh.Normals;
                Int32Collection indices = mesh.TriangleIndices;
                
                Point3DCollection newPositions = new Point3DCollection() ;
                Vector3DCollection newNormals = new Vector3DCollection();
                Int32Collection newIndices = new Int32Collection();
                int c = 0;
                foreach (Point3D p in positions)
                {
                    if (p.Y >= 0)
                    {
                        newPositions.Add(p);
                        newIndices.Add(c);
                    }
                    else {
                        newPositions.Add(new Point3D(p.X, 0, p.Z));
                        newIndices.Add(c);
                    }
                    c++;
                }

                GeometryModel3D gd = new GeometryModel3D();
                MeshGeometry3D g = new MeshGeometry3D();
                g.Positions = newPositions;
                g.TriangleIndices = newIndices;
                gd.Geometry = g;
                gd.Material = MaterialHelper.CreateMaterial(Colors.Aquamarine);
                nm.Children.Add(gd);
                nm.Transform = new TranslateTransform3D(new Vector3D(d.Bounds.X, d.Bounds.Y, d.Bounds.Z));
            }
            ModelVisual3D v = new ModelVisual3D();
            v.Content = nm;

            view1.Children.Add(v);

            foreach (GeometryModel3D gm in mg.Children)
            {
                MeshGeometry3D mesh = (MeshGeometry3D)gm.Geometry;
                
                Point3DCollection newPositions = new Point3DCollection();
                Vector3DCollection newNormals = new Vector3DCollection();
                Int32Collection newIndices = new Int32Collection();

                for (int i = 0; i < mesh.TriangleIndices.Count; i = i + 3)
                {
                    int index1 = mesh.TriangleIndices[i];
                    int index2 = mesh.TriangleIndices[i + 1];
                    int index3 = mesh.TriangleIndices[i + 2];

                    Point3D point1 = mesh.Positions[index1];
                    Point3D point2 = mesh.Positions[index2];
                    Point3D point3 = mesh.Positions[index3];

                    if (point1.Y < 0 || point2.Y < 0 || point3.Y < 0)
                        continue;

                    newIndices.Add(index1);
                    newIndices.Add(index2);
                    newIndices.Add(index3);

                    newPositions.Add(point1);
                    newPositions.Add(point2);
                    newPositions.Add(point3);

                    newNormals.Add(CalculateNormal(ref point1, ref point2, ref point3));

                }

                GeometryModel3D gd = new GeometryModel3D();
                MeshGeometry3D g = new MeshGeometry3D();
                g.Positions = newPositions;
                g.Normals = newNormals;
                g.TriangleIndices = newIndices;
                gd.Geometry = g;
                gd.Material = MaterialHelper.CreateMaterial(Colors.AliceBlue);

                Model3DGroup x = new Model3DGroup();

                x.Children.Add(gd);
                x.Transform = new TranslateTransform3D(new Vector3D(-d.Bounds.X, -d.Bounds.Y, -d.Bounds.Z));

                ModelVisual3D v2 = new ModelVisual3D();
                v2.Content = x;

                view1.Children.Add(v2);

            }
        }

        private Vector3D CalculateNormal(ref Point3D p0, ref Point3D p1, ref Point3D p2)
        {
            Vector3D v0 = new Vector3D(
              p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            Vector3D v1 = new Vector3D(
              p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            return Vector3D.CrossProduct(v0, v1);
        }



       
       private void cloneObject_Click(object sender, RoutedEventArgs e)
        {
            Model3DGroup mg = (Model3DGroup) vm.CurrentModel;
            Model3DGroup cl = mg.Clone();
            foreach (GeometryModel3D gm in mg.Children)
            {
                MeshGeometry3D mesh = (MeshGeometry3D) gm.Geometry;
                gm.Material = MaterialHelper.CreateMaterial(Colors.Gold);
            } 
            
            Transform3DGroup tg = new Transform3DGroup();
            tg.Children.Add(new TranslateTransform3D(100, 0, -100)); 
            cl.Transform = tg;
            
            Model3DGroup d = (Model3DGroup) vm.CurrentModel;
            d.Children.Add(cl);
        }

       ModelVisual3D GetHitTestResult(Point location)
       {
           HitTestResult result = VisualTreeHelper.HitTest(view1, location);
           if (result != null && result.VisualHit is ModelVisual3D)
           {
               ModelVisual3D visual = (ModelVisual3D)result.VisualHit;
               return visual;
           }

           return null;
       }


       Point3DCollection points;
       Vector3DCollection vectors ;
       List<BraceVisual3D> bracesModel;

        private void view1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mousePoint = e.GetPosition(view1);
            if (teethMarkerBool)
            {
                if (addMarkerToTeeth(mousePoint))               
                    return;                                
            }


            enableRemoveTeethButton( false);

            ModelVisual3D result = GetHitTestResult(mousePoint);
            if (result == null)
            {
                //Console.WriteLine("NULL");
                return;
            }


            if (result is TeethVisual3D)
            {
                TeethVisual3D teeth = (TeethVisual3D)result;
                vm.showHideManipulator(teeth);
                selectTeethChart(teeth.Model.Id);
                //TOOD: show enable icons/buttons
                enableRemoveTeethButton(true);
                showTeethProperty(teeth);

                if (MeasurementLineBtn.IsChecked == true)
                {
                    System.Windows.Point position = Mouse.GetPosition(this);
                    HitTestResult resultM = VisualTreeHelper.HitTest(view1, position);
                    RayMeshGeometry3DHitTestResult result3d = resultM as RayMeshGeometry3DHitTestResult;
                    if (result3d != null)
                    {
                        if (hit == 0)
                        {
                            mpoints.Add(result3d.PointHit); hit++; this.Show();
                        }
                        else if (hit == 1)
                        {
                            mpoints.Add(result3d.PointHit); hit = 0;
                            double length = MathHelper.calculate_distance(mpoints[0].X, mpoints[0].Y, mpoints[0].Z, mpoints[1].X, mpoints[1].Y, mpoints[1].Z);

                            //teeth.Model.Length = Math.Round(vm.mm_converter(length), 4);    
                            teeth.Model.Length = MathHelper.mm_converter(length);
                            teeth.Model.StartPosition = mpoints[0].X;
                            teeth.Model.EndPosition = mpoints[1].X;
                            createLine(mpoints[0], mpoints[1]);
                            measurementForm.addRow(teeth, "auto"); 
                        }
                    }
                }
                else if (AutoMeasurementLineBtn.IsChecked == true)
                {
                    var centerPoint = teeth.centroid();
                    double length = centerPoint.X * 2;
                    teeth.Model.Length = MathHelper.mm_converter(length);
                    measurementForm.addRow(teeth, "auto");
                }
				
	
                return;
            }
            else if (result is BraceVisual3D)
            {
                BraceVisual3D brace = (BraceVisual3D)result;
                
                //TOOD: show enable icons/buttons
                enableRemoveBraceButton(true);

                _propertyGrid.Visibility = System.Windows.Visibility.Visible;                
                _propertyGrid.SelectedObject = CustomAttributeEditorBrace.CreateCustomAttributEditorBrace(brace.Model);

                if (MakeWireBtn.IsChecked == true)
                {
                    if (bracesModel == null) bracesModel = new List<BraceVisual3D>();
                    bracesModel.Add(brace);
                }
                else
                {
                    vm.showHideManipulator(brace.Parent.Parent);
                }

                vm.showHideManipulator(brace);
                selectTeethChart(brace.Parent.Model.Id);

                return;
            }else if (result is GumVisual3D)
            {
                GumVisual3D gum = (GumVisual3D)result;
                vm.JawVisual.selectedGum = gum;
                vm.showHideManipulator(gum);

                enableAddTeethButton(true);
                resetTeethChart();

                _propertyGrid.Visibility = System.Windows.Visibility.Hidden;
                _propertyGrid.SelectedObject = null; 

                return;
            }
            else if (result is RawVisual3D)
            {
                Console.WriteLine("RawVisual3D");
                ((RawVisual3D)result).showHideManipulator();

                _propertyGrid.Visibility = System.Windows.Visibility.Hidden;
                _propertyGrid.SelectedObject = null;
                
                return;
            }


            //var pMenu = (ContextMenu)this.Resources["MyContextMenu"];
            //pMenu.IsOpen = true;
            //Console.WriteLine("MouseRight");

        }

        private bool addMarkerToTeeth(Point mousePoint)
        {
            var pt = view1.FindNearestPoint(mousePoint);
            if (pt is Point3D)
            {
                if (points == null) points = new Point3DCollection();
                if (vectors == null) vectors = new Vector3DCollection();

                //Console.WriteLine(pt.Value.ToString());
                //var pv = new PointsVisual3D();

                //pv.Points.Add((Point3D) pt);
                vm.RawVisual.AddBox(pt);
                //vm.HelixView.Viewport.Children.Add(b);

                points.Add((Point3D)pt);
                if (points.Count > 1)
                {
                    Vector3DCollection v = new Vector3DCollection();
                    for (var i = 1; i < points.Count; i++)
                    {
                        Point3D ps = points[i];
                        Point3D p0 = points[i - 1];

                        Vector3D n0 = Point3D.Subtract(ps, p0);

                        if (i == 1)
                        {
                            n0.Normalize();
                            v.Add(n0);
                            //view1.Children.Add(new RectangleVisual3D { Origin = p0, Normal = n0, Fill = new SolidColorBrush(Color.FromArgb(80, 255, 0, 0)), BackMaterial = MaterialHelper.CreateMaterial(new SolidColorBrush(Colors.Green)) });
                        }
                        n0.Negate();
                        n0.Normalize();
                        v.Add(n0);
                        //view1.Children.Add(new RectangleVisual3D { Origin = ps, Normal = n0, Fill = new SolidColorBrush(Color.FromArgb(80, 255, 0, 0)), BackMaterial = MaterialHelper.CreateMaterial(new SolidColorBrush(Colors.Green)) });
                    }
                    vectors = v;
                }
            }
            return true;
        }

        private void showTeethProperty(TeethVisual3D teeth)
        {
            _propertyGrid.Visibility = System.Windows.Visibility.Visible;
            _propertyGrid.SelectedObject = CustomAttributeEditorTeeth.CreateCustomAttributEditorTeeth(teeth.Model);
        }


        void _propertyGrid_PropertyValueChanged(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyValueChangedEventArgs e)
        {
            var item = e.OriginalSource as Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem;
            if (item.DisplayName == "TeethNumber") {
                CustomAttributeEditorTeeth t = (CustomAttributeEditorTeeth)_propertyGrid.SelectedObject;
                String oldid = t.Id;
                int newValue = (int)e.NewValue;
                String newid = Regex.Replace(oldid, @"teeth\d\d", "teeth" + newValue.ToString("00"), RegexOptions.IgnoreCase);
                t.Id = newid;
                _propertyGrid.SelectedObject = null;
                _propertyGrid.SelectedObject = t;

                vm.updateTeethMap(oldid, newid);
                selectTeethChart(newid);
                
                //TODO: save to DB
            }

        }


        private void enableAddBraceButton(bool b)
        {
            AddBraceBtn.IsEnabled = b;
        }

        private void enableRemoveBraceButton(bool b)
        {
            RemoveBraceBtn.IsEnabled = b;
            enableAddBraceButton(b);
        }

        private void enableAddTeethButton(bool b)
        {
            AddTeethBtn.IsEnabled = b;
        }

        private void enableRemoveTeethButton(bool b)
        {
            RemoveTeethBtn.IsEnabled = b;
            enableAddTeethButton(b);
        }

        
        private void view1_MouseMove(object sender, MouseEventArgs e)
        {
            mousePoint = e.GetPosition(view1);
            //var pt = view1.FindNearestPoint(e.GetPosition(view1));
            
            //TODO: detect collision between visual3d
            /*ModelVisual3D result = GetHitTestResult(mousePoint);
            if (result == null)
            {
                return;
            }


            if (result is TeethVisual3D)
            {
                Console.WriteLine("teeth");    
            }*/
			
			/*
			double x = 0, y = 0, z = 0;
            var pt = view1.FindNearestPoint(mousePoint);
            if (pt.HasValue)
            {
                text1.Text = String.Format("X: {0:0.00} Y: {1:0.00} Z: {1:0.000}", pt.Value.X, pt.Value.Y, pt.Value.Z);
            }
            else { text1.Text = String.Format("X: {0:0.00} Y: {1:0.00} Z: {1:0.000}", x, y, z); }
            return;
             */ 
        }

        private void manip(object sender, MouseButtonEventArgs e)
        {
            var pt = view1.FindNearestPoint(e.GetPosition(view1));
            if (cutLineBool && pt != null)
            {
                Point3D endPoint = pt.Value;


                Model3D d = vm.CurrentModel;

                //var position = new Point3D(startPoint.Y + ((endPoint.Y - startPoint.Y) / 2), startPoint.X, startPoint.Z);
                //Vector3D v = (endPoint - startPoint);
                var position = startPoint + (endPoint - startPoint) * 0.5;
                //var position = new Point3D(0, 0, 0);

                RectangleVisual3D r = new RectangleVisual3D();
                //r.LengthDirection = new Vector3D(0, 1, 0);
                if (d != null)
                {
                    r.Normal = new Vector3D(1,0,0);
                    r.LengthDirection = new Vector3D(0,1,0);
                    //startPoint + (endPoint - startPoint) * 0.5;
                    r.Width = (d.Bounds.SizeX* 1.5);
                    r.Length = (d.Bounds.SizeY * 1.5);
                    r.Origin = position;
                }

                var combinedM = new CombinedManipulator();
                combinedM.Position = position;
                combinedM.Offset = new Vector3D(0, 0, 0);
                combinedM.Diameter = r.Length/2;
                combinedM.Bind(r);

                view1.Children.Add(combinedM);
                view1.Children.Add(r);

                cutLineBool = false;
            }

        }

        private void backup_alignObject_Click(object sender, RoutedEventArgs e)
        {
            if (alignObjectBool)
            {
                //ModelVisual3D m = new ModelVisual3D();
                //m.Content = vmodel.Content.Clone();

                //view1.Children.Clear();
                //view1.Children.Add(m);
                alignObjectBool = false;
            }
            else
            {
                alignObjectBool = true;
                Model3DGroup d = (Model3DGroup)vm.CurrentModel;

                if (d != null)
                {
                    Rect3D r = d.Bounds;
                    RotateTransform3D rt = new RotateTransform3D();
                    Console.WriteLine("rect3D:" + r.X + "," + r.Y + "," + r.Z);
                    Console.WriteLine("rect3D:" + r.SizeX + "," + r.SizeY + "," + r.SizeZ);
                    //rect3D:105.253304,-5.142087,17.12525
                    //rect3D:64.387794,75.531301,78.013559

                    CombinedManipulator vModelManipulator = new CombinedManipulator();
                    vModelManipulator.Diameter = r.SizeX;
                    vmodel.Content = vm.CurrentModel;
                    vModelManipulator.Bind(vmodel);
                    //view1.Children.Clear();
                    view1.Children.Add(vModelManipulator);
                }
            }            
        }

        private void OpenContextMenu(FrameworkElement element)
        {
            if (element.ContextMenu != null)
            {
                element.ContextMenu.PlacementTarget = element;
                element.ContextMenu.IsOpen = true;
            }
        }

        private void GrabUpperMeshBtn_Click(object sender, RoutedEventArgs e)
        {
            busyIndicator.IsBusy = true;
            busyIndicator.BusyContent = "Processing Segmentation....Please Wait....";
            Thread.Sleep(1000);

            if (AddPlaneBtn.IsChecked == true)
            {
                vm.grabUpperMesh();
            }
            else
            {
                MessageBox.Show("Display the Plane, first!");
            }


            busyIndicator.IsBusy = false;
            //busyIndicator.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ShowHideRawVisualBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.showHideRawVisual();
        }

        private void ShowHideJawVisualBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.showHideJawVisual();
        }

        private void ShowHideTeethVisualBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.ShowHideTeethVisual(ShowHideTeethVisualBtn.IsChecked.Value);
        }
        private void ShowHideBraceVisualBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.ShowHideBraceVisual(ShowHideBraceVisualBtn.IsChecked.Value);
        }
        private void ShowHideWireVisualBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.ShowHideWireVisual(ShowHideWireVisualBtn.IsChecked.Value);
        }

        private void AddTeethBtn_Click(object sender, RoutedEventArgs e)
        {
            var pt = view1.FindNearestPoint(mousePoint);
            TeethVisual3D t = vm.addTeeth((Point3D)pt);
            //if (t != null) world.AddBody(t.getRigidBody());
        }

        private void AlignObjectBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.alignObject();
        }

        private void AddPlaneBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.addPlane();
        }

        private void view1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenContextMenu(this);
        }

        private void RemoveTeethBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.removeTeeth();
            enableRemoveTeethButton(false);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            vm.selectTeeth(0);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            vm.selectTeeth(1);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            vm.selectTeeth(2);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            vm.selectTeeth(3);
        }

        private void AddBraceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button b = sender as Button;
                if (b.Name == AddAllBraceBtn.Name)
                {
                    vm.addAllBrace();
                    return;
                }
            }
            
            var pt = view1.FindNearestPoint(mousePoint);
            if (pt == null)
            {
                if (vm.JawVisual.selectedGum != null && vm.JawVisual.selectedGum.selectedTeeth != null)
                {
                    Point3D p = vm.JawVisual.selectedGum.selectedTeeth.centroid();
                    BraceVisual3D b = vm.addBrace(p);
                }
                else
                {
                    MessageBox.Show("Please select a point");
                }
            }
            else
            {
                BraceVisual3D b = vm.addBrace((Point3D)pt);
            }
            //if (b != null) world.AddBody(b.getRigidBody());
        }

        private void RemoveBraceBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.removeBrace();
        }
        
        private void TeethMarkerBtn_Click(object sender, RoutedEventArgs e)
        {
            teethMarkerBool = !teethMarkerBool;
            displayHelp("Slices");
            if (TeethMarkerBtn.IsChecked == false)
            {
                busyIndicator.IsBusy  = true;
                Thread.Sleep(1000);
                vm.manualSegment(points, vectors);
                busyIndicator.IsBusy = false;
            }
            
            points = null;
            vectors = null;
        }


        private void ShowVerticesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ShowVerticesBtn.IsChecked == true)
            {

                Console.WriteLine("show vertices");
            }
            else
            {
                Console.WriteLine("hide vertices");
            }
        }

        private void displayHelp(String title)
        {
            // Load and display the RTF file.
            Uri uri = new Uri("pack://application:,,,/Helps/"+title+"Text.rtf");
            StreamResourceInfo info = Application.GetResourceStream(uri);
            using (StreamReader txtReader = new StreamReader(info.Stream))
            {
                _txtHelpContent.Text = txtReader.ReadToEnd();
                try
                {
                    BitmapImage bitmap = new BitmapImage(new Uri("pack://application:,,,/Helps/" + title + ".png"));
                    if (bitmap != null)
                    {
                        Paragraph para = new Paragraph();
                        para.Inlines.Add(new Run("Image "));
                        fld.Blocks.Add(para);

                        // Add an image
                        para = new Paragraph();
                        Image image = new Image();
                        image.Source = bitmap;
                        Figure figure = new Figure();
                        figure.Width = new FigureLength(200);
                        BlockUIContainer container = new BlockUIContainer(image);
                        figure.Blocks.Add(container);
                        para.Inlines.Add(figure);
                        fld.Blocks.Add(para);
                    }
                }
                catch (Exception e)
                {}
            }
        }

        private void MakeWireBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button b = sender as Button;
                if (b.Name == MakeAllWireBtn.Name)
                {
                    vm.drawWires();
                    displayHelp("WireDrawed");
                    return;
                }
            }
            
            if (MakeWireBtn.IsChecked == true)
            {
                //display help/guide to select braces
                displayHelp("Wire");
            }
            else
            {
                //draw wire
                if (bracesModel != null)
                {
                    vm.drawWire(bracesModel);
                    displayHelp("WireDrawed");
                }
            }

            //reset the bracesModel list
            bracesModel = null;

        }

        public void reset()
        {
            chartPanel.Visibility = System.Windows.Visibility.Hidden;
        }

        private void loadTeethNumberToChart()
        {
            chartPanel.EllipseCentreX = (350 / 2.5) ;
            chartPanel.EllipseCentreY = (350 / 2.5) ;
            chartPanel.EllipseWidth = 120;
            chartPanel.EllipseHeight = 140;

            chartPanel.Background = Brushes.AliceBlue;

            for (var i = 1; i < 33; i++)
            {
                BitmapImage bitmap = new BitmapImage(new Uri("pack://application:,,,/Icons/teeth16.jpg"));
                ImageToggleButton button = new ImageToggleButton(bitmap, "" + (chartPanel.Children.Count + 1));

                button.Click += new RoutedEventHandler(button_Click);
                button.Checked += new RoutedEventHandler(button_Checked);
                button.Unchecked += new RoutedEventHandler(button_Checked);

                chartPanel.Children.Add(button);
            }
            reset();
        }
 
        void button_Checked(object sender, RoutedEventArgs e)
        {
            var btn = sender as ImageToggleButton;
            if (btn.IsChecked == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri("pack://application:,,,/Icons/teeth16s.jpg"));
                Image img = new Image();
                img.Source = bitmap;
                btn.Image= img;
            }
            else
            {
                BitmapImage bitmap = new BitmapImage(new Uri("pack://application:,,,/Icons/teeth16.jpg"));
                Image img = new Image();
                img.Source = bitmap;
                btn.Image = img;
            }

        }

        private void resetTeethChart()
        {
            //set FALSE another button checked
            foreach (var b in chartPanel.Children)
            {
                if (b is ImageToggleButton)
                {
                    ((ImageToggleButton)b).IsChecked = false;
                }
            }
        }
        void button_Click(object sender, RoutedEventArgs e)
        {
            var btn = e.OriginalSource as ImageToggleButton;
            //set FALSE another button checked
            foreach (var b in chartPanel.Children)
            {
                if (b.Equals(btn))
                {
                    continue;
                }
                if (b is ImageToggleButton)
                {
                    ((ImageToggleButton)b).IsChecked = false;
                }
            }
            if (btn.IsChecked == true)
            {
                //should be false
                int p = 0; 
                int.TryParse(btn.Text.Text, out p);
                TeethVisual3D teeth = vm.selectTeeth(p);
                //teeth.showHideManipulator();
                teeth.displayManipulator();
                showTeethProperty(teeth);
            }
            //MessageBox.Show(""+e.ToString());
        }

        private void selectTeethChart(String id)
        {
            //String id = teeth.Id;
            Match mt = Regex.Match(id, @"teeth\d\d");
            if(mt != null && mt.Length > 0)
                id = mt.Value.Substring("teeth".Length, 2);
            int p = 0;
            int.TryParse(id, out p);
            foreach (var b in chartPanel.Children)
            {                
                if (b is ImageToggleButton)
                {
                    var btn = (ImageToggleButton)b;
                    int pp = 0;
                    int.TryParse(btn.Text.Text, out pp);
                    if(pp == p) ((ImageToggleButton)b).IsChecked = true;
                    else ((ImageToggleButton)b).IsChecked = false;
                }
            }
            
            vm.selectTeeth(p);
        }
		
		 //added by Achie
        private void MeasurementLineBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MeasurementLineBtn.IsChecked == true)
            {
                AutoMeasurementLineBtn.IsChecked = false;
                measurementForm.clear();
                measurementForm.Show();
                measurementForm.TopMost = true;
                measurementForm.AddColumnsManualMeasurement();
            }
            else
            {
                measurementForm.Hide();
            }
        }

        private void AutoMeasurementLineBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AutoMeasurementLineBtn.IsChecked == true)
            {
                MeasurementLineBtn.IsChecked = false;
                measurementForm.clear();
                measurementForm.Show();
                measurementForm.TopMost = true;
            }
            else
            {
                measurementForm.Hide(); 
            }
        }

        internal void createLine(Point3D p1, Point3D p2)
        {

            var group = new JawVisual3D(p1, p2);
            view1.Children.Add(group);
            mpoints.Clear();
        }

         private void SearchPatient_Click(object sender, RoutedEventArgs e)
         {
             SearchPatient a = new SearchPatient();
             a.Show();
         }

         private void Patient_Click(object sender, RoutedEventArgs e)
         {
             PatientRecordForm a = new PatientRecordForm();
             a.Show();
         }
    }

}
