
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;

namespace smileUp
{
    public class Smile
    {
        public const string SCANNED_PATH = "pack://application:,,,/Resources/ScannedFiles/";
        public const string MANIPULATED_PATH = "pack://application:,,,/Resources/ManipulatedFiles/";
        public const string PHOTOS_PATH = "pack://application:,,,/Resources/Photos/";

        public const int NONE = 0;
        public const int SCANNING = 1;
        public const int MANIPULATION = 2;
        public const int PRINTING = 3;

        public static string DATE_FORMAT = "yyyy-MM-dd";
        public static string TIME_FORMAT = "hh:i:s";
        public static string LONG_DATE_FORMAT = DATE_FORMAT + " " + TIME_FORMAT;

        public bool writeFile(int type, string filename)
        {
            return false;
        }

        public bool deletFile(int type, string filename)
        {
            return false;
        }
    }
}