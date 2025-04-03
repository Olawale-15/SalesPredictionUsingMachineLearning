namespace SalesPrediction.Response
{
    public class TraderResponseModel
    {
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;
        public ICollection<string> Roles { get; set; } = new List<string>();
    }
}
