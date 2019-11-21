using AngleSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace JokesCategorizationBG
{
    public class DataGatherer
    {
        public async Task<List<JokeModel>> GatherData(int startJokeId, int endJokeId)
        {
            var jokes = new List<JokeModel>();
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);

            for (int i = startJokeId; i < endJokeId; i++)
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
                        .TrimStart()
                        .Trim();

                        var category = document.QuerySelectorAll(".tag-links-left>a");
                        var jokeCategory = category?[0].TextContent;

                        if (!string.IsNullOrWhiteSpace(jokeCategory) && jokeCategory.ToLower() != "разни")
                        {
                            jokes.Add(new JokeModel { Text = jokeText, Category = jokeCategory });
                            Console.WriteLine($"Add Joke with Id:{i}");
                        }
                    }
                }
                catch (Exception e)
                {
                    continue;
                }
            }

            return jokes;
        }
    }
}
