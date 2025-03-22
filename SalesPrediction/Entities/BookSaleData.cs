using Microsoft.ML.Data;

namespace SalesPrediction.Entities
{
    public class BookSaleData
    {
        [LoadColumn(0)]
        public string BookTitle { get; set; } = default!;
        [LoadColumn(1)]
        public float Discount { get; set; } = default!;
        //public bool IsHoliday { get; set; }  
        [LoadColumn(2)]
        public string Author { get; set; } = default!;
        [LoadColumn(3), ColumnName("Label")]
        public float Sales { get; set; }
    }
}
