using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monostate
{
    public class CEO
    {
        //this is the classic monostate pattern
        private static string name;
        private static int age;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Age
        {
            get => age;
            set => age = value;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //thats bizzare because user of the api can call
            //the ctor but he gets the same instance
            var ceo = new CEO
            {
                Name = "John Smith",
                Age = 60
            };

            var ceo2 = new CEO();
            Console.WriteLine(ceo2);



        }
    }
}
