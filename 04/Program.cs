using System.Linq;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Part1");
await Name.MyClass.Part1();
Console.WriteLine("Part2");
await Name.MyClass.Part2();
namespace Name
{
    public static class MyClass
    {
        public static async Task Part1()
        {
            var bingoBoards = await CreateBingoList();
            var bingoNumbers = await CreateBingoNumbers();
            PlayBingo(bingoBoards, bingoNumbers);
        }
        public static async Task Part2()
        {
            var bingoBoards = await CreateBingoList();
            var bingoNumbers = await CreateBingoNumbers();
            PlayLastBingo(bingoBoards, bingoNumbers);
        }
        public static int PlayBingo(List<bingoValue[][]> boardList, byte[] bingoNumbers)
        {
            foreach (var number in bingoNumbers)
            {
                boardList.NewNumber(number);
                for (int i = 0; i < boardList.Count; i++)
                {
                    if (boardList[i].IsBingo())
                    {
                        boardList[i].PrintBoard();
                        var score = boardList[i].BoardScore();
                        Console.WriteLine($"Score: {score}");
                        Console.WriteLine($"Final number: {score * number}");
                        return i;
                    }
                }
            }
            return 0;

        }
        public static int PlayLastBingo(List<bingoValue[][]> boardList, byte[] bingoNumbers)
        {
            foreach (var number in bingoNumbers)
            {
                boardList.NewNumber(number);
                if (boardList.Count > 1)
                {
                    boardList = boardList.Where(x => !x.IsBingo()).ToList();
                }
                else if (boardList[0].IsBingo())
                {
                    var score = boardList[0].BoardScore();
                    boardList[0].PrintBoard();
                    Console.WriteLine($"Score: {score}");
                    Console.WriteLine($"Final number: {score * number}");
                    break;
                }
            }
            return 0;

        }
        public async static Task<byte[]> CreateBingoNumbers()
        {
            var fileLines = await File.ReadAllLinesAsync("val.txt");
            return fileLines[0]
                .Split(',')
                .Select(s => byte.Parse(s))
                .ToArray();

        }
        public async static Task<List<bingoValue[][]>> CreateBingoList()
        {
            var fileLines = await File.ReadAllLinesAsync("val.txt");
            var bingoBoards = new List<bingoValue[][]>();
            for (int i = 2; i < fileLines.Length; i += 6)
            {
                string[] temp = new string[5];
                Array.Copy(fileLines, i, temp, 0, 5);
                var bingoBoard = new bingoValue[5][];
                for (int j = 0; j < 5; j++)
                {
                    bingoBoard[j] = temp[j]
                        .Split()
                        .Where(x => !String.IsNullOrWhiteSpace(x))
                        .Select(x => new bingoValue((byte)int.Parse(x)))
                        .ToArray();
                }
                bingoBoards.Add(bingoBoard);
            }
            return bingoBoards;

        }
        public static void NewNumber(this List<bingoValue[][]> boardList, byte number)
        {
            //list
            for (int i = 0; i < boardList.Count; i++)
            {
                //single board
                for (int j = 0; j < boardList[i].Length; j++)
                {
                    //row in board
                    for (int k = 0; k < boardList[i][j].Length; k++)
                    {
                        if (boardList[i][j][k].Value == number)
                        {
                            boardList[i][j][k].Unlocked = true;
                            break;
                        }
                    }
                }
            }
        }
        public static void PrintBoard(this bingoValue[][] bingoBoard)
        {
            foreach (var item in bingoBoard)
            {
                Console.WriteLine(String.Join('\t', item.Select(x => x.Value)));
                Console.WriteLine(String.Join('\t', item.Select(x => x.Unlocked)));
            }
        }
        public static bool IsBingo(this bingoValue[][] board)
        {
            for (int i = 0; i < board[0].Length; i++)
            {
                var rowCount = board[i].Where(x => x.Unlocked).Count();
                var columnCount = Enumerable.Range(0, 5)
                    .Select(x => board[x][i])
                    .Where(x => x.Unlocked).Count();
                if (rowCount == 5 || columnCount == 5)
                {
                    return true;
                }

            }

            return false;
        }
        public static int BoardScore(this bingoValue[][] board)
        {
            int score = 0;
            foreach (var item in board)
            {
                score += item.Where(x => !x.Unlocked).Sum(x => x.Value);
            }
            return score;
        }
        public struct bingoValue
        {
            public byte Value { get; set; }
            public bool Unlocked { get; set; }
            public bingoValue(byte value)
            {
                Value = value;
                Unlocked = false;
            }
        }
    }
}
