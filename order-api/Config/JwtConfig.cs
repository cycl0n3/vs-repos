namespace order_api.Config
{
    public class JwtConfig
    {
        public string Secret { get; set; } = String.Empty;
        public String Issuer { get; set; } = String.Empty;
        public String Audience { get; set; } = String.Empty;
    }
}
