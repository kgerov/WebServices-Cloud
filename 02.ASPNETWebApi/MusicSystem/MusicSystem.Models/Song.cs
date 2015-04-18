namespace MusicSystem.Models
{
    public class Song
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public Genre Genre { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual Album Album { get; set; }
    }
}
