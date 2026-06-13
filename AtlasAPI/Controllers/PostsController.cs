using AtlasAPI.Data;
using AtlasAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AtlasApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PostsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Post post)
    {
        _context.Posts.Add(post);

        await _context.SaveChangesAsync();

        return Ok(post);
    }

    [HttpGet]
    public IActionResult GetPosts()
    {
        var posts = _context.Posts
            .OrderByDescending(x => x.CreatedDate)
            .ToList();

        return Ok(posts);
    }
}