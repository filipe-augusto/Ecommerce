using FluentValidation;

public class CriarCategoriaValidator : AbstractValidator<Categoria>
{
    public CriarCategoriaValidator()
    {
        RuleFor(c => c.nome)
        .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
        .Length(2, 100).WithMessage("O nome da categoria deve ter entre 2 e 100 caracteres.")  ;
        RuleFor(c => c.id).GreaterThan(0).WithMessage("O id da categoria deve ser maior que zero.");
    }
}