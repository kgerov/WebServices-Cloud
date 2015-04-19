using BugTracker.Data.Models;
using BugTracker.Data.Repositories;
using BugTracker.Data.UnitOfWork;
using Microsoft.AspNet.Identity;

namespace BugTracker.Tests.Mocks
{
    class BugTrackerMock : IBugTrackerData
    {
        private GenericRepositoryMock<User> usersMock = new GenericRepositoryMock<User>();
        private GenericRepositoryMock<Bug> bugMock = new GenericRepositoryMock<Bug>();
        private GenericRepositoryMock<Comment> commentMock = new GenericRepositoryMock<Comment>();
        private GenericUserStoreMock<User> userStoreMock = new GenericUserStoreMock<User>();

        public bool ChangesSaved { get; set; }

        public IRepository<User> Users
        {
            get { return this.usersMock; }
        }

        public IRepository<Bug> Bugs
        {
            get { return this.bugMock; }
        }

        public IRepository<Comment> Comments
        {
            get { return this.commentMock; }
        }

        public IUserStore<User> UserStore
        {
            get { return this.userStoreMock; }
        }

        public void SaveChanges()
        {
            this.ChangesSaved = true;
        }
    }
}
