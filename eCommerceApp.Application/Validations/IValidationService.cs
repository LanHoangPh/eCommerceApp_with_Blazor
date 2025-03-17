using eCommerceApp.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Validations
{
    public interface IValidationService
    {
        Task<ServicesResponse> ValidateAsync<T>(T model, IValidator<T> validator);
    }
}
