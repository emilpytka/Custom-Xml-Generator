using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomXmlGenerator.Tests.Models;
using System.Xml.Linq;

namespace CustomXmlGenerator.Tests
{
    [TestClass]
    public class TagBinderTest
    {

        private static User _sampleUser;

        [ClassInitialize]
        public static void ClassInitialize(TestContext ctx)
        {
            _sampleUser = SampleDataGenerator.GenerateSampleUser();
        }

        [TestMethod]
        public void CorrectTagBindingWithoutInnerBuilder()
        {
            var dbBinder = new XmlBuilder<User>("u");
            dbBinder.BindTag(e => e.Class, "c");
            var generatedElement = dbBinder.GenerateXml(_sampleUser);

            var correctElement = new XElement("u", new XElement("c", _sampleUser.Class));
            Assert.AreEqual(correctElement.ToString(), generatedElement.ToString());
        }

        [TestMethod]
        public void CorrectTagBindingWithInnerBuilder()
        {
            var classBinder = new XmlBuilder<SchoolClass>("sc");
            classBinder.BindAttribute(e => e.Name, "sname");

            var dbBinder = new XmlBuilder<User>("u");
            dbBinder.BindTag(e => e.Class, classBinder);
            var generatedElement = dbBinder.GenerateXml(_sampleUser);

            var correctElement = new XElement("u", new XElement("sc", new XAttribute("sname", _sampleUser.Class.Name)));

            Assert.AreEqual(correctElement.ToString(), generatedElement.ToString());
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void WrongTagBinding()
        {
            var dbBinder = new XmlBuilder<User>("u");
            dbBinder.BindTag(e => e, "fName");
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void OtherWrongBinding()
        {
            var dbBinder = new XmlBuilder<User>("u");
            dbBinder.BindTag(e => e.Class.Name, "fName");
        }
    }
}
