using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyModels;
using MyModels.Books;
using MyWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(BookContext context) : ControllerBase
    {
        // GET: api/<BooksController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await context.Books.ToListAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error get books from database !");
            }

        }

        [HttpGet("Paged")]
        public async Task<ActionResult> Paged([FromQuery] GetBooksRequest request)
        {
            try
            {
                ////=========分頁查詢========= 
                //int skip = (request.PageNumber - 1) * request.PageSize;
                //// 查詢總筆數
                //int totalCount = await context.Books.CountAsync();
                //var dataList = await context.Books.Skip(skip).Take(request.PageSize).ToListAsync();

                //return Ok(new GetBooksResponse(request.PageNumber, request.PageSize, totalCount, dataList));
                ////=========分頁查詢=========
                //=========分頁查詢=========            
                return Ok(await context.Books.ToPagedResultAsync(request, request.Search()));
                //=========分頁查詢=========
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error get books from database !");
            }
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            try
            {
                var books = await context.Books.FindAsync(id);

                if (books is null)
                {
                    return NotFound();
                }

                return Ok(books);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error get books from database !");
            }
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] Book book)
        {
            try
            {
                if (book is null)
                {
                    return BadRequest();
                }

                await context.Books.AddAsync(book);
                await context.SaveChangesAsync();
                return CreatedAtAction(nameof(Post), new { id = book.Id }, book);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                   "Error inserting boot into database!");
            }

        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> Put(int id, [FromBody] Book book)
        {
            try
            {
                if (id != book.Id)
                {
                    return BadRequest("Id is not the same.");
                }

                var b = await context.Books.FindAsync(id);
                if (b is null)
                {
                    return NotFound($"Book id {id} is not found.");
                }

                context.Entry(b).CurrentValues.SetValues(book);
                await context.SaveChangesAsync();
                return Ok(b);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                   "Update error!");
            }
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> Delete(int id)
        {
            try
            {
                var b = await context.Books.FindAsync(id);
                if (b is null)
                {
                    return NotFound($"Book id {id} is not found.");
                }

                context.Books.Remove(b);
                await context.SaveChangesAsync();
                return Ok(b);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                   "Error deleteing book from database!");
            }
        }
    }
}
