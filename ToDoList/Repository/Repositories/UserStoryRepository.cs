using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using ToDoList.Entities;
using ToDoList.Model.RequestModels.UserStory;
using ToDoList.Repository.IRepositories;

namespace ToDoList.Repository.Repositories
{
    public class UserStoryRepository : BaseRepository<UserStory>, IUserStoryRepository
    {
        public UserStoryRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task<List<UserStory>> GetAllUserStoriesByUserId(Guid userId)
        {
            IQueryable<UserStory> query = _dbSet;
            var userStoryEntity = await query
                .Where(us => us.CreatedBy == userId)
                .Join(
                    _context.Users,
                    userStory => userStory.CreatedBy,
                    user => user.Id,
                    (userStory, user) => new { UserStory = userStory, User = user })
                .Join(
                    _context.Statuses,
                    combined => combined.UserStory.IdStatus,
                    status => status.Id,
                    (combined, status) => new UserStory
                    {
                        Id = combined.UserStory.Id,
                        Name = combined.UserStory.Name,
                        Description = combined.UserStory.Description,
                        CreatedAt = combined.UserStory.CreatedAt,
                        User = combined.User,
                        Status = status,
                    })
                .ToListAsync(); 

            return userStoryEntity;
        }

        public async Task<UserStory?> GetUserStoryByUSId(Guid usId)
        {
            IQueryable<UserStory> query = _dbSet;

            UserStory? dataUserStory = await query
                .Where(us => us.Id == usId)
                .Join(
                    _context.Users,
                    userStory => userStory.CreatedBy,
                    user => user.Id,
                    (userStory, user) => new { UserStory = userStory, User = user })
                .Join(
                    _context.Statuses,
                    combined => combined.UserStory.IdStatus,
                    status => status.Id,
                    (combined, status) => new UserStory
                    {
                        Id = combined.UserStory.Id,
                        Name = combined.UserStory.Name,
                        Description = combined.UserStory.Description,
                        CreatedAt = combined.UserStory.CreatedAt,
                        User = combined.User,
                        Status = status,
                    })
                .FirstOrDefaultAsync();

            return dataUserStory;
        }

        public async Task<UserStory?> CreateUserStory(UserStoryCreationRequestModel userStoryCreation)
        {
            var dataCallAPI = new UserStory()
            {
                Name = userStoryCreation.Name,
                Description = userStoryCreation.Description,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userStoryCreation.CreatedBy,
                IdStatus = userStoryCreation.IdStatus,
            };

            _dbSet.AddAsync(dataCallAPI);
            await _context.SaveChangesAsync();

            return dataCallAPI;
        }

        public async Task<UserStory?> UpdateUserStory(Guid usId, UserStoryUpdationRequestModel userStoryUpdation)
        {
            IQueryable<UserStory> query = _dbSet;

            UserStory? dataUserStory = await query.Where(us => us.Id == usId).FirstOrDefaultAsync();

            if (dataUserStory != null)
            {
                dataUserStory.Name = userStoryUpdation.Name;
                dataUserStory.Description = userStoryUpdation.Description;
                dataUserStory.IdStatus = userStoryUpdation.IdStatus;

                await _context.SaveChangesAsync();
            }

            return dataUserStory;
        }
    }
}
