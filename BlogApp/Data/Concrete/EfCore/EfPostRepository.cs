using BlogApp.Data.Abstract;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete.EfCore;

public class EfPostRepository : IPostRepository
{
    private readonly BlogContext _context;

    public EfPostRepository(BlogContext context)
    {
        _context = context;
    }

    public IQueryable<Post> Posts => _context.Posts;

    public void CreatePost(Post post)
    {
        _context.Posts.Add(post);
        _context.SaveChanges();
    }

    public void EditPost(Post post)
    {
        var entity = _context.Posts.FirstOrDefault(p => p.PostId == post.PostId);

        if(entity != null)
        {
            entity.Title = post.Title;
            entity.Description = post.Description;
            entity.Content = post.Content;
            entity.Url = post.Url;
            entity.IsActive = post.IsActive;
            _context.SaveChanges();
        }
    }
}