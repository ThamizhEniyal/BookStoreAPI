using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BookStoreAPI.ViewModel.Validations;

namespace BookStoreAPI.ViewModel
{
    public class BooksViewModel:IValidatableObject
    {
        public int Id { get; set; }
        public string Isbn { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public int AvailableQuantity { get; set; }

        public DateTime CreateDate { get; set; }

        public int AuthorId { get; set; }

        public string Author { get; set; }


        public int[] Orderlist { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new BooksViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
