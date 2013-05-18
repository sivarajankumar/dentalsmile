/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl
 * Copyright (c) 2010, Matthias Plasch
 * All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Parsley.Core.Extensions;
using MathNet.Numerics.LinearAlgebra;
using Emgu.CV.Structure;
using System.IO;

namespace Parsley
{
    public partial class ScanningSlide : FrameGrabberSlide
    {

        #region inisialisasi variabel

        private Parsley.Draw3D.PointCloud _pointcloud;
        private Core.DensePixelGrid<uint> _pixel_point_ids;
        bool _take_texture_image;
        bool _clear_points;
        bool _update_positioner_transformation;
        Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> _texture_image;
        bool _start_scanning = false; //tambahan fuad

        #endregion

        public ScanningSlide(Context c)
            : base(c)
        {
            this.InitializeComponent();
            _pointcloud = new Parsley.Draw3D.PointCloud(); //variabel penyimpan data 3d, format x,y,z,r,g,b
            _pixel_point_ids = new Parsley.Core.DensePixelGrid<uint>();
            lock (Context.Viewer)
            {
                Context.Viewer.Add(_pointcloud);
            }
        }


        protected override void OnSlidingIn(SlickInterface.SlidingEventArgs e) // hanya untuk animasi tampilan saja
        {
            lock (Context.Viewer)
            {
                Context.Viewer.SetupPerspectiveProjection(
                  Core.BuildingBlocks.Perspective.FromCamera(Context.Setup.Camera, 1.0, 5000).ToInterop()
                );
                Context.Viewer.LookAt(
                  new double[] { 0, 0, 0 },
                  new double[] { 0, 0, 400 },
                  new double[] { 0, 1, 0 }
                );
            }
            base.OnSlidingIn(e);
        }

        private ScanningSlide()
            : base(null)
        {
            InitializeComponent();
        }

        protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img)
        {

            #region take texture
            if (_take_texture_image) //even handler jika mengambil texture image
            {
                _take_texture_image = false;
                _texture_image = img.Copy();
                lock (Context.Viewer)
                {
                    UpdateAllColors();
                }
            }
            #endregion

            #region clear point
            if (_clear_points)
            { //even handler jika menghapus point 3d yg sdh ada
                _clear_points = false;
                _pixel_point_ids.Reset();
                Context.Setup.ScanWorkflow.Reset();
                _pointcloud.ClearPoints();
            }
            #endregion

            #region update positioner
            // Update the transformation between positioner coordinate system and camera coordinate system
            if (_update_positioner_transformation)
            {
                _update_positioner_transformation = false;
                Context.Setup.Positioner.UpdateTransformation(Context.Setup.Camera);
                _pixel_point_ids.Reset();
                Context.Setup.ScanWorkflow.Reset();
            }
            #endregion

            #region penyesuaian proporsi pixel dengan frame size
            if (Context.Setup.Camera.FrameSize != _pixel_point_ids.Size)
            {
                _pixel_point_ids.Size = Context.Setup.Camera.FrameSize;
            }
            #endregion

            List<Vector> points; //array penyimpan koordinat  //sepertinya ini yang out of range
            List<System.Drawing.Point> pixels;

            #region start scanning
            if (_start_scanning)
            {
                //Console.Write(Context.Setup.ScanWorkflow.Process(Context.Setup, img, out points, out pixels));

                if (Context.Setup.ScanWorkflow.Process(Context.Setup, img, out points, out pixels)) //Jika syarat2 dalam scanworkflow.process terpenuhi maka akan ditambahkan pointcloudnya.
                {
                    Console.WriteLine(pixels.Count);
                    lock (Context.Viewer)
                    {                     
                        UpdatePoints(points, pixels); //memanggil fungsi UpdatePoints untuk update model 3d setiap waktu, tanpa perduli terdeteksi ada nilai baru atau tidak
                    }
                    foreach (System.Drawing.Point p in pixels)
                    {
                        img[p.Y, p.X] = new Bgr(Color.Green);
                    }
                }
            }

            #endregion
        }

        private void UpdateAllColors()
        {
            for (int i = 0; i < _pixel_point_ids.PixelData.Length; ++i)
            {
                System.Drawing.Point p = Core.IndexHelper.PixelFromArrayIndex(i, _pixel_point_ids.Size);
                //we currently use image coordinate system as reference
                // Note: 0 is used as not-set marker
                uint id = _pixel_point_ids.PixelData[i];
                if (id > 0)
                {
                    double[] color = GetPixelColor(ref p);
                    _pointcloud.UpdateColor(id - 1, color);
                }
            }
        }

