using Infodengue.Application.Command;
using Infodengue.Application.Validations;
using Infodengue.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Infodengue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfodengueController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ConsultarIBGEValidator _validator;

        public InfodengueController(IMediator mediator)
        {
            _mediator = mediator;
            _validator = new ConsultarIBGEValidator();
        }

        /// <summary>
        /// Consulta relatórios do IBGE
        /// </summary>
        /// <param name="cpf">CPF do solicitante</param>
        /// <param name="nome">Código do IBGE do Município</param>
        /// <param arbovirose="arbovirose">Tipo de Arbovirose</param>
        /// <param name="semana-inicio">Semana de início da pesquisa</param>
        /// <param name="semana-termino">Semana de término da pesquisa</param>
        /// <param codigoIBGE="codigo-ibge">Código do município no IBGE</param>
        /// <response code="200">Retorna relatório</response>
        /// <response code="400">Dados da requisição inválidos</response>
        /// <response code="404">Relatório não encontrado</response>
        /// <response code="500">Erro interno</response>
        [HttpPost("relatorios")]
        [ProducesResponseType(typeof(IEnumerable<CreateIBGEResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRelatorioAsync([FromBody] CreateIBGECommand request)
        {
            try
            {
                var relatorio = await _mediator.Send(request);
                return Ok(relatorio);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Dados não encontrados.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Consulta lista de solicitantes
        /// </summary>
        /// <response code="200">Retorna listagem</response>
        /// <response code="400">Dados da requisição inválidos</response>
        /// <response code="404">Nenhum solicitante encontrado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("solicitantes")]
        [ProducesResponseType(typeof(IEnumerable<SolicitanteResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSolicitantesAsync([FromQuery] GetAllSolicitantesQuery request)
        {
            try
            {
                var relatorio = await _mediator.Send(request);
                return Ok(relatorio);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Solicitantes não encontrados.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Listar casos por arbovirose
        /// </summary>
        /// <response code="200">Retorna listagem</response>
        /// <response code="400">Dados da requisição inválidos</response>
        /// <response code="404">Nenhum relatório encontrado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("relatorio-por-arbovirose")]
        [ProducesResponseType(typeof(IEnumerable<CasosPorArboviroseResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPorArboviroseAsync([FromQuery] GetTotalCasosPorArboviroseQuery request)
        {
            try
            {
                var relatorio = await _mediator.Send(request);
                return Ok(relatorio);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Relatório não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Listar casos por município
        /// </summary>
        /// <response code="200">Retorna listagem</response>
        /// <response code="400">Dados da requisição inválidos</response>
        /// <response code="404">Nenhum relatório encontrado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("relatorio-por-municipio")]
        [ProducesResponseType(typeof(IEnumerable<CasosPorMunicipioResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPorMunicipioAsync([FromQuery] GetCasosPorMunicipioQuery request)
        {
            try
            {
                var relatorio = await _mediator.Send(request);
                return Ok(relatorio);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Relatório não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Listar casos por código IBGE
        /// </summary>
        /// <response code="200">Retorna listagem</response>
        /// <response code="400">Dados da requisição inválidos</response>
        /// <response code="404">Nenhum relatório encontrado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("relatorio-por-codigo-ibge")]
        [ProducesResponseType(typeof(IEnumerable<CasosPorCodigoIBGEResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPorCodigoIBGEAsync([FromQuery] GetCasosPorCodigoIBGEQuery request)
        {
            try
            {
                var relatorio = await _mediator.Send(request);
                return Ok(relatorio);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Relatório não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    
         /// <summary>
        /// Listar casos por código IBGE
        /// </summary>
        /// <response code="200">Retorna listagem</response>
        /// <response code="400">Dados da requisição inválidos</response>
        /// <response code="404">Nenhum relatório encontrado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("relatorio-dados-rj-sp")]
        [ProducesResponseType(typeof(IEnumerable<GetIBGEDadosQuery>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRioSaoPaulo([FromQuery] GetIBGEDadosQuery request)
        {
            try
            {
                var relatorio = await _mediator.Send(request);
                return Ok(relatorio);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Relatório não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
