using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace BookStoreAPI.ViewModel.Validations
{
    public class BooksViewModelValidator: AbstractValidator<BooksViewModel>
    {
        public BooksViewModelValidator()
        {
            RuleFor(book => book.Isbn).NotEmpty().WithMessage("Isbn cannot be empty");
            RuleFor(book => book.Title).NotEmpty().WithMessage("Title cannot be empty");
            RuleFor(book => book.Price).NotEmpty().WithMessage("Price cannot be empty");
          //  RuleFor(book => book.AvailableQuantity).NotEmpty().WithMessage("Available quantity cannot be empty");
        }
    }
}
