using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomXmlSerializer.Tests.Models
{
    public class User
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public List<Teacher> Teachers { get; set; }

        public SchoolClass Class { get; set; }
    }

    public class SchoolClass
    {
        public string Name { get; set; }

        public Teacher Tutor { get; set; }
    }

    public class Teacher
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
