using System.Linq;
using System.Web.Http;
using MusicSystem.Models;

namespace MusicSystem.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/artists")]
    public class ArtistController : BaseController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetArtists()
        {
            var artists = Data.Artists
                .All()
                .OrderBy(a => a.Name)
                .Select(a => new
                {
                    name = a.Name,
                    country = a.Country,
                    birthday = a.BirthDay,
                    songs = a.Songs.Select(s => s.Title),
                    albums = a.Albums.Select(al => al.Title)
                }).ToList();

            return this.Ok(artists);
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetArtist(int id)
        {
            var artist = Data.Artists.GetById(id);

            if (artist == null)
            {
                return NotFound();
            }

            return this.Ok(new
            {
                name = artist.Name,
                country = artist.Country,
                birthday = artist.BirthDay,
                songs = artist.Songs.Select(s => s.Title),
                albums = artist.Albums.Select(al => al.Title)
            });
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateArtist(Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var addArtists = Data.Artists.Add(artist);
            Data.SaveChanges();

            return this.Ok(addArtists.Id);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult EditArtist(int id, Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            artist.Id = id;

            Data.Artists.Update(artist);
            Data.SaveChanges();

            return this.Ok(artist.Id);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteArtist(int id)
        {
            var artist = Data.Artists.GetById(id);

            if (artist == null)
            {
                return this.NotFound();
            }

            Data.Artists.Delete(artist);
            Data.SaveChanges();

            return this.Ok(id);
        }
    }
}
