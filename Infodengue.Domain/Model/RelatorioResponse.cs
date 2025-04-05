namespace Infodengue.Domain.Model
{
    public class RelatorioResponse
    {
        public string? Arbovirose { get; set; }
        public DateTime? SemanaInicio { get; set; }
        public DateTime? SemanaTermino { get; set; }
        public string? CodigoIBGE { get; set; }
        public string? Municipio { get; set; }
        public long? TotalCasos { get; set; }
    }


}