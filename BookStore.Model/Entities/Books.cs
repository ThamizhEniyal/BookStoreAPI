using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Model.Entities
{
    public class Books : IEntityBase
    {
        public Books()
        {
            Orders = new List<Order>();
        }

        public int Id { get; set; }
        public string Isbn { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public int AvailableQuantity { get; set; }

        public DateTime CreateDate { get; set; }
                         
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
