namespace SalesPrediction.Settings
{
    public class JWTSettings
    {
        public string SecretKey { get; set; } = default!;
        public string Issuer { get; set; } = default!;  
        public string Audience { get; set; } = default!;
        public double ExpirationTime { get; set; }
    }
}
