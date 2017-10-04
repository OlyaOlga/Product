using System;
using System.IO;

namespace Product
{
    /// <summary>
    /// Represents prototype entities. Has product name, prototype name. image of prototype and
    /// measurement
    /// </summary>
    public class Prototype :
        IRequirement,
        ICloneable,
        IComparable<Prototype>
    {
        private int size;

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

        public Measurement CurrentMeasurement { get; set; }

        public ImageName Name { get; set; }

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
            var currentMesurement = CheckRequirementDataFormat(data);

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

        public override string ToString() => $"Name: {Name}, Size: {Size} {CurrentMeasurement.ToString()}";

        public object Clone()
        {
            ImageName name = new ImageName(Name.ProductName, Name.PrototypeName, Name.FormatName);
            Prototype cloned = new Prototype(name, Size, CurrentMeasurement);
            return cloned;
        }

        /// <summary>
        /// Try to parse data and mesurement.
        /// </summary>
        /// <param name="data">Data to parse size and measurement</param>
        /// <returns>Returns measurement volume</returns>
        /// <exception cref="ArgumentException">Data has incorect format</exception>
        private int CheckRequirementDataFormat(string[] data)
        {
            int currentMesurement;
            if (!int.TryParse(data[2], out size) ||
                !int.TryParse(data[3], out size) ||
                !int.TryParse(data[4], out currentMesurement))
            {
                throw new ArgumentException("Wrong data!");
            }

            return currentMesurement;
        }

        /// <summary>
        /// Represents name of image.
        /// Has information about product name, prototype name,
        /// system information about image format.
        /// </summary>
        public class ImageName
        {
            public ImageName()
            {
            }

            public ImageName(string productName, string prototypeName, Format formatName)
            {
                ProductName = productName;
                PrototypeName = prototypeName;
                FormatName = formatName;
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

            public override string ToString() => $"{ProductName}{PrototypeName}.{FormatName}";
        }
    }
}
