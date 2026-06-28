using FluentValidation;
using ProductApi.Dto.DtoUser;

namespace ProductApi.Configration
{
    public class FluentValidations : AbstractValidator<SingInUserDto>
    {
        public FluentValidations()
        {
            RuleFor(s => s.Email).NotEmpty().EmailAddress();
            RuleFor(m => m.Password).NotEmpty();
        }
    }
}
