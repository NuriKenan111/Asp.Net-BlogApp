using System.ComponentModel.DataAnnotations;

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
}