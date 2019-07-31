using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace BookStoreAPI.ViewModel.Validations
{
    public class OrderViewModelValidator:AbstractValidator<OrderViewModel>
    {
        public OrderViewModelValidator()
        {
            RuleFor(order => order.BookId).NotEmpty().WithMessage("BookID cannot be empty");
            RuleFor(order => order.OrderQuantity).NotEmpty().WithMessage(" OrderQuantity cannot be empty");
            RuleFor(order => order.Cost).NotEmpty().WithMessage("Cost cannot be empty");
            RuleFor(order => order.OrderDate).NotEmpty().WithMessage("OrderDate cannot be empty");

        }

    }
   
}
