using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MoreLinq;

namespace SingletonImplementation
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }


    public class SingletonDtabase : IDatabase
    {
        private Dictionary<string, int> capitals;

        public SingletonDtabase()
        {
            Console.WriteLine("Initializing Database");
            capitals = File.ReadAllLines("capitals.txt")
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                );
        }
        public int GetPopulation(string name)
        {
            return capitals[name];
        }

        //private static SingletonDtabase instance = new SingletonDtabase();
        //To improve we should add lazyness
        private static Lazy<SingletonDtabase> instance = new Lazy<SingletonDtabase>(()=>new SingletonDtabase());

        public static SingletonDtabase Instance => instance.Value; 
    }

    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
            {
                result += SingletonDtabase.Instance.GetPopulation(name);
            }
            return result;
        }
    }
    //the problem is with testability
    //we are binding the tests with the database
    //with the singleton, You cant substitute it with sth else

    //FOR DIIIIII
    public class ConfdigurableRecordFinder
    {
        private IDatabase database;
        public ConfdigurableRecordFinder(IDatabase database)
        {
            this.database = database ?? throw new ArgumentNullException(paramName: nameof(database));
        }

        public int GetPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
            {
                result += database.GetPopulation(name);
            }
            return result;
        }
    }

    public class DummyDatabase : IDatabase
    {
        public int GetPopulation(string name)
        {
            return new Dictionary<string, int>
            {
                ["alpha"] = 1,
                ["beta"] = 2,
                ["gamma"] = 3
            }[name];
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDtabase.Instance;
            Console.WriteLine(db.GetPopulation("Athens"));
        }
    }
}
