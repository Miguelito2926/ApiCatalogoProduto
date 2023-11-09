using ApiCatalogo.Context; // Importa o namespace que contém o contexto de dados da aplicação.
using Microsoft.EntityFrameworkCore; // Importa o namespace do Entity Framework Core.
using System.Text.Json.Serialization;


// Cria um construtor de aplicativo web usando a classe WebApplication, 
// passando os argumentos fornecidos à aplicação.
var builder = WebApplication.CreateBuilder(args);

// Adiciona o serviço de controle do MVC.
//Defini como o JsonSerializer lida com referências sobre serialização e desserialização
//Ignora o objeto quando um ciclo de referência é detectado durante a serialização
builder.Services.AddControllers().AddJsonOptions(options => 
{ options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

// Adiciona suporte para a exploração de endpoints (Swagger).
builder.Services.AddEndpointsApiExplorer();

// Configura o Swagger para geração de documentação da API.
builder.Services.AddSwaggerGen();

// Obtém a string de conexão do arquivo de configuração "appsettings.json" com o nome "DefaultConnection".
string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

// Configura o Entity Framework Core para usar o MySQL com a string de conexão "mySqlConnection" e a versão do servidor automaticamente detectada.
builder.Services.AddDbContext<ApiCatalogoContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

// Configura o pipeline de solicitação HTTP.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Adiciona o middleware Swagger para a geração de documentação da API.
    app.UseSwaggerUI(); // Adiciona o middleware Swagger UI para explorar a documentação da API no navegador.
}

app.UseHttpsRedirection(); // Redireciona as solicitações HTTP para HTTPS (SSL/TLS).

app.UseAuthorization(); // Adiciona middleware de autorização.

app.MapControllers(); // Mapeia as rotas dos controladores para as ações apropriadas.

app.Run(); // Inicia o aplicativo web e aguarda solicitações.
