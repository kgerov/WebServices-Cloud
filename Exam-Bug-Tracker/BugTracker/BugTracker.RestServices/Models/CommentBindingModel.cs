using System.ComponentModel.DataAnnotations;

namespace BugTracker.RestServices.Models
{
    public class CommentBindingModel
    {
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        public string Text { get; set; }
    }
}