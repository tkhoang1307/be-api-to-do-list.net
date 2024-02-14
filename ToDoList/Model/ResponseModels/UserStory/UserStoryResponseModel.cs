using ToDoList.Entities;
using ToDoList.Model.ResponseModels.Status;
using ToDoList.Model.ResponseModels.User;

namespace ToDoList.Model.ResponseModels.UserStory
{
    public class UserStoryResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public UserInformationResponseModel User { get; set; } = null!;
        public StatusResponseModel Status { get; set; } = null!;
    }
}
