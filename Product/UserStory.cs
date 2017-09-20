using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    public class UserStory :
        IRequirement,
        ICloneable,
        IComparable<UserStory>
    {
        private string productName;
        private string story;
        private string text;

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

        public string Story
        {
            get { return story; }
            set { story = value; }
        }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private string Text // ????
        {
            get { return text; }
            set { text = value; }
        }

        public void ReadRequirement(string element) // product, userStory, text
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
