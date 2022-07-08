﻿using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class BookModel
    {
        public BookModel() { }
        public BookModel(Book c)
        {
            Id= c.Id;
            Name= c.Name;
            Description= c.Description;
            Price= c.Price;
            Categoryid = c.Categoryid;
            Publisherid = c.Publisherid;
            Base64image = c.Base64image;
            Quantity = c.Quantity;
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Base64image { get; set; }
        public int Categoryid { get; set; }
        public int? Publisherid { get; set; }
        public int? Quantity { get; set; }

    }
}
