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
    public class BooksController : Controller
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IOrderRepository _orderRepository;
        int page = 1;
        int pageSize = 4;
        public BooksController(IBooksRepository booksRepository,
                               IAuthorRepository authorRepository,IOrderRepository orderRepository)
        {
            _booksRepository = booksRepository;
            _authorRepository = authorRepository;
            _orderRepository = orderRepository;
        }
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

            IEnumerable<Books> _books = _booksRepository
                .AllIncluding(b => b.Author)
                .OrderBy(b=> b.Id)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            Response.AddPagination(page, pageSize, totalBooks, totalPages);

            IEnumerable<BooksViewModel> _booksVM = Mapper.Map<IEnumerable<Books>, IEnumerable<BooksViewModel>>(_books);

            return new OkObjectResult(_booksVM);
        }

        [HttpGet("{id}", Name = "GetBooks")]
        public IActionResult Get(int id)
        {
            Books _books = _booksRepository
                .GetSingle(b => b.Id == id,b=>b.Author);

            if (_books != null)
            {
                BooksViewModel _booksVM = Mapper.Map<Books, BooksViewModel>(_books);
                return new OkObjectResult(_booksVM);
            }
            else
            {
                return NotFound();
            }
        }

        //[HttpGet("{id}/details", Name = "GetScheduleDetails")]
        //public IActionResult GetScheduleDetails(int id)
        //{
        //    Schedule _schedule = _booksRepository
              //.GetSingle(s => s.Id == id, s => s.Creator, s => s.Attendees);

        //    if (_schedule != null)
        //    {


        //        ScheduleDetailsViewModel _scheduleDetailsVM = Mapper.Map<Schedule, ScheduleDetailsViewModel>(_schedule);

        //        foreach (var attendee in _schedule.Attendees)
        //        {
        //            User _userDb = _userRepository.GetSingle(attendee.UserId);
        //            _scheduleDetailsVM.Attendees.Add(Mapper.Map<User, UserViewModel>(_userDb));
        //        }


        //        return new OkObjectResult(_scheduleDetailsVM);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        [HttpPost]
        public IActionResult Create([FromBody]BooksViewModel books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Books _newbook = Mapper.Map<BooksViewModel, Books>(books);
            _newbook.CreateDate = DateTime.Now;

            _booksRepository.Add(_newbook);
            _booksRepository.Commit();


            books = Mapper.Map<Books, BooksViewModel>(_newbook);

            CreatedAtRouteResult result = CreatedAtRoute("GetBooks", new { controller = "Books", id = books.Id }, books);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]BooksViewModel books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Books _booksDb = _booksRepository.GetSingle(id);

            if (_booksDb == null)
            {
                return NotFound();
            }
            else
            {
                _booksDb.Title = books.Title;
                _booksDb.Isbn = books.Isbn;
                _booksDb.Price = books.Price;
                _booksDb.AvailableQuantity = books.AvailableQuantity;
                _booksDb.AuthorId = books.AuthorId;
               // _booksDb.Author = books.Author;
                _booksDb.CreateDate = books.CreateDate;

             

                _booksRepository.Commit();
            }

            books = Mapper.Map<Books, BooksViewModel>(_booksDb);

            return new NoContentResult();
        }

        [HttpDelete("{id}", Name = "RemoveBooks")]
        public IActionResult Delete(int id)
        {
            Books _booksDb = _booksRepository.GetSingle(id);


            if (_booksDb == null)
            {
                return new NotFoundResult();
            }
            else

            {
                IEnumerable<Order> _order = _orderRepository.FindBy(b => b.BookId == id);
                foreach (var order in _order)
                {

                    _orderRepository.Delete(order);
                    //  _orders = _orderRepository.FindBy(o => o.BookId == books.Id);
                }
                _booksRepository.Delete(_booksDb);

                _booksRepository.Commit();

                return new NoContentResult();
            }
        }

    }
}