var file = File.ReadAllLines("val.txt");
var numbers = file[0]
    .Split(',')
    .Select(x => int.Parse(x))
    .ToArray();
var results = new List<int>();
for (int i = 0; i < numbers.Max(); i++) {
    results.Add(numbers
        .Select(x => (Math.Abs(x-i)*((Math.Abs(x-i)+1))/2))
        .Sum()
    );
}
Console.WriteLine(results.Min());