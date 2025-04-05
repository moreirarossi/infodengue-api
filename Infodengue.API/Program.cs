using FluentValidation;
using FluentValidation.AspNetCore;
using Infodengue.Application.Mapping;
using Infodengue.Application.Validations;
using Infodengue.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Refit;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
{
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ConsultarIBGEValidator).Assembly));
    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings")));
    services.AddFluentValidationAutoValidation();
    services.AddValidatorsFromAssemblyContaining<ConsultarIBGEValidator>();
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddAutoMapper(typeof(IBGEProfile).Assembly);
    builder.Services.AddRefitClient<IIBGEService>().ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("https://info.dengue.mat.br");
    });
}

// Adicionar serviços de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevServer", policy =>
    {
        policy.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseCors("AllowAngularDevServer");

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
}

app.Run();