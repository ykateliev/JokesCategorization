// This file was auto-generated by ML.NET Model Builder. 

using Microsoft.ML.Data;

namespace JokesCategorizationML.Model
{
    public class ModelInput
    {
        [ColumnName("category"), LoadColumn(0)]
        public string Category { get; set; }


        [ColumnName("text"), LoadColumn(1)]
        public string Text { get; set; }


    }
}