using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class CartRepository : BaseRepository
    {
        public ListResponse<Cart> GetCartItems(int userId)
        {
            var query = _context.Carts.Where(c => c.Userid == userId);
            int totalRecords = query.Count();
            List<Cart> cart = query.ToList();

            foreach (Cart cartItem in cart)
            {
                cartItem.Book = _context.Books.FirstOrDefault(c => c.Id == cartItem.Bookid);
            }
            
            return new ListResponse<Cart>()
            {
                Results = cart,
                TotalRecords = totalRecords,

            };
        }

        public Cart GetCarts(int id)
        {
            return _context.Carts.Where(c=>c.Id==id).FirstOrDefault();
        }

        public Book GetCartBook(int id)
        {
            var query=_context.Books.Where(c=>c.Id==id).FirstOrDefault();
            return query;
        }

        public Cart AddCart(Cart cart)
        {
            var qry1 = _context.Carts.Where(c => c.Userid == cart.Userid&&c.Bookid==cart.Bookid).FirstOrDefault();
       
            if(qry1!=null)
            {
                cart.Quantity = qry1.Quantity+1;
                _context.Carts.Remove(qry1);
                _context.SaveChanges();
            }

            var query=_context.Carts.Add(cart);
            _context.SaveChanges();
            return query.Entity;
        }

        public Cart UpdateCart(Cart cart)
        {
            var query = _context.Carts.Update(cart);
            _context.SaveChanges();
            return query.Entity;
        }

        public bool DeleteCart(int id)
        {
            var query = _context.Carts.Where(c => c.Id == id).FirstOrDefault();
            if (query == null)
                return false;

            _context.Carts.Remove(query);
            _context.SaveChanges();
            return true;
        }
    }
}
