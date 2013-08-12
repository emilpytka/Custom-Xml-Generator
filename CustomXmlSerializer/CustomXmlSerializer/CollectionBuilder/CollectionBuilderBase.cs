using System.Collections.Generic;
using System.Xml.Linq;

namespace CustomXmlSerializer.CollectionBuider
{
    internal abstract class CollectionBuilderBase
    {
        public string CollectionName { get; set; }
        
        public abstract XElement GetCollectionXml(object objectToSerialize);
    }
}