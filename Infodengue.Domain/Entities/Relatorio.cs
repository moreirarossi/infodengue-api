namespace Infodengue.Domain.Entities
{
    public class Relatorio
    {
        public int Id { get; set; }
        public int SolicitanteId { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public string? Arbovirose { get; set; }
        public DateTime? SemanaInicio { get; set; }
        public DateTime? SemanaTermino { get; set; }
        public string? CodigoIBGE { get; set; }
        public string? Municipio { get; set; }
        public int? TotalCasos { get; set; }
    }
}
