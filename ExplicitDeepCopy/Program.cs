using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplicitDeepCopy
{
    public interface IPrototype<T>
    {
        T DeepCoppy();
    }

    public class Person:IPrototype<Person>
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names ?? throw new ArgumentNullException(paramName: nameof(names));
            Address = address ?? throw new ArgumentNullException(paramName: nameof(address));
        }

        //Simple : You make a ctor  makes a person
        //from another person
        public Person(Person other)
        {
            Names = other.Names;
            //Address = other.Address; //not suficient ass address makes a shallow copy
            Address = new Address(other.Address);
            //we need to make a copy ctor for the Address to
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {Address.ToString()}";
        }

        public Person DeepCoppy()
        {
            return new Person(Names, Address.DeepCoppy());
        }
    }

    public class Address : IPrototype<Address>
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName ?? throw new ArgumentNullException();
            HouseNumber = houseNumber;
        }

        public Address(Address other)
        {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }

        public Address DeepCoppy()
        {
            return new Address(StreetName, HouseNumber);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var john = new Person(new[] { "John", "Smith" },
                new Address("London Road", 123));

            var jane = john.DeepCoppy();
            //Lets try to change jane's Address
            jane.Address.HouseNumber = 1234567;
            Console.WriteLine(john);
            Console.WriteLine(jane);
            //Both values change (both in jane and john)
            //we copied the reference
        }
    }
}
