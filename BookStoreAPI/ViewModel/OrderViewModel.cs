using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BookStoreAPI.ViewModel.Validations;

namespace BookStoreAPI.ViewModel
{
    public class OrderViewModel: IValidatableObject
    {
        public int Id { get; set; }
       

        public int OrderQuantity { get; set; }

        public decimal Cost { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime CreateDate { get; set; }

        public int BookId { get; set; }
        public string Book { get; set; }
        
        public int TotalQuantity { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new OrderViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    
    }
}
