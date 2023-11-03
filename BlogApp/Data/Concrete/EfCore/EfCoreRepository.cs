using BlogApp.Data.Abstract;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete.EfCore;

public class EfCoreRepository : IPostRepository
{
    private readonly BlogContext _context;

    public EfCoreRepository(BlogContext context)
    {
        _context = context;
    }

    public IQueryable<Post> Posts => _context.Posts;

    public void CreatePost(Post post)
    {
        _context.Posts.Add(post);
        _context.SaveChanges();
    }
}