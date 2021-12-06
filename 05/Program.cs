// See https://aka.ms/new-console-template for more information
var part1 = await Task05.Name.Run(diagonal: false);
var part2 = await Task05.Name.Run(diagonal: true);
Console.WriteLine($"Part 1, without diagonal: {part1}");
Console.WriteLine($"Part 2, with diagonal: {part2}");

namespace Task05
{
    public static class Name
    {
        public static async Task<int> Run(bool diagonal) {
            var fileLines = await File.ReadAllLinesAsync("val.txt");
            var lines = new List<Line>();
            var grid = new int[1024,1024];
            //grid.PrintBoard();
            foreach(var line in fileLines) {
                var coordinateNumbers = line
                    .Split("->")
                    .Select(x => x.Split(',')
                    .Select(x => int.Parse(x.Trim()))
                    .ToArray())
                    .ToArray();
                var start = new Coordinate(coordinateNumbers[0][0], coordinateNumbers[0][1]);
                var end = new Coordinate(coordinateNumbers[1][0], coordinateNumbers[1][1]);
                lines.Add(new Line(start,end));
                grid.InsertPoints(lines.Last().GeneratePoints(diagonal: diagonal));

            }
            //grid.PrintBoard();
            return grid.GetOverlapCount();
        }
        public static void InsertPoints(this int[,] grid, Coordinate[] coordinates) {
            foreach(var coordinate in coordinates) {
                grid[coordinate.X, coordinate.Y]++;
            }
        }
        public static void PrintBoard(this int[,] grid) {
            for (int i = grid.GetLength(0)-1; i >= 0; i--) {
                Console.WriteLine(String.Join('\t', Enumerable.Range(0,grid.GetLength(0)).Select(x => grid[x,i])));
            }
        }
        public static int GetOverlapCount(this int[,] grid) {
            int sum = 0;
            for (int i = 0; i < grid.GetLength(0); i++) {
                sum += Enumerable.Range(0, grid.GetLength(0))
                    .Select(x => grid[i,x])
                    .Where(x => x > 1)
                    .Count();
            }
            return sum;
        }
        public class Line {
            public Coordinate Start {get;set;}
            public Coordinate End {get;set;}
            public Line(Coordinate start, Coordinate end) {
                Start = start;
                End = end;
            }
            public override string ToString()
            {
                return $"Start: {Start}, End: {End}";
            }
            public Coordinate[] GeneratePoints(bool diagonal) {
                // +1 to include only one if the same point and both if they are one apart
                if (!diagonal && (Start.X != End.X) && (Start.Y != End.Y)) {
                    return new Coordinate[0];
                }
                var iterations = Math.Max(
                    Math.Abs(Start.X - End.X),
                    Math.Abs(Start.Y - End.Y)
                    ) + 1;
                var coordinates = new Coordinate[iterations];
                //xdelta and ydelta determines wheather x and y should increment or decrement
                var xDelta = (End.X - Start.X) / (iterations-1);
                var yDelta = (End.Y - Start.Y) / (iterations-1);
                for (int i = 0; i < iterations; i++) {
                    coordinates[i] = new Coordinate(Start.X + xDelta*i, Start.Y + yDelta*i);
                }
                return coordinates;

            }
        }
        public struct Coordinate {
            public int X {get;set;}
            public int Y {get;set;}
            public Coordinate(int x, int y) {
                X = x;
                Y = y;
            }
            public override string ToString()
            {
                return $"X: {X}, Y: {Y}";
            }
        }
    }
}