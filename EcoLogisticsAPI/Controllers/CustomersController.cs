using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcoLogisticsAPI.Models;
using Microsoft.AspNetCore.JsonPatch;


namespace EcoLogisticsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly EcoPower_LogisticsDBContext _context;

        public CustomersController(EcoPower_LogisticsDBContext context)
        {
            _context = context;
        }

        //private method in the API that checks if a Customer exists 
        private bool CustomerExists(short id)
        {
            return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }

        // GET: GET method that retrieves all Customer entries from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
          if (_context.Customers == null)
          {
              return NotFound();
          }
            return await _context.Customers.ToListAsync();
        }


        // GET: GET method that will retrieve one Customer from the database based on the ID parsed through
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(short id)
        {
          if (_context.Customers == null)
          {
              return NotFound();
          }
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(short id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: POST method that will create a new Customer entry on the database
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
          if (_context.Customers == null)
          {
              return Problem("Entity set 'EcoPower_LogisticsDBContext.Customers'  is null.");
          }
            _context.Customers.Add(customer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: DELETE method that will delete an existing Customer entry on the database
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(short id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // PATCH:  PATCH method that will update an existing Customer entry on the database
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCustomer(short id, [FromBody] JsonPatchDocument<Customer> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (_context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(customer); // Apply patch operations to the customer object

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
                if (!CustomerExists(id))
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
