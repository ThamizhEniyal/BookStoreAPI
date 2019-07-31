using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Model.Entities
{
   public class Author:IEntityBase
    {

        public Author()
        {
            BooksCreated = new List<Books>();
          //  OrdersCreated = new List<Order>();
                }   
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContactNumber { get; set; }

        public string Address { get; set; }

        public DateTime CreateDate { get; set; }

        public ICollection<Books> BooksCreated { get; set; }

       // public ICollection<Order> OrdersCreated { get; set; }




    }
}
