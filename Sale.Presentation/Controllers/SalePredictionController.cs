using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SalesPrediction.Entities;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sale.Presentation.Controllers
{
    public class SalePredictionController : Controller
    {
        private readonly HttpClient _httpClient;

        public SalePredictionController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult SalePrediction()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PredictSale(BookSaleData bookSale)
        {
            if (ModelState.IsValid)
            {
                var apiUrl = "https://localhost:7127/api/prediction";
                string json = JsonConvert.SerializeObject(bookSale);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Predicted successful.";
                    return RedirectToAction("PredictSale", "SalePrediction");
                }            
            }
            return View();

        }
    }
}
