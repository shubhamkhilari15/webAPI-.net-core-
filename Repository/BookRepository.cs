using BookAPi.Data;
using BookAPi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BookAPi.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _dbContext;
        public BookRepository(BookDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<List<BooksModel>> GetAllBookAsync() {

            var records = await _dbContext.Books.Select(x => new BooksModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            }).ToListAsync();
            return records;
        }
        public async Task<BooksModel> GetBookByIdAsync(int Id)
        {

            var record = await _dbContext.Books.Where(x => x.Id == Id).Select(x => new BooksModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            }).FirstOrDefaultAsync();
            return record;
        }
        public async Task<int> AddBookAsync(BooksModel booksModel)
        {
            var book = new Book()
            {
                Title = booksModel.Title,
                Description = booksModel.Description
            };

            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
            return book.Id;
        }
        public async Task PutUpdateBookAsync(int Id, BooksModel booksModel)
        {  // with Double DB call
           //var book = await _dbContext.Books.FindAsync(Id); ///db call 1
           //if (book != null) { 

            //    book.Title = booksModel.Title;  
            //    book.Description = booksModel.Description;// db call 2
            //    await _dbContext.SaveChangesAsync();
            //}


            // with single DB call
            var book = new Book()
            {
                Id = Id,
                Title = booksModel.Title,
                Description = booksModel.Description
            };
            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();
        }
        public async Task PatchUpdateBookAsync(int Id, JsonPatchDocument booksModel)
        {
            var book = await _dbContext.Books.FindAsync(Id);
            if (book != null) { 
                booksModel.ApplyTo(book);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteBookAsync(int Id)
        {
            var book = new Book()
            {
                Id = Id
            };
             _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
    }
}
