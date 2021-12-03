/*
var lines = await File.ReadAllLinesAsync("val.txt");
var increases = 0;
for (int i = 3; i < lines.Length; i++) {
    if(int.Parse(lines[i]) > int.Parse(lines[i-3])){
        increases++;
    }
}
Console.WriteLine(increases);
*/

var lines = await File.ReadAllLinesAsync("val.txt");
var increases = 0;
for (int i = 1; i < lines.Length; i++) {
    if(int.Parse(lines[i]) > int.Parse(lines[i-1])){
        increases++;
    }
}
Console.WriteLine(increases);