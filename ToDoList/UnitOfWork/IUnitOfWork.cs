using ToDoList.Repository.IRepositories;

namespace ToDoList.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IStatusRepository StatusRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserStoryRepository UserStoryRepository { get; }

        Task SaveChangeAsync();
    }
}
