using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class PostsController : Controller
{
    public IPostRepository _postRepository;
    public ICommentRepository _commentRepository;
    public ITagRepository _tagRepository;

    public PostsController(IPostRepository postRepository, ICommentRepository commentRepository, ITagRepository tagRepository) 
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _tagRepository = tagRepository;
    }
    public async Task<IActionResult> Index(string tag)
    {
        var claims = User.Claims;
        var model = _postRepository.Posts.Where(p => p.IsActive);
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
                    .Include(p => p.User)
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
    [Authorize]
    public IActionResult Create(){
        return View();
    }
    [HttpPost]
    [Authorize]
    public  IActionResult Create(PostCreateViewModel model){
        if(ModelState.IsValid){
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _postRepository.CreatePost(
                new Post{
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    Url = model.Url,
                    PublishedOn = DateTime.Now,
                    UserId = int.Parse(userId ?? ""),
                    Image = "1.jpg",
                    IsActive = false,
                }
            );
            return RedirectToAction("Index");
        }
        return View();
    }
    [Authorize]
    public async Task<IActionResult> List(){
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
        var role = User.FindFirstValue(ClaimTypes.Role);
        var posts = _postRepository.Posts;
        if(string.IsNullOrEmpty(role))
        {
            posts = posts.Where(p => p.UserId == userId);
        }
        return View(await posts.ToListAsync());
    }

    [Authorize]
    public IActionResult Edit(int? id){
        if(id == null) return NotFound();
        
        var posts = _postRepository
        .Posts
        .Include(p => p.Tags)
        .FirstOrDefault(p => p.PostId == id);

        if(posts == null) return NotFound();
        
        ViewBag.Tags = _tagRepository.Tags.ToList();
        return View(
            new PostCreateViewModel { 
                PostId = posts.PostId,
                Title = posts.Title,
                Description = posts.Description,
                Content = posts.Content,
                Url = posts.Url,
                IsActive = posts.IsActive,
                Tags = posts.Tags

            }
        );
    }

    [Authorize]
    [HttpPost]
    public IActionResult Edit(PostCreateViewModel model,int[] tagIds){
        if(ModelState.IsValid){
            var entityUpdate = new Post{
                PostId = model.PostId,
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                Url = model.Url,
            };
            if(User.FindFirstValue(ClaimTypes.Role) == "admin"){
                entityUpdate.IsActive = model.IsActive;
            }
            _postRepository.EditPost(entityUpdate,tagIds);
            return RedirectToAction("List");
        }
        ViewBag.Tags = _tagRepository.Tags.ToList();
        return View(model);
    }

     public async Task<IActionResult> Delete(int? id)
   {
        if(id == null)
            return NotFound();
        
        var post = await _postRepository.Posts.FirstOrDefaultAsync(p => p.PostId == id);
        if(post == null)
            return NotFound();
        
        return View(post);
   }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id){
        _postRepository.DeletePost(id);
        return RedirectToAction("List");
    }
}

