// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Helix 3D Toolkit">
//   http://helixtoolkit.codeplex.com, license: Ms-PL
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Microsoft.Win32;
using smileUp.DataModel;

namespace smileUp
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Media;
    using smileUp.DataModel;
    using smileUp.Forms;

    public class VisualElement
    {
        public IEnumerable<VisualElement> Children
        {
            get
            {
                var mv = element as ModelVisual3D;
                if (mv != null)
                {
                    if (mv.Content!=null)
                        yield return new VisualElement(mv.Content);
                    foreach (var mc in mv.Children)
                        yield return new VisualElement(mc);
                }
                
                var mg = element as Model3DGroup;
                if (mg != null)
                    foreach (var mc in mg.Children) yield return new VisualElement(mc);

                var gm = element as GeometryModel3D;
                if (gm != null)
                {
                    yield return new VisualElement(gm.Geometry);
                }

                //int n = VisualTreeHelper.GetChildrenCount(element);
                //for (int i = 0; i < n; i++)
                //    yield return new VisualElement(VisualTreeHelper.GetChild(element, i));
                foreach (DependencyObject c in LogicalTreeHelper.GetChildren(element))
                    yield return new VisualElement(c);
            }
        }

        public string TypeName
        {
            get
            {
                return element.GetType().Name;
            }
        }
        public Brush Brush
        {
            get
            {
                if (element.GetType() == typeof(ModelVisual3D))
                    return Brushes.Orange;
                if (element.GetType() == typeof(GeometryModel3D))
                    return Brushes.Green;
                if (element.GetType() == typeof(Model3DGroup))
                    return Brushes.Blue;
                if (element.GetType() == typeof(Visual3D))
                    return Brushes.Gray;
                if (element.GetType() == typeof(Model3D))
                    return Brushes.Black;
                return null;
            }
        }
        public override string ToString()
        {
            return element.GetType().ToString();
        }

        private DependencyObject element;

        public VisualElement(DependencyObject e)
        {
            element = e;
        }

    }

    public class MainViewModel : Observable
    {
        public ICommand FileOpenCommand { get; set; }
        public ICommand FileOpenRawCommand { get; set; }
        public ICommand FileExportCommand { get; set; }
        public ICommand FileExportRawCommand { get; set; }
        public ICommand FileExitCommand { get; set; }
        public ICommand HelpAboutCommand { get; set; }
        public ICommand ViewZoomExtentsCommand { get; set; }
        public ICommand EditCopyXamlCommand { get; set; }
        public ICommand EditClearAreaCommand { get; set; }
        public ICommand FileExportStlCommand { get; set; }

        //private const string OpenFileFilter = "3D model files (*.3ds;*.obj;*.lwo;*.stl)|*.3ds;*.obj;*.objz;*.lwo;*.stl";
        private const string OpenFileFilter = "3D model files (*.obj)|*.obj";
        private const string TitleFormatString = "3D model viewer - {0}";

        private string _currentModelPath;
        public string CurrentModelPath
        {
            get { return _currentModelPath; }
            set { _currentModelPath = value; RaisePropertyChanged("CurrentModelPath"); }
        }

        private string _applicationTitle;
        public string ApplicationTitle
        {
            get { return _applicationTitle; }
            set { _applicationTitle = value; RaisePropertyChanged("ApplicationTitle"); }
        }

        private double expansion;

        public double Expansion
        {
            get { return expansion; }
            set
            {
                if (expansion != value)
                {
                    expansion = value;
                    RaisePropertyChanged("Expansion");
                }
            }
        }

        private RawVisual3D _rawVisual;

        private JawVisual3D _jawVisual;

        private ModelVisual3D _rootVisual;

        MainWindow window;

        public Patient Patient { get; set; }

        public Treatment Treatment { get; set; }

        public SmileFile SmileFile { get; set; }

        string rawFilename = "RAW.obj";
        string jawFilename = "JAW.obj";

        DentalSmileDB DB;
        App app;
        
        private IFileDialogService FileDialogService;
        public IHelixViewport3D HelixView { get; set; }

        public Dictionary<Model3D, BaseMarker> ModelToBaseMarker{ get; private set; }
        public Dictionary<Model3D, Teeth> ModelToTeeth { get; private set; }
        public Dictionary<Model3D, Material> OriginalMaterial { get; private set; }
        private Model3D _currentModel;

        public Model3D CurrentModel
        {
            get { return _currentModel; }
            set { _currentModel = value; RaisePropertyChanged("CurrentModel"); }
        }

        public ModelVisual3D RootVisual
        {
            get { return _rootVisual; }
            set { _rootVisual = value; RaisePropertyChanged("RootVisual"); }
        }

        public RawVisual3D RawVisual
        {
            get { return _rawVisual; }
            set { _rawVisual = value; RaisePropertyChanged("RawVisual"); }
        }

        public JawVisual3D JawVisual
        {
            get { return _jawVisual; }
            set { _jawVisual = value; RaisePropertyChanged("JawVisual"); }
        }

        public List<VisualElement> Elements { get; set; }

//        public MainViewModel(IFileDialogService fds, HelixViewport3D hv, ModelVisual3D rootModel)
        public MainViewModel(IFileDialogService fds, HelixViewport3D hv, MainWindow window)
        {
            Expansion = 1;
            FileDialogService = fds;
            HelixView = hv;
            FileOpenCommand = new DelegateCommand(FileOpen);
            FileOpenRawCommand = new DelegateCommand(FileOpenRaw);
            FileExportCommand = new DelegateCommand(FileExport);
            FileExportRawCommand = new DelegateCommand(FileExportRaw);
            FileExitCommand = new DelegateCommand(FileExit);
            ViewZoomExtentsCommand = new DelegateCommand(ViewZoomExtents);
            EditCopyXamlCommand = new DelegateCommand(CopyXaml);
            EditClearAreaCommand = new DelegateCommand(ClearArea);
            FileExportStlCommand = new DelegateCommand(StlFileExport);            


            ApplicationTitle = "Dental Smile - 3D Viewer";

            ModelToBaseMarker = new Dictionary<Model3D, BaseMarker>();
            OriginalMaterial = new Dictionary<Model3D, Material>();

            //Elements = new List<VisualElement>();
            //foreach (var c in hv.Children) Elements.Add(new VisualElement(c));

            DB = new DentalSmileDB();
            Treatment = new Treatment();
            SmileFile = new SmileFile();
            Patient = new Patient();
            JawVisual = new JawVisual3D(Patient);
            RootVisual = window.vmodel;
            app = Application.Current as App;

            RootVisual.Children.Add(JawVisual);
            this.window = window;

        }

        //INTEGRATION
        public MainViewModel(IFileDialogService fds, HelixViewport3D hv, Treatment treatment, SmileFile file, bool duplicate, MainWindow window)
        {
            Expansion = 1;
            FileDialogService = fds;
            HelixView = hv;
            FileOpenCommand = new DelegateCommand(FileOpen);
            FileOpenRawCommand = new DelegateCommand(FileOpenRaw);
            //FileExportCommand = new DelegateCommand(FileExport);
            FileExportCommand = new DelegateCommand(ConfirmDirectFileExport);            
            FileExportRawCommand = new DelegateCommand(FileExportRaw);
            FileExitCommand = new DelegateCommand(FileExit);
            ViewZoomExtentsCommand = new DelegateCommand(ViewZoomExtents);
            EditCopyXamlCommand = new DelegateCommand(CopyXaml);
            EditClearAreaCommand = new DelegateCommand(ClearArea);
            FileExportStlCommand = new DelegateCommand(StlFileExport);            

            
            ApplicationTitle = "Dental Smile - 3D Viewer";

            ModelToBaseMarker = new Dictionary<Model3D, BaseMarker>();
            OriginalMaterial = new Dictionary<Model3D, Material>();

            //Elements = new List<VisualElement>();
            //foreach (var c in hv.Children) Elements.Add(new VisualElement(c));

            this.window = window;
            RootVisual = window.vmodel;
            
            handleManipulationData(treatment, file, duplicate);
            
            //JawVisual = new JawVisual3D(Patient);
            //RootVisual.Children.Add(JawVisual);
        }

        private void handleManipulationData(Treatment treatment, SmileFile file, bool duplicate)
        {
            DB = new DentalSmileDB();
            app = Application.Current as App;
            if (App.patient == null) App.patient = new Patient();
            Patient = App.patient;

            if (treatment == null)
            {
                Treatment = new Treatment();
            }
            else
            {
                Treatment = treatment;
                if (duplicate)
                {
                    Treatment.Id = null;
                }
                Treatment.RefId = treatment.Id;
            }
            //if (file != null) SmileFile = file;

            if (file.Type == Smile.MANIPULATION)
            {
                //Load Jaw
                LoadJawFile(file.GetFile);
            }
            else if (file.Type == Smile.SCANNING)
            {
                //Load Raw
                LoadRawFile(file.GetFile);
            }

            SmileFile = new SmileFile();
            SmileFile.RefId = file.Id;

            ViewZoomExtents();
        }

        
        private static GeometryModel3D CreateBaseMarkerModel3D(BaseMarker v)
        {
            const double size = 0.98;
            var m = new GeometryModel3D();
            var mb = new MeshBuilder();
            mb.AddBox(new Point3D(0, 0, 0), size, size, size);
            m.Geometry = mb.ToMesh();
            m.Material = MaterialHelper.CreateMaterial(v.Colour);
            m.Transform = new TranslateTransform3D(v.Position.X, v.Position.Y, v.Position.Z);
            return m;
        }


        private void FileExit()
        {
            App.Current.Shutdown();
        }

        private void ConfirmDirectFileExport()
        {
            NotesConfirmationForm n = new NotesConfirmationForm();
            n.ShowDialog();
            if (n != null && n.Notes != null)
            {
                DirectFileExport(n.Notes);
                SaveMedicalResume(n.MedicalResume, n.Notes);

                n.Close();
                MessageBox.Show("Data saved successfully.", "Successfully");
            }
        }

        /**
         * http://www.deltasblog.co.uk/code-snippets/c-resizing-a-bitmap-image/
         */
        private static System.Drawing.Bitmap ResizeBitmap(System.Drawing.Bitmap sourceBMP, int width, int height)
        {
            System.Drawing.Bitmap result = new System.Drawing.Bitmap(width, height);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }

        /**
         * http://www.smokycogs.com/blog/proportionately-resizing-a-bitmap-in-c-sharp/
         */
        public static System.Drawing.Bitmap ResizeBitmapProportionally(System.Drawing.Bitmap sourceBitmap, int maxWidth, int maxHeight)
        {
            // original dimensions  
            int width = sourceBitmap.Width;
            int height = sourceBitmap.Height;

            // Find the longest and shortest dimentions  
            int longestDimension = (width > height) ? width : height;
            int shortestDimension = (width < height) ? width : height;

            double factor = ((double)longestDimension) / (double)shortestDimension;

            // Set width as max  
            double newWidth = maxWidth;
            double newHeight = maxWidth / factor;

            //If height is actually greater, then we reset it to use height instead of width  
            if (width < height)
            {
                newWidth = maxHeight / factor;
                newHeight = maxHeight;
            }

            // Create new Bitmap at new dimensions based on original bitmap  
            System.Drawing.Bitmap resizedBitmap = new System.Drawing.Bitmap((int)newWidth, (int)newHeight);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage((System.Drawing.Image)resizedBitmap))
                g.DrawImage(sourceBitmap, 0, 0, (int)newWidth, (int)newHeight);
            return resizedBitmap;
        }

        public void StlFileExport()
        {
            var path = FileDialogService.SaveFileDialog(null, null, "StereoLithography Files (*.stl)|*.stl;", ".stl");
            if (path == null)
                return;
            var e = new SmileStlExporter(path);
            //e.Export(CurrentModel);
            if (window.ShowHideJawVisualBtn.IsChecked.Value)
            {
                if (window.ShowHideWireVisualBtn.IsChecked.Value && window.ShowHideBraceVisualBtn.IsChecked.Value && window.ShowHideTeethVisualBtn.IsChecked.Value)
                {
                    ((SmileStlExporter)e).Export(JawVisual, Patient);
                }
                else
                {
                    if (window.ShowHideTeethVisualBtn.IsChecked.Value)
                    {
                        ((SmileStlExporter)e).Export(JawVisual.tc, Patient);
                    }

                    if (window.ShowHideBraceVisualBtn.IsChecked.Value)
                    {
                        ((SmileStlExporter)e).Export(JawVisual.bc, Patient);
                    }

                    if (window.ShowHideWireVisualBtn.IsChecked.Value)
                    {
                        ((SmileStlExporter)e).Export(JawVisual.wc, Patient);
                    }
                }
                
                ((SmileStlExporter)e).WriteExport();
                
                ((SmileStlExporter)e).Close();

                if(((SmileStlExporter)e).Finish)
                    MessageBox.Show("Data Exported to STL file format at "+path+".", "STL Export - Successfully");
            }            
        }


        private void DirectFileExport(string notes)
        {
            bool newFile = false;
            SmileFile.Patient = Patient;
            SmileFile.Description = notes;
            SmileFile.Type = Smile.MANIPULATION;
            if (SmileFile.Id == null)
            {
                SmileFile.Id = DB.getSmileFileNewId(Patient.Id);
                newFile = true;
            }

            jawFilename = "JAW" + SmileFile.Id + ".obj";
            
            var path = Smile.MANIPULATED_PATH + jawFilename;
            var e = new SmileObjExporter(path);
            //e.Export(CurrentModel);
            ((SmileObjExporter)e).jawVisual = JawVisual;
            ((SmileObjExporter)e).Export(JawVisual, Patient);
            e.Close();

            string screenShot = "JAW" + SmileFile.Id + ".png";
            path = Smile.MANIPULATED_PATH + screenShot;
            HelixView.Export(path);

            //TODO save to table
            SmileFile.FileName = jawFilename;
            SmileFile.Screenshot = screenShot;

            Treatment.Phase = Smile.GetPhase(Smile.MANIPULATION);

            if (Treatment.Id != null)
            {
                DB.UpdateTreatment(Treatment);
            }
            else
            {
                Treatment.Id = DB.getTreatmentNewId(Patient.Id);
                Treatment.TreatmentDate = DateTime.Now;
                Treatment.TreatmentTime = DateTime.Now.ToString(Smile.TIME_FORMAT);
                DB.InsertTreatment(Treatment);
            }
            if (newFile)
            {
                DB.InsertFileInfo(SmileFile);
                DB.insertTreatmentFiles(Treatment, SmileFile);
            }
            else
            {
                DB.UpdateFileInfo(SmileFile);
            }
        }

        private void SaveMedicalResume(string resume, string description)
        {
            DB.insertTreatmentNotes(Treatment, resume, SmileFile, description);
        }

        private void DirectFileExportRaw()
        {
            var path = Smile.MANIPULATED_PATH + "RAW" + "SmileFile.Id" + ".obj";
            var e = new SmileObjExporter(path);
            //e.Export(CurrentModel);
            ((SmileObjExporter)e).rawVisual = RawVisual;
            ((SmileObjExporter)e).Export(RawVisual, Patient);
            e.Close();
        }

        private void FileExport()
        {
            var path = FileDialogService.SaveFileDialog(null, null, Exporters.Filter, ".png");
            if (path == null)
                return;
                //HelixView.Export(path);
            ///*
                        var ext = Path.GetExtension(path).ToLowerInvariant();
                        switch (ext)
                        {
                            case ".png":
                            case ".jpg":
                                HelixView.Export(path);
                                break;
                            case ".xaml":
                                {
                                    var e = new XamlExporter(path);
                                    e.Export(CurrentModel);
                                    e.Close();
                                    break;
                                }

                            case ".xml":
                                {
                                    var e = new KerkytheaExporter(path);
                                    e.Export(HelixView.Viewport);
                                    e.Close();
                                    break;
                                }
                            case ".obj":
                                {
                                    var patient = new Patient();
                                    var e = new SmileObjExporter(path);
                                    //e.Export(CurrentModel);
                                        ((SmileObjExporter)e).jawVisual = JawVisual;
                                        ((SmileObjExporter)e).Export(JawVisual, patient);
                                    e.Close();
                                    break;
                                }
                            case ".objz":
                                {
                                    var tmpPath = Path.ChangeExtension(path, ".obj");
                                     var e = new ObjExporter(tmpPath);
                                     e.Export(CurrentModel);
                                     e.Close();
                                    GZipHelper.Compress(tmpPath);
                                    break;
                                }
                            case ".x3d":
                                {
                                    var e = new X3DExporter(path);
                                    e.Export(CurrentModel);
                                    e.Close();
                                    break;
                                }
                        }
            //*/
        }

        private void FileExportRaw()
        {
            var path = FileDialogService.SaveFileDialog(null, null, Exporters.Filter, ".png");
            if (path == null)
                return;
            //HelixView.Export(path);
            ///*
            var ext = Path.GetExtension(path).ToLowerInvariant();
            switch (ext)
            {
                case ".png":
                case ".jpg":
                    HelixView.Export(path);
                    break;
                case ".xaml":
                    {
                        var e = new XamlExporter(path);
                        e.Export(CurrentModel);
                        e.Close();
                        break;
                    }

                case ".xml":
                    {
                        var e = new KerkytheaExporter(path);
                        e.Export(HelixView.Viewport);
                        e.Close();
                        break;
                    }
                case ".obj":
                    {
                        var patient = new Patient();
                        var e = new SmileObjExporter(path);
                        //e.Export(CurrentModel);
                            ((SmileObjExporter)e).rawVisual = RawVisual;
                            ((SmileObjExporter)e).Export(RawVisual, patient);
                        e.Close();
                        break;
                    }
                case ".objz":
                    {
                        var tmpPath = Path.ChangeExtension(path, ".obj");
                        var e = new ObjExporter(tmpPath);
                        e.Export(CurrentModel);
                        e.Close();
                        GZipHelper.Compress(tmpPath);
                        break;
                    }
                case ".x3d":
                    {
                        var e = new X3DExporter(path);
                        e.Export(CurrentModel);
                        e.Close();
                        break;
                    }
            }
            //*/
        }
        private void CopyXaml()
        {
            var rd = XamlExporter.WrapInResourceDictionary(_rootVisual);
            Clipboard.SetText(XamlHelper.GetXaml(rd));
        }

        private void ViewZoomExtents()
        {
            HelixView.ZoomExtents(500);
        }

        private void LoadRawFile(string CurrentModelPath)
        {        
            CurrentModel = ModelImporter.Load(CurrentModelPath);
            Model3DGroup group = new Model3DGroup();
            if (CurrentModel != null)
            {
                var mb = new MeshBuilder(false, false);
                Rect3D r = CurrentModel.Bounds;

                Model3DGroup g = (Model3DGroup)CurrentModel;
                foreach (GeometryModel3D gm in g.Children)
                {
                    MeshGeometry3D mesh = (MeshGeometry3D)gm.Geometry;
                    Point3DCollection ind = mesh.Positions;
                    for (int i = 0; i < ind.Count; i++)
                    {
                        var p0 = ind[i];

                        p0 = new Point3D(p0.X + (-(r.X + (r.SizeX / 2))), p0.Y + (-(r.Y + (r.SizeY / 2))), p0.Z + (-(r.Z + (r.SizeZ / 2))));

                        mb.Positions.Add(p0);
                    }

                    for (int i = 0; i < mesh.TriangleIndices.Count; i++)
                    {
                        mb.TriangleIndices.Add(mesh.TriangleIndices[i]);
                    }

                }
                var geom = new GeometryModel3D(mb.ToMesh(), MaterialHelper.CreateMaterial(Colors.BlueViolet));
                geom.BackMaterial = MaterialHelper.CreateMaterial(Colors.Chocolate);
                group.Children.Add(geom);

                CurrentModel = group;
                if (RawVisual == null) RawVisual = new RawVisual3D(RootVisual);
                RawVisual.Content = CurrentModel;
                RawVisual.ShowBoundingBox = true;
                RawVisual.showHideBoundingBox();
                RootVisual.Children.Add(RawVisual);
                smileMode = "RAW";
                ApplicationTitle = String.Format(TitleFormatString, CurrentModelPath);
                HelixView.ZoomExtents(100);
            }
        }

        private void LoadJawFile(string CurrentModelPath)
        {
            JawVisual3D jv = null;
            jv = (JawVisual3D)SmileModelImporter.Load(CurrentModelPath);
            if (jv != null)
            {
                if(RootVisual.Children.Contains(JawVisual)) RootVisual.Children.Remove(JawVisual);
                JawVisual = null;
                JawVisual = jv;
                RootVisual.Children.Add(JawVisual);
                smileMode = "JAW";

                if (JawVisual.selectedGum != null)
                {
                    drawWires();
                }

                //if (!HelixView.Viewport.Children.Contains(RootVisual))                    HelixView.Viewport.Children.Add(RootVisual);
                window.chartPanel.Visibility = Visibility.Visible;
                ApplicationTitle = String.Format(TitleFormatString, CurrentModelPath);
            }

            HelixView.ZoomExtents(100);
        }

        private void FileOpen()
        {
            CurrentModelPath = FileDialogService.OpenFileDialog("models", null, OpenFileFilter, ".3ds");
#if !DEBUG
            try
            {
#endif
            /*
            CurrentModel = ModelImporter.Load(CurrentModelPath);
            Rect3D r = CurrentModel.Bounds;
            CurrentModel.Transform = new TranslateTransform3D(-(r.X + (r.SizeX / 2)), -(r.Y + (r.SizeY / 2)), -(r.Z + (r.SizeZ / 2)));
            CurrentModel.Freeze();
            ModelVisual3D rawVisual = null;
            if (rawVisual == null) rawVisual = new RawVisual3D();
            rawVisual.Content = CurrentModel;
            _rootVisual.Children.Add(rawVisual);

            //*/
            LoadJawFile(CurrentModelPath);
#if !DEBUG
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
#endif
        }



        private void FileOpenRaw()
        {
            CurrentModelPath = FileDialogService.OpenFileDialog("models", null, OpenFileFilter, ".3ds");
#if !DEBUG
            try
            {
#endif
            ///*
            CurrentModel = ModelImporter.Load(CurrentModelPath);
            Model3DGroup group = new Model3DGroup();
            if (CurrentModel != null)
            {
                var mb = new MeshBuilder(false, false);
                Rect3D r = CurrentModel.Bounds;

                Model3DGroup g = (Model3DGroup)CurrentModel;
                foreach (GeometryModel3D gm in g.Children)
                {
                    MeshGeometry3D mesh = (MeshGeometry3D)gm.Geometry;
                    Point3DCollection ind = mesh.Positions;
                    for (int i = 0; i < ind.Count; i++)
                    {
                        var p0 = ind[i];

                        p0 = new Point3D(p0.X + (-(r.X + (r.SizeX / 2))), p0.Y + (-(r.Y + (r.SizeY / 2))), p0.Z + (-(r.Z + (r.SizeZ / 2))));

                        mb.Positions.Add(p0);
                    }

                    for (int i = 0; i < mesh.TriangleIndices.Count; i++)
                    {
                        mb.TriangleIndices.Add(mesh.TriangleIndices[i]);
                    }

                }
                var geom = new GeometryModel3D(mb.ToMesh(), MaterialHelper.CreateMaterial(Colors.BlueViolet));
                geom.BackMaterial = MaterialHelper.CreateMaterial(Colors.Chocolate);
                group.Children.Add(geom);


                /*
                //ADD COUNTOUR
                var segments = MeshGeometryHelper.GetContourSegments(geom.Geometry as MeshGeometry3D, new Point3D(0,0,0), new Vector3D(0,0,1));
                foreach (var contour in MeshGeometryHelper.CombineSegments(segments, 1e-6))
                {
                    if (contour.Count == 0)
                        continue;
                    HelixView.Viewport.Children.Add(new TubeVisual3D { Diameter = 0.03, Path = new Point3DCollection(contour), Fill = Brushes.Green });
                }
                 //*/ 
                /*
                //FIND EDGES
                var edges = MeshGeometryHelper.FindEdges(geom.Geometry as MeshGeometry3D);
                for (int i = 0; i < edges.Count; i+=2)
                {
                    Point3DCollection points = new Point3DCollection();
                    MeshGeometry3D ms = geom.Geometry as MeshGeometry3D;
                    Point3D p0 = ms.Positions[edges[i]];
                    Point3D p1 = ms.Positions[edges[i+1]];
                    points.Add(p0);
                    points.Add(p1);
                    HelixView.Viewport.Children.Add(new TubeVisual3D { Diameter = 0.03,  Path = new Point3DCollection(points), Fill = Brushes.Black});
                }
                //*/
                CurrentModel = group;
                //Transform3DGroup tgroup = new Transform3DGroup();
                //tgroup.Children.Add(new TranslateTransform3D(-(r.X + (r.SizeX / 2)), -(r.Y + (r.SizeY / 2)), -(r.Z + (r.SizeZ / 2))));
                //tgroup.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0,1,0), 45)));
                //CurrentModel.Transform = tgroup;
                //CurrentModel.Freeze();
                if (RawVisual == null) RawVisual = new RawVisual3D(RootVisual);
                //RawVisual.Content = CurrentModel;
                RawVisual.Content = CurrentModel;
                RawVisual.ShowBoundingBox = true;
                RawVisual.showHideBoundingBox();
                RootVisual.Children.Add(RawVisual);
                smileMode = "RAW";

                //if(!HelixView.Viewport.Children.Contains(RootVisual)) HelixView.Viewport.Children.Add(RootVisual);

                //ModelVisual3D v = new ModelVisual3D();
                //v.Content = CurrentModel;
                //RootVisual.Children.Add(v);

                //*/
                /*
                List<Point3D> pts = new List<Point3D>();
                pts.Add(new Point3D(30.4960694492679, 5.52461072334257, 9.44661196022868));
                pts.Add(new Point3D(28.0998337010248, 6.66945199865937, 14.4798151385421));
                pts.Add(new Point3D(28.2102015086476, -0.126599907551222, 19.0159875772706));
                pts.Add(new Point3D(29.7392389441408, -4.1789883548182, 14.8532955485325));
                pts.Add(new Point3D(28.4974026994517, -2.89939719282193, 8.04165108434179));
                pts.Add(new Point3D(24.1236797131164, 1.10693238332609, 6.46814289839299));

                for (var i = 0; i < pts.Count; i++)
                {
                    BoxVisual3D b = new BoxVisual3D();
                    b.Center = pts[i];
                    b.Length = 2;
                    b.Height = 2;
                    b.Width = 2;
                    HelixView.Viewport.Children.Add(b);
                }
                */

                ApplicationTitle = String.Format(TitleFormatString, CurrentModelPath);
                HelixView.ZoomExtents(0);
            }
#if !DEBUG
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
#endif
        }

        /// <summary>
        /// Finds the centroid of the specified face.
        /// </summary>
        /// <param name="vertices">
        /// The vertices.
        /// </param>
        /// <returns>
        /// The centroid. 
        /// </returns>
        public Point3D FindCentroid(IList<Point3D> vertices)
        {
            double x = 0;
            double y = 0;
            double z = 0;
            int n = vertices.Count;
            for (int i = 0; i < n; i++)
            {
                x += vertices[i].X;
                y += vertices[i].Y;
                z += vertices[i].Z;
            }

            if (n > 0)
            {
                x /= n;
                y /= n;
                z /= n;
            }

            return new Point3D(x, y, z);
        }

        internal void showHideRawVisual(bool b)
        {
            if (RawVisual != null)
            {
                try
                {
                    if (b)
                        RootVisual.Children.Add(RawVisual);
                    else
                        RootVisual.Children.Remove(RawVisual);
                }
                catch (Exception e) { }

                /*if (RootVisual.Children.Contains(RawVisual))
                {
                    RootVisual.Children.Remove(RawVisual);
                }
                else
                {
                    RootVisual.Children.Add(RawVisual);
                }*/
            }
        }

        internal void showHideJawVisual(bool b)
        {
            if (JawVisual != null)
            {
                try
                {
                    if (b)
                        RootVisual.Children.Add(JawVisual);
                    else
                        RootVisual.Children.Remove(JawVisual);
                }
                catch (Exception e) { }

                /*
                if (RootVisual.Children.Contains(JawVisual))
                {
                    RootVisual.Children.Remove(JawVisual);
                }
                else
                {
                    RootVisual.Children.Add(JawVisual);
                }*/
            }
        }


        internal void cutMesh()
        {
            if (RawVisual != null)
            {
                GeometryModel3D gumModel = RawVisual.cutByPlane();
                if (JawVisual == null)
                {
                    Patient p = App.patient;
                    if (p == null) p = new Patient();
                    JawVisual = new JawVisual3D(p);
                }
                JawVisual.replaceGum(gumModel);
                MessageBox.Show("Processing is done.", "Cutting Mesh");
            }

        }
        internal void addPlane()
        {
            if(RawVisual != null)
                RawVisual.showHidePlane();

        }
        private void ClearArea()
        {
            RawVisual = null;
            JawVisual = null;
            RootVisual.Children.Clear();

            //((HelixViewport3D)HelixView).Children.Clear();
            //((HelixViewport3D)HelixView).Children.Add(RootVisual);

            window.reset();

        }


        internal void alignObject()
        {
            if (RawVisual != null)
                RawVisual.showHideManipulator();
        }

        internal TeethVisual3D addTeeth(Point3D p)
        {
            if (JawVisual != null)
                return JawVisual.addTeeth(p);

            return null;
        }

        internal void removeTeeth()
        {
            if (MessageBox.Show("Are you sure to remove the Teeth? This action can't be undone!", "Remove Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (JawVisual != null)
                {
                    JawVisual.removeTeeth();
                }
            }
        }

        internal void showHideManipulator(TeethVisual3D teeth)
        {
            JawVisual.selectedGum = teeth.Parent;
            JawVisual.selectedGum.selectedTeeth = teeth;

            //teeth.showHideManipulator();
            teeth.displayManipulator();
        }

        public string smileMode { get; set; }

        internal TeethVisual3D selectTeeth(int p)
        {
            TeethVisual3D r = null;
            if (JawVisual != null)
            {
                //r = JawVisual.selectTeeth(p);
                r = JawVisual.findTeeth(p);
            }
            return r;
        }

        internal BraceVisual3D addBrace(Point3D center)
        {
            if (JawVisual != null)
                return JawVisual.addBrace(center);

            return null;
        }

        internal void removeBrace()
        {
            if (MessageBox.Show("Are you sure to remove the Brace? This action can't be undone!", "Remove Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (JawVisual != null)
                {
                    JawVisual.removeBrace();
                }
            }
  
        }

        internal void showHideManipulator(BraceVisual3D brace)
        {
            JawVisual.selectedGum = brace.Parent.Parent;
            JawVisual.selectedGum.selectedTeeth = brace.Parent;

            //brace.showHideManipulator();
            brace.displayManipulator();
        }


        internal void showHideManipulator(GumVisual3D gum)
        {
            //gum.clearManipulator();
            gum.cleanManipulator();
        }

        internal void manualSegment(Point3DCollection points, Vector3DCollection vectors)
        {
            if (RawVisual != null)
            {
                List<GeometryModel3D> models = RawVisual.manualSegment(points, vectors);
                if (JawVisual != null && models.Count > 0)
                {
                    JawVisual.addTeeth(models);
                    //add points as archs
                    JawVisual.selectedGum.Archs = points;
                }
                MessageBox.Show("Processing is done.", "Manual Segmentation");
            }

        }

        internal void drawWire(List<BraceVisual3D> bracesModel)
        {
            if (JawVisual != null)
            {
                JawVisual.drawWire(bracesModel);
            }
        }

        internal void drawWires()
        {
            if (JawVisual != null)
            {
                JawVisual.drawWires();
            }
        }
        
        internal void updateTeethMap(string oldid, string newid)
        {
            JawVisual.updateTeethMap(oldid, newid);
        }

        internal void addAllBrace()
        {
            if (JawVisual.selectedGum != null)
            {
                //JawVisual.selectedGum.addAllBrace();
                JawVisual.selectedGum.addBraceToAllTooth();
            }
        }
        
        internal void ShowHideBraceVisual(bool f)
        {
            JawVisual.displayBraceContainer(f);
        }

        internal void ShowHideTeethVisual(bool f)
        {
            JawVisual.displayTeethContainer(f);
        }
        
        internal void ShowHideWireVisual(bool f)
        {
            JawVisual.displayWireContainer(f);
        }


        public void updateBraceLocation(string braceid, int oldLocation, int newValue)
        {
            JawVisual.updateBraceLocation(braceid, oldLocation, newValue);
        }


        internal void displayArchs(bool p)
        {
            JawVisual.displayArchs(p);
        }
    }

    public interface IFileDialogService
    {
        string OpenFileDialog(string initialDirectory, string defaultPath, string filter, string defaultExtension);
        string SaveFileDialog(string initialDirectory, string defaultPath, string filter, string defaultExtension);
    }

    public class FileDialogService : IFileDialogService
    {
        public string OpenFileDialog(string initialDirectory, string defaultPath, string filter, string defaultExtension)
        {
            var d = new OpenFileDialog();
            d.InitialDirectory = initialDirectory;
            d.FileName = defaultPath;
            d.Filter = filter;
            d.DefaultExt = defaultExtension;
            if (!d.ShowDialog().Value)
                return null;
            return d.FileName;
        }

        public string SaveFileDialog(string initialDirectory, string defaultPath, string filter, string defaultExtension)
        {
            var d = new SaveFileDialog();
            d.InitialDirectory = initialDirectory;
            d.FileName = defaultPath;
            d.Filter = filter;
            d.DefaultExt = defaultExtension;
            if (!d.ShowDialog().Value)
                return null;
            return d.FileName;
        }

    }
}