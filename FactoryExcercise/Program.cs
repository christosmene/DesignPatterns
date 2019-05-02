using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryExcercise
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
        }
    }

    public class PersonFactory
    {
        private static int id = 0;
        public Person CreatePerson(string name)
        {
            return new Person
            {
                Id = id++,
                Name = name
            };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var pf = new PersonFactory();
            Console.WriteLine(pf.CreatePerson("Giannis"));
            Console.WriteLine(pf.CreatePerson("Christos"));
            Console.WriteLine(pf.CreatePerson("Kostas"));
            Console.WriteLine(pf.CreatePerson("Maria"));

        }
    }
}
