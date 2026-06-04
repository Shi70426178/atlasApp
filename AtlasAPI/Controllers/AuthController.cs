using AtlasAPI.Data;
using AtlasAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AtlasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("signup")]
    public IActionResult Signup(User user)
    {
        var existingUser = _context.Users
            .FirstOrDefault(x => x.Email == user.Email);

        if (existingUser != null)
        {
            return BadRequest("Email already exists");
        }

        user.CreatedDate = DateTime.Now;

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("Signup successful");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginModel loginUser)
    {
        var user = _context.Users.FirstOrDefault(x =>
            x.Email == loginUser.Email &&
            x.Password == loginUser.Password);

        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }

        return Ok(new
        {
            Message = "Login successful",
            UserName = user.UserName,
            Role = user.Role,
            Email = user.Email,
            Branch = user.Branch
        });
    }
}