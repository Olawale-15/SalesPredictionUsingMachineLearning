using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using SalesPrediction.Entities;
using System;
using System.IO;

[Route("api/prediction")]
[ApiController]
public class PredictionController : ControllerBase
{
    private static string salesModelPath = Path.Combine(Directory.GetCurrentDirectory(), "MLModels", "BookSalesModel.zip");
    private static string priceModelPath = Path.Combine(Directory.GetCurrentDirectory(), "MLModels", "BookPriceModel.zip");

    [HttpPost]
    public IActionResult Predict([FromBody] BookSaleData input)
    {
        if (!System.IO.File.Exists(salesModelPath) || !System.IO.File.Exists(priceModelPath))
        {
            return NotFound(new { message = "Trained model not found!" });
        }

        var mlContext = new MLContext();

        // Load Sales Prediction Model
        ITransformer salesModel = mlContext.Model.Load(salesModelPath, out _);
        var salesPredictionEngine = mlContext.Model.CreatePredictionEngine<BookSaleData, BookSalePrediction>(salesModel);
        var salesPrediction = salesPredictionEngine.Predict(input);

        // Load Price Prediction Model
        //ITransformer priceModel = mlContext.Model.Load(priceModelPath, out _);
        //var pricePredictionEngine = mlContext.Model.CreatePredictionEngine<BookSaleData, BookSalePrediction>(priceModel);
        //var pricePrediction = pricePredictionEngine.Predict(input);

        return Ok(new
        {
            message = $"Your prediction for the next sales is {Math.Round(salesPrediction.PredictedSales)}",
            predictedSales = Math.Round(salesPrediction.PredictedSales)
        });

    }
}
