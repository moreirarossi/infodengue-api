using Infodengue.API.Controllers;
using Infodengue.Application.Command;
using Infodengue.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Infodengue.Tests.Controllers
{
    public class InfodengueControllerTests
    {
        private readonly IMediator _mediator;
        private readonly InfodengueController _controller;

        public InfodengueControllerTests()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new InfodengueController(_mediator);
        }

        [Fact]
        public async Task GetRelatorioAsync_ReturnsOkResult_WhenDataIsReturned()
        {
            // Arrange
            var request = new GetIBGECommand { CPF = "12345678900", Nome = "Mauro Rossi" };

            var fakeResponse = new List<RelatorioResponse>
                {
                    new RelatorioResponse { Municipio = "Testópolis", CodigoIBGE = "123456", Arbovirose = "Dengue" }
                };

            _mediator.Send(request, Arg.Any<CancellationToken>())
                .Returns(fakeResponse);

            // Act
            var result = await _controller.GetRelatorioAsync(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(fakeResponse, okResult.Value);
        }

        [Fact]
        public async Task GetRelatorioAsync_ReturnsNotFound_WhenKeyNotFoundExceptionThrown()
        {
            // Arrange
            var request = new GetIBGECommand { CPF = "12345678900", Nome = "Mauro Rossi" };

            _mediator.Send(request, Arg.Any<CancellationToken>())
                .Throws(new KeyNotFoundException());

            // Act
            var result = await _controller.GetRelatorioAsync(request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Dados não encontrados.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetRelatorioAsync_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var request = new GetIBGECommand { CPF = "12345678900", Nome = "Mauro Rossi" };

            _mediator.Send(request, Arg.Any<CancellationToken>())
                .Throws(new System.Exception("Erro inesperado"));

            // Act
            var result = await _controller.GetRelatorioAsync(request);

            // Assert
            var errorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, errorResult.StatusCode);
            Assert.Equal("Erro inesperado", errorResult.Value);
        }

        [Fact]
        public async Task GetSolicitantesAsync_ReturnsNotFound_WhenKeyNotFoundExceptionThrown()
        {
            // Arrange
            var query = new GetAllSolicitantesQuery();

            _mediator.Send(query, Arg.Any<CancellationToken>())
                .Throws(new KeyNotFoundException());

            // Act
            var response = await _controller.GetSolicitantesAsync(query);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Solicitantes não encontrados.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetSolicitantesAsync_ReturnsInternalServerError_WhenGenericExceptionThrown()
        {
            // Arrange
            var query = new GetAllSolicitantesQuery();

            _mediator.Send(query, Arg.Any<CancellationToken>())
                .Throws(new System.Exception("Erro inesperado"));

            // Act
            var response = await _controller.GetSolicitantesAsync(query);

            // Assert
            var errorResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(500, errorResult.StatusCode);
            Assert.Equal("Erro inesperado", errorResult.Value);
        }

        [Fact]
        public async Task GetPorArboviroseAsync_ReturnsNotFound_OnKeyNotFoundException()
        {
            // Arrange
            var request = new GetTotalCasosPorArboviroseQuery();
            _mediator.Send(request, Arg.Any<CancellationToken>())
                     .Throws(new KeyNotFoundException());

            // Act
            var result = await _controller.GetPorArboviroseAsync(request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Relatório não encontrado.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetPorArboviroseAsync_ReturnsServerError_OnException()
        {
            // Arrange
            var request = new GetTotalCasosPorArboviroseQuery();
            _mediator.Send(request, Arg.Any<CancellationToken>())
                     .Throws(new System.Exception("Erro interno"));

            // Act
            var result = await _controller.GetPorArboviroseAsync(request);

            // Assert
            var errorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, errorResult.StatusCode);
            Assert.Equal("Erro interno", errorResult.Value);
        }

        public class CasosPorArboviroseResponse
        {
            public string Arbovirose { get; set; }
            public int TotalCasos { get; set; }
        }

    }
}
