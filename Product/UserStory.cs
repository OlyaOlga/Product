using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    public class UserStory:
        IRequirement,
        ICloneable,
        IComparable<UserStory>
    {
        private string _productName;
        private string _story;
        private string _text;

        public string Story
        {
            get { return _story; }
            set { _story = value; }
        }

        private string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        public UserStory(string productName, string story, string text)
        {
            ProductName = productName;
            Story = story;
            Text = text;
        }

        public UserStory()
        {
        }

        public UserStory(string unparcedString)
        {
            ReadRequirement(unparcedString);
        }
        public void ReadRequirement(string element)// product, userStory, text
        {
            int fieldsQuantity = 3;
            string[] elementData = element.Split(' ');
            if (elementData.Length < fieldsQuantity)
            {
                throw new ArgumentException("Invalid quantity of entities in string");
            }
            ProductName = elementData[0];
            Story = elementData[1];
            Text = elementData[2];
        }

        public void WriteRequirement(string fileName)
        {
            using (StreamWriter writer = File.AppendText(fileName))
            {
                writer.WriteLine(ToString());
            }
        }

        public object Clone()
        {
            UserStory copy = new UserStory(ProductName, Story, Text);
            return copy;
        }

        public int CompareTo(UserStory other)
        {
            return ProductName.CompareTo(other.ProductName);
        }

        public override string ToString()
        {
            return $"ProductName: {ProductName} Story:{Story} Text:{Text}";
        }
    }
}
