using System.Collections.Generic;
using System.Linq;

namespace JokesCategorizationBG
{
    public class DataCleaner
    {
        public List<JokeModel> GetJokesMoreThan20InCategory(List<JokeModel> jokes)
        {
            var categories = jokes.Select(jm => jm.Category).Distinct().ToList();

            foreach (var category in categories)
            {
                var jokesFromCategory = from joke in jokes
                                        where joke.Category == category
                                        select joke;

                if (jokesFromCategory.ToList().Count < 2)
                {
                    jokes.RemoveAll(x => x.Category == category);
                }
            }

            return jokes;
        }

        public List<JokeModel> RemoveJokesFromCategoryRazni(List<JokeModel> jokes)
        {
            jokes.RemoveAll(x => x.Category.ToLower() == "разни");
            return jokes;
        }
    }
}
