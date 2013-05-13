
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;

namespace smileUp.DataModel
{
    public class SmileFile
    {
        private string fileName = "default.obj";
        private string screenshot = "default.png";
        private int type = Smile.REGISTERED; // 0= registered, 1=scanning, 2=manipulation, 3=printing

        public Patient Patient { get; set; }
        public string Id { get; set; }
        public string FileName { get { return fileName; } set { this.fileName = value; } }
        public string Description { get; set; }
        public string Screenshot { get { return screenshot; } set { this.screenshot = value; } }
        public int Type { get { return type; } set { this.type = value; } }
        public Phase Phase { get; set; }
        public string RefId { get; set; }

        
        private string ScannedFile 
        { 
            get {
                return Smile.SCANNED_PATH + "/" + FileName;
            } 
        }

        private string ManipulatedFile 
        {
            get
            {
                return Smile.MANIPULATED_PATH + "/" + FileName;
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
                return Smile.SCANNED_PATH + "/" + Screenshot;
            }
        }

        private string ManipulatedScreenshot
        {
            get
            {
                return Smile.MANIPULATED_PATH + "/" + Screenshot;
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