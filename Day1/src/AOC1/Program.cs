using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AOC1
{
    public class Program
    {
        public enum Orientation
        {
            North=0,
            East=1,
            South=2,
            West=3
        }

        public struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Orientation Orientation { get; set; }

            public Point Combine(Point point)
            {
                return new Point
                {
                    X = this.X + point.X,
                    Y = this.Y + point.Y,
                    Orientation = point.Orientation
                };
            }
        }

        class DirectionSegment
        {
            public  string  Direction { get; set; }
            public  int NumberOfBlocks { get; set; }
            public Point Move(Orientation orientation)
            {
                var updatedOrientation = changeOrientation(orientation, this.Direction);

                switch(updatedOrientation)
                {
                    default:
                    case Orientation.North:
                        return new Point { X = 0, Y = NumberOfBlocks, Orientation = updatedOrientation };
                    case Orientation.East:
                        return new Point { X = NumberOfBlocks, Y = 0, Orientation = updatedOrientation };
                    case Orientation.South:
                        return new Point { X = 0, Y = -NumberOfBlocks, Orientation = updatedOrientation };
                    case Orientation.West:
                        return new Point { X = -NumberOfBlocks, Y = 0, Orientation = updatedOrientation };
                }
            }

            private Orientation changeOrientation(Orientation orientation, string direction)
            {
                switch(orientation)
                {
                    default:
                    case Orientation.North:
                        return direction == "L" ? Orientation.West : Orientation.East;
                    case Orientation.East:
                        return direction == "L" ? Orientation.North : Orientation.South;
                    case Orientation.South:
                        return direction == "L" ? Orientation.East : Orientation.West;
                    case Orientation.West:
                        return direction == "L" ? Orientation.South : Orientation.North;
                }
            }
        }

        public static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var coordinates = input.Split(new []{ ", "}, StringSplitOptions.None).Select(
                (i) => 
                {
                    return new DirectionSegment
                    {
                        Direction = i.Substring(0, 1),
                        NumberOfBlocks = int.Parse(i.Substring(1, (i.Length - 1)))
                    };
                });

            var seedPoint = new Point { X = 0, Y = 0, Orientation = Orientation.North};

            var blocksAway = coordinates.Aggregate(seedPoint, (accumulated, next) =>
            {
                var nextPoint= next.Move(accumulated.Orientation);

                return accumulated.Combine(nextPoint);
            });

            Console.WriteLine("Santa is {0} blocks away", Math.Abs(blocksAway.X) + Math.Abs(blocksAway.Y));
        }
    }
}
