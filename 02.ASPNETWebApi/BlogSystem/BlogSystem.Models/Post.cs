using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlogSystem.Models
{
    [JsonObject]
    public class Post
    {
        private ICollection<Tag> tags;

        public Post()
        {
            this.tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int UserId { get; set; }
        
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }
    }
}
