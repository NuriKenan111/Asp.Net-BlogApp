using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class PostsController : Controller
{
    public IPostRepository _postRepository;

    public PostsController(IPostRepository postRepository) 
    {
        _postRepository = postRepository;
    }
    public async Task<IActionResult> Index(string tag)
    {
        var model = _postRepository.Posts;
        if(!string.IsNullOrEmpty(tag))
        {
            model = model.Where(p => p.Tags.Any(t => t.Url == tag));
        }
        return View(
            await model.ToListAsync()
        );
    }

    public async Task<IActionResult> Details(string url)
    {
       var post = await _postRepository
                    .Posts
                    .Include(p => p.Tags)
                    .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                    .FirstOrDefaultAsync(m => m.Url == url);
        return View(post);
    }
}