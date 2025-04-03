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
        private static string salesModelPath = Path.Combine(modelDir, "BookSalesModel.zip");

        public static void TrainModels()
        {
            var mlContext = new MLContext();

            var data = new List<BookSaleData>
            {
                new BookSaleData { BookTitle = "Data Management", Discount = 10, Author = "Ade", Sales = 200},
                new BookSaleData { BookTitle = "Operating System", Discount = 5, Author = "Ola", Sales = 300 },
                new BookSaleData { BookTitle = "Linear Algebra", Discount = 3, Author = "Samuel", Sales = 400}
            };

            var dataView = mlContext.Data.LoadFromEnumerable(data);

            // ✅ Feature Engineering - Removed 'Price' since we're not predicting it
            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding("BookTitleEncoded", "BookTitle")
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("AuthorEncoded", "Author"))
                .Append(mlContext.Transforms.NormalizeMeanVariance("DiscountNormalized", "Discount"))
                .Append(mlContext.Transforms.NormalizeMeanVariance("SalesNormalized", "Sales"))
                .Append(mlContext.Transforms.Concatenate("Features",
                    "BookTitleEncoded", "AuthorEncoded", "DiscountNormalized", "SalesNormalized"));

            // ✅ **Sales Prediction Model (Predicting Sales)**
            var salesPipeline = pipeline
                .Append(mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "Sales"))
                .Append(mlContext.Regression.Trainers.LbfgsPoissonRegression());

            var salesModel = salesPipeline.Fit(dataView);
            mlContext.Model.Save(salesModel, dataView.Schema, salesModelPath);
            Console.WriteLine($"Sales Model saved at: {salesModelPath}");
        }

        // ✅ **Predict Sales and Round Up**
        public static int PredictSales(MLContext mlContext, BookSaleData newData)
        {
            ITransformer loadedModel = mlContext.Model.Load(salesModelPath, out var modelSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<BookSaleData, BookSalePrediction>(loadedModel);
            var prediction = predEngine.Predict(newData);

            return (int)Math.Ceiling(prediction.PredictedSales); // Rounds up to nearest integer
        }
    }
}