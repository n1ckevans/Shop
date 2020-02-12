namespace Shop.Domain.Infrastructure
{
    public static class DecimalExtensions
    {
        public static string GetPriceString(this decimal price) =>
            $"${price.ToString("N2")}";
    }
}
