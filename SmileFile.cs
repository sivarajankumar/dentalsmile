
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;

namespace smileUp
{
    public class SmileFile
    {
        private string fileName = "default.obj";
        private string screenshot = "default.png";
        private int type = Smile.NONE; // 0= none, 1=scanning, 2=manipulation, 3=printing

        public Patient Patient { get; set; }
        public string Id { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string Screenshot { get; set; }
        public int Type { get; set; }

        
        private string ScannedFile 
        { 
            get {
                return Smile.SCANNED_PATH + "" + fileName;
            } 
        }

        private string ManipulatedFile 
        {
            get
            {
                return Smile.MANIPULATED_PATH + "" + fileName;
            }
        }

        public string GetFile
        {
            get
            {
                if (Type == Smile.SCANNING)
                    return ScannedFile;
                if (Type == Smile.MANIPULATION)
                    return ManipulatedFile;
                return null;
            }
        }
        private string ScannedScreenshot
        {
            get
            {
                return Smile.SCANNED_PATH + "" + screenshot;
            }
        }

        private string ManipulatedScreenshot
        {
            get
            {
                return Smile.MANIPULATED_PATH + "" + screenshot;
            }
        }

        public string GetScreenshot
        {
            get
            {
                if (Type == Smile.SCANNING)
                    return ScannedScreenshot;
                if (Type == Smile.MANIPULATION)
                    return ManipulatedScreenshot;
                return null;
            }
        }
    }
}