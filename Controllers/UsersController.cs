using BookStoresWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BookStoresWebAPI.Controllers
    {

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
        {
        private readonly BookStoresDBContext _context;

   
        public UsersController(BookStoresDBContext context)
            {
            _context = context;
            }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
            {
            return await _context.Users.ToListAsync();


            }


        [HttpGet("GetUser")]
        public async Task<ActionResult<User>> GetUser()
            {
            string emailAddress = HttpContext.User.Identity.Name;

            var user =  _context.Users.Where(u => u.EmailAddress == emailAddress)
                                           .FirstOrDefault();

            user.Password = null;

            return user;


            }

        }
    }
