using System.ComponentModel.DataAnnotations;
using BlogApp.Entity;

namespace BlogApp.Models;

public class PostCreateViewModel
{
    public int PostId { get; set; }
    
    [Required]
    [Display]
    public string? Title { get; set; }

    [Required]
    [Display]
    public string? Description { get; set; }

    [Required]
    [Display]
    public string? Content { get; set; }
    
    
    [Required]
    [Display]
    public string? Url { get; set; }

    public bool IsActive { get; set; }
    public List<Tag> Tags { get; set; } = new();
}