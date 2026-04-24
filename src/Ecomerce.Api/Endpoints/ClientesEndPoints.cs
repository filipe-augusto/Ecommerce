using System.Security.Cryptography.X509Certificates;

public static class ClientesEndPoints
{
    public static void MapClientesEndpoints(this WebApplication app)
    {
        var grupo = app.MapGroup("/clientes")
        //.RequireAuthorization()
        //.AddEndpointFilter<LoggerFilterOptions>()
        .WithTags("Clientes");

        grupo.MapGet("/",  GetTodos);
        grupo.MapPost("/",CriarCliente);
        grupo.MapGet("/{id}", GetPorId).AddEndpointFilter(async (invocationContext, next) =>
        {
            var id = invocationContext.GetArgument<int>(0);
           if(id <= 0)
            {
                return TypedResults.Problem("Id deve ser maior que zero", statusCode: 400);
            }
            return await next(invocationContext);


        });
        grupo.MapDelete("/{id}", (int id) => "cliente deletado com sucesso");
    }

    private static IResult GetTodos()
    {
    return  TypedResults.Ok(new[] {
        new Cliente("João", 1),
        new Cliente("Maria", 2),
        new Cliente("Pedro", 3)
    });
    }
    private static IResult GetPorId(int id)
    {

        if (id == 1)
            return TypedResults.Ok(new Cliente("João", 1));

        return TypedResults.NotFound();
    }
    private static IResult CriarCliente(Cliente cliente)
    {
        // Lógica para criar o cliente
        return TypedResults.Created($"/clientes/{cliente.id}", cliente);
    }

}

public record Cliente(string nome, int id);