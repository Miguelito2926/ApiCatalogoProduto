var builder = WebApplication.CreateBuilder(args);

// Criar uma inst�ncia do aplicativo ASP.NET Core usando o WebApplication.CreateBuilder com argumentos fornecidos.

builder.Services.AddControllers();
// Adicionar servi�os necess�rios para suportar controladores, que s�o usados para gerenciar solicita��es HTTP.

builder.Services.AddEndpointsApiExplorer();
// Adicionar suporte para a gera��o de documenta��o da API usando o pacote EndpointsApiExplorer.

builder.Services.AddSwaggerGen();
// Configurar o SwaggerGen para gerar documenta��o da API Swagger.

var app = builder.Build();

// Construir a inst�ncia do aplicativo usando o builder.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Se o aplicativo estiver em um ambiente de desenvolvimento, habilite o Swagger para documenta��o interativa da API.

    app.UseSwaggerUI();
    // Use o Swagger UI para visualizar a documenta��o da API.

}

app.UseHttpsRedirection();
// Redirecionar solicita��es HTTP para HTTPS, �til para seguran�a e conformidade.

app.UseAuthorization();
// Configurar a autentica��o e autoriza��o para proteger recursos do aplicativo.

app.MapControllers();
// Mapear as rotas para os controladores, permitindo que o aplicativo responda a solicita��es HTTP.

app.Run();
// Iniciar o aplicativo e executar o servidor web.
