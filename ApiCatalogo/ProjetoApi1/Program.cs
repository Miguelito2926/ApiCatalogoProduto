var builder = WebApplication.CreateBuilder(args);

// Criar uma instância do aplicativo ASP.NET Core usando o WebApplication.CreateBuilder com argumentos fornecidos.

builder.Services.AddControllers();
// Adicionar serviços necessários para suportar controladores, que são usados para gerenciar solicitações HTTP.

builder.Services.AddEndpointsApiExplorer();
// Adicionar suporte para a geração de documentação da API usando o pacote EndpointsApiExplorer.

builder.Services.AddSwaggerGen();
// Configurar o SwaggerGen para gerar documentação da API Swagger.

var app = builder.Build();

// Construir a instância do aplicativo usando o builder.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Se o aplicativo estiver em um ambiente de desenvolvimento, habilite o Swagger para documentação interativa da API.

    app.UseSwaggerUI();
    // Use o Swagger UI para visualizar a documentação da API.

}

app.UseHttpsRedirection();
// Redirecionar solicitações HTTP para HTTPS, útil para segurança e conformidade.

app.UseAuthorization();
// Configurar a autenticação e autorização para proteger recursos do aplicativo.

app.MapControllers();
// Mapear as rotas para os controladores, permitindo que o aplicativo responda a solicitações HTTP.

app.Run();
// Iniciar o aplicativo e executar o servidor web.
