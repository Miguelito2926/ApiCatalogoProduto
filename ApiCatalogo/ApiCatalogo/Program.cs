using ApiCatalogo.Context; // Importa o namespace que cont�m o contexto de dados da aplica��o.
using Microsoft.EntityFrameworkCore; // Importa o namespace do Entity Framework Core.
using System.Text.Json.Serialization;


// Cria um construtor de aplicativo web usando a classe WebApplication, 
// passando os argumentos fornecidos � aplica��o.
var builder = WebApplication.CreateBuilder(args);

// Adiciona o servi�o de controle do MVC.
//Defini como o JsonSerializer lida com refer�ncias sobre serializa��o e desserializa��o
//Ignora o objeto quando um ciclo de refer�ncia � detectado durante a serializa��o
builder.Services.AddControllers().AddJsonOptions(options => 
{ options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

// Adiciona suporte para a explora��o de endpoints (Swagger).
builder.Services.AddEndpointsApiExplorer();

// Configura o Swagger para gera��o de documenta��o da API.
builder.Services.AddSwaggerGen();

// Obt�m a string de conex�o do arquivo de configura��o "appsettings.json" com o nome "DefaultConnection".
string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

// Configura o Entity Framework Core para usar o MySQL com a string de conex�o "mySqlConnection" e a vers�o do servidor automaticamente detectada.
builder.Services.AddDbContext<ApiCatalogoContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

// Configura o pipeline de solicita��o HTTP.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Adiciona o middleware Swagger para a gera��o de documenta��o da API.
    app.UseSwaggerUI(); // Adiciona o middleware Swagger UI para explorar a documenta��o da API no navegador.
}

app.UseHttpsRedirection(); // Redireciona as solicita��es HTTP para HTTPS (SSL/TLS).

app.UseAuthorization(); // Adiciona middleware de autoriza��o.

app.MapControllers(); // Mapeia as rotas dos controladores para as a��es apropriadas.

app.Run(); // Inicia o aplicativo web e aguarda solicita��es.
