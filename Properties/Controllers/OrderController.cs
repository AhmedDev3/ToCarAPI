using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToCarAPI.Data;
using ToCarAPI.Models;
using ToCarAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ToCarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Order (Admin only - gets all orders)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Item)
                .ToListAsync();
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Item)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // POST: api/Order (Create a new order from cart)
        [HttpPost]
        public async Task<ActionResult<Order>> Checkout(CheckoutRequest request)
        {
            // Validate customer information
            if (string.IsNullOrWhiteSpace(request.CustomerName) ||
                string.IsNullOrWhiteSpace(request.PhoneNumber) ||
                string.IsNullOrWhiteSpace(request.Location1))
            {
                return BadRequest("Customer name, phone number, and primary location are required");
            }

            // Get cart items
            var cartItems = await _context.CartItems
                .Where(c => c.UserId == request.UserId)
                .Include(c => c.Item)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return BadRequest("Cart is empty");
            }

            // Create order
            var order = new Order
            {
                CustomerName = request.CustomerName,
                PhoneNumber = request.PhoneNumber,
                Location1 = request.Location1,
                Location2 = request.Location2,
                OrderDate = DateTime.UtcNow,
                TotalAmount = cartItems.Sum(ci => ci.Item != null ? ci.Item.Price * ci.Quantity : 0)
            };

            // Create order items
            foreach (var cartItem in cartItems)
            {
                if (cartItem.Item != null)
                {
                    order.OrderItems.Add(new OrderItem
                    {
                        ItemId = cartItem.ItemId,
                        Quantity = cartItem.Quantity,
                        PriceAtOrder = cartItem.Item.Price
                    });
                }
            }

            // Save order
            _context.Orders.Add(order);
            
            // Clear cart
            _context.CartItems.RemoveRange(cartItems);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }
    }
}