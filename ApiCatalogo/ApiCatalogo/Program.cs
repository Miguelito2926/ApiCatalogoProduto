using ApiCatalogo.Context; // Importa o namespace que contém o contexto de dados da aplicação.
using Microsoft.EntityFrameworkCore; // Importa o namespace do Entity Framework Core.

var builder = WebApplication.CreateBuilder(args);

// Cria uma instância do aplicativo web usando o ASP.NET Core.

// Adiciona serviços ao contêiner de injeção de dependência.
builder.Services.AddControllers(); // Adiciona o serviço de controle do MVC.
// Saiba mais sobre a configuração do Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer(); // Adiciona suporte para a exploração de endpoints (Swagger).
builder.Services.AddSwaggerGen(); // Configura o Swagger para geração de documentação da API.
builder.Services.AddControllers();

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
// Obtém a string de conexão do arquivo de configuração "appsettings.json" com o nome "DefaultConnection".

builder.Services.AddDbContext<ApiCatalogoContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));
// Configura o Entity Framework Core para usar o MySQL com a string de conexão "mySqlConnection" e a versão do servidor automaticamente detectada.

var app = builder.Build();

// Configura o pipeline de solicitação HTTP.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Adiciona o middleware Swagger para a geração de documentação da API.
    app.UseSwaggerUI(); // Adiciona o middleware Swagger UI para explorar a documentação da API no navegador.
}

app.UseHttpsRedirection(); // Redireciona as solicitações HTTP para HTTPS (SSL/TLS).

app.UseAuthorization(); // Adiciona middleware de autorização.

app.MapControllers(); // Mapeia as rotas dos controladores para as ações apropriadas.

app.Run(); // Inicia o aplicativo web e aguarda solicitações.
