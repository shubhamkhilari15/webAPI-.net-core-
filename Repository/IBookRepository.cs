using BookAPi.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BookAPi.Repository
{
    public interface IBookRepository
    {
        public Task<List<BooksModel>> GetAllBookAsync();
        public Task<BooksModel> GetBookByIdAsync(int Id);
        Task<int> AddBookAsync(BooksModel booksModel);
        Task PutUpdateBookAsync(int Id, BooksModel booksModel);
        Task PatchUpdateBookAsync(int Id, JsonPatchDocument booksModel);
        Task DeleteBookAsync(int Id);
    }
}
