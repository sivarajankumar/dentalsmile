using System;
using System.IO;
using System.Windows.Media.Media3D;

namespace smileUp
{
    public static class SmileModelImporter 
    {
        public static ModelVisual3D Load(string path)
        {
            if (path == null)
            {
                return null;
            }
            ModelVisual3D model= new ModelVisual3D();
            string ext = Path.GetExtension(path).ToLower();
            switch (ext)
            {
                case ".obj":
                    {
                        var r = new SmileObjReader();
                        model = r.Read(path);
                        break;
                    }

                case ".objz":
                    {
                        var r = new SmileObjReader();
                        model = r.ReadZ(path);
                        break;
                    }                

                default:
                    throw new InvalidOperationException("File format not supported.");
            }
           return model;
        }
    }
}
