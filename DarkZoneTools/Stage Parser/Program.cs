using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Stage_Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Send in the filepath to the source file.");
                return;
            }
            string fileName = args.FirstOrDefault();
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"File {fileName} does not exist.");
                return;
            }

            string[] fileContents = File.ReadAllLines(fileName);
            var parsedFileContents = fileContents.Select(l => ParseLine(l)).Where(l => !String.IsNullOrEmpty(l)).ToArray();
            var destFileName = Path.Combine(Path.GetDirectoryName(fileName),$"{Path.GetFileNameWithoutExtension(fileName)}-parsed{Path.GetExtension(fileName)}");
            
            File.WriteAllLines(destFileName, parsedFileContents);
            Console.WriteLine("Success");
        }

        static string ParseLine(string input)
        {
            if (String.IsNullOrWhiteSpace(input)) return null;
            if (!Char.IsDigit(input.First())) return input;

            var stages = input.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(s =>
                {
                    int.TryParse(s, out int stage);
                    return stage;
                }).ToList();

            var finalOutput = new List<(int beginStage, int endStage)>();
            foreach (var stage in stages)
            {
                if (finalOutput.Any())
                {
                    var lastRange = finalOutput.Last();
                    if (lastRange.endStage + 1 == stage)
                    {
                        finalOutput.Remove(lastRange);
                        finalOutput.Add((lastRange.beginStage, stage));
                        continue;
                    }
                }
                finalOutput.Add((stage, stage));
            }
            return String.Join("<br/>", finalOutput.Select(r => r.beginStage == r.endStage ? r.beginStage.ToString() : $"{r.beginStage} - {r.endStage}"));
        }
    }
}