        private void UpdatePoints(List<Vector> points, List<System.Drawing.Point> pixels)
        {
            for (int i = 0; i < points.Count; ++i)
            {
                //Console.WriteLine("point{0}:{1}", i,points[i]);
                //Console.WriteLine("pixels{0}:{1}", i,pixels[i]);
                System.Drawing.Point pixel = pixels[i];
                //System.Drawing.Point rel_p = Core.IndexHelper.MakeRelative(pixel, Context.Setup.ScanWorkflow.ROI);
                uint id = _pixel_point_ids[pixel];
                if (id > 0)
                { // we use default value as not-set
                    // Update point
                    _pointcloud.UpdatePoint(id - 1, points[i].ToInterop());
                }
                else
                {
                    id = _pointcloud.AddPoint(points[i].ToInterop(), GetPixelColor(ref pixel));
                    _pixel_point_ids[pixel] = id + 1; // 0 is used as not-set marker
                }
            }
        }

        private double[] GetPixelColor(ref System.Drawing.Point pixel)
        {
            if (_texture_image != null)
            {
                Bgr bgr = _texture_image[pixel.Y, pixel.X];
                return bgr.ToInterop();
            }
            else
            {
                // default color
                return new double[] { 0.7, 0.7, 255, 1.0 };
            }
        }

