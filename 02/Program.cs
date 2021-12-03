// See https://aka.ms/new-console-template for more information
using Name;
await MyClass.Part1();
Console.WriteLine();
await MyClass.Part2();

public struct Coordinate {
    public int x {get;set;}
    public int y {get;set;}
    public int aim {get;set;}
}

namespace Name
{
    public class MyClass {
        public static async Task Part1() {
            var lines = await File.ReadAllLinesAsync("val.txt");
            var coordinates = new Coordinate();
            foreach(var line in lines) {
                var lineValues = line.Split();
                switch(lineValues[0]) {
                    case "forward":
                        coordinates.x += int.Parse(lineValues[1]);
                        break;
                    case "down":
                        coordinates.y += int.Parse(lineValues[1]);
                        break;
                    case "up":
                        coordinates.y -= int.Parse(lineValues[1]);
                        break;

                    default:
                    break;
                }
            }
            Console.WriteLine($"{coordinates.x}\t{coordinates.y}");
            Console.WriteLine(coordinates.x * coordinates.y);
        }

        public static async Task Part2() {
            var lines = await File.ReadAllLinesAsync("val.txt");
            var coordinates = new Coordinate();
            foreach(var line in lines) {
                var lineValues = line.Split();
                switch(lineValues[0]) {
                    case "forward":
                        coordinates.x += int.Parse(lineValues[1]);
                        coordinates.y += int.Parse(lineValues[1]) * coordinates.aim;
                        break;
                    case "down":
                        coordinates.aim += int.Parse(lineValues[1]);
                        break;
                    case "up":
                        coordinates.aim -= int.Parse(lineValues[1]);
                        break;

                    default:
                    break;
                }
            }
            Console.WriteLine($"{coordinates.x}\t{coordinates.y}");
            Console.WriteLine(coordinates.x * coordinates.y);
        }
    }
    
}
