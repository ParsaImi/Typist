using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using MassTransit;
using MassTransit.Testing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TypistApi.Data;
using TypistApi.DTOs;
using TypistApi.Entity;

namespace TypistApi.Controller;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public UserController(UserDbContext context , IMapper mapper , IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }
    [Authorize]
    [HttpGet]

    // public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    // {
    //     var users = await _context.Users
    //         .OrderBy(x => x.UserName)
    //         .ToListAsync();

    //         return _mapper.Map<List<UserDto>>(users);
    // }

    public async Task<ActionResult<List<UserAllDto>>> GetAllUsers(string date)
    {
        var query = _context.Users.OrderBy(x => x.CreatedAt).AsQueryable();

        if (!string.IsNullOrEmpty(date))
        {
            query = query.Where(x => x.CreatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
        }

        return await query.ProjectTo<UserAllDto>(_mapper.ConfigurationProvider).ToListAsync();
        // var users = await _context.Users
        //     .OrderBy(x => x.UserName)
        //     .ToListAsync();

        //     return _mapper.Map<List<UserAllDto>>(users);
    }
    [Authorize]
    [HttpGet("{id}")]

    public async Task<ActionResult<UserDto>> GetUserById(Guid id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) return NotFound();

        return _mapper.Map<UserDto>(user);
    }
    [Authorize]
    [HttpPost]

    public async Task<ActionResult<UserDto>> PostUser(UserAllDto userAllDto)
    {
        var user = _mapper.Map<User>(userAllDto);
        _context.Users.Add(user);
        var newUser = _mapper.Map<UserAllDto>(user);
        await _publishEndpoint.Publish(_mapper.Map<UserCreated>(newUser));
        var result = await _context.SaveChangesAsync() > 0;
        // publish message to the bus
        if (!result) return BadRequest("Could not save the changes to the database");
        return Ok(user);
    }
    [Authorize]
    [HttpPut("{id}")]

    public async Task<ActionResult<UpdateUserDto>> UpdateUser(Guid id , UpdateUserDto updateUserDto)
    {
        var entity = _context.Users.FirstOrDefault(e => e.Id == id);
        if (entity == null) return NotFound();

        entity.UserName = updateUserDto.UserName ?? entity.UserName;
        entity.ImageUrl = updateUserDto.ImageUrl ?? entity.ImageUrl;
        await _publishEndpoint.Publish(_mapper.Map<UserUpdated>(entity));
        var result = await _context.SaveChangesAsync() > 0;

        if (result) return Ok();
        return BadRequest($"There is a problem for updating user : {id}");
    }


   
}
