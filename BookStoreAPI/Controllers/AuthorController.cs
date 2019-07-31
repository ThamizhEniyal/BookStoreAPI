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
using Microsoft.Extensions.Logging;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<AuthorController> _logger;


       

        int page = 1;
        int pageSize = 10;
        public AuthorController(IAuthorRepository authorRepository,
                                IBooksRepository booksRepository,IOrderRepository orderRepository,ILogger<AuthorController> logger)
        {
            _authorRepository = authorRepository;
            _booksRepository = booksRepository;
            _orderRepository = orderRepository;
            _logger = logger;

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
            var totalUsers = _authorRepository.Count();
            var totalPages = (int)Math.Ceiling((double)totalUsers / pageSize);

            IEnumerable<Author> _authors = _authorRepository
                .AllIncluding(a => a.BooksCreated)
                .OrderBy(u => u.Id)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            IEnumerable<AuthorViewModel> _authorVM = Mapper.Map<IEnumerable<Author>, IEnumerable<AuthorViewModel>>(_authors);

            Response.AddPagination(page, pageSize, totalUsers, totalPages);

            return new OkObjectResult(_authorVM);
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult Get(int id)
        {
            Author _author = _authorRepository.GetSingle(u => u.Id == id, u => u.BooksCreated);

            if (_author != null)
            {
                AuthorViewModel _userVM = Mapper.Map<Author, AuthorViewModel>(_author);
                return new OkObjectResult(_userVM);
            }
            else
            {
                _logger.LogInformation("No Data for Authorid");
                return NotFound();

            }
        }

        [HttpGet("{id}/books", Name = "GetAuthorBooks")]
        public IActionResult GetBooks(int id)
        {
            IEnumerable<Books> _authorbooks = _booksRepository.FindBy(b => b.AuthorId == id);

            if (_authorbooks != null)
            {
                IEnumerable<BooksViewModel> _authorbooksVM = Mapper.Map<IEnumerable<Books>, IEnumerable<BooksViewModel>>(_authorbooks);
                return new OkObjectResult(_authorbooksVM);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]AuthorViewModel author)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Author _newAuthor = new Author { Name = author.Name, ContactNumber = author.ContactNumber, Address = author.Address, CreateDate = author.CreateDate };

            _authorRepository.Add(_newAuthor);
            _authorRepository.Commit();

            author = Mapper.Map<Author, AuthorViewModel>(_newAuthor);
            author.CreateDate = DateTime.Now;

            CreatedAtRouteResult result = CreatedAtRoute("GetAuthor", new { controller = "Author", id = author.Id }, author);
            return result;
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]AuthorViewModel author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Author _authorDb = _authorRepository.GetSingle(id);

            if (_authorDb == null)
            {
                return NotFound();
            }
            else
            {
                _authorDb.Name = author.Name;
                _authorDb.ContactNumber = author.ContactNumber;
                _authorDb.Address = author.Address;
                _authorDb.CreateDate = author.CreateDate;
                _authorRepository.Commit();
            }

            author = Mapper.Map<Author, AuthorViewModel>(_authorDb);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Author _authorDb = _authorRepository.GetSingle(id);

            if (_authorDb == null)
            {
                return new NotFoundResult();
            }
            else
            {
                
                IEnumerable<Books> _books = _booksRepository.FindBy(b => b.AuthorId == id);
               // IEnumerable<Order> _orders= _orderRepository.GetAll();


                foreach (var books in _books)
                {
                    
                    _booksRepository.Delete(books);
                    IEnumerable<Order> _orders = _orderRepository.FindBy(o => o.BookId == books.Id);
                    foreach (var order in _orders)
                    {

                        _orderRepository.Delete(order);
                        
                    }

                }

                //foreach (var order in _orders)
                //{

                //    _orderRepository.Delete(order);
                //    //_orders = _orderRepository.FindBy(o => o.BookId == books.Id);
                //}
                _authorRepository.Delete(_authorDb);

                _authorRepository.Commit();

                return new NoContentResult();
            }
        }


    }
}