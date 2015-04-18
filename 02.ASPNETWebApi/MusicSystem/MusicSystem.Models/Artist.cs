using System;
using System.Collections;
using System.Collections.Generic;

namespace MusicSystem.Models
{
    public class Artist
    {
        private ICollection<Album> albums;
        private ICollection<Song> songs;

        public Artist()
        {
            this.albums = new HashSet<Album>();
            this.songs = new HashSet<Song>();
            this.BirthDay = DateTime.Now;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime BirthDay { get; set; }

        public virtual ICollection<Album> Albums
        {
            get { return this.albums; }
            set { this.albums = value; }
        }

        public virtual ICollection<Song> Songs
        {
            get { return this.songs; }
            set { this.songs = value; }
        }
    }
}
