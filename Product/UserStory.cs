using System;
using System.IO;

namespace Product
{
    /// <summary>
    /// Represent single requirement for product - product name and story
    /// </summary>
    public class UserStory :
        IRequirement,
        ICloneable,
        IComparable<UserStory>
    {
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

        public string Story { get; set; }

        public string ProductName { get; set; }

        private string Text { get; set; }

        /// <summary>
        /// Read UserStiry from single line
        /// </summary>
        /// <param name="element">Line to parse element</param>
        public void ReadRequirement(string element)
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

        public int CompareTo(UserStory other) => string.Compare(ProductName, other.ProductName, StringComparison.Ordinal);

        public override string ToString() => $"ProductName: {ProductName} Story:{Story} Text:{Text}";
    }
}
