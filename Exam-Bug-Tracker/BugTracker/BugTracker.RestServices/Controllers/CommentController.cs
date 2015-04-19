using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Http;
using BugTracker.Data.Models;
using BugTracker.RestServices.Models;
using Microsoft.AspNet.Identity;

namespace BugTracker.RestServices.Controllers
{
    [RoutePrefix("api")]
    public class CommentController : BaseApiController
    {
        [HttpGet]
        [Route("comments")]
        public IHttpActionResult GetComments()
        {
            var comments = Data.Comments
                .All()
                .OrderByDescending(x => x.DateCreated)
                .Select(x => new
                {
                    Id = x.Id,
                    Text = x.Text,
                    Author = (x.Author != null) ? x.Author.UserName : null,
                    DateCreated = x.DateCreated,
                    BugId = x.Bug.Id,
                    BugTitle = x.Bug.Title
                });

            return this.Ok(comments);
        }

        [HttpGet]
        [Route("bugs/{id:int}/comments")]
        public IHttpActionResult GetCommentsForBug(int id)
        {
            var bug = Data.Bugs.All().FirstOrDefault(x => x.Id == id);

            if (bug == null)
            {
                return this.NotFound();
            }

            var comments = Data.Comments
                .All()
                .Where(x => x.Bug.Id == bug.Id)
                .OrderByDescending(x => x.DateCreated)
                .Select(x => new
                {
                    Id = x.Id,
                    Text = x.Text,
                    Author = (x.Author != null) ? x.Author.UserName : null,
                    DateCreated = x.DateCreated
                });

            return this.Ok(comments);
        }

        [HttpPost]
        [Route("bugs/{id:int}/comments")]
        public IHttpActionResult AddCommentToBug(int id, CommentBindingModel newComment)
        {
            if (newComment == null)
            {
                return this.BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var bug = Data.Bugs.All().FirstOrDefault(x => x.Id == id);

            if (bug == null)
            {
                return this.NotFound();
            }

            var userId = User.Identity.GetUserId();
            var user = Data.Users.Find(userId);

            Comment comment = new Comment()
            {
                Text = newComment.Text,
                DateCreated = DateTime.Now,
                Author = user,
                Bug = bug
            };

            Data.Comments.Add(comment);
            Data.SaveChanges();

            if (user == null)
            {
                return this.Ok(new
                {
                    comment.Id,
                    Message = "Added anonymous comment for bug #" + bug.Id
                });
            }
            else
            {
                return this.Ok(new
                {
                    comment.Id,
                    Author = comment.Author.UserName,
                    Message = "User comment added for bug #" + bug.Id
                });
            }
        }
    }
}
