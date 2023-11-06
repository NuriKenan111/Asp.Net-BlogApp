using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class PostsController : Controller
{
    public IPostRepository _postRepository;
    public ICommentRepository _commentRepository;

    public PostsController(IPostRepository postRepository, ICommentRepository commentRepository) 
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }
    public async Task<IActionResult> Index(string tag)
    {
        var claims = User.Claims;
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
    [HttpPost]
    public JsonResult AddComment(int PostId,string UserName,string Text)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var username = User.FindFirstValue(ClaimTypes.Name);
        var avatar = User.FindFirstValue(ClaimTypes.UserData);
        
        var entity = new Comment()
        {
            PostId = PostId,
            Text = Text,
            PublishedOn = DateTime.Now,
            UserId = int.Parse(userId ?? ""),
        };
        _commentRepository.CreateComment(entity);
        return Json(new {
            username,
            Text,
            entity.PublishedOn,
            avatar
        });
    }
}