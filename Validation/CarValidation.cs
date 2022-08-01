using Cars.Api.Models;
using FluentValidation;

namespace Cars.Api.Validation
{
    public class CarCreateValidation : AbstractValidator<CarCreateModel>
    {
        public CarCreateValidation()
        {
            RuleFor(x => x.Brand).NotNull();
            RuleFor(x => x.Brand).NotEmpty();   
        }
    }

    public class CarValidation : AbstractValidator<CarModel>
    {
        public CarValidation()
        {
            RuleFor(x => x.Brand).NotNull();
            RuleFor(x => x.Brand).NotEmpty();
        }
    }
}
