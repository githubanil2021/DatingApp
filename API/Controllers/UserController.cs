using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
         
        private readonly DC _DC;
        public UserController(DC dc)
        {
            this._DC=dc;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _DC.Users.ToListAsync();
            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user =await _DC.Users.FirstOrDefaultAsync(u=>u.Id == id);
            return user;
        }
    }
}