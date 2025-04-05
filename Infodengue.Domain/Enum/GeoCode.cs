namespace Infodengue.Domain.Enum
{
    public static class GeoCode
    {
        private static readonly Dictionary<string, string> _mapaGeoCode = new()
        {
            { "3304557", "Rio de Janeiro" },
            { "3550308", "São Paulo" }
        };

        public static IEnumerable<string> GeoCodes => _mapaGeoCode.Keys;

        public static string? ObterMunicipio(string geocode)
        {
            return _mapaGeoCode.TryGetValue(geocode, out var nome) ? nome : null;
        }
    }
}
