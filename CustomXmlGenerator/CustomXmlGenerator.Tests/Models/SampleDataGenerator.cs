using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomXmlGenerator.Tests.Models
{
    class SampleDataGenerator
    {
        public static User GenerateSampleUser()
        {
            var teacher1 = new Teacher() { FirstName = "Joanna", LastName = "Nowak" };
            var teacher2 = new Teacher() { FirstName = "Małgorzata", LastName = "Kowalska" };

            var uStudent = new User()
            {
                Id = "1",
                Age = 13,
                FirstName = "Kuba",
                LastName = "Polak",
                Class = new SchoolClass() { Name = "3d", Tutor = teacher1 },
                Teachers = new List<Teacher> { teacher1, teacher2 }
            };
            return uStudent;
        }

    }
}
