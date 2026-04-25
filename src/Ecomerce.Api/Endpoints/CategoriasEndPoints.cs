using System.Security.Cryptography.X509Certificates;
using idCategoria = int;

public static class CategoriasEndPoints
{
    public static void MapCategoriasEndpoints(this WebApplication app)
    {
        var grupo = app.MapGroup("/categorias")
        .WithTags("Categorias");

        grupo.MapGet("/",  GetTodos);
        grupo.MapPost("/",CriarCategoria)
        .AddEndpointFilter<ValidationFilter<Categoria>>();

        grupo.MapGet("/{id}", GetPorId).AddEndpointFilter(async (invocationContext, next) =>
        {
            var id = invocationContext.GetArgument<int>(0);
           if(id <= 0)
            {
                return TypedResults.Problem("Id deve ser maior que zero", statusCode: 400);
            }
            return await next(invocationContext);


        });
        grupo.MapDelete("/{id}", (int id) => "categoria deletada com sucesso");
    }

    private static IResult GetTodos()
    {
    return  TypedResults.Ok(new[] {
        new Categoria("Eletrônicos", 1),
        new Categoria("Livros", 2),
        new Categoria("Roupas", 3)
    });
    }
    private static IResult GetPorId(idCategoria id)
    {

        if (id == 1)
            return TypedResults.Ok(new Categoria("Eletrônicos", 1));

        return TypedResults.NotFound();
    }
    private static IResult CriarCategoria(Categoria categoria)
    {
        // Lógica para criar a categoria



        return TypedResults.Created($"/categorias/{categoria.id}", categoria);
    }

}


