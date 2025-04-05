using Infodengue.Domain.Model;
using Refit;

public interface IIBGEService
{
    [Get("/api/alertcity")]
    Task<IEnumerable<IBGEResponse>> ConsultarRelatorioAsync(
        [Query] string geocode,
        [Query] string disease,
        [Query] string format,
        [Query] int ew_start,
        [Query] int ew_end,
        [Query] int ey_start,
        [Query] int ey_end
    );
}
