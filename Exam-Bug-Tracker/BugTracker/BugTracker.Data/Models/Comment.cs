using System;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        public string Text { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public User Author { get; set; }

        [Required]
        public Bug Bug { get; set; }
    }
}
