using System.Linq;
// See https://aka.ms/new-console-template for more information
await Name.MyClass.Part1();
namespace Name
{
    class MyClass {
        public static async Task Part1() {
            var bingoBoards = await CreateBingoList();
        }
        public async static Task<List<bingoValue[][]>> CreateBingoList() {
            var fileLines = await File.ReadAllLinesAsync("val.txt");
            var bingoBoards = new List<bingoValue[][]>();
            var bingoNumbers = fileLines[0]
                .Split(',')
                .Select(s => byte.Parse(s))
                .ToArray();
            for (int i = 2; i < fileLines.Length; i+=6) {
                string[] temp = new string[5];
                Array.Copy(fileLines,i,temp,0,5);
                var bingoBoard = new bingoValue[5][];
                for(int j = 0; j < 5; j++) {
                    bingoBoard[j] = temp[j]
                        .Split()
                        .Where(x => !String.IsNullOrWhiteSpace(x))
                        .Select(x => new bingoValue((byte)int.Parse(x)))
                        .ToArray();
                }
                bingoBoards.Add(bingoBoard);
                /*
                foreach(var item in bingoBoard) {
                    Console.WriteLine(String.Join(' ', item.Select(x => x.Value)));
                }
                */
            }
            return bingoBoards;

        }
        public struct bingoValue {
            public byte Value {get;set;}
            public bool Unlocked {get;set;}
            public bingoValue (byte value) {
                Value = value;
                Unlocked = false;
            }
        }
    }
}
