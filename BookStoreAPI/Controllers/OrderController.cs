using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data.Abstract;
using BookStore.Model;
using BookStoreAPI.ViewModel;
using BookStoreAPI.Core;
using Microsoft.AspNetCore.Mvc;
using BookStore.Model.Entities;
using AutoMapper;



namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
       public class OrderController : Controller
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthorRepository _authorRepository;
        int page = 1;
        int pageSize = 4;
        public OrderController(IBooksRepository booksRepository,
                                     IOrderRepository orderRepository,
                                    IAuthorRepository authorRepository)
        {
            _booksRepository = booksRepository;
            _orderRepository = orderRepository;
            _authorRepository = authorRepository;
        }


        [HttpGet]
       public IActionResult Get()
        {
            var pagination = Request.Headers["Pagination"];

            if (!string.IsNullOrEmpty(pagination))
            {
                string[] vals = pagination.ToString().Split(',');
                int.TryParse(vals[0], out page);
                int.TryParse(vals[1], out pageSize);
            }

            int currentPage = page;
            int currentPageSize = pageSize;
            var totalBooks = _booksRepository.Count();
            var totalPages = (int)Math.Ceiling((double)totalBooks / pageSize);

            IEnumerable<Order> _order = _orderRepository
                .AllIncluding(b => b.Books)
                .OrderBy(b => b.Id)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)

                .ToList();

            Response.AddPagination(page, pageSize, totalBooks, totalPages);

            IEnumerable<OrderViewModel> _orderVM = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(_order);

            return new OkObjectResult(_orderVM);
        }

     
       [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(int id)
        {
            Order _order = _orderRepository
                .GetSingle(b => b.Id == id, b => b.Books);

            if (_order != null)
            {
                OrderViewModel _booksVM = Mapper.Map<Order, OrderViewModel>(_order);
                return new OkObjectResult(_booksVM);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public IActionResult Create([FromBody]OrderViewModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order _neworder = Mapper.Map<OrderViewModel, Order>(order);
            _neworder.CreateDate = DateTime.Now;

            _orderRepository.Add(_neworder);
            _orderRepository.Commit();


            order = Mapper.Map<Order, OrderViewModel>(_neworder);

            CreatedAtRouteResult result = CreatedAtRoute("GetBooks", new { controller = "Order", id = order.Id }, order);
            return result;
        }


        [HttpPut("{id}")]
              public IActionResult Put(int id, [FromBody]OrderViewModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            Order _orderDb = _orderRepository.GetSingle(id);

            if (_orderDb == null)
            {
                return NotFound();
            }
            else
            {
                _orderDb.BookId =order.BookId;
                _orderDb.OrderQuantity = order.OrderQuantity;
                _orderDb.Cost = order.Cost;
                _orderDb.CreateDate = order.CreateDate;
                _orderDb.OrderDate =order.OrderDate;
                _orderRepository.Commit();
            }

            order = Mapper.Map<Order, OrderViewModel>(_orderDb);

            return new NoContentResult();
        }


        [HttpDelete("{id}", Name = "RemoveOrder")]
        public IActionResult Delete(int id)
        {
            Order _ordersDb = _orderRepository.GetSingle(id);

            if (_ordersDb == null)
            {
                return new NotFoundResult();
            }
            else

            {

                _orderRepository.Delete(_ordersDb);

                _orderRepository.Commit();

                return new NoContentResult();
            }
        }

        [HttpGet]
      [Route("GetDashboard")]
        public IActionResult GetAll()
        {

               var model = _orderRepository
               .AllIncluding(b => b.Books)
               .OrderBy(b => b.Id)
               .GroupBy(o => new
               {
                  // Month = o.OrderDate.Month,
                   Year = o.OrderDate.Year
               })
        .Select(g => new DashboardViewModel
        {
            Month = g.Key.Year,
            Year = g.Key.Year,
            Total = g.Count()
        })
        .OrderByDescending(a => a.Year)
        .ThenByDescending(a => a.Month)
        .ToList();

      var model2=      _orderRepository
               .AllIncluding(b => b.Books)
               .OrderBy(b => b.Id).Select(o => new { o.OrderDate.Year, o.OrderDate.Month, o.OrderQuantity,o.Books.AvailableQuantity
               })
               .GroupBy(x => new {x.Year, x.Month }, (key, group) => new
           {
                Year = key.Year,
                Month = key.Month,
                BooksOrdered = group.Sum(k => k.OrderQuantity),
                TotalBooks= group.Sum(k => k.AvailableQuantity),

               }).ToList();
            return new OkObjectResult(model2);
        }





    }
}