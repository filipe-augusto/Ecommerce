using System.Security.Cryptography.X509Certificates;
using Coordenada = (int x, int y);
using idCliente = int;

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
        grupo.MapGet("/desconto/{tipoCliente}",Desconto);
        grupo.MapGet("/devolveIds",DevolveIds);
        grupo.MapGet("/cordenada",ObterCoordenada);
    }

    private static IResult GetTodos()
    {
    return  TypedResults.Ok(new[] {
        new Cliente("João", 1),
        new Cliente("Maria", 2),
        new Cliente("Pedro", 3)
    });
    }
    private static IResult GetPorId(idCliente id)
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
    private static IResult Desconto(string tipoCliente)
    {
        decimal desconto = tipoCliente switch
        {
            "Premium" => 0.20m,
            "Regular" => 0.10m,
            _ => 0.00m
        };
        return TypedResults.Ok(desconto);
    }
    private static IResult DevolveIds()
    {
        int[] ids1 = [ 1, 2, 3 ];
        int [] ids2 = [ 4, 5, 6 ];
        int[] todos = [..ids1, ..ids2];   

        return TypedResults.Ok(todos);
    }
    
    private static Coordenada ObterCoordenada()
    {
         return (x: 10, y: 20);
    }
}

