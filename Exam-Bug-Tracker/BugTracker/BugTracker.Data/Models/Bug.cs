using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Data.Models
{
    public class Bug
    {
        private ICollection<Comment> comments;

        public Bug()
        {
            this.comments = new HashSet<Comment>();
        }
        
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public Stutus Status { get; set; }

        public User Author { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
