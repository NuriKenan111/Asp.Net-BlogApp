using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlogApp.ViewComponents;

public class TagsMenu:ViewComponent
{
    private  ITagRepository _tagRepository;

    public TagsMenu(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync(){
        var model = await _tagRepository.Tags.ToListAsync();
        return View(model);
    }
}