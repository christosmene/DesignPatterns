using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

//thats how the prototype pattern is done
//in the real world
namespace CopyThroughSerialization
{
    public static class ExtensionMethods
    {
        //it is accepting every type
        public static T DeepCopy<T>(this T self)
        {
            //we need to serialize and deserialize the
            //object to make a deep copy of it
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();//8elei to [Serializable]
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T)copy;
        }


        //the xml requires parametrless contructors in each one 
        public static T DeepCopyXml<T>(this T self)
        {
            using (var ms = new MemoryStream()) {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms,self);
                ms.Position = 0;//alternative of the above
                return (T)s.Deserialize(ms);
            }
        }
    }

    [Serializable]
    public class Person 
    {
        public string[] Names;
        public Address Address;

        public Person()
        {

        }
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
    }

    [Serializable]
    public class Address
    {
        public string StreetName;
        public int HouseNumber;

        public Address()
        {

        }

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
    }

    class Program
    {
        static void Main(string[] args)
        {
            var john = new Person(new[] { "John", "Smith" },
                new Address("London Road", 123));

            var jane = john.DeepCopyXml();
            //Lets try to change jane's Address
            jane.Address.HouseNumber = 1234567;
            Console.WriteLine(john);
            Console.WriteLine(jane);
            //Both values change (both in jane and john)
            //we copied the reference
        }
    }
}
