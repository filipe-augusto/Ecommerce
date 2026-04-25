using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<TokenService>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ClockSkew = TimeSpan.Zero
        };
    });


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
app.MapProdutosEndpoints();
app.MapClientesEndpoints();
app.MapUsuarioEndpoints();
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

