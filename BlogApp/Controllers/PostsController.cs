using System.Diagnostics;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

public class PostsController : Controller
{

    public IPostRepository _repository { get; set; }

    public PostsController(IPostRepository repository) 
    {
        _repository = repository;
    }
    public IActionResult Index()
    {
        return View(_repository.Posts.ToList());
    }
}