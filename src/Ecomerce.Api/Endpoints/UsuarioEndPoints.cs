public static class UsuarioEndPoints
{
    public static void MapUsuarioEndpoints(this WebApplication app)
    {
        var grupo = app.MapGroup("/usuarios")
        //.RequireAuthorization()
        //.AddEndpointFilter<LoggerFilterOptions>()
        .WithTags("Usuarios");

        grupo.MapPost("/login",  Login);
    }

    private static IResult Login(Usuario usuario, TokenService tokenService)
    {
        // Lógica de autenticação (exemplo simples)
        if (usuario.Email == "admin@mail.com" && usuario.Senha == "password")
        {
            var token = tokenService.GenerateToken(usuario);

            return TypedResults.Ok(new { Token = token });
        }

        return TypedResults.Unauthorized();
    }

}