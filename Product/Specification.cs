using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    /// <summary>
    /// Represent specification which consists of caption, list of UserStories and Prototypes (optional)
    /// </summary>
    public class Specification :
        IRequirement,
        ICloneable,
        IComparable<Specification>
    {
        public Specification(string name)
        {
            UserStories = new List<UserStory>();
            Prototypes = new List<Prototype>();
            ReadRequirement(name);
        }

        public Specification()
        {
            UserStories = new List<UserStory>();
            Prototypes = new List<Prototype>();
        }

        public string ProductName { get; set; }

        public List<UserStory> UserStories { get; }

        public List<Prototype> Prototypes { get; }

        public void AddUserStory(UserStory obj)
        {
            UserStories.Add(obj);
        }

        public void AddPrototype(Prototype obj)
        {
            Prototypes.Add(obj);
        }

        /// <summary>
        /// Parse element in order to read productn name, list of user stories and list of prototypes
        /// </summary>
        /// <param name="element">Line to prase element</param>
        public void ReadRequirement(string element)
        {
            string[] data = element.Split(',');
            ProductName = data[0];
            string[] currentUserStories = data[1].Split('/');
            string[] currentPrototype = data[2].Split('/');
            foreach (string userStory in currentUserStories)
            {
                UserStory current = new UserStory(userStory);
                UserStories.Add(current);
            }

            foreach (string prototype in currentPrototype)
            {
                Prototype current = new Prototype(prototype);
                Prototypes.Add(current);
            }
        }

        public void WriteRequirement(string fileName)
        {
            using (StreamWriter writer = File.AppendText(fileName))
            {
                writer.WriteLine(ProductName);
                writer.WriteLine("User Stories");
                foreach (var item in UserStories)
                {
                    writer.WriteLine(item);
                }

                writer.WriteLine("Prototypes");
                foreach (var item in Prototypes)
                {
                    writer.WriteLine(item);
                }

                writer.WriteLine();
            }
        }

        public object Clone()
        {
            Specification res = new Specification(ProductName);
            for (int i = 0; i < UserStories.Count; ++i)
            {
                res.UserStories[i] = (UserStory)UserStories[i].Clone();
            }

            for (int i = 0; i < Prototypes.Count; ++i)
            {
                res.Prototypes[i] = (Prototype)Prototypes[i].Clone();
            }

            return res;
        }

        public int CompareTo(Specification other) => string.Compare(ProductName, other.ProductName, StringComparison.Ordinal);

        public override string ToString()
        {
            var res = ProductName;
            res += '\n';
            foreach (var i in UserStories)
            {
                res += i.ToString();
                res += '\n';
            }

            foreach (var i in Prototypes)
            {
                res += i.ToString();
                res += '\n';
            }

            return res;
        }
    }
}
