using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    public class Prototype :
        IRequirement,
        ICloneable,
        IComparable<Prototype>
    {
        private ImageName name;
        private int size;
        private Measurement measurement;

        public Prototype()
        {
            Name = new ImageName();
        }

        public Prototype(ImageName name, int size, Measurement measurement)
        {
            Name = name;
            Size = size;
            CurrentMeasurement = measurement;
        }

        public Prototype(string data)
        {
            Name = new ImageName();
            ReadRequirement(data);
        }

        /// <summary>
        /// Represent types of copmuter volume measeres
        /// </summary>
        public enum Measurement
        {
            /// <summary>
            /// Measures has 1e+0 bytes
            /// </summary>
            Byte,

            /// <summary>
            /// Measures has 1,0.24e+3 bytes
            /// </summary>
            Kilobyte,

            /// <summary>
            /// Measures has 1,0.24e+6 bytes
            /// </summary>
            Megabyte,

            /// <summary>
            /// Measures has 1,0.24e+9 bytes
            /// </summary>
            Gigabyte
        }

        public Measurement CurrentMeasurement
        {
            get
            {
                return measurement;
            }

            set
            {
                measurement = value;
            }
        }

        public ImageName Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public void ReadRequirement(string element)
        {
            string[] data = element.Split(' ');
            Name.ProductName = data[0];
            Name.PrototypeName = data[1];
            if (int.TryParse(data[2], out size) == false)
            {
                throw new ArgumentException("Wrong data!");
            }

            if (int.TryParse(data[3], out size) == false)
            {
                throw new ArgumentException("Wrong data!");
            }

            int currentMesurement;
            if (int.TryParse(data[4], out currentMesurement) == false)
            {
                throw new ArgumentException("Wrong data!");
            }

            CurrentMeasurement = (Measurement)currentMesurement;
        }

        public void WriteRequirement(string fileName)
        {
            using (StreamWriter writer = File.AppendText(fileName))
            {
                writer.WriteLine(ToString());
            }
        }

        public int CompareTo(Prototype other)
        {
            int res = CurrentMeasurement.CompareTo(other.CurrentMeasurement);
            if (res == 0)
            {
                res = Size.CompareTo(other.Size);
            }

            return res;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Size: {Size} {CurrentMeasurement.ToString()}";
        }

        public object Clone()
        {
            ImageName name = new ImageName(Name.ProductName, Name.PrototypeName, Name.FormatName);
            Prototype cloned = new Prototype(name, Size, CurrentMeasurement);
            return cloned;
        }

        public class ImageName
        {
            public ImageName()
            {
            }

            public ImageName(string productName, string prototypeName, Format formatName)
            {
                productName = ProductName;
                prototypeName = PrototypeName;
                formatName = FormatName;
            }

            public enum Format
            {
                /// <summary>
                /// Joint Photographic Experts Group
                /// </summary>
                Jpg,

                /// <summary>
                /// Portable Network Graphics
                /// </summary>
                Png,

                /// <summary>
                /// Graphics Interchange Format
                /// </summary>
                Gif
            }

            public string ProductName { get; set; }

            public string PrototypeName { get; set; }

            public Format FormatName { get; set; }

            public override string ToString()
            {
                return $"{ProductName}{PrototypeName}.{FormatName}";
            }
        }
    }
}
