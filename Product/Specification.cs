using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    public class Specification :
        IRequirement,
        ICloneable,
        IComparable<Specification>
    {
        private string productName;
        private List<UserStory> userStories;
        private List<Prototype> prototypes;

        public Specification(string name)
        {
            userStories = new List<UserStory>();
            prototypes = new List<Prototype>();
            ReadRequirement(name);
        }

        public Specification()
        {
            userStories = new List<UserStory>();
            prototypes = new List<Prototype>();
        }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public List<UserStory> UserStories
        {
            get
            {
                return userStories;
            }
        }

        public List<Prototype> Prototypes
        {
            get
            {
                return prototypes;
            }
        }

        public void AddUserStory(UserStory obj)
        {
            userStories.Add(obj);
        }

        public void AddPrototype(Prototype obj)
        {
            prototypes.Add(obj);
        }

        public void ReadRequirement(string element)
        {
            string[] data = element.Split(',');
            ProductName = data[0];
            string[] currentUserStories = data[1].Split('/');
            string[] currentPrototype = data[2].Split('/');
            for (int i = 0; i < currentUserStories.Length; ++i)
            {
                UserStory current = new UserStory(currentUserStories[i]);
                userStories.Add(current);
            }

            for (int i = 0; i < currentPrototype.Length; ++i)
            {
                Prototype current = new Prototype(currentPrototype[i]);
                prototypes.Add(current);
            }
        }

        public void WriteRequirement(string fileName)
        {
            using (StreamWriter writer = File.AppendText(fileName))
            {
                writer.WriteLine(ProductName);
                writer.WriteLine("User Stories");
                foreach (var item in userStories)
                {
                    writer.WriteLine(item);
                }

                writer.WriteLine("Prototypes");
                foreach (var item in prototypes)
                {
                    writer.WriteLine(item);
                }

                writer.WriteLine();
            }
        }

        public object Clone()
        {
            Specification res = new Specification(ProductName);
            for (int i = 0; i < userStories.Count; ++i)
            {
                res.userStories[i] = (UserStory)userStories[i].Clone();
            }

            for (int i = 0; i < prototypes.Count; ++i)
            {
                res.prototypes[i] = (Prototype)prototypes[i].Clone();
            }

            return res;
        }

        public int CompareTo(Specification other)
        {
            return ProductName.CompareTo(other.ProductName);
        }

        public override string ToString()
        {
            string res;
            res = ProductName;
            res += '\n';
            foreach (var i in userStories)
            {
                res += i.ToString();
                res += '\n';
            }

            foreach (var i in prototypes)
            {
                res += i.ToString();
                res += '\n';
            }

            return res;
        }
    }
}
