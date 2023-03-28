using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {

        private readonly DC _DC;
        private readonly IMapper _mapper;
        public UserRepository(DC DC, IMapper mapper)
        {
            _DC = DC;
            _mapper = mapper;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
          return await _DC.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _DC.Users
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
             return await _DC.Users.FindAsync(id);
            
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _DC.Users
                .Include(p=>p.Photos)
                .FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _DC.Users
                .Include(p=>p.Photos)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _DC.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _DC.Entry(user).State = EntityState.Modified;
        }
    }
}