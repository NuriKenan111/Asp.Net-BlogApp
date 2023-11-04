using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents;

public class NewPosts:ViewComponent
{
    private  IPostRepository _postRepository;

    public NewPosts(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync(){
        var model = await _postRepository
        .Posts
        .OrderByDescending(x => x.PublishedOn)
        .Take(5)
        .ToListAsync();
        return View(model);
    }
}