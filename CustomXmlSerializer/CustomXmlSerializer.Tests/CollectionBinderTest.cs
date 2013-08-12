using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomXmlSerializer.Tests.Models;
using System.Xml.Linq;

namespace CustomXmlSerializer.Tests
{
    [TestClass]
    public class CollectionBinderTest
    {

        private static User _sampleUser;

        [ClassInitialize]
        public static void ClassInitialize(TestContext ctx)
        {
            _sampleUser = SampleDataGenerator.GenerateSampleUser();
        }

        [TestMethod]
        public void CorrectCollectionBindingWithoutBuilder()
        {
            var dbBinder = new XmlBuilder<User>("u");
            dbBinder.BindCollection(e => e.Teachers, "ts", "t");
            var generatedElement = dbBinder.GenerateXml(_sampleUser);

            var correctElement = new XElement("u", 
                new XElement("ts", 
                    new XElement("t", _sampleUser.Teachers[0]),
                    new XElement("t", _sampleUser.Teachers[1])));

            Assert.AreEqual(correctElement.ToString(), generatedElement.ToString());
        }

        [TestMethod]
        public void CorrectCollectionBindingWithBuilder()
        {
            var tBuilder = new XmlBuilder<Teacher>("t");
            tBuilder.BindAttribute(e => e.FirstName, "name");

            var dbBuilder = new XmlBuilder<User>("u");
            dbBuilder.BindCollection(e => e.Teachers, "ts", tBuilder);

            var generatedElement = dbBuilder.GenerateXml(_sampleUser);

            var correctElement = new XElement("u",
                new XElement("ts",
                    new XElement("t", new XAttribute("name", _sampleUser.Teachers[0].FirstName)),
                    new XElement("t", new XAttribute("name", _sampleUser.Teachers[1].FirstName))));

            Assert.AreEqual(correctElement.ToString(), generatedElement.ToString());
        }
    }
}
