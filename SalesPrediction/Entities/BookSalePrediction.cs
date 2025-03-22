using Microsoft.ML.Data;

namespace SalesPrediction.Entities
{
    public class BookSalePrediction
    {
        [ColumnName("Score")]
        public float PredictedSales { get; set; }
    }
}
