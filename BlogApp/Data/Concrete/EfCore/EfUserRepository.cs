using BlogApp.Data.Abstract;
using BlogApp.Entity;
using SQLitePCL;

namespace BlogApp.Data.Concrete.EfCore;

public class EfUserRepository : IUserRepository
{
    private readonly BlogContext _context;

    public EfUserRepository(BlogContext context)
    {
        _context = context;
    }

    public IQueryable<User> Users => _context.Users;

    public void CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}