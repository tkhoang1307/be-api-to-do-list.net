using ToDoList.Entities;
using ToDoList.Model.RequestModels.UserStory;

namespace ToDoList.Repository.IRepositories
{
    public interface IUserStoryRepository : IBaseRepository<UserStory>
    {
        Task<List<UserStory>> GetAllUserStoriesByUserId(Guid userId);

        Task<UserStory?> GetUserStoryByUSId(Guid usId);

        Task<UserStory?> CreateUserStory(UserStoryCreationRequestModel userStoryCreation);
        Task<UserStory?> UpdateUserStory(Guid usId, UserStoryUpdationRequestModel userStoryUpdation);
    }
}
