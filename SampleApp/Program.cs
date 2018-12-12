using Microsoft.ML.Legacy;
using Microsoft.ML.Legacy.Data;
using Microsoft.ML.Legacy.Models;
using Microsoft.ML.Legacy.Trainers;
using Microsoft.ML.Legacy.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var pipeline = new LearningPipeline();

            pipeline.Add(new TextLoader("SalaryData.csv").CreateFrom<SalaryData>(useHeader: true, separator: ','));
            pipeline.Add(new ColumnConcatenator("Features", "YearsExperience"));
            pipeline.Add(new GeneralizedAdditiveModelRegressor());

            var model = pipeline.Train<SalaryData, SalaryPrediction>();

            var testData = new TextLoader("SalaryData-test.csv").CreateFrom<SalaryData>(useHeader: true, separator: ',');

            var evaluator = new RegressionEvaluator();
            var metrics = evaluator.Evaluate(model, testData);

            Console.WriteLine("RMS - " + metrics.Rms);
            Console.WriteLine("R^2 - " + metrics.RSquared);

            float yearsExperience = 7;

            var prediction = model.Predict(new SalaryData() { YearsExperience = yearsExperience });

            Console.WriteLine("Salary prediction for employee with " + yearsExperience + " years experience is " + prediction.PredictedSalary);

            Console.ReadLine();

        }
    }
}
