using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BookStoreAPI.ViewModel.Validations;

namespace BookStoreAPI.ViewModel
{
    public class AuthorViewModel:IValidatableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContactNumber { get; set; }   

        public string Address { get; set; }

        public DateTime CreateDate { get; set; }

        public int BooksCreated { get; set; }

       // public int OrderCreated { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new AuthorViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }

   

}
