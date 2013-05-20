﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using QuantumConcepts.Common.Extensions;

namespace smileUp.Stl
{
    public class Vertex : IEquatable<Vertex>
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vertex() { }

        public Vertex(float x, float y, float z)
            : this()
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static Vertex Read(StreamReader reader)
        {
            const string regex = @"\s*(facet normal|vertex)\s+(?<X>[^\s]+)\s+(?<Y>[^\s]+)\s+(?<Z>[^\s]+)";

            string data = null;
            float x, y, z;
            Match match = null;
            NumberStyles numberStyle = (NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);

            if (reader == null)
                return null;

            data = reader.ReadLine();

            if (data == null)
                return null;

            match = Regex.Match(data, regex, RegexOptions.IgnoreCase);

            if (!match.Success)
                return null;

            if (!float.TryParse(match.Groups["X"].Value, numberStyle, CultureInfo.CurrentCulture, out x))
                //throw new FormatException(string.Format("Could not parse X coordinate \"{0}\" as a decimal.",match.Groups["X"]));
            throw new FormatException("Could not parse X coordinate \"{0}\" as a decimal.".FormatString(match.Groups["X"]));

            if (!float.TryParse(match.Groups["Y"].Value, numberStyle, CultureInfo.CurrentCulture, out y))
                //throw new FormatException(string.Format("Could not parse Y coordinate \"{0}\" as a decimal.",match.Groups["Y"]));
            throw new FormatException("Could not parse Y coordinate \"{0}\" as a decimal.".FormatString(match.Groups["Y"]));

            if (!float.TryParse(match.Groups["Z"].Value, numberStyle, CultureInfo.CurrentCulture, out z))
                //throw new FormatException(string.Format("Could not parse Z coordinate \"{0}\" as a decimal.",match.Groups["Z"]));
            throw new FormatException("Could not parse Z coordinate \"{0}\" as a decimal.".FormatString(match.Groups["Z"]));

            return new Vertex()
            {
                X = x,
                Y = y,
                Z = z
            };
        }

        public static Vertex Read(BinaryReader reader)
        {
            if (reader == null)
                return null;

            byte[] data = new byte[sizeof(float) * 3];
            int bytesRead = reader.Read(data, 0, data.Length);

            //If no bytes are read then we're at the end of the stream.
            if (bytesRead == 0)
                return null;
            else if (bytesRead != data.Length)
                //throw new FormatException(string.Format("Could not convert the binary data to a vertex. Expected 12 bytes but found {0}.",bytesRead));
            throw new FormatException("Could not convert the binary data to a vertex. Expected 12 bytes but found {0}.".FormatString(bytesRead));

            return new Vertex()
            {
                X = (float)BitConverter.ToDouble(data, 0),
                Y = (float)BitConverter.ToDouble(data, 4),
                Z = (float)BitConverter.ToDouble(data, 8)
            };
        }

        public void Write(StreamWriter writer)
        {
            writer.WriteLine("\t\t\t{0}".FormatString(this.ToString()));
            //writer.WriteLine(string.Format("\t\t\t{0}",this.ToString()));
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.X);
            writer.Write(this.Y);
            writer.Write(this.Z);
        }

        public override string ToString()
        {
            return "vertex {0} {1} {2}".FormatString(this.X, this.Y, this.Z);
            //return string.Format("vertex {0} {1} {2}", this.X, this.Y, this.Z);
        }

        public bool Equals(Vertex other)
        {
            return (this.X.Equals(other.X)
                    && this.Y.Equals(other.Y)
                    && this.Z.Equals(other.Z));
        }
    }
}
