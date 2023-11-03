using BlogApp.Entity;

namespace BlogApp.Models;

public class PostViewModel
{
    public List<Post> Posts { get; set; }
    public required List<Tag> Tags { get; set; }

}