using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

await Name.MyClass.Part1();
await Name.MyClass.Part2();

namespace Name
{
    static class MyClass
    {
        public static async Task Part1()
        {
            var lines = await File.ReadAllLinesAsync("val.txt");
            var bitCount = new int[12];
            var gammaArray = new byte[12];
            var epsilonArray = new byte[12];
            foreach (var line in lines)
            {
                var token = line.ToCharArray();
                for (int i = 0; i < 12; i++)
                {
                    bitCount[i] += (int) Char.GetNumericValue(token[i]);
                }
            }

            int gamma = 0;
            int epsilon = 0;
            for (int i = 0; i < 12; i++)
            {
                switch ((lines.Length - bitCount[i]) > lines.Length / 2)
                {
                    case true:
                        epsilon += (int) Math.Pow(2, 11 - i);
                        epsilonArray[i] = 1;
                        gammaArray[i] = 0;
                        break;
                    default:
                        gamma += (int) Math.Pow(2, 11 - i);
                        epsilonArray[i] = 0;
                        gammaArray[i] = 1;
                        break;
                }
            }

            Console.WriteLine($"Power: {epsilon * gamma}");
        }

        public static async Task Part2()
        {
            var lines = await File.ReadAllLinesAsync("val.txt");
            var oxygen = new byte[lines.Length][];
            int i = 0;
            foreach (var line in lines)
            {
                var token = line.ToCharArray();
                oxygen[i] = new byte[12];
                for (int j = 0; j < 12; j++)
                {
                    oxygen[i][j] = (byte) Char.GetNumericValue(token[j]);
                }

                i++;
            }

            var co2 = new byte[oxygen.Length][];
            Array.Copy(oxygen, co2, oxygen.Length);
            int m = 0;
            while (m < 12)
            {
                var oxygenOneCount = oxygen.Count(x => x[m] == 1);
                var oxygenZeroCount = oxygen.Length - oxygenOneCount;
                if (oxygen.Length > 1)
                {
                    oxygen = oxygenOneCount >= oxygenZeroCount
                        ? oxygen
                            .Where(x => x[m] == 1).ToArray()
                        : oxygen.Where(x => x[m] == 0).ToArray();
                }


                var co2OneCount = co2.Count(x => x[m] == 1);
                var co2ZeroCount = co2.Length - co2OneCount;
                if (co2.Length > 1)
                {
                    co2 = co2OneCount >= co2ZeroCount
                        ? co2
                            .Where(x => x[m] == 0).ToArray()
                        : co2.Where(x => x[m] == 1).ToArray();
                }

                m++;
            }

            var k = 11;
            var o2Val = oxygen[0].Sum(t => (int) Math.Pow(t * 2, k--));
            k = 11;
            var co2Val = co2[0].Sum(t => (int) Math.Pow(t * 2, k--));
            Console.WriteLine($"Result {co2Val * o2Val}");
        }
    }
}