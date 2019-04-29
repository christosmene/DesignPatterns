using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DependencyInversion 
//high level parts of the system should not depend on low level parts
//of the system directly but instead they should depend on some kind of abstraction
{
    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name;
        //public DateTime DateOfBirth;
    }

    //low level
    //public class Relationships
    //{
    //    //we use the c# 7 tuples here
    //    //se c# sharp 7 8a eelga
    //    //Gia na pai3ei ekana reference to System.ValueTuple kai paizei kala to c#7 feature
    //    //private List<(Person, Relationship, Person)> relations =
    //    //new List<(Person, Relationship, Person)>();
    //    private List<Tuple<Person, Relationship, Person>> relations =
    //        new List<Tuple<Person, Relationship, Person>>();

    //    public void AddParentAndChild(Person parent, Person child)
    //    {
    //        relations.Add(new Tuple<Person, Relationship, Person>(parent, Relationship.Parent, child));
    //        relations.Add(new Tuple<Person, Relationship, Person>(child, Relationship.Child, parent));
    //    }

    //    public List<Tuple<Person, Relationship, Person>> Relations => relations;//we made a getter
    //}

    //high level module
    //class Research
    //{
    //    public Research(Relationships relationships)
    //    {
    //        var relations = relationships.Relations;
    //        foreach (var r in relations.Where(x=>x.Item1.Name=="John" && x.Item2 == Relationship.Parent))
    //        {
    //            WriteLine($"John has a child called {r.Item3.Name}");
    //        }
    //    }

    //    static void Main(string[] args)
    //    {
    //        var parent = new Person { Name = "John" };
    //        var child1 = new Person { Name = "Chris" };
    //        var child2 = new Person { Name = "Mary" };

    //        var relationships = new Relationships();
    //        relationships.AddParentAndChild(parent, child1);
    //        relationships.AddParentAndChild(parent, child2);

    //        new Research(relationships);
    //    }
    //}
    //The problem we this scenario and the reason dependency invertion exists is that:
    //We access a low level part of the relationshos class (we access its data store).
    //So the relation ships can't change its mind of how we access the relationships.
    //We need to provide a form of abstraction

    //I create an interface
    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    public class Relationships :IRelationshipBrowser
    {
        //we use the c# 7 tuples here
        //se c# sharp 7 8a eelga
        //Gia na pai3ei ekana reference to System.ValueTuple kai paizei kala to c#7 feature
        //private List<(Person, Relationship, Person)> relations =
        //new List<(Person, Relationship, Person)>();
        private List<Tuple<Person, Relationship, Person>> relations =
            new List<Tuple<Person, Relationship, Person>>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add(new Tuple<Person, Relationship, Person>(parent, Relationship.Parent, child));
            relations.Add(new Tuple<Person, Relationship, Person>(child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            //foreach (var r in relations.Where(x => x.Item1.Name ==name && x.Item2 == Relationship.Parent))
            //{
            //    yield return r.Item3;
            //}
            return relations.Where(
                x => x.Item1.Name == name &&
                x.Item2 == Relationship.Parent
                ).Select(r => r.Item3);
        }//we now depend on an abstraction --> IRelationshipBrowser

        // public List<Tuple<Person, Relationship, Person>> Relations => relations;//we made a getter
    }

    class Research
    {
        //public Research(Relationships relationships)
        //{
        //    var relations = relationships.Relations;
        //    foreach (var r in relations.Where(x => x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
        //    {
        //        WriteLine($"John has a child called {r.Item3.Name}");
        //    }
        //}
        public Research(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
                WriteLine($"John has a child called {p.Name}");
        }

        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);
        }
    }
}

































