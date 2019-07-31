using BookStore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.Abstract
{
    public interface IBooksRepository : IEntityBaseRepository<Books> { }

    public interface IAuthorRepository : IEntityBaseRepository<Author> { }

    public interface IOrderRepository: IEntityBaseRepository<Order> { }
}
