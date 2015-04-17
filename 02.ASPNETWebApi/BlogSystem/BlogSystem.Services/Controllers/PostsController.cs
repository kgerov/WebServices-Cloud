using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Http;
using BlogSystem.Data;
using BlogSystem.Models;
using Microsoft.AspNet.Identity;

namespace BlogSystem.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/posts")]
    public class PostsController : BaseApiController
    {
        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetPost(int id)
        {
            var post = Data.Posts.Find(x => x.Id == id).FirstOrDefault();

            if (post == null)
            {
                return NotFound();
            }

            return this.Ok(post);
        }

        [Route("")]
        [HttpGet]
        public IList GetPosts()
        {
            var posts = Data.Posts.All().Select(p => new
            {
                content = p.Content,
                title = p.Title,
                user = p.UserId,
                tags = p.Tags.Select(t => t.Name)
            }).ToList();

            return posts;
        }

        [Route("")]
        [HttpPost]
        //[ActionName("create")]
        public IHttpActionResult CreatePost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            post.User = UserManager.FindById(HttpContext.Current.User.Identity.GetUserId()); ;
            this.Data.Posts.Add(post);
            this.Data.SaveChanges();

            return this.Ok("Bravo");
        }

        [Route("{id:int}")]
        [HttpPut]
        //[ActionName("posts")]
        public IHttpActionResult PutPost(int id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            post.Id = id;

            Data.Posts.Update(post);
            Data.SaveChanges();

            return this.Ok(post.Id);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeletePost(int id)
        {
            var post = Data.Posts.Find(x => x.Id == id).FirstOrDefault();

            if (post == null)
            {
                return NotFound();
            }

            Data.Posts.Delete(post);
            Data.SaveChanges();

            return this.Ok(post.Id);
        }
    }
}


//[RoutePrefix("api/products")]
//public class ProductController : ApiController
//{
//    [Route("list")]
//    [HttpGet]
//    public string ListProducts()
//    {
//        return "products";
//    }

//    [Route("{id:int}")]
//    public string GetProduct(int id)
//    {
//        return "product " + id;
//    }

//    [Route("{name}")]
//    public IHttpActionResult GetProductByName(string name)
//    {
//        var product = new Product(name);

//        return NotFound();
//    }
//}