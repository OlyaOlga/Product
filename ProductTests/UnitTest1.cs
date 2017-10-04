using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Product;

namespace ProductTests
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void TestUserStoryReadRequirement()
        {
            UserStory userStory = new UserStory();
            string elementToParse = "pr1 us1 aaa";
            userStory.ReadRequirement(elementToParse);
            UserStory expectedUserStory = new UserStory("pr1", "us1", "aaa");
            Assert.AreEqual(userStory.ToString(), expectedUserStory.ToString());
        }

        [TestMethod]
        public void TestUserStoryClone()
        {
            UserStory item = new UserStory("pr1", "us1", "aaa");
            UserStory clonedItem = (item.Clone() as UserStory);
            Assert.AreNotSame(item, clonedItem);
        }

        [TestMethod]
        public void TestPrototypenReadRequirement()
        {
            Prototype prototype = new Prototype();
            string elementToParse = "prod2 us2 0 500 1";
            prototype.ReadRequirement(elementToParse);
            Prototype.ImageName.Format format = 0;

            Prototype.ImageName imgName = new Prototype.ImageName("prod2","us2", format);
            Prototype.Measurement mes = (Prototype.Measurement)1;
            Prototype expectedPrototype = new Prototype(imgName, 500, mes);
            Assert.AreEqual(expectedPrototype.ToString(), prototype.ToString());
        }

        [TestMethod]
        public void TestPrototypeClone()
        {
            Prototype item = new Prototype("product2 us2 0 550 1");
            Prototype clonedItem = (item.Clone() as Prototype);
            Assert.AreNotSame(item, clonedItem);
        }

        [TestMethod]
        public void TestSpecificationReadRequirement()
        {
            Specification spec = new Specification();
            spec.ReadRequirement("prod2,prot1 story1 info,prod5 user2 1 555 0");
            Specification expectedSpecification = new Specification();
            expectedSpecification.ProductName = "prod2";
            Prototype prototypeToAdd= new Prototype("prod5 user2 1 555 0");
            expectedSpecification.AddPrototype(prototypeToAdd);
            UserStory story = new UserStory("prot1 story1 info");
            expectedSpecification.AddUserStory(story);
            Assert.AreEqual(expectedSpecification.ToString(), spec.ToString());
        }

        [TestMethod]
        public void TestSpecificationClone()
        {
            Specification item = new Specification("prod2,prot1 story1 info,prod5 user2 1 555 0");
            Specification clonedItem = (item.Clone() as Specification);
            Assert.AreNotSame(item, clonedItem);
        }
    }
}
