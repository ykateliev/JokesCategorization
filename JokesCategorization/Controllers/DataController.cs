namespace JokesCategorization.Controllers
{
    using AngleSharp;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public class DataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetData()
        {
            int maxJokeId = 50000;
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);

            var lines = new List<string>();

            for (int i = 0; i < maxJokeId; i++)
            {
                try
                {
                    var document = await context.OpenAsync("https://fun.dir.bg/vic_open.php?id=" + i);

                    var text = document.QuerySelectorAll("#newsbody");

                    if (text.Length > 0)
                    {
                        var jokeText = text[0]
                        .TextContent
                        .Replace("\n", string.Empty)
                        .Replace("\t", string.Empty)
                        .Replace('"', '\'')
                        .TrimStart();

                        var category = document.QuerySelectorAll(".tag-links-left>a");

                        if (category.Length > 0)
                        {
                            var jokeCategory = category[0].TextContent;

                            lines.Add(string.Join(',', $"\"{jokeText}\"", $"\"{jokeCategory}\""));
                            Console.WriteLine($"Add Joke with Id:{i}");
                        }
                    }
                }
                catch (Exception e)
                {
                    continue;
                }
            }

            Console.WriteLine("---- Creating data file. ----");
            using (var writer = new StreamWriter("data.csv"))
            {
                writer.WriteLine("\"text\", \"category\"");
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }


            return View();
        }
    }
}