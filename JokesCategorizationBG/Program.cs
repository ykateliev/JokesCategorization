using System;
using System.Collections.Generic;
using System.IO;

namespace JokesCategorizationBG
{
    class Program
    {
        public static void Main(string[] args)
        {
            var jokes = new DataGatherer().GatherData(1, 48000);
            var cleaner = new DataCleaner();
            cleaner.RemoveJokesFromCategoryRazni(jokes);
            cleaner.GetJokesMoreThan20InCategory(jokes);
            

            var dataFile = "data.csv";

            Console.WriteLine("---- Creating data file. ----");
            Console.WriteLine($"{jokes.Count} jokes.");

            using (var writer = new StreamWriter(dataFile))
            {
                writer.WriteLine("category, text");
                foreach (var joke in jokes)
                {
                    writer.WriteLine($"{joke.Category}, \"{joke.Text}\"");
                }
            }

            Console.WriteLine("---- Data file is ready. ----");
            Console.WriteLine("done");
        }
    }
}
