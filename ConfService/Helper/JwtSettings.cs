namespace ConfService.Helper
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int LifetimeSeconds { get; set; }
    }
}
