using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFFormWithCommand.Model;

namespace WPFFormWithCommand.FakeRepo
{
    public class FakeRepository
    {
        public static List<Person> GetPerson()
        {
            return new List<Person>
            {
                new Person
                {
                    FirstName= "Aydin",
                    LastName= "Hasimli",
                    PhoneNumber="+994 70 852 94 10",
                    EmailAddress= "aydinhsimli@gmail.com"
                },
                new Person
                {
                    FirstName= "Kamran",
                    LastName= "Karimzada",
                    PhoneNumber="+994 70 370 20 16",
                    EmailAddress= "kamran@mail.ru"
                }
            };
        }
    }
}
