using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomXmlSerializer.Tests.Models;
using System.Xml.Linq;
using CustomXmlSerializer;

namespace CustomXmlSerializer.Tests
{
    [TestClass]
    public class AttributesBinderTest
    {
        private static User _sampleUser;

        [ClassInitialize]
        public static void ClassInitialize(TestContext ctx)
        {
            _sampleUser = SampleDataGenerator.GenerateSampleUser();
        }

        [TestMethod]
        public void CorrectAttributeBinding()
        {
            var dbBinder = new XmlBuilder<User>("u");
            dbBinder.BindAttribute(e => e.FirstName, "fName");

            var xml = dbBinder.GenerateXml(_sampleUser);

            var correctXml = new XElement("u", new XAttribute("fName", "Kuba"));

            Assert.AreEqual(correctXml.ToString(), xml.ToString());
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void WrongAttributeBinding()
        {
            var dbBinder = new XmlBuilder<User>("u");
            dbBinder.BindAttribute(e => e, "fName");
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void OtherWrongAttributeBinding()
        {
            var dbBinder = new XmlBuilder<User>("u");
            dbBinder.BindAttribute(e => e.Class.Name, "fName");
        }

    }
}
