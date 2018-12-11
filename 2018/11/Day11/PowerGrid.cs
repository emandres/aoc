using System;
using System.Linq;

namespace Day11
{
    public class PowerGrid
    {
        public static int PowerLevel(int x, int y, int serialNumber)
        {
            var rackId = x + 10;
            return (rackId * y + serialNumber) * rackId / 100 % 10 - 5;
        }

        public static int[,] Grid(int serialNumber)
        {
            var grid = new int[300, 300];
            for (int x = 0; x < 300; x++)
            {
                for (int y = 0; y < 300; y++)
                {
                    grid[x, y] = PowerLevel(x + 1, y + 1, serialNumber);
                }
            }

            return grid;
        }

        public static (int x, int y) Max3Square(int serialNumber)
        {
            var grid = Grid(serialNumber);
            var (x, y, _) = MaxNSquare(3, grid);
            return (x, y);
        }

        public static (int x, int y, int size) MaxSquare(int serialNumber)
        {
            var grid = Grid(serialNumber);

            return Enumerable.Range(1, 301)
                .AsParallel()
                .Select(n =>
                {
                    var (x, y, value) = MaxNSquare(n, grid);
                    Console.WriteLine($"{n}: ({x},{y}) = {value}");
                    return (x: x, y: y, value: value, size: n);
                })
                .OrderByDescending(x => x.value)
                .Select(x => (x.x, x.y, x.size))
                .First();
        }

        public static (int x, int y, int value) MaxNSquare(int n, int[,] grid)
        {
            int max = int.MinValue;
            int maxX = 0;
            int maxY = 0;

            for (int x = 0; x < 300 - n; x++)
            {
                for (int y = 0; y < 300 - n; y++)
                {
                    int square = 0;
                    for (int dx = 0; dx < n; dx++)
                    {
                        for (int dy = 0; dy < n; dy++)
                        {
                            square += grid[x + dx, y + dy];
                        }
                    }

                    if (square > max)
                    {
                        max = square;
                        maxX = x + 1;
                        maxY = y + 1;
                    }
                }
            }

            return (maxX, maxY, max);
        }
    }
}