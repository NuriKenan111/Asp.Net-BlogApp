using BlogApp.Entity;

namespace BlogApp.Data.Abstract;

public interface IPostRepository
{
    IQueryable<Post> Posts { get; }

    void CreatePost(Post post);
    void DeletePost(int postId);
    void EditPost(Post post);
    void EditPost(Post post,int[] tagIds);
}