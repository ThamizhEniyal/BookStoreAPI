using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreAPI.ViewModel
{
    public class BooksDetailViewModel
    {
        public int Id { get; set; }
        public string Isbn { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public int AvailableQuantity { get; set; }

        public DateTime CreateDate { get; set; }

        public int AuthorId { get; set; }

        public string Author { get; set; }
    }
}
