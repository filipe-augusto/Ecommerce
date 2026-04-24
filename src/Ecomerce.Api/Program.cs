var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();

app.MapClientesEndpoints();
app.MapGet("/produtos", () => $"Recuperando o produto ");
// app.MapPost("/produtos", (Produto produto) => "produto criado com sucesso");
// app.MapPut("/produtos/{id}", (int id, Produto produto) => "produto atualizado com sucesso");
// app.MapDelete("/produtos/{id}", (int id) => "produto deletado com sucesso");
// app.MapGet("/saudacao", ()=>
// {
//     var dados = new {Mensagem = "olá", Data = DateTime.Now};
//     return TypedResults.Ok(dados);
// });



app.Run();
//dotnet add package Swashbuckle.AspNetCore
//dotnet add package microsoft.AspNetCore.OpenApi

public record Produto(int Id, string Nome, decimal Preco);