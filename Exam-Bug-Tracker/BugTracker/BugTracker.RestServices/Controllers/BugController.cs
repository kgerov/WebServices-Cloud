using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Http;
using BugTracker.Data.Models;
using BugTracker.Data.UnitOfWork;
using BugTracker.RestServices.Models;
using Microsoft.AspNet.Identity;

namespace BugTracker.RestServices.Controllers
{
    [RoutePrefix("api/bugs")]
    public class BugController : ApiController
    {
        private readonly IBugTrackerData Data;

        public BugController()
            : this (new BugTrackerData())
        {
            
        }
        public BugController(IBugTrackerData data)
        {
            this.Data = data;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetBugs()
        {
            var bugs = Data.Bugs
                .All()
                .OrderByDescending(x => x.DateCreated)
                .Select(x => new
                {
                    Id = x.Id,
                    Title = x.Title,
                    Status = x.Status.ToString(),
                    Author = (x.Author != null) ? x.Author.UserName : null,
                    DateCreated = x.DateCreated
                });

            return this.Ok(bugs);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetBug(int id)
        {
            var dbbug = Data.Bugs.All()
                .Select(bug => new
                {
                    Id = bug.Id,
                    Title = bug.Title,
                    Description = bug.Description ?? null,
                    Status = bug.Status.ToString(),
                    Author = (bug.Author != null) ? bug.Author.UserName : null,
                    DateCreated = bug.DateCreated,
                    Comments = bug.Comments
                        .OrderByDescending(c => c.DateCreated)
                        .Select(c => new { Id = c.Id, Text = c.Text, Author = (c.Author != null) ? c.Author.UserName : null, DateCreated = c.DateCreated })
                })
                .FirstOrDefault(x => x.Id == id);

            if (dbbug == null)
            {
                return this.NotFound();
            }

            return this.Ok(dbbug);
        }

        [HttpGet]
        [Route("filter")]
        public IHttpActionResult GetBugsWithFilter([FromUri] string keyword = null, [FromUri] string statuses = null,
            [FromUri] string author = null)
        {
            IList<Stutus> statusEnumArr = new List<Stutus>();

            if (statuses != null)
            {
                string[] statusArr = statuses.Split('|');


                try
                {
                    foreach (var status in statusArr)
                    {
                        statusEnumArr.Add((Stutus) Enum.Parse(typeof (Stutus), status));
                    }
                }
                catch (ArgumentException)
                {
                    return this.BadRequest("Invalid Status");
                }
            }

            var bugs = Data.Bugs
                .All()
                .OrderByDescending(b => b.DateCreated)
                .Where(b => ((keyword == null) || b.Title.Contains(keyword)) &&
                    ((statuses == null) || statusEnumArr.Contains(b.Status)) &&
                    ((author == null) || b.Author.UserName == author))
                .Select(x => new
                {
                    Id = x.Id,
                    Title = x.Title,
                    Status = x.Status.ToString(),
                    Author = (x.Author != null) ? x.Author.UserName : null,
                    DateCreated = x.DateCreated
                });
            
            return this.Ok(bugs);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateBug(BugBindingModel newBug)
        {
            if (newBug == null)
            {
                return this.BadRequest("Missing bug data.");
            }

            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId();
            var user = Data.Users.Find(userId);

            Bug bug = new Bug()
            {
                Title = newBug.Title,
                Description = newBug.Description ?? null,
                DateCreated = DateTime.Now,
                Status = Stutus.Open,
                Author = user
            };

            Data.Bugs.Add(bug);
            Data.SaveChanges();

            if (user == null)
            {
                return this.CreatedAtRoute(
                    "DefaultApi",
                    new { controller = "bugs", id = bug.Id},
                    new { bug.Id, Message = "Anonymous bug submitted." });
            }
            else
            {
                return this.CreatedAtRoute(
                    "DefaultApi",
                    new { controller = "bugs", id = bug.Id },
                    new { bug.Id, Author = bug.Author.UserName, Message = "User bug submitted." });
            }
        }

        //empty title or invalid status
        [HttpPatch]
        [Route("{id:int}")]
        public IHttpActionResult EditBug(int id, BugPatchBindingModel newBug)
        {
            if (newBug == null)
            {
                return this.BadRequest("Missing data.");
            }

            if (newBug.Title == "")
            {
                return this.BadRequest("Empty title.");
            }

            try
            {
                if (newBug.Status != null)
                {
                    Stutus test = (Stutus)Enum.Parse(typeof(Stutus), newBug.Status);
                }
            }
            catch (ArgumentException)
            {
                return this.BadRequest("Invallid Status");
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

            if (!String.IsNullOrEmpty(newBug.Title))
            {
                bug.Title = newBug.Title;
            }

            if (newBug.Description != null)
            {
                bug.Description = newBug.Description;
            }

            if (newBug.Status != null)
            {
                bug.Status = (Stutus)Enum.Parse(typeof(Stutus), newBug.Status);
            }
            

            Data.Bugs.Update(bug);
            Data.SaveChanges();

            return this.Ok(new
            {
                Message = "Bug #" + bug.Id + " patched."
            });
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteBug(int id)
        {
            var bug = Data.Bugs.Find(id);

            if (bug == null)
            {
                return this.NotFound();
            }

            Data.Bugs.Remove(bug);
            Data.SaveChanges();

            return this.Ok(new
            {
                Message = "Bug #" + bug.Id + " deleted."
            });
        }
    }
}
