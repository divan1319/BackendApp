using BackendApp.DTOs;
using FluentValidation;

namespace BackendApp.Validators
{
    public class UpdateBeerValidator : AbstractValidator<UpdateBeerDto>
    {
        public UpdateBeerValidator() {

            RuleFor(x => x.Id).NotNull().WithMessage("El id es obligatorio");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El Nombre es obligatorio");
            RuleFor(x => x.Name).Length(3, 25).WithMessage("Longitud de nombre invalida");
            RuleFor(b => b.BrandId).NotNull().WithMessage("El {PropertyName} no puede nulo");
            RuleFor(b => b.BrandId).GreaterThan(0).WithMessage("Valor {PropertyName} es invalido");
            RuleFor(a => a.Alcohol).GreaterThan(0).WithMessage("El {PropertyName} no puede ser 0.");
        }
    }
}
