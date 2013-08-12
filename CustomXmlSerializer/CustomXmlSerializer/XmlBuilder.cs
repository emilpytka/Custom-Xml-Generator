using CustomXmlSerializer.CollectionBuider;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;

namespace CustomXmlSerializer
{
    public class XmlBuilder<T> : IXmlBuilder<T>
        where T : class
    {
        private readonly string _rootName;
        private readonly Dictionary<string, string> _properties;
        private readonly Dictionary<string, string> _tags;
        private readonly Dictionary<string, IXmlBuilder> _tagsWithBuilders;
        private readonly Dictionary<string, CollectionBuilderBase> _collections; 

        public XmlBuilder(string rootName)
        {
            _properties = new Dictionary<string, string>();
            _tags = new Dictionary<string, string>();
            _tagsWithBuilders = new Dictionary<string, IXmlBuilder>();
            _collections = new Dictionary<string, CollectionBuilderBase>();
            _rootName = rootName;
        }

        public void BindAttribute<T1>(Expression<Func<T, T1>> e, string propName) 
        {
            var mInfo = TryGetChildMemberInfo<T1>(e);
            _properties.Add(mInfo.Name, propName);
        }

        public void BindTag<T1>(Expression<Func<T, T1>> e, IXmlBuilder builder) where T1 : class
        {
            var mInfo = TryGetChildMemberInfo<T1>(e);

            var genericBuilder = builder as XmlBuilder<T1>;
            _tagsWithBuilders.Add(mInfo.Name, genericBuilder);
        }

        public void BindTag<T1>(Expression<Func<T, T1>> e, string tagName)
        {
            var mInfo = TryGetChildMemberInfo<T1>(e);
            _tags.Add(mInfo.Name, tagName);
        }

        public void BindCollection<T1>(Expression<Func<T, T1>> e, string collectionName, IXmlBuilder builder)
        {
            var colBuilder = new CustomCollectionBuilder(collectionName, builder);
            var mInfo = TryGetChildMemberInfo<T1>(e);
            _collections.Add(mInfo.Name, colBuilder);
        }

        public void BindCollection<T1>(Expression<Func<T, T1>> e, string collectionName, string elementName)
        {
            var collBuilder = new SimpleCollectionBuilder() {CollectionName = collectionName, ItemName = elementName};
            var mInfo = TryGetChildMemberInfo<T1>(e);
            _collections.Add(mInfo.Name, collBuilder);
        }

        public XElement GenerateXml(object objectToSerialize)
        {
            var result = new XDocument(new XElement(_rootName));
            var root = result.Root;

            if (root == null)
                return null;

            foreach (var customAttribute in typeof(T).GetProperties())
            {
                string propertyName = customAttribute.Name;
                if (_properties.ContainsKey(propertyName))
                {
                    root.Add(new XAttribute(_properties[propertyName], customAttribute.GetValue(objectToSerialize, null)));
                }
                if (_tags.ContainsKey(propertyName))
                {
                    root.Add(new XElement(_tags[propertyName], customAttribute.GetValue(objectToSerialize, null)));
                }
                if (_tagsWithBuilders.ContainsKey(propertyName))
                {
                    var builder = _tagsWithBuilders[propertyName];
                    var schoolClass = customAttribute.GetValue(objectToSerialize, null);
                    var element = builder.GenerateXml(schoolClass);
                    root.Add(element);
                } 
                if (_collections.ContainsKey(propertyName))
                {
                    var buidler = _collections[propertyName];
                    var col = customAttribute.GetValue(objectToSerialize, null);
                    root.Add(buidler.GetCollectionXml(col));
                }
            }

            return root;
        }

        private MemberInfo GetMemberInfo(Expression expression)
        {

            if (expression == null)
            {
                throw new ArgumentException(
                    "The expression cannot be null.");
            }

            if (expression is MemberExpression)
            {
                var memberExpression =
                    (MemberExpression)expression;
                return memberExpression.Member;
            }

            throw new ArgumentException("Invalid expression");
        }

        private MemberInfo TryGetChildMemberInfo<T1>(Expression<Func<T, T1>> e)
        {
            if (e == null)
                throw new ArgumentException("Expression cannot be null");

            var mInfo = GetMemberInfo(e.Body);
            
            if (mInfo.DeclaringType != typeof(T))
                throw new ArgumentException("Invalid expression, only main element property");
            
            return mInfo;
        }


        public T GetObjectFromXml(string xml)
        {
            throw new NotImplementedException();
        }
    }
}
