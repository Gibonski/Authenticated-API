using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity; // For getting the current user
using MyAuthenticatedAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace MyAuthenticatedAPI.Api.Controllers
{
  
[Authorize]
[ApiController]
[Route("[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ShoppingCartController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
{
    if (User.Identity == null) 
    {
        return Unauthorized(); 
    }

    var userEmail = User.Identity.Name;

    // Find the shopping cart or return NotFound
    var shoppingCart = await _context.ShoppingCarts
    .Include(s => s.Products)
    .FirstOrDefaultAsync(s => s.User == userEmail);
    if (shoppingCart == null)
    {
        return NotFound();
    }

    // Safely return products
    return Ok(shoppingCart.Products); 
}

    [HttpPost("add/{id}")]
    public async Task<IActionResult> AddProduct(int id)
    {
        if (User.Identity == null) 
    {
        return Unauthorized(); 
    }
        var userEmail = User.Identity.Name;
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        var shoppingCart = await _context.ShoppingCarts.Include(s => s.Products).FirstOrDefaultAsync(s => s.User == userEmail);

        if (shoppingCart == null)
        {
            shoppingCart = new ShoppingCart { User = userEmail, Products = new List<Product> { product } };
            await _context.ShoppingCarts.AddAsync(shoppingCart);
        }
        else
        {
            if (shoppingCart.Products != null && shoppingCart.Products.Any(p => p.Id == id))
            {
                shoppingCart.Products.Add(product);
            }
        }

        await _context.SaveChangesAsync();

        return Ok(product);
    }

    [HttpPost("remove/{id}")]
    public async Task<IActionResult> RemoveProduct(int id)
    {
        if (User.Identity == null) 
    {
        return Unauthorized(); 
    }
        var userEmail = User.Identity.Name;
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        var shoppingCart = await _context.ShoppingCarts.Include(s => s.Products).FirstOrDefaultAsync(s => s.User == userEmail);

        if (shoppingCart == null)
        {
            return NotFound();
        }

        
        shoppingCart.Products?.Remove(product);

        await _context.SaveChangesAsync();

        return Ok(product);
    }
}
}