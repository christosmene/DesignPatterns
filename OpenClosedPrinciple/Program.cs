using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace OpenClosedPrinciple
{
    public enum Color
    {
        Red,Green,Blue
    }

    public enum Size
    {
        Small,Medium,Large,Yuge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }
            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products,
            Size size)
        {
            foreach (var p in products)
            {
                if (p.Size == size) yield return p;
            }
        }

        //apofasisame oti 8eloume kai auto to filter
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products,
            Color color)
        {
            foreach (var p in products)
            {
                if (p.Color == color) yield return p;
            }
        }

        //apofasisame 3afnika oti 8eloume kai ta duo proigoumena filterw
        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products,
            Size size, Color color)
        {
            foreach (var p in products)
            {
                if (p.Size == size && p.Color == color) yield return p;
            }
        }

        //we broke thew open close principle. Classes open for extensoin cloesd for modification
    }

    //create a bunch of interfaces
    public interface ISpecification<T>
    {
        bool IsSutisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpefification : ISpecification<Product>
    {
        public Color color;

        public ColorSpefification(Color color)
        {
            this.color = color;
        }

        public bool IsSutisfied(Product t)
        {
            return t.Color == color;
        }
    }

    public class SizeSpefification : ISpecification<Product>
    {
        public Size size;

        public SizeSpefification(Size size)
        {
            this.size = size;
        }

        public bool IsSutisfied(Product t)
        {
            return t.Size == size;
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
            {
                if (spec.IsSutisfied(i))
                    yield return i;
            }
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;

        public AndSpecification(ISpecification<T>  first, ISpecification<T>  second)
        {
            this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
            this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
        }

        public bool IsSutisfied(T t)
        {
            return first.IsSutisfied(t) && second.IsSutisfied(t);
        }
    }

    class Demo
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };
            var pf = new ProductFilter();
            WriteLine("Green pruducts (old):");
            foreach (var p in pf.FilterByColor(products, Color.Green))
            {
                WriteLine($" - {p.Name} is green");
            }


            var bf = new BetterFilter();
            WriteLine("Green Poducts (new):");
            foreach (var p in bf.Filter(products, new ColorSpefification(Color.Green)))
            {
                WriteLine($" - {p.Name} is green");
            }

            WriteLine("Large blue items");
            foreach (var p in bf.Filter(products,
                new AndSpecification<Product>(
                    new ColorSpefification(Color.Blue), 
                    new SizeSpefification(Size.Large))))
            {
                WriteLine($" - {p.Name} is big and blue");
            }
        }
    }
}



























