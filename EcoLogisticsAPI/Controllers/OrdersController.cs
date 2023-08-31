using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcoLogisticsAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace EcoLogisticsAPI.Controllers
{
    [AllowAnonymous]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly EcoPower_LogisticsDBContext _context;

        public OrdersController(EcoPower_LogisticsDBContext context)
        {
            _context = context;
        }

        //private method in the API that checks if an Order exists 
        private bool OrderExists(short id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }

        // GET: Create a GET method that retrieves all Order entries from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            return await _context.Orders.ToListAsync();
        }

        // GET: GET method that will retrieve one Order from the database based on the ID parsed through
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(short id)
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // GET: GET method that retrieves all orders for a specific customer 
        [HttpGet("ByCustomer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByCustomer(short customerId)
        {
            var orders = await _context.Orders
                .Where(order => order.CustomerId == customerId)
                .ToListAsync();

            if (OrderExists(customerId) || orders.Count == 0)
            {
                return NotFound();
            }

            return orders;
        }


        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(short id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: POST method that will create a new Order entry on the database
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
          if (_context.Orders == null)
          {
              return Problem("Entity set 'EcoPower_LogisticsDBContext.Orders'  is null.");
          }
            _context.Orders.Add(order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: DELETE method that will delete an existing Order entry on the database
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(short id)
        {
            if (!OrderExists(id))
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: PATCH method that will update an existing Order entry on the database
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateOrder(short id, [FromBody] JsonPatchDocument<Order> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (_context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(order); // Apply patch operations to the Order object

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

    }
}
