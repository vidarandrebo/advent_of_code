var lines = await File.ReadAllLinesAsync("val.txt");
var bitCount = new int[12];
var gammaArray = new byte[12];
var epsilonArray = new byte[12];
foreach(var line in lines) {
    var token = line.ToCharArray();
    for (int i = 0; i < 12; i++) {
        bitCount[i] += (int)Char.GetNumericValue(token[i]);
    }
}
int gamma = 0;
int epsilon = 0;
Console.WriteLine(String.Join(' ',bitCount));
for (int i = 0; i < 12; i++) {
    switch((lines.Length - bitCount[i]) > lines.Length/2) {
        case true:
            epsilon += (int)Math.Pow(2, 11-i);
            epsilonArray[i] = 1;
            gammaArray[i] = 0;
            break;
        default:
            gamma += (int)Math.Pow(2, 11-i);
            epsilonArray[i] = 0;
            gammaArray[i] = 1;
            break;
    }
        
}
Console.WriteLine($"Gamma: {gamma},\t{String.Join(' ', gammaArray[..])}");
Console.WriteLine($"Espilon: {epsilon},\t{String.Join(' ', epsilonArray[..])}");
Console.WriteLine($"Power: {epsilon*gamma}");