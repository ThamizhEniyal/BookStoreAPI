using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Model.Entities;
using BookStore.Data;
using BookStore.Data.Repositories;
using BookStore.Data.Abstract;

namespace BookStore.Data.Repositories
{
    public class OrderRepository : EntityBaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(BookContext context)
            : base(context)
        { }
    
    }
}
