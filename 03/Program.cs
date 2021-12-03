await Name.MyClass.Part2();

namespace Name
{
    class MyClass
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
                    bitCount[i] += (int)Char.GetNumericValue(token[i]);
                }
            }
            int gamma = 0;
            int epsilon = 0;
            Console.WriteLine(String.Join(' ', bitCount));
            for (int i = 0; i < 12; i++)
            {
                switch ((lines.Length - bitCount[i]) > lines.Length / 2)
                {
                    case true:
                        epsilon += (int)Math.Pow(2, 11 - i);
                        epsilonArray[i] = 1;
                        gammaArray[i] = 0;
                        break;
                    default:
                        gamma += (int)Math.Pow(2, 11 - i);
                        epsilonArray[i] = 0;
                        gammaArray[i] = 1;
                        break;
                }

            }
            Console.WriteLine($"Gamma: {gamma},\t{String.Join(' ', gammaArray[..])}");
            Console.WriteLine($"Espilon: {epsilon},\t{String.Join(' ', epsilonArray[..])}");
            Console.WriteLine($"Power: {epsilon * gamma}");

        }
        public static async Task Part2()
        {
            var lines = await File.ReadAllLinesAsync("val.txt");
            var bitCount = new int[12];
            var gammaArray = new byte[12];
            var epsilonArray = new byte[12];
            var oxygen = new byte[lines.Length][];
            int i = 0;
            foreach (var line in lines)
            {
                var token = line.ToCharArray();
                oxygen[i] = new byte[12];
                for (int j = 0; j < 12; j++)
                {
                    oxygen[i][j] = (byte)Char.GetNumericValue(token[j]);
                }
                i++;
            }
            var co2 = new byte[oxygen.Length][];
            Array.Copy(oxygen, co2, oxygen.Length);
            int m = 0;
            while (m < 12)
            {
                var oxygenOneCount = oxygen.Where(x => x[m] == 1).Count();
                var oxygenZeroCount = oxygen.Length - oxygenOneCount;
                if (oxygen.Length > 1)
                {
                    if (oxygenOneCount >= oxygenZeroCount)
                    {
                        oxygen = oxygen.Where(x => x[m] == 1).ToArray();
                    }
                    else
                    {
                        oxygen = oxygen.Where(x => x[m] == 0).ToArray();

                    }

                }



                var co2OneCount = co2.Where(x => x[m] == 1).Count();
                var co2ZeroCount = co2.Length - co2OneCount;
                if (co2.Length > 1)
                {
                    if (co2OneCount >= co2ZeroCount)
                    {
                        co2 = co2.Where(x => x[m] == 0).ToArray();
                    }
                    else
                    {
                        co2 = co2.Where(x => x[m] == 1).ToArray();

                    }

                }
                m++;
            }
            var o2val = 0;
            var k = 11;
            foreach (var t in oxygen[0])
            {
                o2val += (int)Math.Pow(t*2, k--);
            }
            Console.WriteLine(String.Join(' ', oxygen[0]));
            Console.WriteLine(o2val);
            var co2val = 0;
            k = 11;
            foreach (var t in co2[0])
            {
                co2val += (int)Math.Pow(t*2, k--);
            }
            Console.WriteLine(String.Join(' ', co2[0]));
            Console.WriteLine(co2val);
            Console.WriteLine(co2val * o2val);

        }
    }
}