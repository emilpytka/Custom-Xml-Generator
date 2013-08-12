using System;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace CustomXmlGenerator
{
    public interface IXmlBuilder<T> : IXmlBuilder
    {
        void BindAttribute<T1>(Expression<Func<T, T1>> e, string propName);

        void BindTag<T1>(Expression<Func<T, T1>> e, IXmlBuilder builder) where T1 : class;

        void BindTag<T1>(Expression<Func<T, T1>> e, string tagName);

        void BindCollection<T1>(Expression<Func<T, T1>> e, string collectionName, IXmlBuilder builder);

        void BindCollection<T1>(Expression<Func<T, T1>> e, string collectionName, string elementName);

        
    }

    public interface IXmlBuilder
    {
        XElement GenerateXml(object objectToSerialize);
    }
}
