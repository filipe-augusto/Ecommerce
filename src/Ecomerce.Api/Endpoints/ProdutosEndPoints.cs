using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

public static class ProdutosEndPoints
{
    public static void MapProdutosEndpoints(this WebApplication app)
    {
        var grupo = app.MapGroup("/produtos")
        .RequireAuthorization()
        //.AddEndpointFilter<LoggerFilterOptions>()
        .WithTags("Produtos");

        grupo.MapGet("/",  GetTodos);
        grupo.MapPost("/",CriarProduto).RequireAuthorization(policy => policy.RequireRole("admin"));
        grupo.MapGet("/{id}", GetPorId).AddEndpointFilter(async (invocationContext, next) =>
        {
            var id = invocationContext.GetArgument<int>(0);
           if(id <= 0)
            {
                return TypedResults.Problem("Id deve ser maior que zero", statusCode: 400);
            }
            return await next(invocationContext);


        });
        grupo.MapDelete("/{id}", (int id) => "produto deletado com sucesso");
    }

    private static IResult GetTodos()
    {
    return  TypedResults.Ok(new[] {
        new Produto("Notebook", 1, 3500.00m),
        new Produto("Mouse", 2, 50.00m),
        new Produto("Teclado", 3, 150.00m)
    });
    }
    private static IResult GetPorId(int id)
    {

        if (id == 1)
            return TypedResults.Ok(new Produto("Notebook", 1, 3500.00m));

        return TypedResults.NotFound();
    }
    private static IResult CriarProduto(Produto produto, ClaimsPrincipal user)
    {
        // Lógica para criar o produto
        return TypedResults.Created($"/produtos/{produto.id}", "produto criado com sucesso pelo usuário "
         + user.FindFirst(ClaimTypes.Name)?.Value);
    }

}


