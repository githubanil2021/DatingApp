using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
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
         
        //private readonly DC _DC;
        //public UsersController(DC dc)
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        // [AllowAnonymous] 
        [HttpGet]

        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            // var users = await _DC.Users.ToListAsync();

                var users = await _userRepository.GetMembersAsync();// .GetUsersAsync();
                // var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

                return Ok(users);
                
        }

        
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            // var user =await _DC.Users.FirstOrDefaultAsync(u=>u.Id == id);
            // var user = await _userRepository.GetUserByUsernameAsync(username);

            return await _userRepository.GetMemberAsync(username);    
            // return _mapper.Map<MemberDto>(user);

        }
    }
}