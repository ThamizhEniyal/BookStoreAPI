using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Model.Entities
{
    public class Order:IEntityBase
    {
        public int Id { get; set; }
       

        //public string Title { get; set; }

        
        public int OrderQuantity { get; set; }

        public decimal Cost { get; set; }


        public DateTime OrderDate { get; set; }

        public DateTime CreateDate { get; set; }

        public int BookId { get; set; }
        //public string Isbn { get; set; }
        public Books Books { get; set; }
    }
}
