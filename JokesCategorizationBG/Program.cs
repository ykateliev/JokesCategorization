using System;
using System.IO;

namespace JokesCategorizationBG
{
    class Program
    {
        public static void Main(string[] args)
        {
            var jokes = new DataGatherer().GatherData(1, 50000).Result;

            Console.WriteLine("---- Creating data file. ----");
            Console.WriteLine($"{jokes.Count} jokes.");

            using (var writer = new StreamWriter("data.csv"))
            {
                writer.WriteLine("text, category");
                foreach (var joke in jokes)
                {
                    writer.WriteLine(string.Join(',', joke.Text, joke.Category));
                }
            }

            Console.WriteLine("---- Data file is ready. ----");
        }
    }
}
