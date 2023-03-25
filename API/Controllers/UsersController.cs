using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    [Authorize]
    public class UsersController : BaseApiController
    {
         
        private readonly DC _DC;
        public UsersController(DC dc)
        {
            this._DC=dc;
        }

        [AllowAnonymous]
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