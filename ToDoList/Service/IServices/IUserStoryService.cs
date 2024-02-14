using ToDoList.Model.RequestModels.User;
using ToDoList.Model.RequestModels.UserStory;
using ToDoList.Model.ResponseModels;

namespace ToDoList.Service.IServices
{
    public interface IUserStoryService
    {
        Task<ApiResponse> GetUserStoryByUserId(Guid userId);
        Task<ApiResponse> GetUserStoryByUSId(Guid usId);
        Task<ApiResponse> CreateUserStory(UserStoryCreationRequestModel userStoryCreationRequest);
        Task<ApiResponse> UpdateUserStory(Guid usId, UserStoryUpdationRequestModel userStoryUpdation);
    }
}
