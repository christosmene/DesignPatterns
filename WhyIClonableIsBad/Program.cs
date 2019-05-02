using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhyIClonableIsBad
{
    public class Person: ICloneable
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names ?? throw new ArgumentNullException(paramName: nameof(names));
            Address = address ?? throw new ArgumentNullException(paramName: nameof(address));
        }

        //h me8odos auth einai epifobh afou de 3ereis
        //an 8a exeis deep h shallow copy kai sou epistrefei
        //kai object 8eleis cast nklp
        //KAI BASIKA EPISTREFEI SHALLOW COPY!
        public object Clone()
        {
            //it will be shallow copy (we just coppy the reference)
          ////  return new Person(Names, Address);

            //to support the deep copy pou doulevei opws 8a 8elame
            return new Person(Names, (Address)Address.Clone());
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ",Names)}, {Address.ToString()}";
        }
    }

    public class Address:ICloneable
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName ?? throw new ArgumentNullException();
            HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }

        public object Clone()
        {
            //or a deep copy because string are immutable
            return new Address(StreetName, HouseNumber);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var john = new Person(new[] { "John", "Smith" },
                new Address("London Road", 123));


            var jane =(Person) john.Clone();
            //Lets try to change jane's Address
            jane.Address.HouseNumber = 1234567;
            Console.WriteLine(john);
            Console.WriteLine(jane);
            //Both values change (both in jane and john)
            //we copied the reference


        }
    }
}
