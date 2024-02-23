using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity; // For getting the current user
using MyAuthenticatedAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAuthenticatedAPI.Api.Controllers
{

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

 [HttpGet("{categoryId}")]
public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int categoryId)
{
    var products = await _context.Products
                                 .Join(_context.Categories,
                                       p => p.CategoryId, 
                                       c => c.Id,
                                       (p, c) => p) 
                                 .Where(p => p.CategoryId == categoryId)
                                 .ToListAsync();

    if (products.Any()) 
    {
        return Ok(products);
    } 
    else 
    {
        return NotFound(); 
    } 
}



    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct([FromBody] Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
    }
}
}