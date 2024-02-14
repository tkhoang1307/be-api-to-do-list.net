namespace ToDoList.Model.RequestModels.UserStory
{
    public class UserStoryUpdationRequestModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid IdStatus { get; set; }
    }
}
