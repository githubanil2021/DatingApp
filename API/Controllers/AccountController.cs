using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Entities;
using System.Security.Cryptography; 
using Microsoft.EntityFrameworkCore;
using API.Data;
using System.Text;
using API.DTOs;
using System.Numerics;
using API.Interfaces;
using System.Runtime.CompilerServices;

namespace API.Controllers
{ 
    public class AccountController : BaseApiController
    {
        private readonly ILogger<AccountController> _logger;

        private readonly DC _DC;
        private readonly ITokenService _tokenService;
        public AccountController(DC DC, ITokenService tokenService)
        {
            _DC = DC;
            _tokenService = tokenService;
        }

        [HttpPost("register")] //api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)//(string username, string password)
        {
            if(await UserExists(registerDto.UserName))
            {
                return BadRequest("Already");
            }
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _DC.Users.Add(user);
            await _DC.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username){
            return await _DC.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    
        [HttpPost("login")] //api/account/register
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)//(string username, string password)
        { 

            var user = await _DC.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName);
            if(user == null) return Unauthorized("Invalid user");
            
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(int i=0; i<computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            }; 
        }


    }
}