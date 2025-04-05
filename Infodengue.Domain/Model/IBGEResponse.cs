namespace Infodengue.Domain.Model
{
    public class IBGEResponse
    {
        public long Id { get; set; }
        public long? DataIniSE { get; set; }
        public int? SE { get; set; }
        public double? CasosEst { get; set; }
        public int CasosEstMin { get; set; }
        public int? CasosEstMax { get; set; }
        public int? Casos { get; set; }
        public double? PRt1 { get; set; }
        public double? PInc100k { get; set; }
        public int? LocalidadeId { get; set; }
        public int? Nivel { get; set; }
        public string? VersaoModelo { get; set; }
        public double? Tweet { get; set; }
        public double? Rt { get; set; }
        public double? Pop { get; set; }
        public double? TempMin { get; set; }
        public double? UmidMax { get; set; }
        public int? Receptivo { get; set; }
        public int? Transmissao { get; set; }
        public int? NivelInc { get; set; }
        public double? UmidMed { get; set; }
        public double? UmidMin { get; set; }
        public double? TempMed { get; set; }
        public double? TempMax { get; set; }
        public int? CasProv { get; set; }
        public int? CasProvEst { get; set; }
        public int? CasProvEstMin { get; set; }
        public int? CasProvEstMax { get; set; }
        public int? CasConf { get; set; }
        public int? NotifAccumYear { get; set; }
    }
}