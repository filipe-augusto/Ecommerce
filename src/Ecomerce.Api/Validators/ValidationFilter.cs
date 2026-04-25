using FluentValidation;

public class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, 
    EndpointFilterDelegate next)
    {
        //tenta achar os argumentos da rota que seja do tipo T
        var arguments = context.Arguments.FirstOrDefault(a => a?.GetType() == typeof(T));
        //tenta achar o validador no container de injeção de dependência
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator is not null && arguments is T entidade)
        {
            var validationResult = await validator.ValidateAsync((T)entidade);
            if (!validationResult.IsValid)
            {
             
                return TypedResults.ValidationProblem(validationResult.ToDictionary());
            }
        }
        return await next(context);
    }
}