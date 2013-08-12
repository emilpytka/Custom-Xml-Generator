using System.Collections.Generic;
using System.Xml.Linq;

namespace CustomXmlGenerator.CollectionBuider
{
    internal abstract class CollectionBuilderBase
    {
        public string CollectionName { get; set; }
        
        public abstract XElement GetCollectionXml(object objectToSerialize);
    }
}