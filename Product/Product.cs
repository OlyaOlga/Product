using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    /// <summary>
    /// This class contains main method. Represent  interface for reading/writing
    /// data from/to file.
    /// </summary>
    public class Product
    {
        private const string dataFile = "data.txt";
        private const string resFile = "res.txt";

        public static void Main()
        {
            try
            {
                var str = File.Open(resFile, FileMode.OpenOrCreate);
                str.SetLength(0);
                str.Close();
                List<IRequirement> userStoryRequirements = new List<IRequirement>();
                List<IRequirement> prototypeRequirement = new List<IRequirement>();
                List<IRequirement> specificationRequirements = new List<IRequirement>();
                string[] dataFileByLines = File.ReadAllLines(dataFile);
                int userStorySize;
                int prototypeSize;
                int specificationSize;

                string[] sizes = dataFileByLines[0].Split(' ');
                int.TryParse(sizes[0], out userStorySize);
                int.TryParse(sizes[1], out prototypeSize);
                int.TryParse(sizes[2], out specificationSize);

                int totalSize = userStorySize + prototypeSize + specificationSize;

                for (int i = 1; i <= userStorySize; ++i)
                {
                    try
                    {
                        userStoryRequirements.Add(new UserStory(dataFileByLines[i]));
                    }
                    catch (Exception obj)
                    {
                        Console.WriteLine(obj.Message);
                    }
                }

                for (int i = userStorySize + 1; i <= userStorySize + prototypeSize; ++i)
                {
                    try
                    {
                        prototypeRequirement.Add(new Prototype(dataFileByLines[i]));
                    }
                    catch (Exception obj)
                    {
                        Console.WriteLine(obj.Message);
                    }
                }

                for (int i = userStorySize + prototypeSize + 1; i <= totalSize; ++i)
                {
                    try
                    {
                        specificationRequirements.Add(new Specification(dataFileByLines[i]));
                    }
                    catch (Exception obj)
                    {
                        Console.WriteLine(obj.Message);
                    }
                }

                var sortedUserStory = userStoryRequirements.OrderBy(i => (i as UserStory).ProductName).ToList();
                var sortedPrototype = prototypeRequirement.OrderByDescending(i => (i as Prototype).CurrentMeasurement).ThenByDescending(i => (i as Prototype).Size).ToList();
                var sortedSpecification = specificationRequirements.OrderBy(i => (i as Specification).ProductName).ToList();

                Console.WriteLine("Users stories sorted by name:");
                Print(userStoryRequirements);

                Console.WriteLine("Prototypes sorted by size: ");
                Print(prototypeRequirement);

                Console.WriteLine("Specifications sorted by name:");
                Print(specificationRequirements);

                AddPrototypesToSpecification(specificationRequirements, userStoryRequirements, prototypeRequirement);
            }
            catch (Exception obj)
            {
                Console.WriteLine(obj.Message);
            }

            Console.ReadKey();
        }

        public static void Print(List<IRequirement> lst)
        {
            foreach (var item in lst)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
        }

        public static void AddPrototypesToSpecification(List<IRequirement> specificationInInterface, List<IRequirement> userStoriesInInterface, List<IRequirement> prototypesInInterface)
        {
            var specifications = specificationInInterface.ConvertAll(i => (Specification)i);
            var prototypes = prototypesInInterface.ConvertAll(i => (Prototype)i);
            var userStories = userStoriesInInterface.ConvertAll(i => (UserStory)i);

            AddUserStoriesToSpecification(specifications, userStories);
            AddPrototypesToSpecification(specifications, prototypes);
            foreach (var item in specifications)
            {
                item.WriteRequirement(resFile);
            }
        }

        private static void AddPrototypesToSpecification(List<Specification> specifications, List<Prototype> prototypes)
        {
            var chosenPrototypes =
                from i in specifications
                from k in prototypes
                where i.ProductName == k.Name.ProductName
                select new { Name = i.ProductName, Prototypes = k };
            var groupping =
                (from i in chosenPrototypes
                 group i by i.Name
                    into groupedPrototypes
                 select new
                 {
                     Key = groupedPrototypes.Key,
                     Values =
                         from j in groupedPrototypes
                         select j.Prototypes
                 }).ToList();

            foreach (var groupItem in groupping)
            {
                foreach (Specification specificationItem in specifications)
                {
                    if (specificationItem.ProductName == groupItem.Key)
                    {
                        var listOfPrototypes = groupItem.Values.ToList();
                        foreach (Prototype prototypeItem in listOfPrototypes)
                        {
                            specificationItem.AddPrototype(prototypeItem);
                        }
                    }
                }
            }
        }

        private static void AddUserStoriesToSpecification(List<Specification> specifications, List<UserStory> userStories)
        {
            var chosenUserStories =
                from i in specifications
                from k in userStories
                where i.ProductName == k.ProductName
                select new { Name = i.ProductName, UserStories = k };
            var groupping =
                (from i in chosenUserStories
                 group i by i.Name
                    into groupedPrototypes
                 select new
                 {
                     Key = groupedPrototypes.Key,
                     Values =
                         from j in groupedPrototypes
                         select j.UserStories
                 }).ToList();

            for (int i = 0; i < groupping.Count; ++i)
            {
                for (int j = 0; j < specifications.Count; ++j)
                {
                    if (specifications[j].ProductName == groupping[i].Key)
                    {
                        var listOfUserStories = groupping[i].Values.ToList();
                        for (int k = 0; k < listOfUserStories.Count; ++k)
                        {
                            specifications[j].AddUserStory(listOfUserStories[k]);
                        }
                    }
                }
            }
        }
    }
}
