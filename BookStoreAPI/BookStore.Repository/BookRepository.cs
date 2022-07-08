using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository : BaseRepository
    {
        public ListResponse<Book> GetBooks(string? keyword,int pageIndex,int pageSize)
        {
            keyword= keyword?.ToLower()?.Trim();
            var query =_context.Books.Where(c => keyword == null || c.Name.Contains(keyword)).AsQueryable();
            int totalReocrds=query.Count();
            List<Book> books=query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Book>()
            {
                Results = books,
                TotalRecords = totalReocrds,
            };
        }

        public Book GetBook(int id)
        {
            return _context.Books.FirstOrDefault(c => c.Id == id);
        }

        public Book AddBook(Book book)
        {
            var entry = _context.Books.Add(book);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Book UpdateBook(Book book)
        {
            var entry = _context.Books.Update(book);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteBook(int id)
        {
            var entry = _context.Books.FirstOrDefault(c => c.Id == id);
            if (entry == null) 
            { 
                return false; 
            }    
            _context.Books.Remove(entry);
            _context.SaveChanges();
            return true;
        }
    }
}
