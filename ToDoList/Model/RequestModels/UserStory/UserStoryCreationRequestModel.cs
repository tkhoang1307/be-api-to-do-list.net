namespace ToDoList.Model.RequestModels.UserStory
{
    public class UserStoryCreationRequestModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public Guid CreatedBy { get; set; }
        public Guid IdStatus { get; set; }
    }
}
