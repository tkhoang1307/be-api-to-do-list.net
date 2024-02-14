using ToDoList.Repository.IRepositories;
using ToDoList.Repository.Repositories;

namespace ToDoList.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public IStatusRepository StatusRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserStoryRepository UserStoryRepository { get; }

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            StatusRepository = new StatusRepository(_context);
            UserRepository = new UserRepository(_context);
            UserStoryRepository = new UserStoryRepository(_context);
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
