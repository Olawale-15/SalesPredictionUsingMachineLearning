using Microsoft.ML;
using Microsoft.ML.Data;
using SalesPrediction.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace SalesPrediction.TrainedModel
{
    public class SaleModelTrainer
    {
        private static string modelDir = "MLModels";
        private static string modelPath = Path.Combine(modelDir, "BookSalesModel.zip");

        public static void TrainModel()
        {
            var mlContext = new MLContext();

            var data = new List<BookSaleData>
    {
        new BookSaleData { BookTitle = "Data Management", Discount = 10, Author = "Ade", Sales = 200 },
        new BookSaleData { BookTitle = "Operating System", Discount = 5, Author = "Ola" , Sales = 300},
        new BookSaleData { BookTitle = "Linear Algebra", Discount = 3, Author = "Samuel", Sales = 400 }
    };

            var dataView = mlContext.Data.LoadFromEnumerable(data);
            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding("BookTitleEncoded", "BookTitle")
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("AuthorEncoded", "Author")) // Include Author
                .Append(mlContext.Transforms.NormalizeMeanVariance("DiscountNormalized", "Discount"))
                .Append(mlContext.Transforms.Concatenate("Features", "BookTitleEncoded", "AuthorEncoded", "DiscountNormalized"))
                .Append(mlContext.Regression.Trainers.LbfgsPoissonRegression(labelColumnName: "Label")); // Use Sales as label

            var model = pipeline.Fit(dataView);

            // Save the model
            if (!Directory.Exists(modelDir))
            {
                Directory.CreateDirectory(modelDir);
            }

            mlContext.Model.Save(model, dataView.Schema, modelPath);
            Console.WriteLine($" Model saved at: {modelPath}");

            // TESTING MODEL HERE FOR PROPER TRAINING 
            var predictionEngine = mlContext.Model.CreatePredictionEngine<BookSaleData, BookSalePrediction>(model);
            var testSample = new BookSaleData { BookTitle = "Operating System", Discount = 5, Author = "Ola" };
            var prediction = predictionEngine.Predict(testSample);

            Console.WriteLine($" Predicted Sales: {prediction.PredictedSales}");
        }
    }
}
