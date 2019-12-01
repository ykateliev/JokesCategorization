using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JokesCategorizationML.Model;
using Microsoft.ML;

namespace JokesCategorizationBG
{
    class Program
    {
        public static void Main(string[] args)
        {
            var dataFile = "data.csv";
            if (!File.Exists(dataFile))
            {
                var jokes = new DataGatherer().GatherData(1, 48000);
                var cleaner = new DataCleaner();
                cleaner.RemoveJokesFromCategoryRazni(jokes);
                cleaner.GetJokesMoreThan20InCategory(jokes);




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
            }

            var testModelData = new List<string>
                                    {
                                        "Попитали Радио Ереван - Какво е парадокс?- Това е, когато щастието чука на вратата ти, а ти се оплакваш от шума!",
                                        "Преди ерата на мобилните телефони:- Скъпи, кой звъня?- Ами да ти кажа честно не разбрах, някъв синоптик...- Е, как синоптик!? Ти, защо така реши?- Ами питаше: -Как е, слънце, чист ли е вече хоризонтът ? ",
                                        "- Докторе, как е съпругът ми?- Съжалявам, почина. Ще дарите ли някой орган?- Какъв орган, докторе? Той свиреше на цигулка...",
                                        "Заедно умират поп и шофьор на такси. Господ посреща с най-големи почести шофьора. Попът се чувства засегнат: Господи, защо така.. цял живот съм ти служил? Господ му отговаря: Да, но на твоите проповеди всички спяха.. а когато той, караше всички се молеха и кръстеха..",
                                        "И се яви Бог на Ной и му каза: Прави бекъп, че ще форматирам!.",
                                        "На пейката пред входа:- Сийо, ти разбра ли, че преместили края на света за 2043...- Ми сега, че то аз няма да го доживея..",
                                    };

            var modelFile = "MLModels//MLModel.zip";
            TestModel(modelFile, testModelData);
        }

        private static void TestModel(string modelFile, IEnumerable<string> testModelData)
        {
            var context = new MLContext();
            var model = context.Model.Load(modelFile, out _);
            var predictionEngine = context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);

            foreach (var testData in testModelData)
            {
                var prediction = predictionEngine.Predict(new ModelInput { Text = testData });
                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"Content: {testData}");
                Console.WriteLine($"Prediction: {prediction.Prediction}");
                Console.WriteLine($"Score: {prediction.Score.Max()}");
            }
        }
    }
}
