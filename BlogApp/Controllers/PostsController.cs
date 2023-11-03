using BlogApp.Data.Abstract;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

public class PostsController : Controller
{
    public IPostRepository _postRepository { get; set; }
    public ITagRepository _tagRepository { get; set; }

    public PostsController(IPostRepository postRepository,ITagRepository tagRepository) 
    {
        _postRepository = postRepository;
        _tagRepository = tagRepository;
    }
    public IActionResult Index()
    {
        var model = new PostViewModel{
                Posts = _postRepository.Posts.ToList(),
                Tags = _tagRepository.Tags.ToList()
                };
        return View(model);
    }
}