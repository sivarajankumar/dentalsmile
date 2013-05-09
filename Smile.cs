
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;
using System.Collections.Generic;
using smileUp.DataModel;

namespace smileUp
{
    public class Smile
    {
        //public const string SCANNED_PATH = "pack://application:,,,/Resources/ScannedFiles/";
        //public const string MANIPULATED_PATH = "pack://application:,,,/Resources/ManipulatedFiles/";
        //public const string SCANNED_PATH = "I:\\opt\\apps\\3d\\dentalsmile\\Resources\\ScannedFiles\\";
        //public const string MANIPULATED_PATH = "I:\\opt\\apps\\3d\\dentalsmile\\Resources\\ManipulatedFiles\\";
        //public static string SCANNED_PATH = "D:\\dbaparm\\dwim\\private\\s2\\thesis\\dentalsmile\\Resources\\ScannedFiles\\";
        //public static string MANIPULATED_PATH = "D:\\dbaparm\\dwim\\private\\s2\\thesis\\dentalsmile\\Resources\\ManipulatedFiles\\";

        public static string SCANNED_PATH = Properties.Settings.Default.ScannedPath;
        public static string MANIPULATED_PATH = Properties.Settings.Default.ManipulatedPath;
        public static string PHOTOS_PATH = Properties.Settings.Default.PhotoPath;

        public static string DbHost = Properties.Settings.Default.DbHost;
        public static string DbPort = Properties.Settings.Default.DbPort;
        public static string DbUserId = Properties.Settings.Default.DbUserId;
        public static string DbPassword = Properties.Settings.Default.DbPassword;
        public static string DbDatabase = Properties.Settings.Default.DbDatabase;

        public static bool INSTALL = Properties.Settings.Default.InstallationMode;

        public const int NONE = -1;
        public const int REGISTERED = 0;
        public const int SCANNING = 1;
        public const int MANIPULATION = 2;
        public const int PRINTING = 3;

        public static string DISPLAY_DATE_FORMAT = "dd-MMM-yyyy";
        public static string DATE_FORMAT = "yyyy-MM-dd";
        public static string TIME_FORMAT = "hh:mm:s";
        public static string LONG_DATE_FORMAT = DATE_FORMAT + " " + TIME_FORMAT;
        public static string DATE_ID_FORMAT = "ddMMyy";

        public const int OUTERBRACE = 1;
        public const int INNERBRACE = 0;
        
        public const string TEETH = "teeth";
        public const string BRACE = "brace";

        public static bool writeFile(int type, string filename)
        {
            return false;
        }

        public static bool deletFile(int type, string filename)
        {
            return false;
        }

        public static List<Phase> GetPhases()
        {
            List<Phase> results = new List<Phase>();
            Phase p = new Phase();            
            p.Id = REGISTERED;
            p.Name = "REGISTERED";
            results.Add(p);
            
            p = new Phase();
            p.Id = SCANNING;
            p.Name = "SCANNING";
            results.Add(p);

            p = new Phase();
            p.Id = MANIPULATION;
            p.Name = "MANIPULATION";
            results.Add(p);

            p = new Phase();
            p.Id = PRINTING;
            p.Name = "PRINTING";
            results.Add(p);

            return results;
        }
        public static Phase GetPhase(int type)
        {
            Phase p = new Phase();
            if (type == REGISTERED)
            {
                p.Id = REGISTERED;
                p.Name = "REGISTERED";
            }

            if (type == SCANNING)
            {
                p.Id = SCANNING;
                p.Name = "SCANNING";
            }

            if (type == MANIPULATION)
            {
                p.Id = MANIPULATION;
                p.Name = "MANIPULATION";
            }
            
            if (type == PRINTING)
            {
                p.Id = PRINTING;
                p.Name = "PRINTING";
            }

            return p;
        }


    }


    public enum Gender
    {
        Male, Female
    }
}