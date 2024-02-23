using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAuthenticatedAPI.Models

{
    public class Product
    {
        public int Id { get; set; }
         public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Category? Category { get; set; }
    }
}