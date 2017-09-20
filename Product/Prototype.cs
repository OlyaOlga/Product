using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    class Prototype:
        IRequirement,
        ICloneable,
        IComparable<Prototype>
    {
        public enum Measurement
        {
            Byte,
            Kilobyte,
            Megabyte,
            Gigabyte
        }

        public class ImageName
        {
            public string ProductName { get; set; }
            public string PrototypeName { get; set; }
            public Format FormatName { get; set; }
            public enum Format
            {
                Jpg,
                Png,
                Gif
            }

            public ImageName()
            {
            }
            public ImageName(string productName, string prototypeName, Format formatName)
            {
                productName = ProductName;
                prototypeName = PrototypeName;
                formatName = FormatName;
            }
            public override string ToString()
            {
                return $"{ProductName}{PrototypeName}.{FormatName}";
            }
        }


        private ImageName _name;
        private int _size;
        private Measurement _measurement;
        public Measurement CurrentMeasurement {
            get
            {
                return _measurement;
                
            }
            set
            {
                _measurement = value;
            }
        }

        public ImageName Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

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

        public void ReadRequirement(string element)
        {
            string[] data = element.Split(' ');
            Name.ProductName = data[0];
            Name.PrototypeName = data[1];
            if (int.TryParse(data[2], out _size) == false)
            {
                throw new ArgumentException("Wrong data!");
            }
            if (int.TryParse(data[3], out _size) == false)
            {
                throw new ArgumentException("Wrong data!");
            }
            int currentMesurement;
            if (int.TryParse(data[4], out currentMesurement) == false)
            {
                throw new ArgumentException("Wrong data!");
            }

            CurrentMeasurement = (Measurement) currentMesurement;
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
    }
}