        /// <summary>
        /// Read pointcloud file saved as *csv. 
        /// load_points = true -> load pointcloud to viewer
        /// </summary>
        /// <param name="load_points"></param>
        /// <param name="filename"></param>
        /// <param name="i"></param>
        /// <param name="points"></param>
        private void Read_CSV(bool load_points, string filename, out int i, out List<Vector> points)
        {
            points = new List<Vector>();
            i = 0;
            int[] index = new int[6];
            double[] value = new double[6];
            double[] coordinat = new double[3];
            double[] color = new double[4];

            try
            {
                using (CsvFileReader read = new CsvFileReader(filename))
                {
                    CsvRow row = new CsvRow();
                    while (read.ReadRow(row))
                    {
                        foreach (string s in row)
                        {  
                            index[0] = s.IndexOf(' ');
                            string result = s.Substring(0, index[0]);
                            value[0] = Convert.ToDouble(result);

                            for (int j = 1; j < 5; j++)
                            {
                                index[j] = s.IndexOf(' ', index[j - 1] + 1);
                                result = s.Substring(index[j - 1] + 1, index[j] - index[j - 1]);
                                value[j] = Convert.ToDouble(result);
                            }

                            index[5] = index[4] + 1;
                            result = s.Substring(index[5], s.Count() - index[5]);
                            value[5] = Convert.ToDouble(result);

                         
                            for (int j = 0; j < 3; j++)
                            {
                                coordinat[j] = value[j];
                                color[j] = value[j + 3];
                                color[3] = 1.0;
                            }

                            i++;
                            Vector vector = new Vector(coordinat);
                            points.Add(vector);    

                            try
                            {
                                if(load_points)
                                    _pointcloud.AddPoint(coordinat, color); //menampilkan titik pointcloud ke viewer 3D satu persatu
                            }
                            catch
                            {
                                MessageBox.Show("Failed to upload.System Busy, try again");
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Failed to upload. File is being used by another application");
            }
            
        }


        /// <summary>
        /// save point to *ply with same name as *.csv
        /// , fill parameter "to_ply" with filename *.csv
        /// </summary>
        /// <param name="to_ply"></param>
        private void Write_PLY(string to_ply)
        {
            string filename = to_ply.Replace(".csv", ".ply");
            List<Vector> points = new List<Vector>();
            int i = 0;
            double[] coordinat = new double[3];
            //Console.WriteLine(to_ply);
            using (CsvFileWriter writer = new CsvFileWriter(filename))
            {
                uint point_count = _pointcloud.NumberOfPoints();
                for (uint y = 0; y < point_count; y++)
                {
                    coordinat = (_pointcloud.ReturnPointAtIndex(y));
                    Vector vector = new Vector(coordinat);
                    points.Add(vector); 
                }

                i = Convert.ToInt16(point_count);
                int index = 0;
                CsvRow row = new CsvRow();
                string header = "ply\nformat ascii 1.0\nelement vertex " + i + "\nproperty float x\nproperty float y\nproperty float z\nend_header";
                row.Add(header);
                writer.WriteRow(row);
                for (int k = 0; k < i; k++)
                {
                    row = new CsvRow();
                    string s = String.Format("{0}", points[k]);
                    s = s.Replace(",", " ");
                    index = s.IndexOf("]");
                    String data = s.Substring(1, index - 1);

                    //for (int j = 1; j < 2; j++)
                    //{
                    //    index[j] = s.IndexOf('[', index[j - 1] + 1);
                    //    Console.WriteLine(index[j]);
                    //}

                    row.Add(data);
                    writer.WriteRow(row);
                }
                Console.WriteLine(i);
            }
        }

        /// <summary>
        /// Class to write data to a CSV file
        /// </summary>
        public class CsvFileWriter : StreamWriter
        {
            public CsvFileWriter(Stream stream)
                : base(stream)
            {
            }

            public CsvFileWriter(string filename)
                : base(filename)
            {
            }

            /// <summary>
            /// Writes a single row to a CSV file.
            /// </summary>
            /// <param name="row">The row to be written</param>
            public void WriteRow(CsvRow row)
            {
                StringBuilder builder = new StringBuilder();
                bool firstColumn = true;
                foreach (string value in row)
                {
                    // Add separator if this isn't the first value
                    if (!firstColumn)
                        builder.Append(',');
                    // Implement special handling for values that contain comma or quote
                    // Enclose in quotes and double up any double quotes
                    if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                        builder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                    else
                        builder.Append(value);
                    firstColumn = false;
                }
                row.LineText = builder.ToString();
                WriteLine(row.LineText);
            }
        }

        /// <summary>
        /// Class to read data from a CSV file
        /// </summary>
        public class CsvFileReader : StreamReader
        {
            public CsvFileReader(Stream stream)
                : base(stream)
            {
            }

            public CsvFileReader(string filename)
                : base(filename)
            {
            }

            /// <summary>
            /// Reads a row of data from a CSV file
            /// </summary>
            /// <param name="row"></param>
            /// <returns></returns>
            public bool ReadRow(CsvRow row)
            {
                row.LineText = ReadLine();
                if (String.IsNullOrEmpty(row.LineText))
                    return false;

                int pos = 0;
                int rows = 0;

                while (pos < row.LineText.Length)
                {
                    string value;

                    // Special handling for quoted field
                    if (row.LineText[pos] == '"')
                    {
                        // Skip initial quote
                        pos++;

                        // Parse quoted value
                        int start = pos;
                        while (pos < row.LineText.Length)
                        {
                            // Test for quote character
                            if (row.LineText[pos] == '"')
                            {
                                // Found one
                                pos++;

                                // If two quotes together, keep one
                                // Otherwise, indicates end of value
                                if (pos >= row.LineText.Length || row.LineText[pos] != '"')
                                {
                                    pos--;
                                    break;
                                }
                            }
                            pos++;
                        }
                        value = row.LineText.Substring(start, pos - start);
                        value = value.Replace("\"\"", "\"");
                    }
                    else
                    {
                        // Parse unquoted value
                        int start = pos;
                        while (pos < row.LineText.Length && row.LineText[pos] != ',')
                            pos++;
                        value = row.LineText.Substring(start, pos - start);
                    }

                    // Add field to list
                    if (rows < row.Count)
                        row[rows] = value;
                    else
                        row.Add(value);
                    rows++;

                    // Eat up to and including next comma
                    while (pos < row.LineText.Length && row.LineText[pos] != ',')
                        pos++;
                    if (pos < row.LineText.Length)
                        pos++;
                }
                // Delete any unused items
                while (row.Count > rows)
                    row.RemoveAt(rows);

                // Return true if any columns read
                return (row.Count > 0);
            }
        }

        /// <summary>
        /// Class to store one CSV row
        /// </summary>
        public class CsvRow : List<string>
        {
            public string LineText { get; set; }
        }


        private void _btn_take_texture_image_Click(object sender, EventArgs e)
        {
            _take_texture_image = true;
        }

        private void _btn_clear_points_Click(object sender, EventArgs e)
        {
            _clear_points = true;
        }

        private void _btn_save_points_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _pointcloud.SaveAsCSV(saveFileDialog1.FileName, " ");
                Write_PLY(saveFileDialog1.FileName);
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btn_update_transformation_Click(object sender, EventArgs e)
        {
            _update_positioner_transformation = true;
        }

        private void StartScanning_Click(object sender, EventArgs e)
        {
            _start_scanning = !_start_scanning;
            if (_start_scanning)
                StartScanning.Text = "Stop Scanning";
            else
                StartScanning.Text = "Start Scanning";
        }

        private void _btn_load_Click(object sender, EventArgs e)
        {
            List<Vector> points = new List<Vector>();
            int i = 0;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Read_CSV(true,openFileDialog1.FileName, out i, out points);
                }
                catch { MessageBox.Show("Failed, try again"); }
            }
            //Console.WriteLine(_pointcloud.NumberOfPoints());
            //_pointcloud.PointCloudToArray(
        }
    }
}
