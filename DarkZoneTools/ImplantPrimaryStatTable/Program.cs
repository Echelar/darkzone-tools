using System;

namespace DarkZoneTools
{
    class Program
    {
        static void Main(string[] args)
        {
            (double min, double max)[] statTable = 
            {
                (1, 3),
                (3, 6),
                (6, 9),
                (9, 12),
                (12, 15),
                (15, 18),
                (18, 21),
                (21, 24),
                (24, 27),
                (27, 30),
                (30, 33),
                (33, 36),
                (36, 40),
                (40, 45)
            };

            for (int tier = 1; tier <= 10; tier++)
            {
                Console.WriteLine($"!Tier {tier}");
                foreach (var statRange in statTable)
                {
                    var multiplier = 1.0 + (.25 * (tier - 1));
                    Console.WriteLine($"| {statRange.min * multiplier}-{statRange.max * multiplier}");
                }
                Console.WriteLine("|-");
            }
        }
    }
}
