﻿using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class GetCartModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public BookModel Book { get; set; }

        public GetCartModel() { }

        public GetCartModel(Cart cart)
        {
            Id = cart.Id;
            UserId = cart.Userid;
            BookId = cart.Bookid;
            Quantity = cart.Quantity;
            Book = new BookModel()
            {
                Base64image = cart.Book.Base64image,
                Quantity = cart.Book.Quantity,
                Id = cart.Book.Id,
                Name = cart.Book.Name,
                Description = cart.Book.Description,
                Categoryid = cart.Book.Categoryid,
                Publisherid = cart.Book.Publisherid,
                Price = cart.Book.Price,
            };
        }
    }
}