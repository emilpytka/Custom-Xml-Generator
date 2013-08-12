using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using XmlCreator.Exceptions;

namespace CustomXmlSerializer.CollectionBuider
{
    internal class CustomCollectionBuilder : CollectionBuilderBase
    {
        public IXmlBuilder InnerItemBuilder { get; set; }

        public CustomCollectionBuilder(string collectionName, IXmlBuilder itemBuilder)
        {
            this.CollectionName = collectionName;
            this.InnerItemBuilder = itemBuilder;
        }

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
                var element = InnerItemBuilder.GenerateXml(enumerator.Current);
                root.Add(element);
            }

            return root;
        }
    }
}
