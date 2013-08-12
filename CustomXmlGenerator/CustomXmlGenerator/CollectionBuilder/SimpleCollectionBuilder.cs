using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using XmlCreator.Exceptions;

namespace CustomXmlGenerator.CollectionBuider
{
    internal class SimpleCollectionBuilder : CollectionBuilderBase
    {
        public string ItemName { get; set; }

        public override XElement GetCollectionXml(object objectToSerialize)
        {
            var doc = new XDocument(new XElement(CollectionName));
            var root = doc.Root;

            var collection = objectToSerialize as IEnumerable;
            if (collection == null)
                throw new CollectionParseException("Object binded as collection have to implement IEnumerable interface");

            var enumerator = collection.GetEnumerator();

            while (enumerator.MoveNext())
            {
                root.Add(new XElement(ItemName, enumerator.Current));
            }

            return root;
        }
    }
}
