using BookStoresWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoresWebAPI.Controllers
    {
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
        {
       
        [HttpGet]
        public IEnumerable<Author> Get()
            {
           using(var context = new BookStoresDBContext())
                {


                //Author author = new Author();
                //author.FirstName = "Chen";
                //author.LastName = "Gao";

                //context.Author.Add(author);

                Author author = context.Author.Where(auth => auth.FirstName == "Chen").FirstOrDefault();

                context.Remove(author);



                context.SaveChanges();
                return context.Author.ToList();
                }
            }
        }
    }
