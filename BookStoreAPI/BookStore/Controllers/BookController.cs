using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/Book")]
    public class BookController : ControllerBase
    {
        BookRepository _bookRepository = new BookRepository();

        [HttpGet]
        [Route("list")]
        //[ProducesResponseType(typeof(ListResponse<BookModel>),(int)HttpStatusCode.OK)]
        public IActionResult GetBooks(string? keyword ,int pageIndex=1,int pageSize=10)
        {
            var books=_bookRepository.GetBooks(keyword, pageIndex, pageSize);
            ListResponse<BookModel> listReponse = new ListResponse<BookModel>()
            {
                Results = books.Results.Select(c => new BookModel(c)),
                TotalRecords = books.TotalRecords,
            };
            return Ok(listReponse);
        }


        [HttpGet]
        [Route("getBook/{id}")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        public IActionResult GetBook(int id)
        {
            var book = _bookRepository.GetBook(id);
            if(book == null)
                return NotFound();
            BookModel bookModel = new BookModel(book);
            return Ok(bookModel);

        }

        [HttpPost]
        [Route("AddBook")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddBook(BookModel model)
        {
            if (model == null)
            {
                return BadRequest("Model is null");
            }
            Book book = new Book()
            {
                Id = model.Id,
                Name=model.Name,
                Price=model.Price,
                Description=model.Description,
                Base64image=model.Base64image,
                Categoryid=model.Categoryid,
                Publisherid=model.Publisherid,
                Quantity=model.Quantity,
            };
            var books = _bookRepository.AddBook(book);
            BookModel bookModel = new BookModel(books);

            return Ok(bookModel);

        }

        [HttpPut]
        [Route("updateBook")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateBook(BookModel model)
        {
            if (model == null)
            {
                return BadRequest("Model is null");
            }
            Book book = new Book()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Base64image = model.Base64image,
                Categoryid = model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity,
            };
            var books = _bookRepository.UpdateBook(book);
            BookModel bookModel = new BookModel(books);

            return Ok(bookModel);

        }

        [HttpDelete]
        [Route("deleteBook/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteBook(int id)
        {
            if (id <= 0)
            {
                return BadRequest("id is null");
            }
            var result = _bookRepository.DeleteBook(id);

            return Ok(result);

        }
    }
}
