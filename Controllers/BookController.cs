using BookAPi.Models;
using BookAPi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }
        [HttpGet("GetAllBookAsync")]
        // [Route("GetAllBookAsync")]
        public async Task<IActionResult> GetAllBookAsync()
        {
            var results = await _bookRepository.GetAllBookAsync();
            return Ok(results);
        }
        [HttpGet("GetBookByIdAsync/{id}")]
        public async Task<IActionResult> GetBookByIdAsync([FromRoute]int Id)
        {
            var result = await _bookRepository.GetBookByIdAsync( Id);
            if (result == null) { 
                return NotFound();
            }
            return Ok(result);
        }


        [HttpPost("AddBookAsync")]
        public async Task<IActionResult> AddBookAsync([FromBody]BooksModel booksModel)
        {
            var result = await _bookRepository.AddBookAsync(booksModel);
            
            return Ok(result);
        }

        [HttpPut("PutUpdateBookAsync/{id}")]
        // To update all columns value of single record/entity row
        // all columns value need to paased for put call ;
        // if we didnt passed value of all columns in body then it will give error 400 

        public async Task<IActionResult> PutUpdateBookAsync([FromRoute]int Id,[FromBody] BooksModel booksModel)
        {
             await _bookRepository.PutUpdateBookAsync(Id, booksModel);

            return Ok(true);
        }
        [HttpPatch("PatchUpdateBookAsync/{id}")]
        // add ms.aspnetcore.JsonPatch and ms.aspnetcore.NewtonssoftJson packegs from nuget package manager
        //  replace builder.Services.AddControllers() by builder.Services.AddControllers().AddNewtonsoftJson();  in starup class.
        // To update alfew columns value of single record/entity row
        // we can pass value like below list with object in body
        //[
        // {
        //   "op":"replace",
        // "path":"title",
        // "value":"patch updated title"
        // },
        //{
        //   "op":"replace",
        // "path":"description",
        // "value":"patch description"
        // }
        // ];
        public async Task<IActionResult> PatchUpdateBookAsync([FromRoute] int Id, [FromBody]JsonPatchDocument booksModel)
        {
            await _bookRepository.PatchUpdateBookAsync(Id, booksModel);

            return Ok(true);
        }
        [HttpDelete("DeleteBookAsync/{id}")]
        public async Task<IActionResult> DeleteBookAsync(int Id) {

            await _bookRepository.DeleteBookAsync(Id);  
            return Ok(true);
        }
    }
}
