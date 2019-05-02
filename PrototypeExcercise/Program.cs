using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeExcercise
{

    public class Point
    {
        public int X, Y;
    }

    public class Line
    {
        public Point Start, End;

        public Line DeepCopy()
        {
            var newStart = new Point { X = Start.X, Y = Start.Y };
            var newEnd = new Point { X = End.X, Y = End.Y };
            return new Line { Start = newStart, End = newEnd };
        }

        public override string ToString()
        {
            return $"{nameof(Start)}:(X={Start.X}, Y={Start.Y}) - {nameof(End)}:(X={End.X}, Y={End.Y})";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var firstLine = new Line
            {
                Start = new Point { X = 0, Y = 0 },
                End = new Point {X=13,Y=23 }
            };
            var secondLine = firstLine.DeepCopy();
            Console.WriteLine(firstLine);
            Console.WriteLine(secondLine);

            secondLine.Start = new Point { X = 123, Y = 432 };
            Console.WriteLine();
            Console.WriteLine(firstLine);
            Console.WriteLine(secondLine);
        }
    }
}

