using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CustomXmlSerializer.CollectionBuider
{
    internal class SimpleCollectionBuilder : CollectionBuilderBase
    {
        public string ItemName { get; set; }

        public override XElement GetCollectionXml(object objectToSerialize)
        {
            var doc = new XDocument(new XElement(CollectionName));
            var root = doc.Root;

            var col = objectToSerialize as IEnumerable;
            var enumerator = col.GetEnumerator();

            while (enumerator.MoveNext())
            {
                root.Add(new XElement(ItemName, enumerator.Current));
            }

            return root;
        }
    }
}
