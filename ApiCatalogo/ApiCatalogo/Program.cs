using ApiCatalogo.Context; // Importa o namespace que cont�m o contexto de dados da aplica��o.
using Microsoft.EntityFrameworkCore; // Importa o namespace do Entity Framework Core.

var builder = WebApplication.CreateBuilder(args);

// Cria uma inst�ncia do aplicativo web usando o ASP.NET Core.

// Adiciona servi�os ao cont�iner de inje��o de depend�ncia.
builder.Services.AddControllers(); // Adiciona o servi�o de controle do MVC.
// Saiba mais sobre a configura��o do Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer(); // Adiciona suporte para a explora��o de endpoints (Swagger).
builder.Services.AddSwaggerGen(); // Configura o Swagger para gera��o de documenta��o da API.
builder.Services.AddControllers();

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
// Obt�m a string de conex�o do arquivo de configura��o "appsettings.json" com o nome "DefaultConnection".

builder.Services.AddDbContext<ApiCatalogoContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));
// Configura o Entity Framework Core para usar o MySQL com a string de conex�o "mySqlConnection" e a vers�o do servidor automaticamente detectada.

var app = builder.Build();

// Configura o pipeline de solicita��o HTTP.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Adiciona o middleware Swagger para a gera��o de documenta��o da API.
    app.UseSwaggerUI(); // Adiciona o middleware Swagger UI para explorar a documenta��o da API no navegador.
}

app.UseHttpsRedirection(); // Redireciona as solicita��es HTTP para HTTPS (SSL/TLS).

app.UseAuthorization(); // Adiciona middleware de autoriza��o.

app.MapControllers(); // Mapeia as rotas dos controladores para as a��es apropriadas.

app.Run(); // Inicia o aplicativo web e aguarda solicita��es.
