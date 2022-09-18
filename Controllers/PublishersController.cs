using BookStoresWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoresWebAPI.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
        {

        private readonly BookStoresDBContext _context;

        public PublishersController(BookStoresDBContext context)
            {
            _context = context;
            }
        [HttpGet]

        [HttpGet("GetPublishers")]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
            {
            return await _context.Publishers.ToListAsync();           
                
                    
                    }

        // GET: api/Publishers/GetPublisherDetails/5
        [HttpGet("GetPublisherDetails/{id}")]

        public async Task<ActionResult<Publisher>> GetPublisherDetails(int id)
            {
            var publisher = await _context.Publishers
                                             .Include(pub=>pub.Books)
                                                .ThenInclude(book=>book.Sales)
                                             .Include(pub => pub.Users)
                                               .Where(pub => pub.PubId == id)
                                            .FirstOrDefaultAsync();

            if (publisher == null)
                {
                return NotFound();
                }

            return publisher;
            }

        // GET: api/Publishers/5
        [HttpGet("GetPublisher/{id}")]

        public async Task<ActionResult<Publisher>> GetPublisher(int id)
            {
            var publisher = await _context.Publishers
                                            .Where(pub => pub.PubId == id)
                                            .FirstOrDefaultAsync();

            if (publisher == null)
                {
                return NotFound();
                }

            return publisher;
            }

        [HttpPost("CreatePublisher")]
        public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher)
            {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();

            return await Task.FromResult(publisher); //CreatedAtAction("GetPublisher", new { id = publisher.PubId }, publisher);
            }


        [HttpPut("UpdatePublisher/{id}")]
        public async Task<IActionResult> PutPublisher(int id, Publisher publisher)
            {
            if (id != publisher.PubId)
                {
                return BadRequest();
                }

            _context.Entry(publisher).State = EntityState.Modified;

            try
                {
                await _context.SaveChangesAsync();
                }
            catch (DbUpdateConcurrencyException)
                {
                if (!PublisherExists(id))
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

        // DELETE: api/Publishers/5
        [HttpDelete("DeletePublisher/{id}")]
        public async Task<ActionResult<Publisher>> DeletePublisher(int id)
            {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
                {
                return NotFound();
                }

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();

            return publisher;
            }
        private bool PublisherExists(int id)
            {
            return _context.Publishers.Any(e => e.PubId == id);
            }

        }



    }
