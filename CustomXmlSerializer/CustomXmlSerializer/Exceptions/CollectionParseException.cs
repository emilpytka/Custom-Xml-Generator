using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlCreator.Exceptions
{
    internal class CollectionParseException : Exception
    {
        public CollectionParseException()
        {

        }

        public CollectionParseException(string message)
            : base(message)
        {

        }
    }
}
