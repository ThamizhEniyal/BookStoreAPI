using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Serilog;
namespace BookStoreAPI.ViewModel.Validations
{
    public class AuthorViewModelValidator : AbstractValidator<AuthorViewModel>
    {
        public AuthorViewModelValidator()
        {
            RuleFor(author => author.Name).NotEmpty().WithMessage("Name cannot be empty");
            Log.Error("Name cannot be empty");
            //RuleFor(author => author.ContactNumber).NotEmpty().WithMessage(" Contact Number cannot be empty");
            //RuleFor(author => author.Address).NotEmpty().WithMessage("Address cannot be empty");
        }

    }
}
