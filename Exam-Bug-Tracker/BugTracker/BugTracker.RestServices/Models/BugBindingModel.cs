using System.ComponentModel.DataAnnotations;

namespace BugTracker.RestServices.Models
{
    public class BugBindingModel
    {
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}