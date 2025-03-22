using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using SalesPrediction.Entities;

[Route("api/prediction")]
[ApiController]
public class PredictionController : ControllerBase
{
    private static string modelPath = Path.Combine(Directory.GetCurrentDirectory(), "MLModels", "BookSalesModel.zip");

    [HttpPost]
    public IActionResult Predict([FromBody] BookSaleData input)
    {
        if (!System.IO.File.Exists(modelPath))
        {
            return NotFound(new { message = "Trained model not found!" });
        }

        var mlContext = new MLContext();
        ITransformer trainedModel = mlContext.Model.Load(modelPath, out _);
        var predictionEngine = mlContext.Model.CreatePredictionEngine<BookSaleData, BookSalePrediction>(trainedModel);

        var prediction = predictionEngine.Predict(input);

        return Ok(new { predictedSales = prediction.PredictedSales });
    }
}
