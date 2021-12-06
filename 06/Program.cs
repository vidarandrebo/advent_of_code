var fishCount = Name.Fish.Run();
Console.WriteLine($"FishCount: {fishCount}");
namespace Name
{
    public static class Fish {
        public static long Run() {
            var file = File.ReadAllLines("val.txt");
            var fishDict = new Dictionary<int, long>();
            var fish = file[0]
                .Split(',')
                .Select(x => byte.Parse(x))
                .ToList();
            for (int i = 0; i < 9; i++) {
                fishDict[i] = 0;
            }
            foreach (var value in fish) {
                fishDict[value]++;
            }
            for (int i = 0; i < 256; i++) {
                fishDict = NextDay(fishDict);
            }
            long totalCount = 0;
            for (int i = 0; i < 9; i++) {
                totalCount += fishDict[i];
            }
            return totalCount;
            }
            public static Dictionary<int, long> NextDay(Dictionary<int, long> fish) {
                long newFish = 0;
                var newDict = new Dictionary<int, long>();
                for (int i = 8; i > 0; i--) {
                    newDict[i-1] = fish[i];
                }
                newFish = fish[0];
                newDict[6] += fish[0];
                newDict[8] = newFish;
                return newDict;
            }
    } 
}