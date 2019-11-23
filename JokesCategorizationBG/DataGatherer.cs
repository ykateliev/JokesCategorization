using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace JokesCategorizationBG
{
    public class DataGatherer
    {
        public List<JokeModel> GatherData(int startJokeId, int endJokeId)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var webClient = new WebClient { Encoding = Encoding.GetEncoding("windows-1251") };
            var parser = new HtmlParser();
            var jokes = new List<JokeModel>();

            for (int i = startJokeId; i < endJokeId; i++)
            {
                try
                {
                    var document = webClient.DownloadString("https://fun.dir.bg/vic_open.php?id=" + i);
                    var parsedDocument = parser.ParseDocument(document);
                    var text = parsedDocument.QuerySelectorAll("#newsbody");
                    var category = parsedDocument.QuerySelectorAll(".tag-links-left>a");

                    if (text.Length > 0)
                    {
                        var jokeText = text[0]
                        .TextContent
                        .TrimStart()
                        .Trim();

                        jokeText = Regex.Replace(jokeText, "[\n|\t]+", string.Empty);
                        jokeText = Regex.Replace(jokeText, "['|\"|.|,|,|+|-|?|!|:]+", " ");

                        var jokeCategory = category?[0].TextContent;

                        if (!string.IsNullOrWhiteSpace(jokeCategory))
                        {
                            jokes.Add(new JokeModel { Text = jokeText, Category = jokeCategory });
                            Console.WriteLine($"Add Joke with id:{i}");
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
