using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacetedBuilder
{
    public class Person
    {
        //address
        public string StreetAdress, Postcode, City;

        //employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(StreetAdress)}: {StreetAdress}, " +
                $"{nameof(Postcode)}: {Postcode}, " +
                $"{nameof(City)}: {City}, " +
                $"{nameof(CompanyName)}: {CompanyName}, " +
                $"{nameof(Position)}: {Position}, " +
                $"{nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    public class PersonBuilder //Actually not a builder but a facade for other builders
    {
        //reference!
        protected Person person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public static implicit operator Person(PersonBuilder pb) {
            return pb.person;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }

    public class PersonAddressBuilder:PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string streetAdress)
        {
            person.StreetAdress = streetAdress;
            return this;
        }

        public PersonAddressBuilder WithZPostcode(string postCode)
        {
            person.Postcode = postCode;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }

    class Demo
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            //var prepei na balw person kai oxi var
               Person person = pb
          .Lives.At("123 London Road")
                .In("London")
                .WithZPostcode("asd123f")
          .Works.At("Fabrika")
                .AsA("Engineer")
                .Earning(123456);

            Console.WriteLine(person);
        }
    }
}










