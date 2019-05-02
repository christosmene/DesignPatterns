using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise
{
    public class Square
    {
        public int Side;
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        private int side;
        public int Width => side;
        public int Height => side;

        public SquareToRectangleAdapter(Square square)
        {
            this.side = square.Side; 
        }
    }

    public class Demo
    {
        public static void Main()
        {
            var tetragono = new Square { Side=4};
            var adapter = new SquareToRectangleAdapter(tetragono);
            Console.WriteLine(adapter.Area());
        }
    }
    
}








