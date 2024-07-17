using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TypistApi.Data;
using TypistApi.DTOs;

namespace TypistApi.Controller;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;
    public UserController(UserDbContext context , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]

    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        var users = await _context.Users
            .OrderBy(x => x.UserName)
            .ToListAsync();

            return _mapper.Map<List<UserDto>>(users);
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<UserDto>> GetUserById(Guid id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) return NotFound();

        return _mapper.Map<UserDto>(user);
    }


   
}
