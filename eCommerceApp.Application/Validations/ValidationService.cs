using eCommerceApp.Application.DTOs;
using FluentValidation;

namespace eCommerceApp.Application.Validations
{
    public class ValidationService : IValidationService
    {
        public async Task<ServicesResponse> ValidateAsync<T>(T model, IValidator<T> validator)
        {
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                string errorToString = string.Join(", ", errors);
                return new ServicesResponse { Message = errorToString }; 
            }
            return new ServicesResponse{ Success = true};
        }
    }
}
