using System.ComponentModel.DataAnnotations;
using BugTracker.Data.Models;

namespace BugTracker.RestServices.Models
{
    public class BugPatchBindingModel
    {
        [MaxLength(100)]
        [MinLength(1)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }
    }
}